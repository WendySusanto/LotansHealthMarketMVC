using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LotansHealthMarketMVC.Models;

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

        #endregion
    }
}
