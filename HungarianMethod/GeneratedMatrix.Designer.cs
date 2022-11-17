
namespace HungarianMethod
{
    partial class GeneratedMatrix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneratedMatrix));
            this.btn_solve = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_solve
            // 
            this.btn_solve.AutoSize = true;
            this.btn_solve.BackColor = System.Drawing.Color.LightCoral;
            this.btn_solve.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_solve.Location = new System.Drawing.Point(0, 546);
            this.btn_solve.Name = "btn_solve";
            this.btn_solve.Size = new System.Drawing.Size(792, 50);
            this.btn_solve.TabIndex = 2;
            this.btn_solve.Text = "Solve";
            this.btn_solve.UseVisualStyleBackColor = false;
            this.btn_solve.Click += new System.EventHandler(this.buttonSolve);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(792, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fill matrix";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_back.Location = new System.Drawing.Point(0, 496);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(792, 50);
            this.btn_back.TabIndex = 4;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.buttonBack);
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_reset.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_reset.Location = new System.Drawing.Point(0, 446);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(792, 50);
            this.btn_reset.TabIndex = 5;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.buttonReset);
            // 
            // GeneratedMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 596);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_solve);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "GeneratedMatrix";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeneratedMatrix";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_solve;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_reset;
    }
}