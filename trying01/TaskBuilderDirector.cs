using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuskManaga
{
    public class TaskBuilderDirector
    {
        private readonly ITaskBuilder _taskBuilder;
        public TaskBuilderDirector(ITaskBuilder taskBuilder)
        {
            _taskBuilder = taskBuilder;
        }

        public void Build()
        {
            _taskBuilder.BuildName();
            _taskBuilder.BuildDescription();
            _taskBuilder.BuildSTime();
            _taskBuilder.BuildETime();
        }
    }
}
