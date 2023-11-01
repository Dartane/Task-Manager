using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static trying01.TaskState;

namespace TuskManaga
{
    public class TaskList
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartTime {  get; set; }
        public string EndTime { get; set; }
        public TaskManage State { get; set; }
        public ITaskState CurrentState { get; set; }
        public int Ident {  get; set; }
    }
}
