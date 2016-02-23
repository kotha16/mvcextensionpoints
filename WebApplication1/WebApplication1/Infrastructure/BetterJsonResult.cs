using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Properties;

namespace WebApplication1.Infrastructure
{
    public class BetterJsonResult : JsonResult
    {
        public IList<string> ErrorMessages { get; private set; }
        public BetterJsonResult()
        {
            ErrorMessages = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }

        //public override void ExecuteResult(ControllerContext context)
        //{
        //    DoUninterestingBaseClassStuff(context);
        //    SerializeData(context.HttpContext.Response);
        //}

        public override void ExecuteResult(ControllerContext context)
        {
            DoUninterestingBaseClassStuff(context);
            SerializeData(context.HttpContext.Response);
        }
        private void SerializeData(HttpResponseBase response)
        {
            if (ErrorMessages.Any())
            {
                Data = new
                {
                    ErrorMessage = string.Join("\n", ErrorMessages),
                    ErrorMessages = ErrorMessages.ToArray()
                };
                response.StatusCode = 400;

            }
            if (Data == null) return;
            response.Write(Data.ToJson());
        }

        private void DoUninterestingBaseClassStuff(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(Resources.JsonRequest_GetNotAllowed);
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
        }
    }
}