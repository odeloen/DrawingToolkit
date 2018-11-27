using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.ToolClasses
{
    class RedoTool : ToolStripButton, ITool
    {
        ICanvas _targetCanvas;

        public RedoTool()
        {
            this.Name = "Line Tool";
            this.Image = new Bitmap("..\\..\\Assets\\redo-6.png");
            this.CheckOnClick = true;
        }

        public ICanvas TargetCanvas { get { return this._targetCanvas; } set { this._targetCanvas = value; } }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {

        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {

        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {

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
            this._targetCanvas.Redo();
        }
    }
}
