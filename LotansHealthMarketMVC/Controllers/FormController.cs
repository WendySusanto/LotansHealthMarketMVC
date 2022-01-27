using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LotansHealthMarketMVC.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Diagnostics;
using System.Web;
using OfficeOpenXml;

namespace LotansHealthMarketMVC.Controllers
{
    public class FormController : Controller
    {

        private readonly ConnectionString _cc;
        db dbop = new db();

        public FormController(ConnectionString cc)
        {
            _cc = cc;
        }

        public IActionResult Index()
        {
            //List<BranchModel> branchs = new List<BranchModel>();

            //branchs = (from x in _cc.Branch select x).ToList();

            //ViewBag.message = branchs;

            var result = _cc.Branch.ToList();

            return View(result);
        }

        public IActionResult ExporttoExcel()
        {
            DataSet ds = dbop.GetRecord();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells.LoadFromDataTable(ds.Tables[0], true);
                package.Save();
            }
            stream.Position = 0;
            string excelname = $"Transaction List - {DateTime.Now.ToString("yyyyMMdd")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
        }

        #region API Calls
        public async Task<IActionResult> GetAllCashierName()
        {
            return Json(new { data = await _cc.Cashier.ToListAsync() });
        }

        public async Task<IActionResult> GetSupervisorByBranch(String branch_id)
        {
            return Json(new { data = await (from x in _cc.Branch where x.BranchID == branch_id select x.SupervisorName).FirstAsync() });
        }

        public async Task<IActionResult> GetItemDetail(String item_id)
        {

            ItemModel item = await (from x in _cc.Item where x.ItemID == item_id select x).FirstAsync();
            return Json(new { data = item });
        }

        public async Task<IActionResult> GetItemCategory()
        {
            return Json(new { data = await _cc.Category.ToListAsync() });
        }

        public async Task<IActionResult> GetAllItemName(String? category_id)
        {
            if (category_id != null)
            {
                return Json(new { data = await (from x in _cc.Item where x.CategoryID == category_id select x).ToListAsync() });
            }
            else
            {
                return Json(new { data = await _cc.Item.ToListAsync() });
            }
        }

        public async Task<IActionResult> GetItemPrice(String item_id)
        {
            return Json(new { data = await (from x in _cc.Item where x.ItemID == item_id select x ).FirstAsync() });
        }

        public async Task<IActionResult> GetAllPaymentName()
        {
            return Json(new { data = await _cc.Payment.ToListAsync() });
        }

        public async Task<IActionResult> GetLastTransaction()
        {
            return Json(new { data = await _cc.Transaction.ToListAsync() });
        }

        public async Task<IActionResult> InsertTransaction(String CashierID, String PaymentID)
        {
            SqlConnection con = new SqlConnection("Data Source=SQL5109.site4now.net,1433;Initial Catalog=db_a81e3f_lotanmarket;User Id=db_a81e3f_lotanmarket_admin;Password=BillyMs49;");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "Execute InsertTransaction @CashID, @PayID";

            cmd.Parameters.Add("@CashID", SqlDbType.NVarChar, 100).Value = CashierID;
            cmd.Parameters.Add("@PayID", SqlDbType.NVarChar, 100).Value = PaymentID;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return Json(new { data = await _cc.Transaction.ToListAsync() });
        }

        public async Task<IActionResult> InsertTransactionDetail(int TransactionNum, String[] ListItemID, String[] ListItemQty)
        {
            SqlConnection con = new SqlConnection("Data Source=SQL5109.site4now.net,1433;Initial Catalog=db_a81e3f_lotanmarket;User Id=db_a81e3f_lotanmarket_admin;Password=BillyMs49;");
            SqlCommand cmd = con.CreateCommand();
            int length = ListItemID.Length;

            for (int i = 0; i < length; i++)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "Execute InsertTransactionDetail @TransNum, @ItID, @ItQty";
                cmd.Parameters.Add("@TransNum", SqlDbType.Int).Value = TransactionNum;
                cmd.Parameters.Add("@ItID", SqlDbType.NVarChar, 100).Value = ListItemID[i];
                cmd.Parameters.Add("@ItQty", SqlDbType.Int).Value = ListItemQty[i];
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return Json(new { data = await _cc.Transaction.ToListAsync() });
        }

        #endregion
    }
}
