using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using DrawingToolkitv01.Interfaces;
using DrawingToolkitv01.StrategyClasses;

namespace DrawingToolkitv01
{
    class DefaultCanvas : Control, ICanvas
    {
        List<IDrawingObject> ObjectsToDraw;
        IUndoRedo undoRedo;
        ITool _activeTool;
        IStrategy strategy;
        bool special;

        public ITool ActiveTool { get { return this._activeTool; } set { this._activeTool = value; } }
        public IStrategy ActiveStrategy { get => this.strategy; set => this.strategy = value;  }

        public void Special(bool value)
        {
            this.special = value;
            this.Invalidate();
            this.Update();
        }

        public DefaultCanvas()
        {
            this.ObjectsToDraw = new List<IDrawingObject>();
            this.undoRedo = new DefaultUndoRedo(this);
            this.DoubleBuffered = true;            
            this._activeTool = null;
            this.strategy = new DefaultStrategy();
            this.special = false;
        }

        public void AddDrawingObject(IDrawingObject obj)
        {
            this.strategy.AddDrawingObject(obj);            
        }

        public void AddDrawingObjectAt(int index, IDrawingObject obj)
        {
            this.strategy.AddDrawingObjectAt(obj,index);            
        }

        public void RemoveDrawingObject(IDrawingObject obj)
        {
            this.strategy.RemoveDrawingObject(obj);            
        }

        public IDrawingObject SelectObjectAt(Point loc)
        {                                  
            return this.strategy.SelectObjectAt(loc);
        }

        public void DeselectAllObject()
        {
            this.strategy.DeselectAllObject();            
        }

        public void AddCommand(ICommand command)
        {
            this.undoRedo.AddCommand(command);
        }

        public void RemoveCommand(ICommand command)
        {
            this.undoRedo.RemoveCommand(command);
        }

        public void Undo()
        {
            this.undoRedo.Undo();
            this.Invalidate();
            this.Update();
        }

        public void Redo()
        {
            this.undoRedo.Redo();
            this.Invalidate();
            this.Update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.strategy.Draw(e.Graphics, this.special);            
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (this._activeTool != null)
            {
                this._activeTool.OnKeyDown(this, e);
                this.Invalidate();
                this.Update();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (this._activeTool != null)
            {                
                this._activeTool.OnKeyUp(this, e);
                this.Invalidate();
                this.Update();
            }
        }
    }
}
