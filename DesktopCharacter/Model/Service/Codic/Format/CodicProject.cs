using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Service.Codic.Format
{
    public class Owner
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class CodicProject
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string created_on { get; set; }
        public int words_count { get; set; }
        public Owner owner { get; set; }
    }
}
