using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LotansHealthMarketMVC.Models
{
    

    public class db
    {
        SqlConnection con = new SqlConnection("Data Source=SQL5109.site4now.net,1433;Initial Catalog=db_a81e3f_lotanmarket;User Id=db_a81e3f_lotanmarket_admin;Password=BillyMs49;");

        public db()
        {
            var configuration = GetConfiguration();
        }

        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public DataSet GetRecord()
        {
            //SqlCommand com = new SqlCommand("Sp_Transaction", con);
            //SqlConnection con = new SqlConnection("Data Source=SQL5109.site4now.net,1433;Initial Catalog=db_a81e3f_lotanmarket;User Id=db_a81e3f_lotanmarket_admin;Password=BillyMs49;");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "Execute Sp_Transaction";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
