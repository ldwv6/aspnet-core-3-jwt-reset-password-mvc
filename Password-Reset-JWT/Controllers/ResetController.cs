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

			SendNoticeMail("ldwv6@interpark.com", user.Token);
			return View();
		}



		[HttpGet]
		public string Pass(string id)
		{
			HttpClient httpCli = new HttpClient();


			httpCli.DefaultRequestHeaders.Add("Authorization", "Bearer " + id);

			HttpResponseMessage message = httpCli.GetAsync("http://localhost:64980/reset/resetPasswd").Result;


			if (message.IsSuccessStatusCode)
			{
				return "succcess";
			}

			return "false";
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
				@"PASSWORD : {0}", token);
			msg.IsBodyHtml = true;
			smtp.Send(msg);
		}
	}
}