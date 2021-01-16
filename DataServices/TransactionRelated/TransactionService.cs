using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpiralWorksWalletBackendExam.DataContexts;
using SpiralWorksWalletBackendExam.Dtos.TransactionDto;
using SpiralWorksWalletBackendExam.Enumerations;
using SpiralWorksWalletBackendExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.DataServices.TransactionRelated
{
    public class TransactionService : ITransactionService
    {
        #region Fields
        private readonly SpiralEWalletContext _context;
        #endregion
        #region Contructor
        public TransactionService(SpiralEWalletContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methods
        public async Task<TransactionReportResultDto> GetAllByUserDateRange(int userAccountId, TransactionReportParameterDto transactionParameter)
        {
            if (transactionParameter.DateFrom > transactionParameter.DateTo)
                return new TransactionReportResultDto("Datefrom should not be greater than Dateto");
            if (transactionParameter.TransactionType.ToLower() != TransactionType.DEPOSIT.ToString().ToLower() &&
                transactionParameter.TransactionType.ToLower() != TransactionType.WITHDRAW.ToString().ToLower() &&
                transactionParameter.TransactionType.ToLower() != TransactionType.TRANSFER.ToString().ToLower() &&
                transactionParameter.TransactionType.ToLower() != TransactionType.ALL.ToString().ToLower())
                return new TransactionReportResultDto("Invalid Transaction Type");
            if (transactionParameter.TransactionType.Equals(TransactionType.ALL.ToString(),StringComparison.OrdinalIgnoreCase))
            {
                var allTransactions = await _context.Transactions
                    .Include(x => x.UserAccount)
                    .OrderBy(x => x.CreatedDate)
                    .Where(x => x.UserAccountId == userAccountId &&
                    (x.CreatedDate >= transactionParameter.DateFrom && x.CreatedDate <= transactionParameter.DateTo))
                    .ToListAsync();
                return new TransactionReportResultDto(allTransactions);
            }
            var transactionsByType = await _context.Transactions
                .Include(x => x.UserAccount)
                .OrderBy(x => x.CreatedDate)
                .Where(x => x.UserAccountId == userAccountId && x.TransactionType.ToLower() == transactionParameter.TransactionType.ToLower() &&
                (x.CreatedDate >= transactionParameter.DateFrom && x.CreatedDate <= transactionParameter.DateTo))
                .ToListAsync();
            return new TransactionReportResultDto(transactionsByType);
        }
        #endregion
    }
}
