using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuskManaga
{
    public interface ITaskState
    {
        string Plan();

        string Active();

        string End();

        string Disable();

        string Enable();
    }
}
