using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Password_Reset_JWT.Models;
using Password_Reset_JWT.Services;

namespace Password_Reset_JWT.Controllers
{
    public class ResetController : Controller
    {

        private IUserService _userService;

        public ResetController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NoLoginID()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TokenExpire()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetSand([FromForm] User obj)
        {

            var user = _userService.Authenticate(obj.UserID);

            if (user == null)
                return RedirectToAction("NoLoginID", "Reset");

            SendNoticeMail(user.UserMail, user.Token);
            return View();
        }

        [HttpGet]
        public IActionResult Pass(string id)
        {
            HttpClient httpCli = new HttpClient();


            httpCli.DefaultRequestHeaders.Add("Authorization", "Bearer " + id);

            ViewBag.id = id;


            HttpResponseMessage message = httpCli.GetAsync("http://localhost:64980/reset/resetPasswd").Result;

            /// 성공 시 패스워드 링크 전달 

            if (message.IsSuccessStatusCode)
            {
                return View();
            }

            return RedirectToAction("TokenExpire", "Reset");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ResetPasswd()
        {

            //https://stackoverflow.com/questions/38340078/how-to-decode-jwt-token >> 토근 클레임 정보 확인 방법

            var principal = HttpContext.User;

            var a = principal.Claims.SingleOrDefault(p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            return View();
        }

        [HttpPost]
        public HttpResponseMessage Test()
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
   
        private static void SendNoticeMail(string addr, string token)
        {
            SmtpClient smtp = new SmtpClient("imail1.interpark.com");

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("ldap@interpark.com");
            msg.To.Add(addr);
            msg.Subject = string.Format("[LDAP] {0} 계정 패스워드 초기화 ", addr);

            msg.Body = string.Format(
                "<br> We heard that you lost your LDAP password.Sorry about that!" +
                "<br><br> But don’t worry! You can use the following link to reset your password:" +
                "<br><br><a href=\"http://localhost:64980/Reset/Pass/" + token + "\"" + "target=\"_blank\">http://localhost:64980/Reset/Pass/" + token + "</a>" +
                "<br><br>If you don’t use this link within 10 min, it will expire. To get a new password reset link, visit http://localhost:64980/Reset"
                );
            msg.IsBodyHtml = true;
            smtp.Send(msg);
        }
    }
}