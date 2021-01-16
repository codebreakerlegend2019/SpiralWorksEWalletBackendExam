using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Dtos.TransactionDto
{
    public class TransferFundDto
    {
        public string DestinationAccountNumber { get; set; }
        public double Amount { get; set; }
    }
}
