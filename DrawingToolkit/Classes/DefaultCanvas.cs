using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DrawingToolkit.Interfaces;

namespace DrawingToolkit.Classes
{
    class DefaultCanvas : Control, ICanvas
    {
        public static int DrawMode = 0;
        public static int MoveMode = 1;
        int mode;

        List<IDrawingObject> ObjectsToDraw= new List<IDrawingObject>();
        IDrawingObject ObjectToMove;
        ITool tool;
        Point LastPoint, CurrentPoint;

        public ITool Tool { get { return this.tool; } set { this.tool = value; } }        
        public ICanvas GetCanvas() { return this; }
        public Control GetControl() { return this; }
        public List<IDrawingObject> GetObjectsToDraw() { return this.ObjectsToDraw; }
        public void SetMode(int x) { this.mode = x; }

        public void AddDrawingObject(IDrawingObject newObject)
        {
            ObjectsToDraw.Add(newObject);
            this.Invalidate();
        }

        public void RemoveDrawingObject(int idx)
        {
            this.ObjectsToDraw.RemoveAt(idx);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            foreach (IDrawingObject objectToDraw in ObjectsToDraw)
                objectToDraw.Draw(g,Pens.Black);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.LastPoint = e.Location;
            if (tool != null && mode == DefaultCanvas.DrawMode)
            {
                tool.OnMouseDown(e.Location);
            }
            else if (tool != null && mode == DefaultCanvas.MoveMode)
            {
                foreach (IDrawingObject currentObject in ObjectsToDraw)
                {
                    IDrawingObject selected = currentObject.Collide(e.Location);

                    if (selected != null)
                    {
                        this.ObjectToMove = selected;
                        Console.WriteLine(selected + " Collided");
                        break;
                    }
                }                    
            }  
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.LastPoint = e.Location;
            if (tool != null && mode == DefaultCanvas.DrawMode)
            {
                tool.OnMouseUp(e.Location);
            }
            else if (this.ObjectToMove != null && tool != null && mode == DefaultCanvas.MoveMode)
            {
                this.ObjectToMove.Move(CurrentPoint);
                this.ObjectToMove = null;                
                this.Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.ObjectToMove != null && tool != null && mode == DefaultCanvas.MoveMode)
            {                
                int tempX = e.Location.X - LastPoint.X;
                int tempY = e.Location.Y - LastPoint.Y;

                this.CurrentPoint = new Point(tempX, tempY);                
            }
        }
    }
}
