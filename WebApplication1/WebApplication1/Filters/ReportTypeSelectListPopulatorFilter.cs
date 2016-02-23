using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class ReportTypeSelectListPopulatorFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;
            if (viewResult != null && viewResult.Model is IHaveReportTypeSelectList)
            {
                ((IHaveReportTypeSelectList)viewResult.Model).AvailableReportTypes = GetAvailableReportTypes();
                //Global Filters are effectively singletons...they are not recreated for each request
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        private SelectListItem[] GetAvailableReportTypes()
        {
            return new List<SelectListItem>().ToArray();
        }
    }

    public interface IHaveReportTypeSelectList
    {
        SelectListItem[] AvailableReportTypes { get; set; }
    }
}