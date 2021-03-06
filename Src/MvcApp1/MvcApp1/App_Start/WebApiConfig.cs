﻿using MvcApp1.Common;
using MvcApp1.MessageHandlers;
using MvcApp1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace MvcApp1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/V{api_v}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                //constraints: null,
                //handler: new MessageHandler1(config)
            );

            //config.EnableSystemDiagnosticsTracing();
            //config.Services.Replace(typeof(ITraceWriter), new WebApiTracer());
            //config.MessageHandlers.Add(new TracingHandler());

            //config.MessageHandlers.Add(new EncodingHandler());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;

            //foreach (var formatter in config.Formatters)
            //{
            //    Trace.WriteLine(formatter.GetType().Name);
            //    Trace.WriteLine("\tCanReadType: " + formatter.CanReadType(typeof(Employee)));
            //    Trace.WriteLine("\tCanWriteType: " + formatter.CanWriteType(typeof(Employee)));
            //    Trace.WriteLine("\tBase: " + formatter.GetType().BaseType.Name);
            //    Trace.WriteLine("\tMedia Types: " + String.Join(", ", formatter.
            //    SupportedMediaTypes));
            //}

            //config.MessageHandlers.Add(new MessageHandler1());

            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));
            
        }
    }
}
