using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpiralWorksWalletBackendExam.DataServices.UserAccountRelated;
using SpiralWorksWalletBackendExam.Dtos.AuthDto;
using SpiralWorksWalletBackendExam.Dtos.TransactionDto;
using SpiralWorksWalletBackendExam.Enumerations;
using SpiralWorksWalletBackendExam.Helpers;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EWalletUserPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        #region Fields
        private readonly IUserAccountService _userAccountService;
        #endregion
        #region Constructor
        public UserAccountController(IUserAccountService userAccountService)
        {
            this._userAccountService = userAccountService;
        }
        #endregion
        #region Endpoints
        /// <summary>
        /// Deposit fund from account, requires token from Login.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromQuery]double amount)
        {
            var currentUserId = Convert.ToInt32(AES.Decrypt(User.FindFirst("LoggedInUserId").Value));
            var processDeposit = await _userAccountService.WithdawOrDeposit(currentUserId, amount,TransactionType.DEPOSIT);
            if (!processDeposit.IsSuccess)
                return BadRequest(processDeposit.Message);
            return Ok("Successfully Deposited");
        }
        /// <summary>
        /// Withdraw fund from account, requires token from Login.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromQuery] double amount)
        {
            var currentUserId = Convert.ToInt32(AES.Decrypt(User.FindFirst("LoggedInUserId").Value));
            var processDeposit = await _userAccountService.WithdawOrDeposit(currentUserId, amount, TransactionType.WITHDRAW);
            if (!processDeposit.IsSuccess)
                return BadRequest(processDeposit.Message);
            return Ok("Successfully Withdraw");
        }
        /// <summary>
        /// Transfer fund from one account to another, requires token from Login.
        /// </summary>
        /// <param name="transferDetails"></param>
        /// <param name="transferDetails.TransactionType"> It can be Deposit, Withdraw and Transfer in any case.</param>"
        /// <returns></returns>
        [HttpPost("transfer")]
        public async Task<IActionResult>TransferFund([FromBody] TransferFundDto transferDetails)
        {
            var currentUserId = Convert.ToInt32(AES.Decrypt(User.FindFirst("LoggedInUserId").Value));
            var processTransfer = await _userAccountService.TransferFundToOtherUser(currentUserId, transferDetails);
            if (!processTransfer.IsSuccess)
                return BadRequest(processTransfer.Message);
            return Ok("Successfull Fund Transfer");
        }
        #endregion
    }
}
