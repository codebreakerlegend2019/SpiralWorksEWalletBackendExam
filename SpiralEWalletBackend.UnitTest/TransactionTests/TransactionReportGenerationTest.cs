using SpiralWorksWalletBackendExam.DataServices.TransactionRelated;
using SpiralWorksWalletBackendExam.Dtos.TransactionDto;
using SpiralWorksWalletBackendExam.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpiralEWalletBackend.UnitTest.TransactionTests
{
    public class TransactionReportGenerationTest
    {
        #region Field
        private readonly ITransactionService _transactionService;
        #endregion
        #region Constructor
        public TransactionReportGenerationTest(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }
        #endregion
        #region Tests
        [Fact]
        public async void InvalidDateRange()
        {
            var transactionParameter = new TransactionReportParameterDto()
            {
                TransactionType = "All",
                DateFrom = DateTime.Parse("January 16, 2021 12:00AM"),
                DateTo = DateTime.Parse("January 14, 2021 12:00AM")
            };
            var getTransactionRequest = await _transactionService.GetAllByUserDateRange(1, transactionParameter);
            Assert.False(getTransactionRequest.IsSuccess);
        }
        [Fact]
        public async void InvalidTransactionType()
        {
            var transactionParameter = new TransactionReportParameterDto()
            {
                TransactionType = "asdasdasdasdasd",
                DateFrom = DateTime.Parse("January 15, 2021 12:00AM"),
                DateTo = DateTime.Parse("January 16, 2021 12:00AM")
            };
            var getTransactionRequest = await _transactionService.GetAllByUserDateRange(1, transactionParameter);
            Assert.False(getTransactionRequest.IsSuccess);
        }
        [Fact]
        public async void GettingAllDepositTransactions()
        {
            var transactionParameter = new TransactionReportParameterDto()
            {
                TransactionType = TransactionType.DEPOSIT.ToString(),
                DateFrom = DateTime.Parse("January 15, 2021 12:00AM"),
                DateTo = DateTime.Parse("January 16, 2021 12:00AM")
            };
            var getTransactionRequest = await _transactionService.GetAllByUserDateRange(1, transactionParameter);
            Assert.True(getTransactionRequest.IsSuccess);
        }
        [Fact]
        public async void GettingAllWithdrawTransactions()
        {
            var transactionParameter = new TransactionReportParameterDto()
            {
                TransactionType = TransactionType.WITHDRAW.ToString(),
                DateFrom = DateTime.Parse("January 15, 2021 12:00AM"),
                DateTo = DateTime.Parse("January 16, 2021 12:00AM")
            };
            var getTransactionRequest = await _transactionService.GetAllByUserDateRange(1, transactionParameter);
            Assert.True(getTransactionRequest.IsSuccess);
        }
        [Fact]
        public async void GettingAllTransferFundTransactions()
        {
            var transactionParameter = new TransactionReportParameterDto()
            {
                TransactionType = TransactionType.TRANSFER.ToString(),
                DateFrom = DateTime.Parse("January 15, 2021 12:00AM"),
                DateTo = DateTime.Parse("January 16, 2021 12:00AM")
            };
            var getTransactionRequest = await _transactionService.GetAllByUserDateRange(1, transactionParameter);
            Assert.True(getTransactionRequest.IsSuccess);
        }
        [Fact]
        public async void GettingAllMixedTransactions()
        { 
            var transactionParameter = new TransactionReportParameterDto()
            {
                TransactionType = TransactionType.ALL.ToString(),
                DateFrom = DateTime.Parse("January 15, 2021 12:00AM"),
                DateTo = DateTime.Parse("January 16, 2021 12:00AM")
            };
            var getTransactionRequest = await _transactionService.GetAllByUserDateRange(1, transactionParameter);
            Assert.True(getTransactionRequest.IsSuccess);
        }
        #endregion
    }
}
