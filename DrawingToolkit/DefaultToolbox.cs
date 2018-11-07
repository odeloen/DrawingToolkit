using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using DrawingToolkitv01.Interfaces;

namespace DrawingToolkitv01
{
    class DefaultToolbox : ToolStrip, IToolbox
    {
        ITool activeTool;
        public DefaultToolbox()
        {
            
        }

        public void AddTool(ITool tool)
        {
            ToolStripButton temp = (ToolStripButton)tool;            
            this.Items.Add(temp);
            temp.CheckedChanged += UnCheckToolStripButton;
            Console.WriteLine(tool.ToString() + " is added to your toolbox");
        }

        public void RemoveTool(ITool tool)
        {
            this.Items.Remove((ToolStripButton)tool);
            Console.WriteLine(tool.ToString() + "is removed from your toolbox");
        }

        public void SetCanvas(ICanvas canvas)
        {
            foreach (ITool tool in this.Items)
                tool.TargetCanvas = canvas;
        }

        public void UnCheckToolStripButton(object sender, EventArgs e)
        {            
            foreach (ToolStripButton tool in this.Items)
            {
                tool.Checked = false;
            }                
        }
    }
}
