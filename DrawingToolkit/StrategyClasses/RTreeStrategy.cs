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
        int MaximumNumberOfRooms = 4;

        public RTreeStrategy()
        {
            this.ObjectsToDraw = new List<IDrawingObject>();
            this.root = new RNode();
            this.root.isLeaf = true;
        }

        public void AddDrawingObject(IDrawingObject obj)
        {
            this.ObjectsToDraw.Add(obj);            
        }

        public void RemoveDrawingObject(IDrawingObject obj)
        {
            //this.ObjectsToDraw.Remove(obj);            
            this.Delete(obj);
        }

        public void AddDrawingObjectAt(IDrawingObject obj, int idx)
        {
            this.ObjectsToDraw.Insert(idx, obj);
        }

        public void Draw(Graphics g, bool special)
        {
            this.DrawMBR(g,special);
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
            IDrawingObject selected = this.Search(loc,this.root);
            if (selected != null) selected.Select();            
            return selected;
        }

        public void DeselectAllObject()
        {
            foreach (IDrawingObject obj in this.ObjectsToDraw)
            {
                obj.Deselect();
            }
        }

        private void DrawMBR(Graphics g, bool drawMBR)
        {
            //Console.WriteLine("Draw MBR Start");
            List<RNode> queue = new List<RNode>();
            queue.Add(this.root);
            //Console.WriteLine("Adding root to queue");
            while (queue.Count > 0)
            {
                RNode currNode = queue.First();
                foreach (REntry entry in currNode.entries)
                {
                    if (drawMBR || currNode.isLeaf)
                    {                        
                        entry.key.TargetGraphics = g;
                        entry.key.Draw();
                    }                    
                    if (!currNode.isLeaf)
                    {
                        queue.Add(entry.child);
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
            this.Insert(new REntry(ObjectsToDraw.Last()));
        }

        private IDrawingObject Search(Point S, RNode T)
        {            
            if (!T.isLeaf)
            {
                foreach(REntry entry in T.entries)
                {                    
                    IDrawingObject intersected = entry.key.Intersect(S);
                    if (intersected != null)
                    {
                        Console.WriteLine("Intersected with MBR");
                        return Search(S, entry.child);
                    }
                }
            }
            else
            {
                foreach (REntry entry in T.entries)
                {
                    IDrawingObject intersected = entry.key.Intersect(S);
                    if (intersected != null)
                    {
                        Console.WriteLine("Intersected with DrawingObject");
                        return entry.key;
                    }
                }
            }

            return null;
        }

        private void Insert(REntry E)
        {
            // I1 Find position for the new record
            RNode L = this.ChooseLeaf(E);
            RNode LL = null;

            // I2 Add record to leaf node                        
            L.AddEntry(E);
            Console.WriteLine("Leaf size : " + L.entries.Count);
            if (L.IsRoot()) Console.WriteLine("Root is selected");
            if (!L.IsRoot() && L.entries.Count > this.MaximumNumberOfRooms)
            {
                Tuple<RNode, RNode> tempNodes = this.NodeSplitting(L);
                L = tempNodes.Item1;
                LL = tempNodes.Item2;
            }

            // I3 Propagates changes upward
            Tuple<RNode, RNode> propagationProduct = this.AdjustTree(L, LL);

            // I4 Grow tree taller
            if (propagationProduct.Item2 != null)
            {
                Console.WriteLine("Object at root before : " + this.root.entries.Count);

                this.root = new RNode();
                this.root.isLeaf = false;
                this.root.parent = null;

                RNode ChildOfRoot1 = propagationProduct.Item1;                
                MBR newMBR = this.CreateMBR(this.makeGroupfromEntries(ChildOfRoot1.entries));
                this.root.AddEntry(new REntry(newMBR, ChildOfRoot1));
                Console.WriteLine("Child1 of root size : " + ChildOfRoot1.entries.Count);

                RNode ChildOfRoot2 = propagationProduct.Item2;                
                newMBR = this.CreateMBR(this.makeGroupfromEntries(ChildOfRoot2.entries));
                this.root.AddEntry(new REntry(newMBR, ChildOfRoot2));
                Console.WriteLine("Child2 of root size : " + ChildOfRoot2.entries.Count);

                Console.WriteLine("Object at root after : " + this.root.entries.Count);
            }

            if (this.root.entries.Count > this.MaximumNumberOfRooms)
            {
                Console.WriteLine("Object at root before : " + this.root.entries.Count);
                Tuple<RNode, RNode> tempNodes = this.NodeSplitting(this.root);                
                this.root = new RNode();
                this.root.isLeaf = false;
                this.root.parent = null;

                RNode ChildOfRoot1 = tempNodes.Item1;                
                MBR newMBR = this.CreateMBR(this.makeGroupfromEntries(ChildOfRoot1.entries));
                this.root.AddEntry(new REntry(newMBR, ChildOfRoot1));
                Console.WriteLine("Child1 of root size : " + ChildOfRoot1.entries.Count);

                RNode ChildOfRoot2 = tempNodes.Item2;
                ChildOfRoot2.parent = this.root;
                newMBR = this.CreateMBR(this.makeGroupfromEntries(ChildOfRoot2.entries));
                this.root.AddEntry(new REntry(newMBR, ChildOfRoot2));
                Console.WriteLine("Child2 of root size : " + ChildOfRoot2.entries.Count);

                Console.WriteLine("Object at root after : " + this.root.entries.Count);
            }
            Console.WriteLine("========= Insert Done ======== " + this.ObjectsToDraw.Count);
        }

        private RNode ChooseLeaf(REntry E)
        {
            IDrawingObject obj = E.key;
            Console.WriteLine("Choosing Leaf");
            RNode N = this.root;
            Console.WriteLine("Root ID : " + N.GetHashCode());
            Console.WriteLine("Root Size : " + N.entries.Count);
            while (!N.isLeaf)
            {                                
                int MinArea = int.MaxValue;
                int index = 0;
                foreach (REntry entry in N.entries)
                {
                    List<IDrawingObject> tempList = new List<IDrawingObject>();
                    tempList.Add(entry.key);
                    tempList.Add(obj);
                    MBR newMBR = this.CreateMBR(tempList);

                    int tempArea = newMBR.GetArea();
                    if (tempArea < MinArea)
                    {
                        MinArea = tempArea;
                        index = N.entries.IndexOf(entry);
                    }
                }
                N = N.entries[index].child;
                Console.WriteLine("Node ID : " + N.GetHashCode());
                Console.WriteLine("Node Size : " + N.entries.Count);
            }
            //Console.WriteLine("Chosen Leaf ID : " + N.GetHashCode());
            Console.WriteLine("Choosing Leaf Done");
            return N;
        }

        private Tuple<RNode, RNode> AdjustTree(RNode L, RNode LL)
        {
            Console.WriteLine("=== Adjusting tree start");
            RNode N = L;
            RNode NN = LL;

            while (!N.IsRoot())
            {
                RNode P = N.parent;
                RNode PP = null;
                int EN = this.getIndexfromChild(P,N);
                Console.WriteLine("Current node : " + N.GetHashCode());
                Console.WriteLine("Parent node : " + P.GetHashCode());
                Console.WriteLine("Parent entries begin size : " + P.entries.Count);

                MBR newMBR = this.CreateMBR(this.makeGroupfromEntries(N.entries));
                P.entries[EN].key = newMBR;

                if (NN != null)
                {
                    newMBR = this.CreateMBR(this.makeGroupfromEntries(NN.entries));
                    REntry entry = new REntry(newMBR, NN);
                    P.AddEntry(entry);                    

                    if (P.entries.Count > this.MaximumNumberOfRooms)
                    {
                        Tuple<RNode, RNode> tempNodes = this.NodeSplitting(P);
                        P = tempNodes.Item1;
                        PP = tempNodes.Item2;
                    }
                }
                Console.WriteLine("Parent1 entries end size : " + P.entries.Count);
                if (PP != null) Console.WriteLine("Parent2 entries end size : " + PP.entries.Count);

                N = P;
                NN = PP;
            }            
            Console.WriteLine(N.entries.Count);
            Console.WriteLine("=== Adjusting tree end");
            return new Tuple<RNode, RNode>(N, NN);
        }

        private void Delete(IDrawingObject obj)
        {
            RNode L = this.FindLeaf(obj, this.root);
            
            foreach(REntry entry in L.entries)
            {
                if (obj.GetHashCode() == entry.key.GetHashCode())
                {
                    L.entries.Remove(entry);
                    break;
                }
            }

            CondenseTree(L);

            if (!L.IsRoot() && this.root.entries.Count == 1)
            {
                this.root = this.root.entries[0].child;
                this.root.parent = null;
            }
        }

        private RNode FindLeaf(IDrawingObject obj, RNode T)
        {            
            Console.WriteLine("== Find leaf start");
            int x = (obj.Start.X + obj.End.X) / 2;
            int y = (obj.Start.Y + obj.End.Y) / 2;
            Point mid = new Point(x,y);
            if (!T.isLeaf)
            {
                foreach(REntry entry in T.entries)
                {
                    IDrawingObject intersected = entry.key.Intersect(mid);
                    if (intersected != null)
                    {
                        RNode leaf = this.FindLeaf(obj, entry.child);
                        if (leaf != null)
                        {
                            return leaf;
                        }
                        
                    }
                }
            }
            else
            {
                foreach (REntry entry in T.entries)
                {
                    IDrawingObject intersected = entry.key.Intersect(mid);
                    if (intersected != null)
                    {
                        Console.WriteLine("Leaf Found");
                        return T;
                    }

                }
            }            
            return null;
        }

        private void CondenseTree(RNode L)
        {
            RNode N = L;
            List<RNode> Q = new List<RNode>();

            while (!N.IsRoot())
            {
                RNode P = N.parent;
                int EN = this.getIndexfromChild(P, N);
                
                if (N.entries.Count < this.MaximumNumberOfRooms / 2)
                {
                    P.entries.RemoveAt(EN);
                    Q.Add(N);                    
                }
                else
                {
                    MBR newMBR = this.CreateMBR(this.makeGroupfromEntries(N.entries));
                    P.entries[EN].key = newMBR;
                }

                N = P;
            }

            List<REntry> entriesTobeInserted = new List<REntry>();

            while (Q.Count > 0)
            {
                RNode currNode = Q.First();
                if (currNode.isLeaf)
                {
                    entriesTobeInserted.AddRange(currNode.entries);
                }
                else
                {
                    foreach(REntry entry in currNode.entries)
                    {
                        Q.Add(entry.child);
                    }
                }
                Q.Remove(currNode);
            }

            foreach(REntry entry in entriesTobeInserted)
            {
                this.Insert(entry);
            }
        }

        private Tuple<RNode, RNode> NodeSplitting(RNode N)
        {
            Console.WriteLine("== Splitting Node : " + N.GetHashCode());
            Tuple<RNode, RNode> temp = this.QuadraticSplit(N);            
            Console.WriteLine("Splitting Node To " + temp.Item1.GetHashCode() + " and " + temp.Item2.GetHashCode());
            Console.WriteLine("== Splitting Complete");
            return temp;
        }

        private Tuple<RNode,RNode> QuadraticSplit(RNode N)
        {
            
            Console.WriteLine("=== Quadratic Split Start");
            RNode NN = new RNode();
            NN.isLeaf = N.isLeaf;            

            List<REntry> tempEntries = N.entries;
            N.entries = new List<REntry>();            
            Tuple <REntry, REntry> seed = this.PickSeeds(tempEntries);
            N.AddEntry(seed.Item1);            
            tempEntries.Remove(seed.Item1);
            NN.AddEntry(seed.Item2);            
            tempEntries.Remove(seed.Item2);
            Console.WriteLine("N begin size " + N.entries.Count);
            Console.WriteLine("NN begin size " + NN.entries.Count);

            MBR NMBR = new MBR();
            MBR NNMBR = new MBR();
            while (tempEntries.Count > 0)
            {
                if (N.entries.Count > this.MaximumNumberOfRooms / 2)
                {
                    Console.WriteLine("N is full");
                    while (tempEntries.Count > 0)
                    {
                        REntry entry = tempEntries.Last();
                        NN.AddEntry(entry);                        
                        tempEntries.Remove(entry);
                        Console.WriteLine("Adding to NN");
                    }
                }
                else if (NN.entries.Count > this.MaximumNumberOfRooms / 2)
                {
                    Console.WriteLine("NN is full");
                    while (tempEntries.Count > 0)
                    {
                        REntry entry = tempEntries.Last();
                        N.AddEntry(entry);                        
                        tempEntries.Remove(entry);
                        Console.WriteLine("Adding to N");
                    }
                }
                else
                {
                    List<IDrawingObject> list1 = new List<IDrawingObject>();
                    list1.AddRange(this.makeGroupfromEntries(N.entries));                    
                    MBR newMBR1 = this.CreateMBR(list1);

                    List<IDrawingObject> list2 = new List<IDrawingObject>();
                    list2.AddRange(this.makeGroupfromEntries(NN.entries));                    
                    MBR newMBR2 = this.CreateMBR(list2);

                    REntry entry = PickNext(tempEntries, newMBR1, newMBR2);

                    list1.Add(entry.key);
                    newMBR1 = this.CreateMBR(list1);

                    list2.Add(entry.key);
                    newMBR2 = this.CreateMBR(list2);

                    if (newMBR1.GetArea() < newMBR2.GetArea())
                    {
                        N.AddEntry(entry);                        
                    }
                    else if (newMBR1.GetArea() > newMBR2.GetArea())
                    {
                        NN.AddEntry(entry);                        
                    }
                    else if (N.entries.Count < N.entries.Count)
                    {
                        N.AddEntry(entry);                        
                    }
                    else
                    {
                        NN.AddEntry(entry);                        
                    }
                    tempEntries.Remove(entry);
                }
            }
            Console.WriteLine("N end size " + N.entries.Count);
            Console.WriteLine("NN end size " + NN.entries.Count);
            Tuple<RNode, RNode> res = new Tuple<RNode, RNode>(N, NN);
            Console.WriteLine("=== Quadratic Split End");
            return res;
        }

        private Tuple<REntry, REntry> PickSeeds(List<REntry> entries)
        {
            
            int selected1 = -1, selected2 = -1;
            int MaxD = int.MinValue;
            for (int i = 0;i < entries.Count; i++)
            {
                for(int j = 0; j< entries.Count; j++)
                {
                    if (i == j) continue;                    
                    IDrawingObject e1 = entries[i].key;
                    IDrawingObject e2 = entries[j].key;
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
            Console.WriteLine("Size : " + entries.Count + " selected 1 : " + selected1 + " selected 2 : " + selected2);            
            Tuple<REntry, REntry> res = new Tuple<REntry, REntry>(entries[selected1], entries[selected2]);            
            return res;
        }

        private REntry PickNext(List<REntry> entries, MBR mbr1, MBR mbr2)
        {
            Console.WriteLine("==== PickNext Start");
            REntry res = null;            

            int MaxD = int.MinValue;
            foreach (REntry entry in entries)
            {
                List<IDrawingObject> Group1 = new List<IDrawingObject>();
                Group1.Add(mbr1);
                Group1.Add(entry.key);
                MBR newMBR1 = CreateMBR(Group1);
                int d1 = newMBR1.GetArea() - mbr1.GetArea();

                List<IDrawingObject> Group2 = new List<IDrawingObject>();
                Group2.Add(mbr2);
                Group2.Add(entry.key);
                MBR newMBR2 = CreateMBR(Group2);
                int d2 = newMBR2.GetArea() - mbr2.GetArea();

                if (Math.Abs(d1 - d2) > MaxD)
                {
                    res = entry; 
                }
            }

            Console.WriteLine("==== PickNext End with result : " + res.GetHashCode());
            return res;
        }

        private void LinearSplit()
        {

        }

        private void LinearPickSeeds()
        {

        }      
        
        private List<IDrawingObject> makeGroupfromEntries(List<REntry> entries)
        {
            List<IDrawingObject> res = new List<IDrawingObject>();

            foreach (REntry entry in entries)
            {
                res.Add(entry.key);
            }

            return res;
        }
        
        private int getIndexfromChild(RNode parent, RNode child)
        {            
            for (int idx = 0; idx < parent.entries.Count ; idx++)
            {
                if(parent.entries[idx].child.GetHashCode() == child.GetHashCode())
                {
                    return idx;
                }
            }
            return -1;
        }
    }
}
