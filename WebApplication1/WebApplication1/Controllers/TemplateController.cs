using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public PartialViewResult Render(string feature, string name)
        {
            return PartialView(string.Format("~/js/app/{0}/templates/{1}", feature, name));
        }
    }
}

/*
routes.MapRoute(
    name: "Templates",
    url: "{feature}/Template/{name}",
    defaults: new {controller="Template", action="Render"}
    );

    */