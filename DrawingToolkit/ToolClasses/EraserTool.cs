using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.ToolClasses
{
    class EraserTool : ToolStripButton, ITool
    {
        ICanvas _targetCanvas;

        public ICanvas TargetCanvas { get { return this._targetCanvas; } set { this._targetCanvas = value; } }

        public EraserTool()
        {
            this.Name = "Eraser Tool";
            this.Image = new Bitmap("..\\..\\Assets\\Square.png");
            this.CheckOnClick = true;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            IDrawingObject selected = this._targetCanvas.SelectObjectAt(e.Location);
            if (selected != null) this._targetCanvas.RemoveDrawingObject(selected);
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this._targetCanvas.ActiveTool = this;
            this._targetCanvas.DeselectAllObject();
            Console.WriteLine("Tool has been changed to " + this.Name);
        }
    }
}
