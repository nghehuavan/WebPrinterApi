using System;
using Nancy.Hosting.Self;

namespace WebPrinter.Web
{
    public static class WebHost
    {
        private const string Url = "http://127.0.0.1:5555";
        public static NancyHost Host = new NancyHost(new Uri(Url));
    }
}
