using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ActionResults;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        //public BetterJsonResult BetterJson<T>(T model)
        //{
        //    return new BetterJsonResult<T>() { Data = model };
        //}
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }
    }
}