using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DB_BureauExpertiseAndEvaluation.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("Бюро_экспертизы_и_оценки")
        {
        }

        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
    }
}