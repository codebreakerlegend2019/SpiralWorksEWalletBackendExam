using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Dtos.TransactionDto
{
    public class WithdrawTransactionReportDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double BeforeBalance { get; set; }
        public double AfterBalance { get; set; }
        public double AmountWithdrew { get; set; }
        public string TransactionType { get; set; }
        public DateTime DateTime { get; set; }
    }
}
