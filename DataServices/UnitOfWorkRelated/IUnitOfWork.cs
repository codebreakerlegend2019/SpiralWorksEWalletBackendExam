using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.DataServices.UnitOfWorkRelated
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}