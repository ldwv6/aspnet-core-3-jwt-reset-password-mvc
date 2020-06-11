using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Password_Reset_JWT.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string UserMail { get; set; }
        public string Token { get; set; }
    }

    public class UserMail
    {
        public string Mail { get; set; }
    }
}
