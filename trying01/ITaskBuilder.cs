using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuskManaga
{
    public interface ITaskBuilder
    {
        void BuildName();

        void BuildDescription();

        void BuildSTime();

        void BuildETime();

        TaskStructureEl GetTask();
    }
}
