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
    class LineConnectorTool : ToolStripButton, ITool
    {
        ICanvas _targetCanvas;
        LineConnector newLine;

        public LineConnectorTool()
        {
            this.Name = "Line Connector Tool";
            this.Image = new Bitmap("..\\..\\Assets\\line.png");
            this.CheckOnClick = true;
        }

        public ICanvas TargetCanvas { get { return this._targetCanvas; } set { this._targetCanvas = value; } }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IDrawingObject temp = this._targetCanvas.SelectObjectAt(e.Location);                
                if (temp != null)
                {
                    newLine = new LineConnector();
                    newLine.A = temp;
                    newLine.End = e.Location;
                    this._targetCanvas.AddDrawingObject(newLine);
                    temp.Deselect();
                }                
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (newLine != null)
            {
                IDrawingObject temp = this._targetCanvas.SelectObjectAt(e.Location);
                if (temp == null) this._targetCanvas.RemoveDrawingObject(newLine);
                else
                {
                    newLine.B = temp;
                    CreateDrawingObjectCommand cmd = new CreateDrawingObjectCommand(this.newLine, this._targetCanvas);
                    this._targetCanvas.AddCommand(cmd);
                    newLine.Deselect();
                    this._targetCanvas.RemoveDrawingObject(newLine);
                    this._targetCanvas.AddDrawingObjectAt(0, newLine);
                    newLine = null;
                }                
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (newLine != null)
            {
                newLine.End = e.Location;
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
