using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using DrawingToolkitv01.Interfaces;
using DrawingToolkitv01.DrawingObjectClasses;

namespace DrawingToolkitv01.ToolClasses
{
    class SquareTool : ToolStripButton, ITool
    {
        ICanvas _targetCanvas;
        Square temp;

        public SquareTool()
        {
            this.Name = "Square Tool";
            this.Image = new Bitmap("..\\..\\Assets\\Square.png");
            this.CheckOnClick = true;
        }

        public ICanvas TargetCanvas { get { return this._targetCanvas; } set { this._targetCanvas = value; } }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                temp = new Square();
                temp.Start = e.Location;
                temp.End = e.Location;
                this._targetCanvas.AddDrawingObject(temp);
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (temp != null)
            {
                //this._targetCanvas.RemoveDrawingObject(temp);
                temp.Deselect();
                temp = null;
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (temp != null)
            {
                temp.End = e.Location;
            }
        }

        public void OnKeyDown(object sender, Keys keyData)
        {

        }

        public void OnKeyUp(object sender, Keys keyData)
        {

        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            this._targetCanvas.ActiveTool = this;
            this._targetCanvas.DeselectAllObject(null);
            Console.WriteLine("Tool has been changed to " + this.Name);
        }
    }
}
