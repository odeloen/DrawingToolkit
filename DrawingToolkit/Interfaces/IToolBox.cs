using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Interfaces
{
    interface IToolBox
    {                
        System.Windows.Forms.Control GetControl();

        void SetCanvas(ICanvas canvas);
        void AddTool(ITool tool);
        void RemoveTool(int x);
    }
}
