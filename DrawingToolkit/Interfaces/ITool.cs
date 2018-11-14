using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkitv01.Interfaces
{
    interface ITool
    {
        ICanvas TargetCanvas { get; set; }

        void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e);
        void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e);
        void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e);
        void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e);
        void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e);
    }
}
