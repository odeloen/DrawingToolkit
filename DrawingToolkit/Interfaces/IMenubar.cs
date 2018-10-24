using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Interfaces
{
    interface IMenubar
    {
        IMenubar GetMenubar();
        System.Windows.Forms.Control GetControl();

        void AddMenuItem(IMenuItem menuItem);
    }
}
