using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using DesktopCharacter.Model.Service.Codic.Format;
using System.Reactive.Linq;

namespace DesktopCharacter.Model.Service.Codic
{
    class CodicAPI
    {
        private static string EngineURL = "https://api.codic.jp/v1.1/engine/translate.json";
        private static string ProjectURL = "https://api.codic.jp/v1/user_projects.json";

        public IObservable<List<CodicProject>> GetUserProjectsAync(string token)
        {
            HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string url = ProjectURL;

            return  GetHttpRequetAsync<CodicProject>(client, new Uri(url));
        }

        public IObservable<List<CodictTanslateResult>> GetTranslateAscyn( string parameter, string token )
        {
            HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string url = EngineURL + parameter;

            return GetHttpRequetAsync<CodictTanslateResult>(client, new Uri(url));
        }

        private IObservable<List<TYPE>> GetHttpRequetAsync<TYPE>(HttpClient client, Uri url)
        {
            return Observable.Create<List<TYPE>>(async (observer) =>
            {
                try
                {
                    using (var stream = await client.GetStreamAsync(url))
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string json = await sr.ReadToEndAsync();
                        var result = JsonConvert.DeserializeObject<List<TYPE>>(json, new JsonSerializerSettings()
                        {
                            Culture = new System.Globalization.CultureInfo("ja-JP"),
                            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                            DateTimeZoneHandling = DateTimeZoneHandling.Local,
                            Formatting = Formatting.Indented,
                            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                        });
                        observer.OnNext(result);
                    }
                }
                catch(Exception e)
                {
                    observer.OnError(e);
                }
            });
        }
    }
}
