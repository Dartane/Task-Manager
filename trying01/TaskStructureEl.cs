using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;


namespace TuskManaga
{
    public class TaskStructureEl
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string STime { get; set; }

        public string ETime { get; set; }


        public override string ToString() =>
            new StringBuilder()
            .Append(Name)
            .Append(Description)
            .Append(STime)
            .Append(ETime)
            .ToString();

    }
}