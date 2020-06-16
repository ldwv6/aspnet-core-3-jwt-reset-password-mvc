using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployreeService
{
    public class EmployeesSecurity
    {
        public static bool Login(string username, string password)
        {
            using(dataEntities entities = new dataEntities())
            {
                return entities.Users.Any(user => user.UserName.Equals(StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}