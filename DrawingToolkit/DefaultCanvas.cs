using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01
{
    class DefaultCanvas : Control, ICanvas
    {
        List<IDrawingObject> ObjectsToDraw;
        ITool _activeTool;

        public ITool ActiveTool { get { return this._activeTool; } set { this._activeTool = value; } }

        public DefaultCanvas()
        {
            this.ObjectsToDraw = new List<IDrawingObject>();
            this.DoubleBuffered = true;            
            this._activeTool = null;
        }

        public void AddDrawingObject(IDrawingObject obj)
        {            
            this.ObjectsToDraw.Add(obj);
        }

        public void RemoveDrawingObject(IDrawingObject obj)
        {
            this.ObjectsToDraw.Remove(obj);
        }

        public IDrawingObject SelectObjectAt(Point loc)
        {            
            IDrawingObject selected = null;

            foreach(IDrawingObject obj in ObjectsToDraw)
            {
                selected = obj.Intersect(loc);
                if (selected != null)
                {
                    selected.Select();
                    break;
                }
            }
            //DeselectAllObject(selected);
            return selected;
        }

        public void DeselectAllObject(List<IDrawingObject> selected)
        {
            foreach (IDrawingObject obj in this.ObjectsToDraw)
            {
                if (selected != null)
                {
                    foreach (IDrawingObject sel in selected)
                    {
                        if (obj != sel)
                            obj.Deselect();
                    }
                }
                else obj.Deselect();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            foreach (IDrawingObject obj in ObjectsToDraw)
            {
                obj.TargetGraphics = e.Graphics;
                obj.Draw();
            }                
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if (this._activeTool != null)
            {
                this._activeTool.OnKeyDown(this, e.KeyData);
            }            
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (this._activeTool != null)
            {
                this._activeTool.OnKeyUp(this, e.KeyData);
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this._activeTool != null)
            {
                this._activeTool.OnMouseDown(this, e);
                this.Invalidate();
                this.Update();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this._activeTool != null)
            {
                this._activeTool.OnMouseUp(this, e);
                this.Invalidate();
                this.Update();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this._activeTool != null)
            {
                this._activeTool.OnMouseMove(this, e);
                this.Invalidate();
                this.Update();
            }
        }
    }
}
