using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KikanPrinter.Helper
{
    public static class WebHelper
    {

        static WebHelper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12
                                                   | SecurityProtocolType.Ssl3;
        }

        public static void DownloadFile(string uri, string saveTo)
        {
            DownloadFileAsync(uri, saveTo).Wait();
        }

        public static async Task DownloadFileAsync(string uri, string saveTo)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.3");

            if (!Uri.TryCreate(uri, UriKind.Absolute, out _))
                throw new InvalidOperationException("URI is invalid.");

            using (var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
            using (var streamToReadFrom = await response.Content.ReadAsStreamAsync())
            {
                using (Stream streamToWriteTo = File.Open(saveTo, FileMode.Create))
                {
                    await streamToReadFrom.CopyToAsync(streamToWriteTo);
                }
            }
        }
    }
}
