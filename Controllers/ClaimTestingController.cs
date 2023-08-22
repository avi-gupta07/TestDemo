using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestDemo.Models;

namespace TestDemo.Controllers
{
    [CustomAuthorize(ClaimType = "Name", ClaimValue = "Test")]
    public class ClaimTestingController : Controller
    {
        // GET: ClaimTesting
        public ActionResult Index()
        {
            return View();
        }


      
        public ActionResult TestingMethod()
        {


            return View();
        }
    }
}