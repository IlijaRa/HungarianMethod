
namespace HungarianMethod
{
    partial class Form1
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
            this.Textbox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Continue = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Textbox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Textbox1
            // 
            this.Textbox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Textbox1.Location = new System.Drawing.Point(245, 26);
            this.Textbox1.Name = "Textbox1";
            this.Textbox1.Size = new System.Drawing.Size(154, 38);
            this.Textbox1.TabIndex = 0;
            this.Textbox1.Text = "6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter rows:";
            // 
            // Continue
            // 
            this.Continue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Continue.Location = new System.Drawing.Point(137, 143);
            this.Continue.Name = "Continue";
            this.Continue.Size = new System.Drawing.Size(153, 48);
            this.Continue.TabIndex = 2;
            this.Continue.Text = "Continue";
            this.Continue.UseVisualStyleBackColor = true;
            this.Continue.Click += new System.EventHandler(this.ButtonContinue);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enter columns:";
            // 
            // Textbox2
            // 
            this.Textbox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Textbox2.Location = new System.Drawing.Point(245, 77);
            this.Textbox2.Name = "Textbox2";
            this.Textbox2.Size = new System.Drawing.Size(154, 38);
            this.Textbox2.TabIndex = 3;
            this.Textbox2.Text = "6";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HungarianMethod.Properties.Resources.introBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(434, 211);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Textbox2);
            this.Controls.Add(this.Continue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Textbox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hungarian method";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Textbox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Continue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Textbox2;
    }
}

