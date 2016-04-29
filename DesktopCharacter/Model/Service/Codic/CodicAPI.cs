using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Util;
using Newtonsoft.Json;
using DesktopCharacter.Model.Service.Codic.Format;

namespace DesktopCharacter.Model.Service.Codic
{
    class CodicAPI
    {
        private static string EngineURL = "https://api.codic.jp/v1.1/engine/translate.json";
        private static string ProjectURL = "https://api.codic.jp/v1/user_projects.json";

        public async Task<List<CodicProject>> GetUserProjectsAync(string token)
        {
            HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string url = ProjectURL;

            var result = await GetHttpRequetAsync<CodicProject>(client, new Uri(url));
            return result;
        }

        public async Task<CodictTanslateResult> GetTranslateAscyn( string parameter, string token )
        {
            HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string url = EngineURL + parameter;

            var result = await GetHttpRequetAsync<CodictTanslateResult>(client, new Uri(url));
            return result?.FirstOrDefault();
        }

        private async Task<List<TYPE>> GetHttpRequetAsync<TYPE>(HttpClient client, Uri url)
        {
            Stream stream = null;
            try
            {
                stream = await client.GetStreamAsync(url);
                string json = await PostJsonStringAsync(stream);
                return JsonConvert.DeserializeObject<List<TYPE>>(json, new JsonSerializerSettings()
                {
                    Culture = new System.Globalization.CultureInfo("ja-JP"),
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local,
                    Formatting = Formatting.Indented,
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                });
            }
            catch (HttpRequestException e)
            {
                Exception ex = e;
                System.Console.WriteLine("Error Message : {0}", ex.Message);
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
