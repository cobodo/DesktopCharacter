using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Util
{
    class ParameterBuild
    {
        public Dictionary<string, string> Parameter { get; set; } = new Dictionary<string, string>();

        public string Convert()
        {
            string result = "?";
            foreach (var item in Parameter)
            {
                result += item.Key + "=" + item.Value + "&";
            }
            return result;
        }
    }
}
