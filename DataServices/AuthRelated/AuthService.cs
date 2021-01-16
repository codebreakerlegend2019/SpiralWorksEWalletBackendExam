using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpiralWorksWalletBackendExam.DataContexts;
using SpiralWorksWalletBackendExam.Dtos.AuthDto;
using SpiralWorksWalletBackendExam.Dtos.UserAccountDto;
using SpiralWorksWalletBackendExam.Helpers;
using SpiralWorksWalletBackendExam.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.DataServices.AuthRelated
{
    public class AuthService : IAuthService
    {
        #region
        private readonly SpiralEWalletContext _context;
        private readonly IConfiguration _configuration;
        #endregion
        #region Constructor
        public AuthService(SpiralEWalletContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }
        #endregion
        #region Methods
        public async Task<UserAccount> UserLogin(UserAccountSaveDto loginCredentials)
        {
            var userAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(x => x.LoginName == loginCredentials.Username && x.Password == AES.Encrypt(loginCredentials.Password));
            return userAccount;
        }
        public LoginResultReadDto GetToken(UserAccount userAccount)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                            new Claim("LoggedInUsername", AES.Encrypt($"{userAccount.LoginName}")),
                            new Claim(ClaimTypes.Role, "EWalletUser"),
                            new Claim("LoggedInUserId", AES.Encrypt(userAccount.Id.ToString())),
                        }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                Issuer = _configuration.GetSection("TokenAuthentication:Issuer").Value,
                Audience = _configuration.GetSection("TokenAuthentication:Audience").Value
            };
            var tokenBytes = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenBytes);

            return  new LoginResultReadDto()
            {
                LoggedInUser = $"{userAccount.LoginName}",
                Token = token,
                Role = "EWalletUser"
            };
        }
        #endregion
    }
}
