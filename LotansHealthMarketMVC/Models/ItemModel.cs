using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LotansHealthMarketMVC.Models
{
    public class ItemModel
    {
        [Key]
        public String ItemID { get; set; }
        public String CategoryID { get; set; }
        public String ItemName { get; set; }
        public int ItemPrice { get; set; }
    }
}
