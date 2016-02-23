using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.ActionResults
{
    public class XMLResult : ActionResult
    {
        private object _data;
        public XMLResult(object data)
        {
            _data = data;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            XmlSerializer serializer = new XmlSerializer(_data.GetType());
            context.HttpContext.Response.ContentType = "text/xml";
            serializer.Serialize(context.HttpContext.Response.Output, _data);


        }
    }

    public class CSVResult : FileResult
    {
        private IEnumerable _data;
        public CSVResult(IEnumerable data, string filename) : base("text/csv")
        {
            _data = data;
            FileDownloadName = filename;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);

            foreach (var item in _data)
            {
                var properties = item.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    stringWriter.Write(GetValue(item, prop.Name));
                    stringWriter.Write(", ");
                }
                stringWriter.WriteLine();
            }
            response.Write(builder);
        }

        public static string GetValue(object item, string propName)
        {
            return item.GetType().GetProperty(propName).GetValue(item, null).ToString() ?? "";
        }
    }
}