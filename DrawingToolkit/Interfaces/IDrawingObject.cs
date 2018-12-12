using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkitv01.Interfaces
{
    interface IDrawingObject
    {
        event EventHandler LocationChanged;

        System.Drawing.Graphics TargetGraphics { set; }
        System.Drawing.Point Start { get; set; }
        System.Drawing.Point End { get; set; }

        void Draw();
        void Select();
        void Deselect();
        void Translate(System.Drawing.Point loc);
        IDrawingObject Intersect(System.Drawing.Point loc);
        List<IDrawingObject> GetComponent();
        void AddComponent(IDrawingObject obj);
        void RemoveComponent(IDrawingObject obj);

        void RenderOnPreview();
        void RenderOnStatic();
        void RenderOnMoveState();
        void RenderOnRotateState();

        int GetArea();
    }
}
