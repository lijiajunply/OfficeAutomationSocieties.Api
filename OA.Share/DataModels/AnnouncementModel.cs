using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class AnnouncementModel
{
    /// <summary>
    /// Markdown
    /// </summary>
    public string Context { get; set; }
    public UserModel User { get; set; }
    public string Time { get; set; }
    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; set; }
    
    public override string ToString() => $"AnnouncementModel is {{Context={Context.Base64Encryption()},Time={Time.Base64Encryption()}}} Other is Private;";
}