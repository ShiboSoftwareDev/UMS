using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Mangment_System.Models
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public int FailedAttempts { get; set; }
        public DateTime? LastAttempt { get; set; }
        public bool IsLocked { get; set; }
    }


    //public override string ToString()
    //    {
    //        return $"{Username} ({Role})";
    //    }
    //}
}