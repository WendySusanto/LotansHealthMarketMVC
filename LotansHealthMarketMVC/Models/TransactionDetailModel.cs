using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LotansHealthMarketMVC.Models
{
    public class TransactionDetailModel
    {
        [Key]
        public int TransactionNum { get; set; }
        public String ItemID { get; set; }
        public int ItemQty { get; set; }
    }
}
