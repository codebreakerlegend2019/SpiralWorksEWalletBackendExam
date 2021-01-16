using SpiralWorksWalletBackendExam.Dtos.TransactionDto;
using SpiralWorksWalletBackendExam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.DataServices.TransactionRelated
{
    public interface ITransactionService
    {
        Task<TransactionReportResultDto> GetAllByUserDateRange(int userAccountId, TransactionReportParameterDto transactionParameter);
    }
}