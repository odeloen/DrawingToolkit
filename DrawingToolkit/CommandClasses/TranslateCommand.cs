using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.CommandClasses
{
    class TranslateCommand : ICommand
    {
        IDrawingObject obj;
        Point loc;

        public TranslateCommand(IDrawingObject obj, Point loc)
        {
            this.obj = obj;
            this.loc = loc;
        }

        public void Execute()
        {
            Point temp = new Point(-this.loc.X, -this.loc.Y);
            this.obj.Translate(temp);
            Console.WriteLine("Translation executed");
        }

        public void UnExecute()
        {            
            this.obj.Translate(this.loc);
            Console.WriteLine("Translation un-executed");
        }
    }
}
