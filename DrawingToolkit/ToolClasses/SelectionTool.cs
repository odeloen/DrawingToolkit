﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using DrawingToolkitv01.Interfaces;
using DrawingToolkitv01.DrawingObjectClasses;
using DrawingToolkitv01.CommandClasses;

namespace DrawingToolkitv01.ToolClasses
{
    class SelectionTool : ToolStripButton, ITool
    {
        ICanvas _targetCanvas;
        List<IDrawingObject> _targetObjects;        
        bool StateShiftDown;
        bool StateLeftMouseDown;
        Point lastPoint;
        Point lastDiff;
        bool Move;

        public SelectionTool()
        {
            this.Name = "Selection Tool";
            this.Image = new Bitmap("..\\..\\Assets\\Cursor.png");
            this.CheckOnClick = true;
            this._targetObjects = new List<IDrawingObject>();            
            this.StateShiftDown = false;
            this.StateLeftMouseDown = false;
            this.Move = false;
        }

        public ICanvas TargetCanvas { get { return this._targetCanvas; } set { this._targetCanvas = value; } }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            this.StateLeftMouseDown = true;
            if (StateShiftDown)
            {
                IDrawingObject temp = this._targetCanvas.SelectObjectAt(e.Location);
                if (temp != null)
                {
                    if (this._targetObjects.Contains(temp))
                    {
                        this._targetObjects.Remove(temp);
                        temp.Deselect();
                    } 
                    else
                    {
                        this._targetObjects.Add(temp);                        
                        lastPoint = e.Location;
                    }
                }
                else
                {
                    foreach (IDrawingObject obj in this._targetObjects) obj.Deselect();
                    this._targetObjects.Clear();
                }
            }
            else
            {
                foreach (IDrawingObject obj in this._targetObjects) obj.Deselect();
                this._targetObjects.Clear();
                IDrawingObject temp = this._targetCanvas.SelectObjectAt(e.Location);
                if (temp != null)
                {
                    this._targetObjects.Add(temp);                                        
                    lastPoint = e.Location;
                }
            }            
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (Move)
            {
                foreach(IDrawingObject obj in this._targetObjects)
                {
                    TranslateCommand cmd = new TranslateCommand(obj, lastDiff);
                    this._targetCanvas.AddCommand(cmd);
                }
                Move = false;
            }            
            StateLeftMouseDown = false;
            this.lastDiff = new Point(0,0);
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (StateLeftMouseDown && this._targetObjects.Any())
            {
                int diffX = e.Location.X - lastPoint.X;
                int diffY = e.Location.Y - lastPoint.Y;                
                foreach (IDrawingObject obj in this._targetObjects)
                {
                    obj.Translate(lastDiff);
                    obj.Translate(new Point(diffX, diffY));
                }
                this.lastDiff = new Point(-diffX, -diffY);
                this.Move = true;
            }            
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {            
            switch (e.KeyData)
            {
                case Keys.Shift | Keys.ShiftKey:
                    Console.WriteLine("Shift down detected");
                    this.StateShiftDown = true;
                    break;

                case Keys.Control | Keys.G:
                    Console.WriteLine("Control + G down detected");
                    if (this._targetObjects.Count() > 1)
                    {
                        for (int i = 1; i< this._targetObjects.Count; i++)
                        {
                            this._targetObjects[0].AddComponent(this._targetObjects[i]);
                            this._targetCanvas.RemoveDrawingObject(this._targetObjects[i]);
                        }
                    }
                    break;

                case Keys.Control | Keys.H:
                    Console.WriteLine("Control + H down detected");
                    Console.WriteLine(this._targetObjects.Count);
                    List<IDrawingObject> temp = this._targetObjects[0].GetComponent();
                    foreach(IDrawingObject obj in temp.ToList())
                    {
                        this._targetObjects[0].RemoveComponent(obj);
                        this._targetObjects.Add(obj);
                        this._targetCanvas.AddDrawingObject(obj);
                    }
                    break;
            }            
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {            
            switch (e.KeyData)
            {
                case Keys.ShiftKey:
                    Console.WriteLine("Shift up detected");
                    this.StateShiftDown = false;
                    break;

                case Keys.Control | Keys.G:
                    Console.WriteLine("Control + G up detected");
                    break;

                case Keys.Control | Keys.H:
                    Console.WriteLine("Control + H up detected");
                    break;
            }
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);         
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this._targetCanvas.ActiveTool = this;
            this._targetCanvas.DeselectAllObject();
            Console.WriteLine("Tool has been changed to " + this.Name);
        }
    }
}
