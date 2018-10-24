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
    class UndoTool : Button, ITool
    {        
        ICanvas canvas;

        public ICanvas Canvas { get { return this.canvas; } set { this.canvas = value; } }
        public ITool GetTool() { return this; }
        public Control GetControl() { return this; }

        public UndoTool()
        {
            this.Size = new Size(46, 23);
            this.Location = new Point(2, 75);
            this.Text = "Undo";
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int idx = this.canvas.GetObjectsToDraw().Count;
            if (idx != 0) this.canvas.RemoveDrawingObject(idx - 1);
        }

        public void OnMouseDown(Point point)
        {
            return;
        }

        public void OnMouseUp(Point point)
        {
            return;
        }
    }
}
