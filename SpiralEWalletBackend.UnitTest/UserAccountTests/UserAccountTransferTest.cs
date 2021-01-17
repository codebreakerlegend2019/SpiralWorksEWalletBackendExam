using SpiralWorksWalletBackendExam.DataServices.UserAccountRelated;
using SpiralWorksWalletBackendExam.Dtos.TransactionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpiralEWalletBackend.UnitTest.UserAccountTests
{
    public class UserAccountTransferTest
    {
        #region Fields
        private readonly IUserAccountService _userAccountService;
        #endregion
        #region Constructor
        public UserAccountTransferTest(IUserAccountService userAccountService)
        {
            this._userAccountService = userAccountService;
        }
        #endregion
        #region Tests
        [Fact]
        public async void DestinationAccountEmpty()
        {
            var transferFundDetails = new TransferFundDto()
            {
                DestinationAccountNumber = string.Empty,
                Amount = 200, 
            };
            var transferFundRequest = await _userAccountService.TransferFundToOtherUser(1, transferFundDetails);
            Assert.False(transferFundRequest.IsSuccess);
        }
        [Fact]
        public async void AmountIsZeroOrNegative()
        {
            var transferFundDetails = new TransferFundDto()
            {
                DestinationAccountNumber = "0000000002",
                Amount = 0,
            };
            var transferFundRequest = await _userAccountService.TransferFundToOtherUser(1, transferFundDetails);
            Assert.False(transferFundRequest.IsSuccess);
        }
        [Fact]
        public async void DestinationAccountNotFound()
        {
            var transferFundDetails = new TransferFundDto()
            {
                DestinationAccountNumber = "XXXXXXXXXXX",
                Amount = 500,
            };
            var transferFundRequest = await _userAccountService.TransferFundToOtherUser(1, transferFundDetails);
            Assert.False(transferFundRequest.IsSuccess);
        }
        [Fact]
        public async void SenderNotFound()
        {
            var transferFundDetails = new TransferFundDto()
            {
                DestinationAccountNumber = "0000000002",
                Amount = 99999,
            };
            var transferFundRequest = await _userAccountService.TransferFundToOtherUser(0, transferFundDetails);
            Assert.False(transferFundRequest.IsSuccess);
        }
        [Fact]
        public async void SenderBalanceIsGreaterThanAmountToBeSent()
        {
            var transferFundDetails = new TransferFundDto()
            {
                DestinationAccountNumber = "0000000002",
                Amount = 99999,
            };
            var transferFundRequest = await _userAccountService.TransferFundToOtherUser(1, transferFundDetails);
            Assert.False(transferFundRequest.IsSuccess);
        }

        [Fact]
        public async void TransferFundNormally()
        {
            var transferFundDetails = new TransferFundDto()
            {
                DestinationAccountNumber = "0000000002",
                Amount = 5,
            };
            var transferFundRequest = await _userAccountService.TransferFundToOtherUser(1, transferFundDetails);
            Assert.True(transferFundRequest.IsSuccess);
        }
        #endregion
    }
}
