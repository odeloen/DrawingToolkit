using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkitv01.Interfaces
{
    interface IState
    {
        //IState GetInstance();

        void Draw(IDrawingObject obj);
        IState NextState();
        IState PrevState();        
    }
}
