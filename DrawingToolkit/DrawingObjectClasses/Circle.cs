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
    class Circle : IDrawingObject
    {
        Point start, end;        
        private Pen pen;

        IState currentState;
        Graphics _g;

        List<IDrawingObject> Component;
        IDrawingObject parent;

        public Point Start { get { return this.start; } set { this.start = value; } }
        public Point End { get { return this.end; } set { this.end = value; } }
        public Graphics TargetGraphics { set { this._g = value; } }
        public IDrawingObject Parent { get { return this.parent; } set { this.parent = value; } }

        public Circle()
        {
            this.currentState = PreviewState.GetInstance();
            this.pen = new Pen(Color.Black);
        }

        public void Draw()
        {
            this.currentState.Draw(this);
        }

        public void Select()
        {
            if (this.currentState.NextState() != null)
                this.currentState = this.currentState.NextState();
        }

        public void Deselect()
        {
            this.currentState = StaticState.GetInstance();
        }

        public IDrawingObject Intersect(Point loc)
        {
            int x = this.start.X, y = this.start.Y;
            if (this.start.X > this.end.X) x = this.end.X;
            if (this.start.Y > this.end.Y) y = this.end.Y;

            if (loc.X > x && loc.X < x + Math.Abs(this.start.X - this.end.X) && loc.Y > y && loc.Y < y + Math.Abs(this.start.Y - this.end.Y)) return this;
            return null;
        }

        public List<IDrawingObject> GetComponent()
        {
            return this.Component;
        }

        public void AddComponent(IDrawingObject obj)
        {
            //this.Component.Add(obj);
        }

        public void RemoveComponent(IDrawingObject obj)
        {
            //this.Component.Remove(obj);
        }

        public void Translate(Point loc)
        {
            this.start.X += loc.X;
            this.start.Y += loc.Y;
            this.end.X += loc.X;
            this.end.Y += loc.Y;
        }

        public void RenderOnPreview()
        {
            pen.Color = Color.Red;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Dash;
            int x = this.start.X, y = this.start.Y;
            if (this.start.X > this.end.X) x = this.end.X;
            if (this.start.Y > this.end.Y) y = this.end.Y;
            if (this._g != null)
            {
                this._g.SmoothingMode = SmoothingMode.AntiAlias;
                this._g.DrawEllipse(pen, x, y, Math.Abs(this.start.X - this.end.X), Math.Abs(this.start.Y - this.end.Y));
            }
        }

        public void RenderOnStatic()
        {
            pen.Color = Color.Black;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;
            int x = this.start.X, y = this.start.Y;
            if (this.start.X > this.end.X) x = this.end.X;
            if (this.start.Y > this.end.Y) y = this.end.Y;
            if (this._g != null)
            {
                this._g.SmoothingMode = SmoothingMode.AntiAlias;
                this._g.DrawEllipse(pen, x, y, Math.Abs(this.start.X - this.end.X), Math.Abs(this.start.Y - this.end.Y));
            }
        }

        public void RenderOnMoveState()
        {
            pen.Color = Color.Blue;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;
            int x = this.start.X, y = this.start.Y;
            if (this.start.X > this.end.X) x = this.end.X;
            if (this.start.Y > this.end.Y) y = this.end.Y;
            if (this._g != null)
            {
                this._g.SmoothingMode = SmoothingMode.AntiAlias;
                this._g.DrawEllipse(pen, x, y, Math.Abs(this.start.X - this.end.X), Math.Abs(this.start.Y - this.end.Y));
            }
        }

        public void RenderOnRotateState()
        {

        }
    }
}
