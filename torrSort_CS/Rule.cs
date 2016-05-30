using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torrSort_CS
{
    public class Rule
    {
        public string destFolder { get; set; }
        public string searchPattern { get; set; }

        public Rule(string df, string sp)
        {
            destFolder = df;
            searchPattern = sp;
        }
    }
}
