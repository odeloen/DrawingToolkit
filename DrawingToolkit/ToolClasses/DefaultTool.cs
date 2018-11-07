using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using DrawingToolkitv01.Interfaces;
using DrawingToolkitv01.DrawingObjectClasses;

namespace DrawingToolkitv01.ToolClasses
{
    class DefaultTool : ToolStripButton, ITool
    {
        ICanvas _targetCanvas;
        List<IDrawingObject> _targetObjects;
        Point lastPoint;
        Point lastDiff;
        bool HotkeyIsPressed;
        bool LeftMouseIsPressed;

        public DefaultTool()
        {
            this.Name = "Default Tool";
            this.Image = new Bitmap("..\\..\\Assets\\Cursor.png");
            this.CheckOnClick = true;
            this.HotkeyIsPressed = false;
            this.LeftMouseIsPressed = false;
            this._targetObjects = new List<IDrawingObject>();
        }

        public ICanvas TargetCanvas { get { return this._targetCanvas; } set { this._targetCanvas = value; } }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            this.LeftMouseIsPressed = true;
            IDrawingObject temp = this._targetCanvas.SelectObjectAt(e.Location);                        
            if (temp != null)
            {

                if (!this._targetObjects.Contains(temp) && this.HotkeyIsPressed) this._targetObjects.Add(temp);
                else if (!this._targetObjects.Contains(temp))
                {
                    this._targetObjects.Clear();
                    this._targetObjects.Add(temp);
                    this._targetObjects.AddRange(temp.GetComponent());
                    this._targetCanvas.DeselectAllObject(this._targetObjects);
                }
                lastPoint = e.Location;
                //Console.WriteLine(this._targetObjects.Count);
            }        
            else
            {
                this._targetObjects.Clear();
                this._targetCanvas.DeselectAllObject(null);
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            this.LeftMouseIsPressed = false;            
            this.lastDiff = new Point(0,0);
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            //Console.WriteLine(this._targetObjects.Count);
            if (this.LeftMouseIsPressed && this._targetObjects.Count != 0)
            {
                int i = 0;
                int diffX = e.Location.X - lastPoint.X;
                int diffY = e.Location.Y - lastPoint.Y;
                foreach (IDrawingObject obj in this._targetObjects)
                {                    
                    obj.Translate(lastDiff);                    
                    obj.Translate(new Point(diffX, diffY));
                }
                this.lastDiff = new Point(-diffX, -diffY);
            }            
        }

        public void OnKeyDown(object sender, Keys keyData)
        {            
            if (keyData == (Keys.ShiftKey | Keys.Shift))
            {                
                this.HotkeyIsPressed = true;
            }
            else if (keyData == (Keys.Control | Keys.G))
            {
                Console.WriteLine("Control + G Pressed " + this._targetObjects.Count());
                
                for (int i = 1; i < this._targetObjects.Count(); i++)
                {
                    this._targetObjects[0].AddComponent(this._targetObjects[i]);
                }
            }
        }

        public void OnKeyUp(object sender, Keys keyData)
        {            
            if (keyData == (Keys.ShiftKey))
            {                
                this.HotkeyIsPressed = false;                
            }
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            this._targetCanvas.ActiveTool = this;
            Console.WriteLine("Tool has been changed to " + this.Name);
        }
    }
}
