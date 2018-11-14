using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkitv01.Interfaces
{
    interface ILocationMonitor
    {
        IDrawingObject A { get; set; }
        IDrawingObject B { get; set; }
    }
}
