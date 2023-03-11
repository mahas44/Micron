using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ElasticSettings
    {
        public string Host { get; set; }
        public string User { get; set; }
        public int Password { get; set; }
        public string GameIndex { get; set; }
        public string RequestResponseIndex { get; set; }
    }
}
