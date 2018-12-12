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
        int MaximumNumberOfRooms = 3;

        public RTreeStrategy()
        {
            this.ObjectsToDraw = new List<IDrawingObject>();
            this.root = new RNode();
        }

        public void AddDrawingObject(IDrawingObject obj)
        {
            this.ObjectsToDraw.Add(obj);
            
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
            this.DrawMBR(g);
            /*
            foreach (IDrawingObject obj in ObjectsToDraw)
            {
                obj.TargetGraphics = g;
                obj.Draw();
            }
            */
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

        private void DrawMBR(Graphics g)
        {
            //Console.WriteLine("Draw MBR Start");
            List<RNode> queue = new List<RNode>();
            queue.Add(this.root);
            //Console.WriteLine("Adding root to queue");
            while (queue.Count > 0)
            {
                RNode currNode = queue.First();
                foreach (IDrawingObject obj in currNode.Objs)
                {
                    obj.TargetGraphics = g;
                    obj.Draw();
                }
                if (!currNode.IsLeaf())
                {
                    foreach (RNode child in currNode.Children)
                    {
                        queue.Add(child);
                    }
                }
                queue.Remove(currNode);
            }
            //Console.WriteLine("Draw MBR End");
        }

        private MBR CreateMBR(List<IDrawingObject> objs)
        {
            //Console.WriteLine("Creating MBR");
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

            MBR newObj = new MBR();            
            newObj.Start = new Point(minX, minY);
            newObj.End = new Point(maxX, maxY);
            //Console.WriteLine("Finish creating MBR");
            return newObj;            
        }

        public void StrategyMouseUp()
        {
            this.Insert(ObjectsToDraw.Last());
        }

        private IDrawingObject Search(Point E, RNode T)
        {
            IDrawingObject res = null;
            if (!T.IsLeaf())
            {
                List<IDrawingObject> TMBRs = T.Objs;
                
                foreach(IDrawingObject MBR in TMBRs)
                {
                    if (MBR.Intersect(E) != null)
                    {
                        int index = TMBRs.IndexOf(MBR);
                        res = Search(E, T.Children[index]);
                        if (res != null) return res;
                    }
                }
                return null;
            }
            else
            {
                List<IDrawingObject> ObjectToSearch = T.Objs;                
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

        private void Insert(IDrawingObject E)
        {
            // I1 Find position for the new record
            RNode L = this.ChooseLeaf(E);
            RNode LL = null;

            // I2 Add record to leaf node
            L.Objs.Add(E);     
            if (!L.IsRoot() && L.Objs.Count > this.MaximumNumberOfRooms)
            {
                Tuple<RNode, RNode> tempNodes = this.NodeSplitting(L);
                L = tempNodes.Item1;
                LL = tempNodes.Item2;
            }

            // I3 Propagates changes upward
            this.AdjustTree(L, LL);

            // I4 Grow tree taller
            if (this.root.Objs.Count > this.MaximumNumberOfRooms)
            {                
                Tuple<RNode, RNode> tempNodes = this.NodeSplitting(this.root);
                Console.WriteLine("Object at root before : " + this.root.Objs.Count);
                RNode ChildOfRoot1 = tempNodes.Item1;
                ChildOfRoot1.parent = this.root;
                this.root.Children.Add(ChildOfRoot1);
                MBR newMBR = this.CreateMBR(ChildOfRoot1.Objs);
                this.root.Objs.Add(newMBR);

                RNode ChildOfRoot2 = tempNodes.Item2;
                ChildOfRoot2.parent = this.root;
                this.root.Children.Add(ChildOfRoot2);
                newMBR = this.CreateMBR(ChildOfRoot2.Objs);
                this.root.Objs.Add(newMBR);
                Console.WriteLine("Object at root after : " + this.root.Objs.Count);
            }
        }

        private RNode ChooseLeaf(IDrawingObject obj)
        {
            Console.WriteLine("Choosing Leaf");
            RNode N = this.root;
            
            while (!N.IsLeaf())
            {
                Console.WriteLine("Node ID : " + N.GetHashCode());
                List<IDrawingObject> NMBRs = N.Objs;
                int MinArea = int.MaxValue;
                int index = 0;
                foreach (IDrawingObject MBR in NMBRs)
                {
                    List<IDrawingObject> tempList = new List<IDrawingObject>();
                    tempList.Add(MBR);
                    tempList.Add(obj);
                    MBR newMBR = this.CreateMBR(tempList);

                    int tempArea = newMBR.GetArea();
                    if (tempArea < MinArea)
                    {
                        MinArea = tempArea;
                        index = NMBRs.IndexOf(MBR);
                        Console.WriteLine("Objects Size  : " + NMBRs.Count);
                        Console.WriteLine("Children Size : " + N.Children.Count);                        
                    }
                }
                N = N.Children[index];
            }
            Console.WriteLine("Chosen Leaf ID : " + N.GetHashCode());
            Console.WriteLine("Choosing Leaf Done");
            return N;
        }

        private void AdjustTree(RNode L, RNode LL)
        {
            Console.WriteLine("Adjusting tree start");
            RNode N = L;
            RNode NN = LL;

            while (!N.IsRoot())
            {
                RNode P = N.parent;
                RNode PP = null;
                int EN = P.Children.IndexOf(N);
                Console.WriteLine("Current Node : " + N.GetHashCode());
                Console.WriteLine("Parent Node : " + P.GetHashCode());
                Console.WriteLine("Objs Size : " + P.Objs.Count);
                Console.WriteLine("Children Size : " + P.Children.Count);
                Console.WriteLine("EN : " + EN);

                MBR newMBR = this.CreateMBR(N.Objs);
                P.Objs[EN] = newMBR;

                if (NN != null)
                {
                    newMBR = this.CreateMBR(NN.Objs);
                    P.Objs.Add(newMBR);
                    P.Children.Add(NN);

                    if (P.Objs.Count > this.MaximumNumberOfRooms)
                    {
                        Tuple<RNode, RNode> tempNodes = this.NodeSplitting(P);
                        P = tempNodes.Item1;
                        PP = tempNodes.Item2;
                    }
                }

                N = P;
                NN = PP;
            }
            Console.WriteLine("Adjusting tree end");
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

        private Tuple<RNode, RNode> NodeSplitting(RNode N)
        {
            Console.WriteLine("Splitting Node : " + N.GetHashCode());
            Tuple<RNode, RNode> temp = this.QuadraticSplit(N);
            Console.WriteLine("Splitting Node To " + temp.Item1.GetHashCode() + " and " + temp.Item2.GetHashCode());
            Console.WriteLine("Splitting Complete");
            return temp;
        }

        private Tuple<RNode,RNode> QuadraticSplit(RNode N)
        {
            Console.WriteLine("Quadratic Split Start");
            RNode Group1 = new RNode();
            RNode Group2 = new RNode();
            Group1.parent = N.parent;
            Group2.parent = N.parent;

            Tuple <IDrawingObject, IDrawingObject> seed = this.PickSeeds(N.Objs);            
            TransferNodeContent(N, Group1, seed.Item1);
            TransferNodeContent(N, Group2, seed.Item2);            

            while (N.Objs.Count > 0)
            {
                if (Group1.Objs.Count > this.MaximumNumberOfRooms / 2)
                {
                    while (N.Objs.Count > 0)
                    {
                        IDrawingObject obj = N.Objs.Last();
                        TransferNodeContent(N, Group2, obj);
                    }
                }
                else if (Group2.Objs.Count > this.MaximumNumberOfRooms / 2)
                {
                    while (N.Objs.Count > 0)
                    {
                        IDrawingObject obj = N.Objs.Last();
                        TransferNodeContent(N, Group2, obj);
                    }
                }
                else
                {
                    IDrawingObject obj = PickNext(N.Objs, Group1.mbr, Group2.mbr);

                    List<IDrawingObject> list1 = new List<IDrawingObject>();
                    list1.AddRange(Group1.Objs);
                    list1.Add(obj);
                    MBR newMBR1 = this.CreateMBR(list1);

                    List<IDrawingObject> list2 = new List<IDrawingObject>();
                    list2.AddRange(Group2.Objs);
                    list2.Add(obj);
                    MBR newMBR2 = this.CreateMBR(list2);

                    if (newMBR1.GetArea() < newMBR2.GetArea())
                    {
                        TransferNodeContent(N, Group1, obj);
                    }
                    else if (newMBR1.GetArea() > newMBR2.GetArea())
                    {
                        TransferNodeContent(N, Group2, obj);
                    }
                    else if (Group1.Objs.Count < Group2.Objs.Count)
                    {
                        TransferNodeContent(N, Group1, obj);
                    }
                    else
                    {
                        TransferNodeContent(N, Group2, obj);
                    }
                }
            }
            
            Tuple<RNode, RNode> res = new Tuple<RNode, RNode>(Group1, Group2);
            Console.WriteLine("Quadratic Split End");
            return res;
        }

        private Tuple<IDrawingObject, IDrawingObject> PickSeeds(List<IDrawingObject> Objs)
        {
            
            int selected1 = -1, selected2 = -1;
            int MaxD = int.MinValue;
            for (int i = 0;i < Objs.Count; i++)
            {
                for(int j = 0; j< Objs.Count; j++)
                {
                    if (i == j) continue;
                    // MBRS
                    IDrawingObject e1 = Objs[i];
                    IDrawingObject e2 = Objs[j];
                    List<IDrawingObject> tempList = new List<IDrawingObject>();
                    tempList.Add(e1); tempList.Add(e2);
                    MBR newMBR = CreateMBR(tempList);

                    int d = newMBR.GetArea() - e1.GetArea() - e2.GetArea();
                    if (d > MaxD)
                    {
                        MaxD = d;
                        selected1 = i;
                        selected2 = j;
                    }
                }
            }
            Tuple<IDrawingObject, IDrawingObject> res = new Tuple<IDrawingObject, IDrawingObject>(Objs[selected1], Objs[selected2]);
            //res.Add(Objs[selected1]); res.Add(Objs[selected2]);
            return res;
        }

        private IDrawingObject PickNext(List<IDrawingObject> Objs, MBR mbr1, MBR mbr2)
        {
            Console.WriteLine("PickNext Start");
            IDrawingObject res = null;            

            int MaxD = int.MinValue;
            foreach (IDrawingObject obj in Objs)
            {
                List<IDrawingObject> Group1 = new List<IDrawingObject>();
                Group1.Add(mbr1);
                Group1.Add(obj);
                MBR newMBR1 = CreateMBR(Group1);
                int d1 = newMBR1.GetArea() - mbr1.GetArea();

                List<IDrawingObject> Group2 = new List<IDrawingObject>();
                Group1.Add(mbr2);
                Group1.Add(obj);
                MBR newMBR2 = CreateMBR(Group1);
                int d2 = newMBR2.GetArea() - mbr2.GetArea();

                if (Math.Abs(d1 - d2) > MaxD)
                {
                    res = obj; 
                }
            }

            Console.WriteLine("PickNext End with result : " + res.GetHashCode());
            return res;
        }

        private void LinearSplit()
        {

        }

        private void LinearPickSeeds()
        {

        }

        private void TransferNodeContent(RNode from, RNode to, IDrawingObject targetObject)
        {
            int index = from.Objs.IndexOf(targetObject);
            Console.WriteLine("From : " + from.GetHashCode());
            Console.WriteLine("Objs Size : " + from.Objs.Count);
            Console.WriteLine("Children Size : " + from.Children.Count);
            Console.WriteLine("Index : " + index);

            Console.WriteLine("To : " + to.GetHashCode());
            Console.WriteLine("Objs Size : " + from.Objs.Count);
            
            to.Objs.Add(targetObject);
            from.Objs.Remove(targetObject);
            if (!from.IsLeaf())
            {
                to.Children.Add(from.Children[index]);
                from.Children.RemoveAt(index);
            }                     
        }
    }
}
