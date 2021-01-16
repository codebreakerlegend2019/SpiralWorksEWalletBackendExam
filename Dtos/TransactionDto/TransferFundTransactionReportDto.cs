using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Dtos.TransactionDto
{
    public class TransferFundTransactionReportDto
    {
        public int Id { get; set; }
        public double BeforeBalance { get; set; }
        public double AfterBalance { get; set; }
        public string TransactionType { get; set; }
        public string SenderAccountNumber { get; set; }
        public string RecipientAccountNumber { get; set; }
        public double AmountTransferred { get; set; }
        public DateTime DateTime { get; set; }
    }
}
