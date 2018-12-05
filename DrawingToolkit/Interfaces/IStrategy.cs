using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace DrawingToolkitv01.Interfaces
{
    interface IStrategy
    {
        void AddDrawingObject(IDrawingObject obj);
        void AddDrawingObjectAt(IDrawingObject obj, int idx);
        void RemoveDrawingObject(IDrawingObject obj);
        void Draw(Graphics g);

        void StrategyMouseUp();

        IDrawingObject SelectObjectAt(Point loc);
        void DeselectAllObject();
    }
}
