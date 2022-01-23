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
        public DbSet<ItemModel> Item { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<PaymentModel> Payment { get; set; }
        public DbSet<TransactionModel> Transaction { get; set; }
        public DbSet<TransactionDetailModel> TransactionDetail { get; set; }
    }
}
