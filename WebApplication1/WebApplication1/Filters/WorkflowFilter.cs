using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Filters
{
    /*
    usage of this filter 
        Attribute for Controller class
        [WorkflowFilter(MinRequiredStage = (int) WorkflowValues.Begin,
                CurrentStage = (int)WorkflowValues.ApplicantInfo)]

        Add to controllers as appropriate with the right enum values.

    */

    public class WorkflowFilter : FilterAttribute, IActionFilter
    {
        private int _highestCompletedStage;

        public int MinRequiredStage { get; set; }
        public int CurrentStage { get; set; }

        private RedirectToRouteResult GenerateRedirectUrl(string action, string controller)
        {
            return new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = action, controller = controller }));
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // get dbcontext or similar thing
            var sessionId = HttpContext.Current.Session["Traker"];
            if (sessionId != null)
            {
                Guid tracker;
                if (Guid.TryParse(sessionId.ToString(), out tracker))
                {
                    if (filterContext.HttpContext.Request.RequestType == "POST" && CurrentStage >= _highestCompletedStage)
                    {
                        // save current stage to database or some other place.


                    }
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var applicantId = filterContext.HttpContext.Session["Tracker"];
            if (applicantId != null)
            {
                Guid tracker;
                if (Guid.TryParse(applicantId.ToString(), out tracker))
                {
                    // some database logic goes here
                    // get highestCompletedStage from database or some other place
                    if (MinRequiredStage > _highestCompletedStage)
                    {
                        switch (_highestCompletedStage)
                        {
                            case (int)WorkflowValues.ApplicantInfo:
                                filterContext.Result = GenerateRedirectUrl("ApplicantInfo", "Applicant");
                                break;
                            case (int)WorkflowValues.AddressInfo:
                                filterContext.Result = GenerateRedirectUrl("AddressInfo", "Address");
                                break;
                                // so on....
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                if (CurrentStage != (int) WorkflowValues.ApplicantInfo)
                {
                    filterContext.Result = GenerateRedirectUrl("ApplicantInfo", "Applicant");
                }
            }
        }
    }
}