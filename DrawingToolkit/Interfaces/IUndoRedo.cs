using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkitv01.Interfaces
{
    interface IUndoRedo
    {
        void AddCommand(ICommand command);
        void RemoveCommand(ICommand command);

        void Undo();
        void Redo();
    }
}
