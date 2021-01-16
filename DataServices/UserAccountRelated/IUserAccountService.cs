using SpiralWorksWalletBackendExam.Dtos.TransactionDto;
using SpiralWorksWalletBackendExam.Enumerations;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.DataServices.UserAccountRelated
{
    public interface IUserAccountService
    {
        Task<string> GenerateAccountNumber();
        Task<TransactionResultDto> TransferFundToOtherUser(int userId, TransferFundDto transferDetail);
        Task<TransactionResultDto> WithdawOrDeposit(int userAccountId, double Amount, TransactionType transactionType);
        Task<bool> IsUsernameAlreadyExist(string username);
    }
}