using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public IActionResult ResetSand([FromForm] UserMail obj)
        {

            var user = _userService.Authenticate(obj.Mail);

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

            HttpResponseMessage message = httpCli.GetAsync("http://localhost:64980/reset/resetPasswd").Result;

            /// 성공 시 패스워드 링크 전달 

            if (message.IsSuccessStatusCode)
            {
                return View();
            }

            return RedirectToAction("NoLoginID", "Reset");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ResetPasswd()
        {
            return View();
        }


        private static void SendNoticeMail(string addr, string token)
        {
            SmtpClient smtp = new SmtpClient("imail1.interpark.com");

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("ldap@interpark.com");
            msg.To.Add(addr);
            msg.Subject = string.Format("[LDAP] {0} 계정 패스워드 초기화 ", addr);
         
            msg.Body = string.Format(
                "<br> We heard that you lost your GitHub password.Sorry about that!" +
                "<br><br> But don’t worry! You can use the following link to reset your password:" +
                "<br><br><a href=\"http://localhost:64980/Reset/Pass/" + token + "\"" + "target=\"_blank\">http://localhost:64980/Reset/Pass/" + token + "</a>" +
                "<br><br>If you don’t use this link within 3 hours, it will expire. To get a new password reset link, visit http://localhost:64980/Reset"
                );
            msg.IsBodyHtml = true;
            smtp.Send(msg);
        }
    }
}