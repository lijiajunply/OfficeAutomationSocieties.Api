using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.WebEncoders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OA.Share.DataModels;
using OfficeAutomationSocieties.Api;

var builder = WebApplication.CreateBuilder(args); // 初始化
var configuration = builder.Configuration; // 读取配置文件

#region 基本配置

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "OA系统",
            Description = "办公自动化"
        }
    );
    var filename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, filename));
});
builder.Services.Configure<HubOptions>(option => option.MaximumReceiveMessageSize = null);
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


builder.Services.AddCors();

builder.Services.Configure<WebEncoderOptions>(options =>
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));
#endregion

#region 数据库依赖注入

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContextFactory<OaContext>(opt =>
        opt.UseSqlite(configuration.GetConnectionString("SQLite")));
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContextFactory<OaContext>(opt =>
        opt.UseNpgsql(configuration.GetConnectionString("PostgreSQL")!));
}

#endregion

#region 添加JWT方案

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddAuthentication(options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false, //是否验证Issuer
            ValidateAudience = false, //是否验证Audience
            ValidateIssuerSigningKey = true, //是否验证SecurityKey
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)), //SecurityKey
            ValidateLifetime = true, //是否验证失效时间
            ClockSkew = TimeSpan.FromSeconds(5), //过期时间容错值，解决服务器端时间不同步问题（秒）
            RequireExpirationTime = true,
        };
    });

builder.Services.AddSingleton(new JwtHelper(configuration));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<TokenActionFilter>();

#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

#region 添加数据库

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OaContext>();
    try
    {
        context.Database.Migrate();
        context.Database.EnsureCreated();
    }
    catch
    {
        var databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
        databaseCreator.CreateTables();
        context.Database.Migrate();
    }

    context.SaveChanges();
    context.Dispose();
}

#endregion

#region 完成

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors();

app.MapControllers();
app.Run();

#endregion