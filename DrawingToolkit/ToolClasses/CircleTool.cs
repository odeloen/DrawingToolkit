using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using DrawingToolkitv01.Interfaces;
using DrawingToolkitv01.DrawingObjectClasses;
using DrawingToolkitv01.CommandClasses;

namespace DrawingToolkitv01.ToolClasses
{
    class CircleTool : ToolStripButton, ITool
    {
        ICanvas _targetCanvas;
        Circle temp;

        public CircleTool()
        {
            this.Name = "Circle Tool";
            this.Image = new Bitmap("..\\..\\Assets\\Circle.png");
            this.CheckOnClick = true;
        }

        public ICanvas TargetCanvas { get { return this._targetCanvas; } set { this._targetCanvas = value; } }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                temp = new Circle();
                temp.Start = e.Location;
                temp.End = e.Location;
                this._targetCanvas.AddDrawingObject(temp);
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (temp != null)
            {
                CreateDrawingObjectCommand cmd = new CreateDrawingObjectCommand(this.temp, this._targetCanvas);
                this._targetCanvas.AddCommand(cmd);
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

        public void OnKeyDown(object sender, KeyEventArgs e)
        {

        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {

        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);            
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
