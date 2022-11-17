using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class HistoryItem
    {
        public string QuestionAnswer { get; set; }
        public string DateTime1 { get; set; }

        public HistoryItem()
        {
            DateTime1 = DateTime.Now.ToShortTimeString();
        }
    }
}
