using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace smaaahh_api.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public AdminFilter()
        {

        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");
        }
        
    }
}