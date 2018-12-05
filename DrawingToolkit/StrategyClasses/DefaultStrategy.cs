using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.StrategyClasses
{
    class DefaultStrategy : IStrategy
    {
        List<IDrawingObject> ObjectsToDraw;

        public DefaultStrategy()
        {
            this.ObjectsToDraw = new List<IDrawingObject>();
        }

        public void AddDrawingObject(IDrawingObject obj)
        {
            this.ObjectsToDraw.Add(obj);
        }

        public void RemoveDrawingObject(IDrawingObject obj)
        {
            this.ObjectsToDraw.Remove(obj);
        }

        public void AddDrawingObjectAt(IDrawingObject obj, int idx)
        {
            this.ObjectsToDraw.Insert(idx, obj);
        }

        public void Draw(Graphics g)
        {
            foreach (IDrawingObject obj in ObjectsToDraw)
            {
                obj.TargetGraphics = g;
                obj.Draw();
            }
        }

        public IDrawingObject SelectObjectAt(Point loc)
        {
            IDrawingObject selected = null;

            foreach (IDrawingObject obj in ObjectsToDraw)
            {
                selected = obj.Intersect(loc);
                if (selected != null)
                {
                    selected.Select();
                    break;
                }
            }
            return selected;
        }

        public void DeselectAllObject()
        {
            foreach (IDrawingObject obj in this.ObjectsToDraw)
            {
                obj.Deselect();
            }
        }

        public void StrategyMouseUp()
        {
            
        }
    }
}
