using Microsoft.EntityFrameworkCore;
using SpiralWorksWalletBackendExam.DataContexts;
using SpiralWorksWalletBackendExam.DataServices.UnitOfWorkRelated;
using SpiralWorksWalletBackendExam.Dtos.TransactionDto;
using SpiralWorksWalletBackendExam.Enumerations;
using SpiralWorksWalletBackendExam.Helpers;
using SpiralWorksWalletBackendExam.Interfaces;
using SpiralWorksWalletBackendExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.DataServices.UserAccountRelated
{
    public class UserAccountService : ICreate<UserAccount>, IUserAccountService
    {
        #region Fields
        private readonly SpiralEWalletContext _context;
        #endregion
        #region Constructor
        public UserAccountService(SpiralEWalletContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methods
        public void Create(UserAccount model)
        {
            _context.Add(model);
        }
        public async Task<bool> IsUsernameAlreadyExist(string username)
        {
            return await _context.UserAccounts.AnyAsync(x => x.LoginName == username);
        }
        public async Task<TransactionResultDto> WithdawOrDeposit(int userAccountId, double Amount, TransactionType transactionType)
        {
            if (Amount <= 0)
                return new TransactionResultDto("Amount Should be greater than 0");
            var userAccount = await _context.UserAccounts.FindAsync(userAccountId);
            if (userAccount == null)
                return new TransactionResultDto("UserAccount NotFound");
            var userCurrentBalance = userAccount.Balance;
            if (transactionType == TransactionType.WITHDRAW && Amount > userCurrentBalance)
                return new TransactionResultDto("Withdraw amount is greater than current balance");
                userAccount.Balance = (transactionType == TransactionType.DEPOSIT) ? userCurrentBalance + Amount 
                : userCurrentBalance - Amount;
            var transaction = new Transaction()
            {
                TransactionType = transactionType.ToString(),
                UserAccountId = userAccount.Id,
                BeforeBalance = userCurrentBalance,
                AfterBalance = (transactionType == TransactionType.DEPOSIT) ? userCurrentBalance + Amount : userCurrentBalance - Amount,
                AmountDeposited = (transactionType == TransactionType.DEPOSIT)? Amount:0,
                AmountWithdrew = (transactionType == TransactionType.WITHDRAW)? Amount:0
            };
            _context.Add(transaction);
            if (await _context.SaveChangesAsync() == 0)
                return new TransactionResultDto("No Deposit Happpened");
            return new TransactionResultDto();
        }

        public async Task<TransactionResultDto> TransferFundToOtherUser(int userId, TransferFundDto transferDetail)
        {
            if (string.IsNullOrEmpty(transferDetail.DestinationAccountNumber))
                return new TransactionResultDto("Destination Account Number is Empty");
            if (transferDetail.Amount <= 0)
                return new TransactionResultDto("Amount should be greater than 0");
            var sender = await _context.UserAccounts.FindAsync(userId);
            if (sender == null)
                return new TransactionResultDto("Sender Not Found!");

            if (transferDetail.Amount > sender.Balance)
                return new TransactionResultDto("Amount to transfer is greater than existing Balance!");

            var senderCurrentBalance = sender.Balance;

            var recipient = await _context.UserAccounts.FirstOrDefaultAsync(x => x.AccountNumber == transferDetail.DestinationAccountNumber);
            if (recipient == null)
                return new TransactionResultDto("Destination Account Number Not Found");

            var reciepientCurrentBalance = recipient.Balance;

            sender.Balance = senderCurrentBalance - transferDetail.Amount;
            recipient.Balance = reciepientCurrentBalance + transferDetail.Amount;

            var senderTransaction = new Transaction()
            {
                TransactionType = TransactionType.TRANSFER.ToString(),
                RecipientAccountNumber = transferDetail.DestinationAccountNumber,
                AmountTransferred = transferDetail.Amount,
                SenderAccountNumber =sender.AccountNumber,
                BeforeBalance = senderCurrentBalance,
                AfterBalance = senderCurrentBalance - transferDetail.Amount,
                UserAccountId = userId
            };
            var receiptiendTransaction = new Transaction()
            {
                TransactionType = TransactionType.TRANSFER.ToString(),
                SenderAccountNumber = sender.AccountNumber,
                RecipientAccountNumber = recipient.AccountNumber,
                AmountTransferred = transferDetail.Amount,
                BeforeBalance = reciepientCurrentBalance,
                AfterBalance = reciepientCurrentBalance + transferDetail.Amount,
                UserAccountId = recipient.Id
            };

            _context.Add(senderTransaction);
            _context.Add(receiptiendTransaction);

            if (await _context.SaveChangesAsync() == 0)
                return new TransactionResultDto("No Transfer Happened");
            return new TransactionResultDto();

        }
        public async Task<string> GenerateAccountNumber()
        {
            var previousUser = await _context.UserAccounts.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (previousUser == null)
                return 1.ToString("0000000000");
            else
                return $"{previousUser.Id+1.ToString("0000000000")}";
        }
        #endregion


    }
}
