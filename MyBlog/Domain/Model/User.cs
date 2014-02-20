using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public string DisplayName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int Role { get; set; }
    }
}