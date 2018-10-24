using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Interfaces
{
    interface IDrawingObject
    {
        Point A { get; set; }
        Point B { get; set; }

        void SwapX();
        void SwapY();

        void Draw(Graphics graphics, Pen pen);

        IDrawingObject Collide(Point X);
        void Move(Point target);
    }
}
