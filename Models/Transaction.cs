using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public double BeforeBalance { get; set; }
        public double AfterBalance { get; set; }
        public string TransactionType { get; set; }
        public string SenderAccountNumber { get; set; }
        public string RecipientAccountNumber { get; set; }
        public double AmountDeposited { get; set; }
        public double AmountWithdrew { get; set; }
        public double AmountTransferred { get; set; }
        public DateTime CreatedDate {get; set; }
        public int UserAccountId { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        public Transaction()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
