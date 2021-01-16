using SpiralWorksWalletBackendExam.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.DataServices.UnitOfWorkRelated
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly SpiralEWalletContext _context;
        #endregion
        public UnitOfWork(SpiralEWalletContext context)
        {
            this._context = context;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
