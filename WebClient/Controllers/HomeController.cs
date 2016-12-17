using Interfaces;
using Microsoft.Practices.Unity;
using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IUnitOfWork UnitOfWork { get; set; }

        public ActionResult Index()
        {
            UnitOfWork.Kategorie.Add(new Kategoria() { Nazwa = "Owoce" });
            UnitOfWork.Commit();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}