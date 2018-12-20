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
        public RNode parent;
        public List<REntry> entries;
        public bool isLeaf;

        public RNode()
        {
            this.parent = null;       
            this.mbr = new MBR();
            this.entries = new List<REntry>();
        }
        
        public void AddEntry(REntry entry)
        {
            this.entries.Add(entry);
            if (!this.isLeaf) entry.child.parent = this;
        }

        public MBR GetMBR()
        {
            return this.mbr;
        }        

        public bool IsRoot()
        {
            if (this.parent != null) return false;
            else return true;
        }        
    }
}
