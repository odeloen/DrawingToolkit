using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.StateClasses
{
    class PreviewState : IState
    {
        private static PreviewState instance;

        public static IState GetInstance()
        {
            if (instance == null)
            {
                instance = new PreviewState();
            }
            return instance;
        }

        public void Draw(IDrawingObject obj)
        {
            obj.RenderOnPreview();
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
