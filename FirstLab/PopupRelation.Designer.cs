namespace FirstLab
{
    partial class PopupRelation
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
            this.Nothing = new System.Windows.Forms.RadioButton();
            this.Vertical = new System.Windows.Forms.RadioButton();
            this.Horizontal = new System.Windows.Forms.RadioButton();
            this.OkayButton = new System.Windows.Forms.Button();
            this.groupBoxOption = new System.Windows.Forms.GroupBox();
            this.groupBoxOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // Nothing
            // 
            this.Nothing.AutoSize = true;
            this.Nothing.Location = new System.Drawing.Point(14, 39);
            this.Nothing.Name = "Nothing";
            this.Nothing.Size = new System.Drawing.Size(84, 24);
            this.Nothing.TabIndex = 0;
            this.Nothing.TabStop = true;
            this.Nothing.Text = "Nothing";
            this.Nothing.UseVisualStyleBackColor = true;
            // 
            // Vertical
            // 
            this.Vertical.AutoSize = true;
            this.Vertical.Location = new System.Drawing.Point(14, 69);
            this.Vertical.Name = "Vertical";
            this.Vertical.Size = new System.Drawing.Size(79, 24);
            this.Vertical.TabIndex = 1;
            this.Vertical.TabStop = true;
            this.Vertical.Text = "Vertical";
            this.Vertical.UseVisualStyleBackColor = true;
            this.Vertical.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Horizontal
            // 
            this.Horizontal.AutoSize = true;
            this.Horizontal.Location = new System.Drawing.Point(14, 99);
            this.Horizontal.Name = "Horizontal";
            this.Horizontal.Size = new System.Drawing.Size(100, 24);
            this.Horizontal.TabIndex = 2;
            this.Horizontal.TabStop = true;
            this.Horizontal.Text = "Horizontal";
            this.Horizontal.UseVisualStyleBackColor = true;
            // 
            // OkayButton
            // 
            this.OkayButton.Location = new System.Drawing.Point(24, 206);
            this.OkayButton.Name = "OkayButton";
            this.OkayButton.Size = new System.Drawing.Size(94, 29);
            this.OkayButton.TabIndex = 3;
            this.OkayButton.Text = "Okay";
            this.OkayButton.UseVisualStyleBackColor = true;
            this.OkayButton.Click += new System.EventHandler(this.OkayButton_Click);
            // 
            // groupBoxOption
            // 
            this.groupBoxOption.Controls.Add(this.Nothing);
            this.groupBoxOption.Controls.Add(this.Vertical);
            this.groupBoxOption.Controls.Add(this.Horizontal);
            this.groupBoxOption.Location = new System.Drawing.Point(24, 39);
            this.groupBoxOption.Name = "groupBoxOption";
            this.groupBoxOption.Size = new System.Drawing.Size(124, 125);
            this.groupBoxOption.TabIndex = 4;
            this.groupBoxOption.TabStop = false;
            this.groupBoxOption.Text = "Option";
            this.groupBoxOption.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // PopupRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 253);
            this.Controls.Add(this.groupBoxOption);
            this.Controls.Add(this.OkayButton);
            this.MaximumSize = new System.Drawing.Size(300, 400);
            this.Name = "PopupRelation";
            this.Text = "PopupRelation";
            this.groupBoxOption.ResumeLayout(false);
            this.groupBoxOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RadioButton Nothing;
        private RadioButton Vertical;
        private RadioButton Horizontal;
        private Button OkayButton;
        private GroupBox groupBoxOption;
    }
}