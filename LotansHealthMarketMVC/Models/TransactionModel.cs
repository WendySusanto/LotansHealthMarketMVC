using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LotansHealthMarketMVC.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionNum { get; set; }
        public String TransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public String CashierID { get; set; }
        public String PaymentID { get; set; }      

    }
}
