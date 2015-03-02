using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApp1.Controllers
{
    public class AutoController : ApiController
    {
        public string GetOK()
        {
            return "Auto OK!";
        }
    }
}
