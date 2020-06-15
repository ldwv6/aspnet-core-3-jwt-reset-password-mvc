using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployreeService.Controllers
{
    public class EmployeesController : ApiController
    {

        //GetSomthing 식으로 변경 가능 
        //완전히 변경하고 싶다면 [HttpGet] 와 같은 필터 사용 


        public HttpResponseMessage Get(string gender = "All")
        {

            using (dataEntities entities = new dataEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList<Employees>());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList<Employees>());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                           entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList<Employees>());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for gender must be All, Male or Female." + gender + " is invaled");

                }
            }
        }

        [HttpGet]
        public HttpResponseMessage LoadAllEmployeesById(int id)
        {
            using (var data = new dataEntities())
            {
                var entity = data.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not Found");
                }
            }
        }


        public HttpResponseMessage Post([FromBody] Employees employee)
        {
            try
            {
                using (dataEntities entities = new dataEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Delete(int id)
        {

            try
            {
                using (dataEntities entities = new dataEntities())
                {

                    //삭제한걸 또 삭제할 경우 Rmove 에 null 전달 후 예외 발생 

                    //entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.ID == id));

                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not Found to delete");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Employees employee)
        {

            // 500 internal server error 가 발생할 수 있다. 

            using (dataEntities entities = new dataEntities())
            {
                try
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not Found to update");
                    }

                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}
