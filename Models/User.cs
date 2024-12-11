using System;

namespace LogisticsApi.Models
{

    public class User
    {
        public int Id { get; set; } // 用户主键
        public string Username { get; set; } // 用户名
        public string Email { get; set; } // 邮箱
        public string Password { get; set; } // 原始密码，仅注册时使用，不存入数据库
        public string PasswordHash { get; set; } // 密码哈希值
        public DateTime CreatedTime { get; set; } // 创建时间
        public DateTime UpdatedTime { get; set; } // 更新时间
    }

}
