using System;
using Nancy.Hosting.Self;

namespace KikanPrinter.Web
{
    public static class WebHost
    {
        private const string Url = "http://127.0.0.1:8555";
        public static NancyHost Host = new NancyHost(new Uri(Url));
    }
}
