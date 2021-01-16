using Microsoft.EntityFrameworkCore;
using SpiralWorksWalletBackendExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.DataContexts
{
    public class SpiralEWalletContext:DbContext
    {
        #region Constructor
        public SpiralEWalletContext(DbContextOptions<SpiralEWalletContext> options):base(options)
        {

        }
        #endregion

        #region Database Tables / DbSets
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        #endregion
    }
}
