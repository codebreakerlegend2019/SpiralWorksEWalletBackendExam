using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpiralWorksWalletBackendExam.DataContexts;
using SpiralWorksWalletBackendExam.DataServices.TransactionRelated;
using SpiralWorksWalletBackendExam.DataServices.UserAccountRelated;
using SpiralWorksWalletBackendExam.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace SpiralEWalletBackend.UnitTest
{
    public class Startup
    {
        private const string _spiralEWalletDatabaseConnectionString = "server=spiralewallet.database.windows.net; database=spiralewallet ; user id=sa-ad; password=Admin12345!; Application Name=Development SpiralEWallet;";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SpiralEWalletContext>(options => options.UseLazyLoadingProxies()
           .UseSqlServer(_spiralEWalletDatabaseConnectionString,
          sqlServerOptions => sqlServerOptions.CommandTimeout(999)));
  
            services.AddScoped<IUserAccountService, UserAccountService>();  
            services.AddScoped<ITransactionService, TransactionService>();
            services.AutoRegisterByInterfaceName("ICreate");
        }
      
    }
}
