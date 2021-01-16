using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AccountNumber { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public UserAccount()
        {
            Transactions = new Collection<Transaction>();
            CreatedDate = DateTime.Now;
        }
    }
}
