using SpiralWorksWalletBackendExam.DataServices.UserAccountRelated;
using SpiralWorksWalletBackendExam.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpiralEWalletBackend.UnitTest.UserAccountTests
{
    public class UserAccountWithdrawTest
    {
        #region Fields
        private readonly IUserAccountService _userAccountService;
        #endregion
        #region Constructor
        public UserAccountWithdrawTest(IUserAccountService userAccountService)
        {
            this._userAccountService = userAccountService;
        }
        #endregion
        #region Tests 
        [Fact]
        public async void WithdrawGreaterAmountToCurrentBalance()
        {
            // TODO: Always Check the Current Balance of user in the Database Before Testing.
            var withdrawRequest = await _userAccountService.WithdawOrDeposit(1, 999999, TransactionType.WITHDRAW);
            Assert.False(withdrawRequest.IsSuccess);
        }
        [Fact]
        public async void WithdrawZeroAmount()
        {
            // TODO: Always Check the Current Balance of user in the Database Before Testing.
            var withdrawRequest = await _userAccountService.WithdawOrDeposit(1, 0, TransactionType.WITHDRAW);
            Assert.False(withdrawRequest.IsSuccess);
        }
        [Fact]
        public async void NormalWithdraw()
        {
            // TODO: Always Check the Current Balance of user in the Database Before Testing.
            var withdrawRequest = await _userAccountService.WithdawOrDeposit(1, 200, TransactionType.WITHDRAW);
            Assert.True(withdrawRequest.IsSuccess);
        }
        [Fact]
        public async void InvalidUserWithdraw()
        {
            // TODO: Always Check the Current Balance of user in the Database Before Testing.
            var withdrawRequest = await _userAccountService.WithdawOrDeposit(-1, 200, TransactionType.WITHDRAW);
            Assert.False(withdrawRequest.IsSuccess);
        }
        #endregion
    }
}
