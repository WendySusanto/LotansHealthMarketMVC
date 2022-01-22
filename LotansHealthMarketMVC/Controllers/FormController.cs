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
    }
}
