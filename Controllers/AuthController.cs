using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpiralWorksWalletBackendExam.DataServices.AuthRelated;
using SpiralWorksWalletBackendExam.DataServices.UnitOfWorkRelated;
using SpiralWorksWalletBackendExam.DataServices.UserAccountRelated;
using SpiralWorksWalletBackendExam.Dtos.AuthDto;
using SpiralWorksWalletBackendExam.Dtos.UserAccountDto;
using SpiralWorksWalletBackendExam.Helpers;
using SpiralWorksWalletBackendExam.Interfaces;
using SpiralWorksWalletBackendExam.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Field
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;
        private readonly ICreate<UserAccount> _createUserAccountService;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructor
        public AuthController(IAuthService authService, IMapper mapper,
            IUserAccountService userAccountService,ICreate<UserAccount> createUserAccountService,
            IUnitOfWork unitOfWork)
        {
            this._authService = authService;
            this._mapper = mapper;
            this._userAccountService = userAccountService;
            this._createUserAccountService = createUserAccountService;
            this._unitOfWork = unitOfWork;
        }
        #endregion
        #region Endpoints
        /// <summary>
        /// Generate Token to use the other services like Deposit, Withdraw, Transfer and Getting Transaction Reports.
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserAccountSaveDto loginCredentials) 
        {
            var userAccount = await _authService.UserLogin(loginCredentials);
            if (userAccount == null)
                return Unauthorized();
            return Ok(_authService.GetToken(userAccount));
        }
        /// <summary>
        /// Register to the E-Wallet
        /// </summary>
        /// <param name="userAccountDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserAccountSaveDto userAccountDto)
        {
            if (await _userAccountService.IsUsernameAlreadyExist(userAccountDto.Username.Trim()))
                return BadRequest("Username Already Existed");
            var userAccount = _mapper.Map<UserAccount>(userAccountDto);
            userAccount.AccountNumber = await _userAccountService.GenerateAccountNumber();
            _createUserAccountService.Create(userAccount);
            if (!await _unitOfWork.SaveChangesAsync())
                return BadRequest("No UserAccount Saved!");
            return StatusCode(201);
        }
        #endregion
    }
}
