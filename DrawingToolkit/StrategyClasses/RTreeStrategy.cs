using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using DrawingToolkitv01.Interfaces;
using DrawingToolkitv01.DrawingObjectClasses;
using DrawingToolkitv01.RTree;

namespace DrawingToolkitv01.StrategyClasses
{
    class RTreeStrategy : IStrategy
    {
        List<IDrawingObject> ObjectsToDraw;
        RNode root;

        public RTreeStrategy()
        {
            this.ObjectsToDraw = new List<IDrawingObject>();
            this.root = new RNode();
        }

        public void AddDrawingObject(IDrawingObject obj)
        {
            this.ObjectsToDraw.Add(obj);
            //this.CreateMBR(this.ObjectsToDraw);
        }

        public void RemoveDrawingObject(IDrawingObject obj)
        {
            this.ObjectsToDraw.Remove(obj);
        }

        public void AddDrawingObjectAt(IDrawingObject obj, int idx)
        {
            this.ObjectsToDraw.Insert(idx, obj);
        }

        public void Draw(Graphics g)
        {
            foreach (IDrawingObject obj in ObjectsToDraw)
            {
                obj.TargetGraphics = g;
                obj.Draw();
            }
        }

        public IDrawingObject SelectObjectAt(Point loc)
        {
            IDrawingObject selected = null;

            foreach (IDrawingObject obj in ObjectsToDraw)
            {
                selected = obj.Intersect(loc);
                if (selected != null)
                {
                    selected.Select();
                    break;
                }
            }
            return selected;
        }

        public void DeselectAllObject()
        {
            foreach (IDrawingObject obj in this.ObjectsToDraw)
            {
                obj.Deselect();
            }
        }

        private void CreateMBR(List<IDrawingObject> objs)
        {
            Console.WriteLine("Creating MBR");
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (IDrawingObject obj in objs)
            {
                minX = Math.Min(minX, obj.Start.X);
                minX = Math.Min(minX, obj.End.X);

                minY = Math.Min(minY, obj.Start.Y);
                minY = Math.Min(minY, obj.End.Y);

                maxX = Math.Max(maxX, obj.Start.X);
                maxX = Math.Max(maxX, obj.End.X);

                maxY = Math.Max(maxY, obj.Start.Y);
                maxY = Math.Max(maxY, obj.End.Y);
            }

            IDrawingObject newObj = new MBR();
            newObj.Start = new Point(minX, minY);
            newObj.End = new Point(maxX, maxY);
            //newObj.Deselect();
            this.ObjectsToDraw.Add(newObj);
            Console.WriteLine("Finish creating MBR");
        }

        public void StrategyMouseUp()
        {
            this.CreateMBR(this.ObjectsToDraw);
        }

        private IDrawingObject Search(Point E, RNode T)
        {
            IDrawingObject res = null;
            if (!T.IsLeaf())
            {
                List<RNode> TChildren = T.GetChildren();
                
                foreach(RNode node in TChildren)
                {
                    if (node.GetMBR().Intersect(E) != null)
                    {
                        res = Search(E, node);
                    }
                }
                return res;
            }
            else
            {
                List<IDrawingObject> ObjectToSearch = T.GetObjs();                
                foreach (IDrawingObject obj in ObjectToSearch)
                {
                    res = obj.Intersect(E);
                    if (res != null)
                    {
                        return res;
                    }
                }
                return null;
            }
        }

        private void Insert(IDrawingObject obj)
        {

        }

        private void ChooseLeaf(IDrawingObject obj)
        {

        }

        private void AdjustTree()
        {

        }

        private void Delete(IDrawingObject obj)
        {

        }

        private RNode FindLeaf(IDrawingObject obj)
        {
            return null;
        }

        private void CondenseTree()
        {

        }

        private void NodeSplitting()
        {

        }

        private void QuadraticSplit()
        {

        }

        private void PickSeeds()
        {

        }

        private void PickNext()
        {

        }

        private void LinearSplit()
        {

        }

        private void LinearPickSeeds()
        {

        }
    }
}
