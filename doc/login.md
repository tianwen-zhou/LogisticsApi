# 用户登录功能实现

本文档介绍了如何实现用户登录功能，包括前端和后端的实现步骤。

## 目录

- [背景](#背景)
- [后端实现](#后端实现)
  - [注册功能](#注册功能)
  - [登录功能](#登录功能)
- [前端实现](#前端实现)
  - [React登录页面](#react登录页面)
  - [调用后端API](#调用后端api)

---

## 背景

本功能允许用户通过提供用户名和密码来进行身份验证，成功后将返回一个登录成功的响应。如果用户输入的凭证无效，后端会返回一个“用户名或密码错误”的响应。这个功能通常用于用户认证，在应用程序中处理登录和身份验证。

---

## 后端实现

后端使用 **ASP.NET Core** 和 **Entity Framework** 来实现用户的注册与登录功能。

### 注册功能

当用户注册时，后端会首先检查数据库中是否已存在相同的用户名。如果用户名不存在，则将用户的信息（包括哈希处理后的密码）存储在数据库中。

```csharp
// POST: api/Auth/register
[HttpPost("register")]
public async Task<ActionResult<User>> Register(RegisterModel registerModel)
{
    if (await _context.Users.AnyAsync(u => u.Username == registerModel.Username))
    {
        return Conflict("用户名已存在！");
    }

    User user = new User();
    user.Username = registerModel.Username;
    user.Password = registerModel.Password;
    user.Email = registerModel.Email;
    // 设置用户密码哈希（示例使用简单哈希，建议改为更安全的方式）
    user.PasswordHash = GeneratePasswordHash(user.Password);
    user.CreatedTime = DateTime.UtcNow;
    user.UpdatedTime = DateTime.UtcNow;

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
}
