using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Dtos.TransactionDto
{
    public class TransactionReportParameterDto
    {
        public string TransactionType { get; set; } // Can be Withdraw, Deposit and Transfer in any case (capital,small or mixed).
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
