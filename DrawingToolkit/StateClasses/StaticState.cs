using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.StateClasses
{
    class StaticState : IState
    {
        private static StaticState instance;

        public static IState GetInstance()
        {
            if(instance == null)
            {
                instance = new StaticState();
            }
            return instance;
        }

        public void Draw(IDrawingObject obj)
        {
            obj.RenderOnStatic();
        }

        public IState NextState()
        {
            return MoveState.GetInstance();
        }

        public IState PrevState()
        {
            return null;
        }
        
    }
}
