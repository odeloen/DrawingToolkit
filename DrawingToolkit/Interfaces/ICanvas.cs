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
        void AddDrawingObjectAt(int index, IDrawingObject obj);
        void RemoveDrawingObject(IDrawingObject obj);

        IDrawingObject SelectObjectAt(System.Drawing.Point loc);
        void DeselectAllObject();

        void AddCommand(ICommand cmd);
        void RemoveCommand(ICommand cmd);

        void Undo();
        void Redo();
    }
}
