using Codeplex.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabumiGraphics.Utility
{
    public class JsonObject
    {
        public dynamic LoadObject { get; set; }

        public void LoadJson( string filePath )
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();
                LoadObject = DynamicJson.Parse(json);
            }
        }
    }
}
