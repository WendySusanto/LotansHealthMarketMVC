using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LotansHealthMarketMVC.Models
{
    public class CashierModel
    {
        [Key]
        public String CashierID { get; set; }
        public String BranchID { get; set; }
        public String CashierName { get; set; }
    }
}
