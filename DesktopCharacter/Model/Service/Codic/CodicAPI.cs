using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Util;
using Newtonsoft.Json;

namespace DesktopCharacter.Model.Service.Codic
{
    class CodicAPI
    {
        private static string EngineURL = "https://api.codic.jp/v1/engine/translate.json";

        public async Task<CodicFormat> translateAscyn( string parameter, string token )
        {
            HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string url = EngineURL + parameter;

            Stream stream = null;
            try
            {
                stream = await client.GetStreamAsync(new Uri(url));
                string json = await PostJsonStringAsync(stream);
                var result = JsonConvert.DeserializeObject<CodicFormat>(json, new JsonSerializerSettings()
                {
                    Culture = new System.Globalization.CultureInfo("ja-JP"),
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local,
                    Formatting = Formatting.Indented,
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                });
                System.Console.WriteLine(result);
                return JsonConvert.DeserializeObject<CodicFormat>(result.ToString());
            }
            catch (HttpRequestException e)
            {
                Exception ex = e;
                while (ex != null)
                {
                    System.Console.WriteLine("Error Message : {0}", ex.Message);
                }
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine("\n Time Out!");
                Console.WriteLine("Error Message : {0} ", e.Message);
            }
            return null;
        }

        private async Task<string> PostJsonStringAsync( Stream stream )
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                return await sr.ReadToEndAsync();
            }
        }
    }
}
