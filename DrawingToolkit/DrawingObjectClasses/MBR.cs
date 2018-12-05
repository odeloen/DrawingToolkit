using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;
using DrawingToolkitv01.StateClasses;
using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.DrawingObjectClasses
{
    class MBR : IDrawingObject
    {
        public event EventHandler LocationChanged;

        Point start, end;
        private Pen pen;

        List<IDrawingObject> Component;
        List<ControlPoint> ControlPoints;

        IState currentState;
        Graphics _g;

        public Point Start { get { return this.start; } set { this.start = value; } }
        public Point End { get { return this.end; } set { this.end = value; } }
        public Graphics TargetGraphics { set { this._g = value; } }

        public MBR()
        {
            this.currentState = StaticState.GetInstance();
            this.pen = new Pen(Color.Black);
            this.Component = new List<IDrawingObject>();
        }

        public void Draw()
        {
            this.currentState.Draw(this);
            foreach (IDrawingObject obj in this.Component)
            {
                obj.TargetGraphics = this._g;
                obj.Draw();
            }
        }

        public void Select()
        {
            
        }

        public void Deselect()
        {
            
        }

        public List<IDrawingObject> GetComponent()
        {
            return this.Component;
        }

        public void AddComponent(IDrawingObject obj)
        {
            this.Component.Add(obj);
        }

        public void RemoveComponent(IDrawingObject obj)
        {
            this.Component.Remove(obj);
        }

        public IDrawingObject Intersect(Point loc)
        {
            int x = this.start.X, y = this.start.Y;
            if (this.start.X > this.end.X) x = this.end.X;
            if (this.start.Y > this.end.Y) y = this.end.Y;

            if (loc.X > x && loc.X < x + Math.Abs(this.start.X - this.end.X) && loc.Y > y && loc.Y < y + Math.Abs(this.start.Y - this.end.Y)) return this;
            foreach (IDrawingObject obj in this.Component)
            {
                IDrawingObject temp = obj.Intersect(loc);
                if (temp != null) return this;
            }
            return null;
        }

        public void Translate(Point loc)
        {
            
        }

        public void RenderOnPreview()
        {
            
        }

        public void RenderOnStatic()
        {
            pen.Color = Color.Green;
            pen.Width = 2.0f;
            pen.DashStyle = DashStyle.Dash;
            int x = this.start.X, y = this.start.Y;
            if (this.start.X > this.end.X) x = this.end.X;
            if (this.start.Y > this.end.Y) y = this.end.Y;
            if (this._g != null)
            {
                this._g.SmoothingMode = SmoothingMode.AntiAlias;
                this._g.DrawRectangle(pen, x, y, Math.Abs(this.start.X - this.end.X), Math.Abs(this.start.Y - this.end.Y));                
            }
        }

        public void RenderOnMoveState()
        {
            
        }

        public void RenderOnRotateState()
        {

        }

        void OnLocationChanged()
        {
            if (LocationChanged != null)
            {
                LocationChanged(this, EventArgs.Empty);
            }
        }
    }
}
