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
    class Square : IDrawingObject
    {
        Point start, end;        
        private Pen pen;

        List<IDrawingObject> Component;
        IDrawingObject parent;
        List<ControlPoint> ControlPoints;

        IState currentState;
        Graphics _g;

        public Point Start { get { return this.start; } set { this.start = value; } }
        public Point End { get { return this.end; } set { this.end = value; } }
        public Graphics TargetGraphics { set { this._g = value; } }        
        public IDrawingObject Parent { get { return this.parent; } set { this.parent = value; } }

        public Square()
        {
            this.currentState = PreviewState.GetInstance();
            this.pen = new Pen(Color.Black);
            this.Component = new List<IDrawingObject>();
            this.parent = null;
        }

        public void Draw()
        {
            this.currentState.Draw(this);                            
        }

        public void Select()
        {
            //Console.WriteLine("There are " + this.Component.Count());
            if (this.parent != null) this.parent.Select();
            if (this.currentState.NextState() != null)
            {
                this.currentState = this.currentState.NextState();
                foreach (IDrawingObject obj in this.Component)
                    obj.Select();
            }                
        }

        public void Deselect()
        {
            this.currentState = StaticState.GetInstance();
            foreach (IDrawingObject obj in this.Component)
                obj.Deselect();
            ControlPoints = null;
        }

        public IDrawingObject Intersect(Point loc)
        {            
            if (this.ControlPoints != null)
            {
                //Console.WriteLine("ASD");
                foreach (ControlPoint cp in this.ControlPoints)
                {
                    IDrawingObject selected = cp.Intersect(loc);
                    if (selected != null)
                    {
                        Console.WriteLine("ControlPoint Intersected");
                        return selected;
                    }
                }
            }

            int x = this.start.X, y = this.start.Y;
            if (this.start.X > this.end.X) x = this.end.X;
            if (this.start.Y > this.end.Y) y = this.end.Y;

            if (loc.X > x && loc.X < x + Math.Abs(this.start.X - this.end.X) && loc.Y > y && loc.Y < y + Math.Abs(this.start.Y - this.end.Y))
            {
                //this.ControlPoints = new List<ControlPoint>();
                //this.ControlPoints.Add(new ControlPoint(start.X, start.Y));
                //this.ControlPoints.Add(new ControlPoint(end.X, end.Y));
                return this;
            }
            return null;
        }

        public List<IDrawingObject> GetComponent()
        {
            return this.Component;
        }

        public void AddComponent(IDrawingObject obj)
        {
            obj.Parent = this;
            this.Component.Add(obj);
        }

        public void RemoveComponent(IDrawingObject obj)
        {
            obj.Parent = null;
            this.Component.Remove(obj);
        }

        public void Translate(Point loc)
        {
            this.start.X += loc.X;
            this.start.Y += loc.Y;
            this.end.X += loc.X;
            this.end.Y += loc.Y;            
            //foreach (ControlPoint cp in this.ControlPoints) cp.Translate(loc);
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
                this._g.DrawRectangle(pen, x, y, Math.Abs(this.start.X-this.end.X), Math.Abs(this.start.Y-this.end.Y));
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
                this._g.DrawRectangle(pen, x, y, Math.Abs(this.start.X - this.end.X), Math.Abs(this.start.Y - this.end.Y));
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
                this._g.DrawRectangle(pen, x, y, Math.Abs(this.start.X - this.end.X), Math.Abs(this.start.Y - this.end.Y));
            }
            /*foreach (ControlPoint cp in this.ControlPoints)
            {
                cp.TargetGraphics = this._g;
                cp.Draw();
            } */               
        }

        public void RenderOnRotateState()
        {

        }
    }
}
