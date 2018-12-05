using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrawingToolkitv01.Interfaces;
using DrawingToolkitv01.DrawingObjectClasses;

namespace DrawingToolkitv01.RTree
{
    class RNode
    {
        private const int MaxDegree = 4;
        IDrawingObject MBR;
        List<IDrawingObject> objs;
        RNode parent;
        List<RNode> Children;

        public RNode()
        {
            this.MBR = new MBR();
            this.Children = new List<RNode>();
        }
        
        public IDrawingObject GetMBR()
        {
            return this.MBR;
        }

        public List<IDrawingObject> GetObjs()
        {
            return this.objs;
        }

        public RNode GetParent()
        {
            return this.parent;
        }

        public List<RNode> GetChildren()
        {
            return this.Children;
        }        

        public bool IsRoot()
        {
            if (this.parent != null) return false;
            else return true;
        }

        public bool IsLeaf()
        {
            if (this.Children.Count > 0) return false;
            else return true;
        }
    }
}
