using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using DrawingToolkit.Interfaces;

namespace DrawingToolkit.Classes
{
    class MenuItemFile : Button, IMenuItem
    {
        public MenuItemFile()
        {
            this.Text = "File";
            this.Size = new Size(40,20);
        }

        public IMenuItem GetMenuItem() { return this; }
        public Control GetControl() { return this; }
    }
}
