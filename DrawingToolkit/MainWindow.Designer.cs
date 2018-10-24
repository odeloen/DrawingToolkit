using DrawingToolkit.Interfaces;
using DrawingToolkit.Classes;

namespace DrawingToolkit
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 468);
            this.Name = "MainWindow";
            this.Text = "Drawing Toolkit";
            this.ResumeLayout(false);

        }

        #endregion

        private void InitializeCanvas()
        {
            ///
            /// Canvas
            ///
            this.canvas = new DefaultCanvas();
            this.canvas.GetControl().Location = new System.Drawing.Point(50, 40);
            this.canvas.GetControl().Size = this.ClientSize;
            this.canvas.GetControl().BackColor = System.Drawing.Color.White;
            this.Controls.Add(canvas.GetControl());
        }

        private void InitializeToolBox()
        {
            ///
            /// Toolbox
            ///          
            this.toolbox = new DefaultToolbox();
            this.toolbox.SetCanvas(this.canvas);
            this.toolbox.GetControl().Location = new System.Drawing.Point(0, 40);
            this.toolbox.GetControl().Size = new System.Drawing.Size(50, this.ClientSize.Height);
            this.Controls.Add(this.toolbox.GetControl());
        }

        private void InitializeMenubar()
        {
            ///
            /// Menubar
            /// 
            this.menubar = new DefaultMenubar();
            this.menubar.GetControl().Location = new System.Drawing.Point(0, 0);
            this.menubar.GetControl().Size = new System.Drawing.Size(this.ClientSize.Width, 20);
            this.Controls.Add(this.menubar.GetControl());
        }

        private ICanvas canvas = null;
        private IToolBox toolbox = null;
        private IMenubar menubar = null;
    }
}

