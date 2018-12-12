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
        public MBR mbr;
        public List<IDrawingObject> Objs;
        public RNode parent;
        public List<RNode> Children;

        public RNode()
        {
            this.Objs = new List<IDrawingObject>();
            this.Children = new List<RNode>();
            this.mbr = new MBR();
        }
        
        public MBR GetMBR()
        {
            return this.mbr;
        }

        public void AddObj(IDrawingObject obj)
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            // With new obj
            minX = Math.Min(minX, obj.Start.X);
            minX = Math.Min(minX, obj.End.X);

            minY = Math.Min(minY, obj.Start.Y);
            minY = Math.Min(minY, obj.End.Y);

            maxX = Math.Max(maxX, obj.Start.X);
            maxX = Math.Max(maxX, obj.End.X);

            maxY = Math.Max(maxY, obj.Start.Y);
            maxY = Math.Max(maxY, obj.End.Y);

            // With MBR
            minX = Math.Min(minX, mbr.Start.X);
            minX = Math.Min(minX, mbr.End.X);

            minY = Math.Min(minY, mbr.Start.Y);
            minY = Math.Min(minY, mbr.End.Y);

            maxX = Math.Max(maxX, mbr.Start.X);
            maxX = Math.Max(maxX, mbr.End.X);

            maxY = Math.Max(maxY, mbr.Start.Y);
            maxY = Math.Max(maxY, mbr.End.Y);

            this.mbr.Start = new System.Drawing.Point(minX, minY);
            this.mbr.End = new System.Drawing.Point(maxX, maxY);
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
