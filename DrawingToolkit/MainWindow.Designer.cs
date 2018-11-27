namespace DrawingToolkitv01
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 484);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainWindow";
            this.Text = "Drawing Toolkit";
            this.ResumeLayout(false);

        }

        #endregion

        private void InitializeCanvas()
        {
            this.canvas = new DefaultCanvas();
            System.Windows.Forms.Control temp = (System.Windows.Forms.Control)canvas;
            temp.Location = new System.Drawing.Point(0, 25);
            temp.Size = this.ClientSize;
            temp.BackColor = System.Drawing.Color.White;
            this.Controls.Add(temp);
            System.Console.WriteLine("Canvas Successfully Initialized");
        }
        
        private void InitializeToolbox()
        {
            this.toolbox = new DefaultToolbox();
            toolbox.AddTool(new ToolClasses.SelectionTool());
            toolbox.AddTool(new ToolClasses.LineTool());
            toolbox.AddTool(new ToolClasses.SquareTool());
            toolbox.AddTool(new ToolClasses.CircleTool());
            toolbox.AddTool(new ToolClasses.LineConnectorTool());
            toolbox.AddTool(new ToolClasses.UndoTool());
            toolbox.AddTool(new ToolClasses.RedoTool());
            System.Windows.Forms.ToolStrip temp = (System.Windows.Forms.ToolStrip)toolbox;
            temp.Location = new System.Drawing.Point(0,0);
            //temp.Size = new System.Drawing.Size(25,this.ClientSize.Height);
            temp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.toolbox.SetCanvas(canvas);            
            this.Controls.Add(temp);
            System.Console.WriteLine("Toolbox Successfully Initialized");
        }

        Interfaces.ICanvas canvas;
        Interfaces.IToolbox toolbox;
    }
}

