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
            ((System.ComponentModel.ISupportInitialize)(this.splitMainContainer)).BeginInit();
            this.splitMainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMainContainer
            // 
            this.splitMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMainContainer.IsSplitterFixed = true;
            this.splitMainContainer.Location = new System.Drawing.Point(0, 0);
            this.splitMainContainer.Name = "splitMainContainer";
            // 
            // splitMainContainer.Panel2
            // 
            this.splitMainContainer.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitMainContainer.Size = new System.Drawing.Size(800, 450);
            this.splitMainContainer.SplitterDistance = 654;
            this.splitMainContainer.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitMainContainer);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.splitMainContainer)).EndInit();
            this.splitMainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitMainContainer;
    }
}