using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using DrawingToolkit.Interfaces;

namespace DrawingToolkit.Classes
{
    class Circle : IDrawingObject
    {
        Point a, b;

        public Point A { get { return this.a; } set { this.a = value; } }
        public Point B { get { return this.b; } set { this.b = value; } }

        public void SwapX() { int temp = a.X; a.X = b.X; b.X = temp; }
        public void SwapY() { int temp = a.Y; a.Y = b.Y; b.Y = temp; }

        public void Draw(Graphics graphics, Pen pen)
        {
            graphics.DrawEllipse(pen, a.X, a.Y, b.X - a.X, b.Y - a.Y);
        }

        public IDrawingObject Collide(Point target)
        {
            IDrawingObject drawingObject = null;

            if (target.X >= a.X && target.X <= b.X && target.Y >= a.Y && target.Y <= b.Y)
                drawingObject = this;

            return drawingObject;
        }

        public void Move(Point target)
        {
            a.X += target.X;
            b.X += target.X;

            a.Y += target.Y;
            b.Y += target.Y;
        }
    }
}
