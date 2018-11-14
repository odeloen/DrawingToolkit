using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;
using DrawingToolkitv01.Interfaces;
using DrawingToolkitv01.StateClasses;

namespace DrawingToolkitv01.DrawingObjectClasses
{
    class Line : IDrawingObject
    {
        public event EventHandler LocationChanged;

        protected Point start, end;
        private Pen pen;

        List<IDrawingObject> Component;

        IState currentState;
        Graphics _g;

        public Point Start { get { return this.start; } set { this.start = value; } }
        public Point End { get { return this.end; } set { this.end = value; } }
        public Graphics TargetGraphics { set { this._g = value; } }

        public Line()
        {
            this.currentState = PreviewState.GetInstance();
            this.pen = new Pen(Color.Black);
            this.Component = new List<IDrawingObject>();
        }

        public void Draw()
        {
            this.currentState.Draw(this);
            this.currentState.Draw(this);
            foreach (IDrawingObject obj in this.Component)
            {
                obj.TargetGraphics = this._g;
                obj.Draw();
            }
        }

        public void Select()
        {
            if (this.currentState.NextState() != null)
            {
                this.currentState = this.currentState.NextState();
                foreach (IDrawingObject obj in this.Component)
                {
                    obj.Select();
                }
            }
        }

        public void Deselect()
        {
            this.currentState = StaticState.GetInstance();
            foreach (IDrawingObject obj in this.Component)
            {
                obj.Deselect();
            }
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
            this.start.X += loc.X;
            this.start.Y += loc.Y;
            this.end.X += loc.X;
            this.end.Y += loc.Y;

            foreach (IDrawingObject obj in this.Component)
            {
                obj.Translate(loc);
            }
        }

        public void RenderOnPreview()
        {
            pen.Color = Color.Red;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Dash;
            if (this._g != null)
            {
                this._g.SmoothingMode = SmoothingMode.AntiAlias;
                this._g.DrawLine(pen, this.start, this.end);
            }
        }

        public void RenderOnStatic()
        {            
            pen.Color = Color.Black;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;
            if (this._g != null)
            {                
                this._g.SmoothingMode = SmoothingMode.AntiAlias;
                this._g.DrawLine(pen, this.start, this.end);
            }
        }

        public void RenderOnMoveState()
        {
            pen.Color = Color.Blue;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;
            if (this._g != null)
            {
                this._g.SmoothingMode = SmoothingMode.AntiAlias;
                this._g.DrawLine(pen, this.start, this.end);
            }
        }

        public void RenderOnRotateState()
        {

        }        
    }
}
