using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkitv01.Interfaces
{
    interface ICanvas
    {
        ITool ActiveTool { get; set; }

        void AddDrawingObject(IDrawingObject obj);
        void RemoveDrawingObject(IDrawingObject obj);

        IDrawingObject SelectObjectAt(System.Drawing.Point loc);
        void DeselectAllObject(List<IDrawingObject> obj);
    }
}
