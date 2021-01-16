using SpiralWorksWalletBackendExam.DataServices.UserAccountRelated;
using SpiralWorksWalletBackendExam.Enumerations;
using System;
using Xunit;

namespace SpiralEWalletBackend.UnitTest.UserAccountTests
{
    public class UserAccountDepositTest
    {
        #region Fields
        private readonly IUserAccountService _userAccountService;
        #endregion
        #region Constructor
        public UserAccountDepositTest(IUserAccountService userAccountService)
        {
            this._userAccountService = userAccountService;
        }
        #endregion
        #region Tests
        [Fact]
        public async void DepositWithZeroAmount()
        {
            // TODO: Always Check the Current Balance of user in the Database Before Testing.
            var depositRequest = await _userAccountService.WithdawOrDeposit(1, 0, TransactionType.DEPOSIT);
            Assert.False(depositRequest.IsSuccess);
        }
        [Fact]
        public async void NormalDeposit()
        {
            // TODO: Always Check the Current Balance of user in the Database Before Testing.
            var depositRequest = await _userAccountService.WithdawOrDeposit(1, 1000, TransactionType.DEPOSIT);
            Assert.True(depositRequest.IsSuccess);
        }
        [Fact]
        public async void InvalidUserDeposit()
        {
            // TODO: Always Check the Current Balance of user in the Database Before Testing.
            var withdrawRequest = await _userAccountService.WithdawOrDeposit(-1, 200, TransactionType.DEPOSIT);
            Assert.False(withdrawRequest.IsSuccess);
        }
        #endregion
    }
}
