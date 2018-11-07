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
    class ControlPoint : IDrawingObject
    {
        Point start, end;        
        private Pen pen;

        IState currentState;
        Graphics _g;

        IDrawingObject parent;

        public Point Start { get { return this.start; } set { this.start = value; } }
        public Point End { get { return this.end; } set { this.end = value; } }
        public Graphics TargetGraphics { set { this._g = value; } }
        public IDrawingObject Parent { get { return this.parent; } set { this.parent = value; } }

        public ControlPoint()
        {
            this.currentState = MoveState.GetInstance();
            this.pen = new Pen(Color.Black);
        }

        public ControlPoint(int x, int y)
        {
            this.start.X = x - 5;
            this.start.Y = y - 5;
            this.end.X = x + 5;
            this.end.Y = y + 5;
            this.currentState = MoveState.GetInstance();
            this.pen = new Pen(Color.Black);
        }

        public void Draw()
        {
            this.currentState.Draw(this);            
        }

        public void Select()
        {
            
        }

        public void Deselect()
        {
            
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
            return null;
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
            
        }

        public void RenderOnStatic()
        {
            
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
                this._g.DrawRectangle(pen, x, y, Math.Abs(this.start.X - this.end.X), Math.Abs(this.start.Y - this.end.Y));
            }
        }

        public void RenderOnRotateState()
        {

        }
    }
}
