using FirstLab.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstLab
{
    public partial class PopupRelation : Form
    {
        public PopupRelation(bool AllowShowVertical, bool AllowShowHorizontal, OptionTypeEnum LineType)
        {
            //InitializeComponent();
            Form1.OptionChosenPopup = LineType;
            this.Nothing = new System.Windows.Forms.RadioButton();
            if (AllowShowVertical)
                this.Vertical = new System.Windows.Forms.RadioButton();

            if (AllowShowHorizontal)
                this.Horizontal = new System.Windows.Forms.RadioButton();
            this.OkayButton = new System.Windows.Forms.Button();
            this.groupBoxOption = new System.Windows.Forms.GroupBox();
            this.groupBoxOption.SuspendLayout();
            this.SuspendLayout();

            if (LineType.Equals(OptionTypeEnum.Nothing))
                this.Nothing.Checked = true;
            else if (LineType.Equals(OptionTypeEnum.Vertical))
                this.Vertical.Checked = true;
            else if (LineType.Equals(OptionTypeEnum.Horizontal))
                this.Horizontal.Checked = true;
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

            if (AllowShowVertical)
            {
                this.Vertical.AutoSize = true;
                this.Vertical.Location = new System.Drawing.Point(14, 69);
                this.Vertical.Name = "Vertical";
                this.Vertical.Size = new System.Drawing.Size(79, 24);
                this.Vertical.TabIndex = 1;
                this.Vertical.TabStop = true;
                this.Vertical.Text = "Vertical";
                this.Vertical.UseVisualStyleBackColor = true;
                this.Vertical.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            }

            // 
            // Horizontal
            // 

            if (AllowShowHorizontal)
            {
                this.Horizontal.AutoSize = true;
                this.Horizontal.Location = new System.Drawing.Point(14, 99);
                this.Horizontal.Name = "Horizontal";
                this.Horizontal.Size = new System.Drawing.Size(100, 24);
                this.Horizontal.TabIndex = 2;
                this.Horizontal.TabStop = true;
                this.Horizontal.Text = "Horizontal";
                this.Horizontal.UseVisualStyleBackColor = true;
            }

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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void OkayButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("yeah");
            if (this.Vertical != null && this.Vertical.Checked)
            {
                Form1.OptionChosenPopup = OptionTypeEnum.Vertical;
            } else if (this.Horizontal != null && this.Horizontal.Checked)
            {
                Form1.OptionChosenPopup = OptionTypeEnum.Horizontal;
            } else
            {
                Form1.OptionChosenPopup = OptionTypeEnum.Nothing;
            }

            this.Close();
        }
    }
}
