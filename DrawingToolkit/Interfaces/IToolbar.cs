using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Interfaces
{
    interface IToolbar
    {
        IToolbar GetToolbar();
        System.Windows.Forms.Control GetControl();

        void AddToolItem(System.Windows.Forms.Control control);
    }
}
