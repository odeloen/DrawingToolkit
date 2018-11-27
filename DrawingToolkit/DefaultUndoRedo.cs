using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01
{
    class DefaultUndoRedo : IUndoRedo
    {
        ICanvas TargetCanvas;

        List<ICommand> UndoStack;
        List<ICommand> RedoStack;

        public DefaultUndoRedo(ICanvas canvas)
        {
            this.TargetCanvas = canvas;
            this.UndoStack = new List<ICommand>();
            this.RedoStack = new List<ICommand>();
        }

        public void AddCommand(ICommand cmd)
        {
            this.UndoStack.Add(cmd);
            this.RedoStack.Clear();
        }

        public void RemoveCommand(ICommand cmd)
        {
            this.UndoStack.Remove(cmd);
        }

        public void Undo()
        {
            if (this.UndoStack.Count > 0)
            {
                ICommand temp = this.UndoStack.Last();
                temp.UnExecute();
                RedoStack.Add(temp);
                UndoStack.Remove(temp);
            }
        }

        public void Redo()
        {
            if (this.RedoStack.Count > 0)
            {
                ICommand temp = this.RedoStack.Last();
                temp.Execute();
                UndoStack.Add(temp);
                RedoStack.Remove(temp);
            }            
        }
    }
}
