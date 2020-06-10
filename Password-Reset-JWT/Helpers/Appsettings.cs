using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Password_Reset_JWT.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string DomainControlerIP { get; set; }

        public string fqdn { get; set; }

        public string AdminID{ get; set; }

        public string Passwd { get; set; }
    }
}
