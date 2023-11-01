using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuskManaga;

namespace trying01
{
    public class TaskState
    {
        public class PlanMod : ITaskState
        {

            public string Active()
            {
                return "Task is activated!";
            }

            public string Disable()
            {
                return "Task is no active!";
            }

            public string Enable()
            {
                return "Task is no activated!";
            }

            public string End()
            {
                return "Task is no activated!";
            }

            public string Plan()
            {
                return "Task is already planned!";
            }
        }
        public class ActiveMod : ITaskState
        {

            public string Active()
            {
                return "Task is already activated!";
            }

            public string Disable()
            {
                return "Task is disable.";
            }

            public string Enable()
            {
                return "Task was no disabled!";
            }

            public string End()
            {
                return "Task is ended.";
            }

            public string Plan()
            {
                return "Task is already activated!";
            }
        }
        public class DisableMod : ITaskState
        {

            public string Active()
            {
                return "Task is already activated!";
            }

            public string Disable()
            {
                return "Task is already disabled!";
            }

            public string Enable()
            {
                return "Task is enable.";
            }

            public string End()
            {
                return "Task is ended.";
            }

            public string Plan()
            {
                return "Task is already activated!";
            }
        }
        public class EnableMod : ITaskState
        {

            public string Active()
            {
                return "Task is already activated!";
            }

            public string Disable()
            {
                return "Task is disable.";
            }

            public string Enable()
            {
                return "Task is already enabled!";
            }

            public string End()
            {
                return "Task is ended.";
            }

            public string Plan()
            {
                return "Task is already activated!";
            }
        }
        public class EndMod : ITaskState
        {

            public string Active()
            {
                return "Task is already ended!";
            }

            public string Disable()
            {
                return "Task is already ended!";
            }

            public string Enable()
            {
                return "Task is already ended!";
            }

            public string End()
            {
                return "Task is already ended!";
            }

            public string Plan()
            {
                return "Task is already ended!";
            }
        }
        public class Ident
        {

        }
        public class TaskManage
        {
            private ITaskState _state;
            public TaskManage(ITaskState state)
            {
                _state = state;
            }
            public void SetState(ITaskState state)
            {
                _state = state;
            }
            public string Active()
            {
                return _state.Active();
            }

            public string Disable()
            {
                return _state.Disable();
            }

            public string Enable()
            {
                return _state.Enable();
            }

            public string End()
            {
                return _state.End();
            }

            public string Plan()
            {
                return _state.Plan();
            }
        }
    }
}
