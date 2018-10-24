using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit.Interfaces
{
    interface ICanvas
    {
        ITool Tool { get; set; }

        ICanvas GetCanvas();
        Control GetControl();
        List<IDrawingObject> GetObjectsToDraw();

        void SetMode(int x);

        void AddDrawingObject(IDrawingObject newObject);
        void RemoveDrawingObject(int idx);
    }
}
