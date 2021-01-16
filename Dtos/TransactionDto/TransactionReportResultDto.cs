using SpiralWorksWalletBackendExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Dtos.TransactionDto
{
    public class TransactionReportResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<Transaction> Data { get; set; }
        public TransactionReportResultDto(string message)
        {
            Message = message;
        }
        public TransactionReportResultDto(List<Transaction> transactions)
        {
            Data = transactions;
            IsSuccess = true;
        }
    }
}
