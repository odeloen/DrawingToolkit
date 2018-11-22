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
    class LineTool : ToolStripButton, ITool
    {
        ICanvas _targetCanvas;
        Line tempLine;

        public LineTool()
        {
            this.Name = "Line Tool";
            this.Image = new Bitmap("..\\..\\Assets\\line.png");
            this.CheckOnClick = true;
        }

        public ICanvas TargetCanvas { get { return this._targetCanvas; } set { this._targetCanvas = value; } }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tempLine = new Line();
                tempLine.Start = e.Location;
                tempLine.End = e.Location;
                this._targetCanvas.AddDrawingObject(tempLine);
            }            
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (tempLine != null)
            {                
                tempLine.Deselect();
                tempLine = null;
            }            
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (tempLine != null)
            {
                tempLine.End = e.Location;
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
