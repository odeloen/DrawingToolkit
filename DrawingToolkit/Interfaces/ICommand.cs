using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Interfaces
{
    interface ICommand
    {
        ICommand GetCommand();
    }
}
