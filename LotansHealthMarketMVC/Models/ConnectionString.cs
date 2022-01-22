using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LotansHealthMarketMVC.Models
{
    public class ConnectionString : DbContext
    {
        public ConnectionString(DbContextOptions<ConnectionString> options) : base(options)
        {

        }

        public DbSet<BranchModel> Branch { get; set; }
        public DbSet<CashierModel> Cashier { get; set; }

    }
}
