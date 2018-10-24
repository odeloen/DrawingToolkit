using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Interfaces
{
    interface IToolItem
    {
        IToolItem GetToolItem();
        System.Windows.Forms.Control GetControl();
    }
}
