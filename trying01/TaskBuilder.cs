using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuskManaga
{
    public class TaskBuilder : ITaskBuilder
    {
        private TaskStructureEl _taskstr;
        private readonly TaskElEnter _taskelEnter;
        public TaskBuilder(TaskElEnter taskElEnter, string name, string desc, string stime, string etime)
        {
            _taskelEnter = taskElEnter;
            _taskelEnter.NameCase = name;
            _taskelEnter.DescriptionCase = desc;
            _taskelEnter.STimeCase = stime;
            _taskelEnter.ETimeCase = etime;
            _taskstr = new TaskStructureEl();
        }

        public void BuildName()
        {
            string name = _taskelEnter.NameCase;
            _taskstr.Name = name;
            _taskstr.Name += "\n";
        }
        public void BuildDescription()
        {
            string description = _taskelEnter.DescriptionCase;
            _taskstr.Description = description;
            _taskstr.Description += "\n";
        }
        public void BuildSTime()
        {
            string stime = _taskelEnter.STimeCase;
            
            _taskstr.STime = stime;
            _taskstr.STime += "\n";
        }
        public void BuildETime()
        {
            string etime = _taskelEnter.ETimeCase;

            _taskstr.ETime = etime;
            _taskstr.ETime += "\n";
        }
        public TaskStructureEl GetTask()
        {
            TaskStructureEl task = _taskstr;
            _taskstr = new TaskStructureEl();
            return task;
        }
    }
}
