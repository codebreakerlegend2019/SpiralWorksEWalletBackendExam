using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Dtos.TransactionDto
{
    public class TransactionResultDto
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public TransactionResultDto(string errorMessage)
        {
            Message = errorMessage;
        }
        public TransactionResultDto()
        {
            IsSuccess = true;
        }
    }
}
