using SpiralWorksWalletBackendExam.Dtos.AuthDto;
using SpiralWorksWalletBackendExam.Dtos.UserAccountDto;
using SpiralWorksWalletBackendExam.Models;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.DataServices.AuthRelated
{
    public interface IAuthService
    {
        Task<UserAccount> UserLogin(UserAccountSaveDto loginCredentials);
        LoginResultReadDto GetToken(UserAccount userAccount);
    }
}