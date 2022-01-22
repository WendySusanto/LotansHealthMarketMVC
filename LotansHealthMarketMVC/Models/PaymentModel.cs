using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LotansHealthMarketMVC.Models
{
    public class PaymentModel
    {
        [Key]
        public string PaymentID { get; set; }
        public string PaymentName { get; set; }

    }
}