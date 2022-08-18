using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KikanPrinter.Helper;
using KikanPrinter.Model;
using Nancy;
using Nancy.ModelBinding;

namespace KikanPrinter.Web
{
    public class WebModule : NancyModule
    {
        public WebModule()
        {
            EnableCors();

            Get["/"] = x => Response.AsJson("Welcome");

            Get["/printer"] = x =>
            {
                try
                {
                    var printers = new List<string>();
                    foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters) printers.Add(s);

                    var resp = new
                    {
                        version = Application.ProductVersion,
                        device = new
                        {
                            id = WebHost.DeviceId,
                            name = Environment.MachineName,
                        },
                        printers = printers.Where(p => p.ToUpper() != "FAX" && p.ToUpper() != "MICROSOFT XPS DOCUMENT WRITER")
                    };

                    return Response.AsJson(resp);
                }
                catch (Exception e)
                {
                    return HttpStatusCode.InternalServerError;
                }
            };

            Post["/printer"] = x =>
            {
                var body = this.Bind<PrintActionModel>();
                var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
                if (string.IsNullOrEmpty(body.printer_name)
                    || string.IsNullOrEmpty(body.file_url))
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ReasonPhrase = "missing printer_name or file_url !",
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
                    PrintHelper.PrintPdf(body.printer_name, filePath);
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

            Get["/printer/run"] = x =>
            {
                var body = this.Bind<PrintActionModel>();
                var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
                if (string.IsNullOrEmpty(body.printer_name)
                    || string.IsNullOrEmpty(body.file_url))
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ReasonPhrase = "missing printer_name or file_url !",
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
                    PrintHelper.PrintPdf(body.printer_name, filePath);
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
                    .WithHeader("Access-Control-Allow-Headers", "*")
                    .WithHeader("Access-Control-Max-Age", "3600");
            });
        }
    }
}
