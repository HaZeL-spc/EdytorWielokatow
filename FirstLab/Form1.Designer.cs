namespace FirstLab
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitMainContainer = new System.Windows.Forms.SplitContainer();
            this.Canvas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitMainContainer)).BeginInit();
            this.splitMainContainer.Panel1.SuspendLayout();
            this.splitMainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // splitMainContainer
            // 
            this.splitMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMainContainer.IsSplitterFixed = true;
            this.splitMainContainer.Location = new System.Drawing.Point(0, 0);
            this.splitMainContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitMainContainer.Name = "splitMainContainer";
            // 
            // splitMainContainer.Panel1
            // 
            this.splitMainContainer.Panel1.Controls.Add(this.Canvas);
            // 
            // splitMainContainer.Panel2
            // 
            this.splitMainContainer.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitMainContainer.Size = new System.Drawing.Size(914, 600);
            this.splitMainContainer.SplitterDistance = 767;
            this.splitMainContainer.SplitterWidth = 5;
            this.splitMainContainer.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(767, 600);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.splitMainContainer);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitMainContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMainContainer)).EndInit();
            this.splitMainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitMainContainer;
        public PictureBox Canvas;
    }
}