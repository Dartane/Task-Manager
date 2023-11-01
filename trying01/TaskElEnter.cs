using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuskManaga
{
    public class TaskElEnter
    {
        public string NameCase { get; set; }
        public string DescriptionCase { get; set; }
        public string STimeCase { get; set; }
        public string ETimeCase { get; set; }
        public string NameCont(string namex)
        {
            NameCase = namex;
            return namex.ToString();
        }
        public string DescriptionCont(string desex)
        {
            DescriptionCase = desex;
            return desex.ToString();
        }
        public string STimeCont(string timex)
        {
            STimeCase = timex;
            return timex.ToString();
        }
        public string ETimeCont(string timex)
        {
            ETimeCase = timex;
            return timex.ToString();
        }
    }
}
