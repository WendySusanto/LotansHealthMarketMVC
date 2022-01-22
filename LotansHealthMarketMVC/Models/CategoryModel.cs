using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LotansHealthMarketMVC.Models
{
    public class CategoryModel
    {
        [Key]
        public String CategoryID { get; set; }
        public String CategoryName { get; set; }
    }
}
