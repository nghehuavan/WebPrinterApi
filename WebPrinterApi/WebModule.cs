using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using RawPrint;
using WebPrinterApi.Model;

namespace WebPrinterApi
{
    public class WebModule : NancyModule
    {
        public WebModule()
        {
            EnableCors();

            Get["/printer"] = x =>
            {
                try
                {
                    var printers = new List<string>();
                    foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters) printers.Add(s);
                    return Response.AsJson(printers);
                }
                catch (Exception e)
                {
                    return HttpStatusCode.InternalServerError;
                }
            };

            Post["/printAction"] = x =>
            {
                var body = this.Bind<PrintActionModel>();
                var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
                if (string.IsNullOrEmpty(body.printer_name)
                    || string.IsNullOrEmpty(body.file_url))
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ReasonPhrase = "missing printer_name or file_url !"
                    };
                }

                try
                {
                    WebHelper.DownloadFile(body.file_url, filePath);
                }
                catch (Exception e)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ReasonPhrase = "file_url can't download !"
                    };
                }

                try
                {
                    IPrinter printer = new Printer();
                    printer.PrintRawFile(body.printer_name, filePath);
                    return HttpStatusCode.OK;
                }
                catch (Exception e)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ReasonPhrase = $"[{body.printer_name}] can't print!"
                    };
                }
            };
        }

        public void EnableCors()
        {
            // Enable cors
            After.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "*")
                    .WithHeader("Access-Control-Allow-Headers", "*");
            });
        }
    }
}
