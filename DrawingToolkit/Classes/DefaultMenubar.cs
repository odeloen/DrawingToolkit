using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DrawingToolkit.Interfaces;

namespace DrawingToolkit.Classes
{
    class DefaultMenubar : Control, IMenubar
    {
        List<IMenuItem> menuItems;
        public DefaultMenubar()
        {
            // Controls
            this.BackColor = System.Drawing.Color.AliceBlue;

            // This Class
            this.menuItems = new List<IMenuItem>();
            this.menuItems.Add(new MenuItemFile());
            foreach(IMenuItem menuItem in menuItems)
            {
                this.Controls.Add(menuItem.GetControl());
            }
        }

        public IMenubar GetMenubar() { return this; }                            
        public Control GetControl() { return this; }       

        public void AddMenuItem(IMenuItem menuItem)
        {
            this.menuItems.Add(menuItem);
        }
    }
}
