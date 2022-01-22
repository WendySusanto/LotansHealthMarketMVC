using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LotansHealthMarketMVC.Models
{
    public class BranchModel
    {
        [Key]
        public String BranchID { get; set; }
        public String BranchLocation { get; set; }
        public String SupervisorName { get; set; }
    }
}
