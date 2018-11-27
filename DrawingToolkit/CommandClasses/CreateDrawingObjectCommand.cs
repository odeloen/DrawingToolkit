using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using DrawingToolkitv01.Interfaces;
using DrawingToolkitv01.DrawingObjectClasses;

namespace DrawingToolkitv01.CommandClasses
{
    class CreateDrawingObjectCommand : ICommand
    {
        IDrawingObject obj;
        ICanvas canvas;

        public CreateDrawingObjectCommand(IDrawingObject obj, ICanvas canvas)
        {
            this.obj = obj;
            this.canvas = canvas;
        }

        public void Execute()
        {
            if (this.obj is LineConnector)
            {
                this.canvas.AddDrawingObjectAt(0,this.obj);
            }
            else
            {
                this.canvas.AddDrawingObject(this.obj);
            }
            Console.WriteLine("Create executed");
        }

        public void UnExecute()
        {
            this.canvas.RemoveDrawingObject(this.obj);
            Console.WriteLine("Create un-executed");
        }
    }
}
