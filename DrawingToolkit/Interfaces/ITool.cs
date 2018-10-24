using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit.Interfaces
{
    interface ITool
    {
        ITool GetTool();
        Control GetControl();

        ICanvas Canvas { get; set; }

        void OnMouseDown(System.Drawing.Point x);
        void OnMouseUp(System.Drawing.Point x);
    }
}
