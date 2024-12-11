using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsApi.Data;
using LogisticsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LogisticsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LogisticsDbContext _context;

        public AuthController(LogisticsDbContext context)
        {
            _context = context;
        }

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

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginModel.Username);
            if (user == null || !VerifyPasswordHash(loginModel.Password, user.PasswordHash))
            {
                return Unauthorized("用户名或密码错误！");
            }

            // 登录成功返回用户信息
            return Ok(new { Message = "登录成功！", Username = user.Username });
        }

        // GET: api/Auth/users/5
        [HttpGet("users/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        private string GeneratePasswordHash(string password)
        {
            // 示例：使用简单的 SHA256 哈希生成密码（建议使用更安全的 bcrypt 或 PBKDF2）
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes) == storedHash;
            }
        }
    }

}
