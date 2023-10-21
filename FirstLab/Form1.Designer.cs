using FirstLab.models;

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
            this.AlgorytmGroupBox = new System.Windows.Forms.GroupBox();
            this.bibliotecznyRadio = new System.Windows.Forms.RadioButton();
            this.bresenhamRadio = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitMainContainer)).BeginInit();
            this.splitMainContainer.Panel1.SuspendLayout();
            this.splitMainContainer.Panel2.SuspendLayout();
            this.splitMainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.AlgorytmGroupBox.SuspendLayout();
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
            this.splitMainContainer.Panel2.Controls.Add(this.AlgorytmGroupBox);
            this.splitMainContainer.Size = new System.Drawing.Size(914, 600);
            this.splitMainContainer.SplitterDistance = 764;
            this.splitMainContainer.SplitterWidth = 5;
            this.splitMainContainer.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(764, 600);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // AlgorytmGroupBox
            // 
            this.AlgorytmGroupBox.Controls.Add(this.bibliotecznyRadio);
            this.AlgorytmGroupBox.Controls.Add(this.bresenhamRadio);
            this.AlgorytmGroupBox.Location = new System.Drawing.Point(13, 63);
            this.AlgorytmGroupBox.Name = "AlgorytmGroupBox";
            this.AlgorytmGroupBox.Size = new System.Drawing.Size(118, 112);
            this.AlgorytmGroupBox.TabIndex = 2;
            this.AlgorytmGroupBox.TabStop = false;
            this.AlgorytmGroupBox.Text = "Algorytm";
            // 
            // bibliotecznyRadio
            // 
            this.bibliotecznyRadio.AutoSize = true;
            this.bibliotecznyRadio.Checked = true;
            this.bibliotecznyRadio.Location = new System.Drawing.Point(6, 26);
            this.bibliotecznyRadio.Name = "bibliotecznyRadio";
            this.bibliotecznyRadio.Size = new System.Drawing.Size(111, 24);
            this.bibliotecznyRadio.TabIndex = 1;
            this.bibliotecznyRadio.TabStop = true;
            this.bibliotecznyRadio.Text = "Biblioteczny";
            this.bibliotecznyRadio.UseVisualStyleBackColor = true;
            this.bibliotecznyRadio.CheckedChanged += new System.EventHandler(this.bibliotecznyRadio_CheckedChanged);
            // 
            // bresenhamRadio
            // 
            this.bresenhamRadio.AutoSize = true;
            this.bresenhamRadio.Location = new System.Drawing.Point(6, 56);
            this.bresenhamRadio.Name = "bresenhamRadio";
            this.bresenhamRadio.Size = new System.Drawing.Size(103, 24);
            this.bresenhamRadio.TabIndex = 0;
            this.bresenhamRadio.Text = "Bresenham";
            this.bresenhamRadio.UseVisualStyleBackColor = true;
            this.bresenhamRadio.CheckedChanged += new System.EventHandler(this.bresenhamRadio_CheckedChanged);
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
            this.splitMainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMainContainer)).EndInit();
            this.splitMainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.AlgorytmGroupBox.ResumeLayout(false);
            this.AlgorytmGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitMainContainer;
        public PictureBox Canvas;
        private GroupBox AlgorytmGroupBox;
        private RadioButton bibliotecznyRadio;
        private RadioButton bresenhamRadio;

        public static OptionTypeEnum OptionChosenPopup;
    }
}