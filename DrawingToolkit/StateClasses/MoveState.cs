using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.StateClasses
{
    class MoveState : IState
    {
        private static MoveState instance;

        public static IState GetInstance()
        {
            if (instance == null)
            {
                instance = new MoveState();
            }
            return instance;
        }

        public void Draw(IDrawingObject obj)
        {
            obj.RenderOnMoveState();
        }

        public IState NextState()
        {
            return null;
        }

        public IState PrevState()
        {
            return null;
        }

    }
}
