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
    class MoveTool : Button, ITool
    {        
        ICanvas canvas;

        public ICanvas Canvas { get { return this.canvas; } set { this.canvas = value; } }
        public ITool GetTool() { return this; }
        public Control GetControl() { return this; }

        public MoveTool()
        {
            this.Size = new Size(46, 23);
            this.Location = new Point(2, 100);
            this.Text = "Move";
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            this.canvas.Tool = this;
            this.canvas.SetMode(DefaultCanvas.MoveMode);
        }

        public void OnMouseDown(Point point)
        {
            
        }        

        public void OnMouseUp(Point point)
        {
            
        }
    }
}
