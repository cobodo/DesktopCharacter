﻿using System.Collections.Generic;

namespace DesktopCharacter.Util.Web
{
    class ParameterBuild
    {
        public Dictionary<string, string> Parameter { get; set; } = new Dictionary<string, string>();

        public string Convert()
        {
            string result = "?";
            foreach (var item in Parameter)
            {
                if(item.Value == string.Empty)
                {
                    continue;
                }
                result += item.Key + "=" + item.Value + "&";
            }
            result = result.TrimEnd('&');
            return result;
        }
    }
}
