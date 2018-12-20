using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01.RTree
{
    class REntry
    {
        public IDrawingObject key;
        public RNode child;

        public REntry(IDrawingObject obj)
        {
            this.key = obj;
            this.child = null;
        }

        public REntry(IDrawingObject obj, RNode child)
        {
            this.key = obj;
            this.child = child;
        }
    }
}
