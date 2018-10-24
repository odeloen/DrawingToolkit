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
    class SquareTool : Button, ITool
    {
        Point A;
        ICanvas canvas;

        public ICanvas Canvas { get { return this.canvas; } set { this.canvas = value; } }
        public ITool GetTool() { return this; }
        public Control GetControl() { return this; }

        public SquareTool()
        {
            this.Size = new Size(46, 23);
            this.Location = new Point(2, 50);
            this.Text = "Square";
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            this.canvas.Tool = this;
            this.canvas.SetMode(DefaultCanvas.DrawMode);
        }

        public void OnMouseDown(Point point)
        {
            this.A = point;
        }

        public void OnMouseUp(Point point)
        {
            Square square = new Square();
            square.A = this.A; square.B = point;

            if (square.A.X > square.B.X) square.SwapX();
            if (square.A.Y > square.B.Y) square.SwapY();

            canvas.AddDrawingObject(square);
        }
    }
}
