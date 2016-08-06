using MyPokemonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPokemonShop.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                model.Save();
                ListModel listModel = new ListModel();
                listModel.FillModel();
                return View("List", listModel);
    
            }
            return View();
       }
	}
}