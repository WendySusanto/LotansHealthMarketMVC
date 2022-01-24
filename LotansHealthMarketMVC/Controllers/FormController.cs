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

namespace LotansHealthMarketMVC.Controllers
{
    public class FormController : Controller
    {

        private readonly ConnectionString _cc;

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
            return Json(new { data = await (from x in _cc.Item where x.ItemID == item_id select x.ItemPrice ).FirstAsync() });
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

        public IActionResult Excel(string transactionID, string itemCategory, string itemName, string itemPrice, string quantity, string subTotal, string branchLocation, string date, string cashierName)
        {

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Transaction");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Transaction ID";
                worksheet.Cell(currentRow, 2).Value = "No";
                worksheet.Cell(currentRow, 3).Value = "Item Category";
                worksheet.Cell(currentRow, 4).Value = "Item Name";
                worksheet.Cell(currentRow, 5).Value = "Item Price";
                worksheet.Cell(currentRow, 6).Value = "Quantity";
                worksheet.Cell(currentRow, 7).Value = "Sub Total";
                worksheet.Cell(currentRow, 8).Value = "Branch Location";
                worksheet.Cell(currentRow, 9).Value = "Date";
                worksheet.Cell(currentRow, 10).Value = "Cashier Name";

                //logic masukkin data
                currentRow = 2;
                worksheet.Cell(currentRow, 1).Value = transactionID;
                worksheet.Cell(currentRow, 2).Value = "1";
                worksheet.Cell(currentRow, 3).Value = itemCategory;
                worksheet.Cell(currentRow, 4).Value = itemName;
                worksheet.Cell(currentRow, 5).Value = itemPrice;
                worksheet.Cell(currentRow, 6).Value = quantity;
                worksheet.Cell(currentRow, 7).Value = subTotal;
                worksheet.Cell(currentRow, 8).Value = branchLocation;
                worksheet.Cell(currentRow, 9).Value = date;
                worksheet.Cell(currentRow, 10).Value = cashierName;


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TransactionForm.xlsx");
                }
            }


        }



        #endregion
    }
}
