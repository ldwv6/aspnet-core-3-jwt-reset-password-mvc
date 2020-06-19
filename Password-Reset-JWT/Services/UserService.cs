using Password_Reset_JWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Password_Reset_JWT.Helpers;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Password_Reset_JWT.Services
{

    public interface IUserService
    {
        User Authenticate(string username);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username)
        {
            User aduserlist = new User();

            var context = new PrincipalContext(ContextType.Domain, _appSettings.DomainControlerIP, _appSettings.fqdn, _appSettings.AdminID,_appSettings.Passwd);

            using (var aduser = UserPrincipal.FindByIdentity(context, username))
            {
                if (aduser == null)
                    return null;

                aduserlist.UserID = aduser.SamAccountName;
                aduserlist.UserMail = aduser.EmailAddress;

                // return null if user not found           
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, aduserlist.UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            aduserlist.Token = tokenHandler.WriteToken(token);

            return aduserlist;
        }

    }

}
