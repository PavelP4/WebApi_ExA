using MvcApp1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public Auto GetAuto()
        {
            return new Auto() { Id = 1, Mark = "Opel", Model = "Insignia", Country = 9};
        }
    }
}
