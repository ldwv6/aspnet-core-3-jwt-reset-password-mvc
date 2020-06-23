using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmpoyeeService.Controllers
{
    public class EmployeeServiceController : ApiController
    {
        [Authorize]
        public IEnumerable<EMployees> Get()
        {
            using(employeeDBEntities entites = new employeeDBEntities())
            {
                return entites.EMployees.ToList();
            }
        }
    }
}
