# Api
简单讲一下Api的使用:

## 登录
使用[登录 POST]("https://api.luckyfishes.com/api/User/Login")  获得Token(JWT)  
或者[注册 POST]("https://api.luckyfishes.com/api/User/SignUp") 获得Token(JWT)  
之后再使用[获得用户数据 GET]("https://api.luckyfishes.com/api/User/GetData") 获得用户数据
```csharp
await SharedClient.PostAsJsonAsync("/api/User/Login", model); // model is LoginModel
await SharedClient.PostAsJsonAsync("/api/User/SignUp", model); // model is SignModel
await SharedClient.GetAsync("/api/User/GetData"); // result is UserModel 's JsonData
```

## 加入或创建项目

