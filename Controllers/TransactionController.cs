using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiralWorksWalletBackendExam.DataServices.TransactionRelated;
using SpiralWorksWalletBackendExam.Dtos.AuthDto;
using SpiralWorksWalletBackendExam.Dtos.TransactionDto;
using SpiralWorksWalletBackendExam.Enumerations;
using SpiralWorksWalletBackendExam.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EWalletUserPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        #region Fields
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public TransactionController(ITransactionService transactionService,IMapper mapper)
        {
            this._transactionService = transactionService;
            this._mapper = mapper;
        }
        #endregion
        #region Endpoints
        /// <summary>
        /// Get Transaction Reports by Transaction Type (Withdraw, Deposit and Transfer)
        /// </summary>
        /// <remarks>
        /// Sample JSON body to be send:
        /// 
        ///     {
        ///         "transactionType": "string",// It can be "Withdraw", "Deposit", "Transfer" or "All" in any case.
        ///         "dateFrom": "2021-01-16T16:00:51.924Z",
        ///         "dateTo": "2021-01-16T16:00:51.924Z"
        ///     }
        ///     
        /// Sample JSON Body of Withdraw Reports:
        /// 
        ///     [
        ///         {
        ///             "id": 4,
        ///             "accountNumber": "0000000001",
        ///             "beforeBalance": 5000,
        ///             "afterBalance": 4500,
        ///             "amountWithdrew": 500,
        ///             "transactionType": "WITHDRAW",
        ///             "dateTime": "2021-01-15T23:43:27.4095009"
        ///         }
        ///     ]
        ///     
        /// Sample JSON Body of Deposit Reports:
        /// 
        ///     [
        ///         {
        ///             "id": 3,
        ///             "accountNumber": "0000000001",
        ///             "beforeBalance": 2500,
        ///             "afterBalance": 5000,
        ///             "amountWithdrew": 2500,
        ///             "transactionType": "Deposit",
        ///             "dateTime": "2021-01-15T23:42:48.4361285"
        ///         }
        ///     ]
        ///     
        /// Sample JSON Body of Transfer Reports:
        /// 
        ///     [
        ///         {
        ///             "id": 6,
        ///             "beforeBalance": 4000,
        ///             "afterBalance": 2500,
        ///             "transactionType": "TRANSFER",
        ///             "senderAccountNumber": "0000000001 - (You)",
        ///             "recipientAccountNumber": "0000000002",
        ///             "amountTransferred": 1500,
        ///             "dateTime": "2021-01-15T23:49:54.3603526"
        ///         }
        ///     ]
        /// 
        /// Sample JSON Body Mixed or All Types of Transactions:
        /// 
        ///     [
        ///         {
        ///             "id": 3,
        ///             "accountNumber": "0000000001",
        ///             "beforeBalance": 2500,
        ///             "afterBalance": 5000,
        ///             "transactionType": "DEPOSIT",
        ///             "senderAccountNumber": "N/A",
        ///             "recipientAccountNumber": "N/A",
        ///             "amountDeposited": 2500,
        ///             "amountWithdrew": 0,
        ///             "amountTransferred": 0,
        ///             "dateTime": "2021-01-15T23:42:48.4361285"
        ///         },
        ///         {
        ///             "id": 4,
        ///             "accountNumber": "0000000001",
        ///             "beforeBalance": 5000,
        ///             "afterBalance": 4500,
        ///             "transactionType": "WITHDRAW",
        ///             "senderAccountNumber": "N/A",
        ///             "recipientAccountNumber": "N/A",
        ///             "amountDeposited": 0,
        ///             "amountWithdrew": 500,
        ///             "amountTransferred": 0,
        ///             "dateTime": "2021-01-15T23:43:27.4095009"
        ///         }
        ///     ]
        /// </remarks>
        /// <param name="transactionReportParameter"></param>
        /// <returns></returns>
        [HttpPost("generate/report")]
        public async Task<IActionResult> GetReports([FromBody] TransactionReportParameterDto transactionReportParameter)
        {
            var currentUserId = Convert.ToInt32(AES.Decrypt(User.FindFirst("LoggedInUserId").Value));
            var reportRequest = (await _transactionService.GetAllByUserDateRange(currentUserId, transactionReportParameter));
            if (!reportRequest.IsSuccess)
                return BadRequest(reportRequest.Message);
            if (reportRequest.Data == null)
                return NoContent();
            if (transactionReportParameter.TransactionType.Equals(TransactionType.DEPOSIT.ToString(), StringComparison.OrdinalIgnoreCase))
                return Ok(_mapper.Map<List<DepositTransactionReportDto>>(reportRequest.Data));
            if (transactionReportParameter.TransactionType.Equals(TransactionType.WITHDRAW.ToString(), StringComparison.OrdinalIgnoreCase))
                return Ok(_mapper.Map<List<WithdrawTransactionReportDto>>(reportRequest.Data));
            if (transactionReportParameter.TransactionType.Equals(TransactionType.TRANSFER.ToString(), StringComparison.OrdinalIgnoreCase))
                return Ok(_mapper.Map<List<TransferFundTransactionReportDto>>(reportRequest.Data));
            return Ok(_mapper.Map<List<MixedTransactionReportDto>>(reportRequest.Data));
        }
        #endregion
    }
}
