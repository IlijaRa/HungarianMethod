
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
            this.minimization = new System.Windows.Forms.RadioButton();
            this.maximization = new System.Windows.Forms.RadioButton();
            this.btn_solve = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // minimization
            // 
            this.minimization.AutoSize = true;
            this.minimization.Location = new System.Drawing.Point(18, 256);
            this.minimization.Name = "minimization";
            this.minimization.Size = new System.Drawing.Size(286, 35);
            this.minimization.TabIndex = 0;
            this.minimization.TabStop = true;
            this.minimization.Text = "Minimization problem";
            this.minimization.UseVisualStyleBackColor = true;
            // 
            // maximization
            // 
            this.maximization.AutoSize = true;
            this.maximization.Location = new System.Drawing.Point(18, 316);
            this.maximization.Name = "maximization";
            this.maximization.Size = new System.Drawing.Size(293, 35);
            this.maximization.TabIndex = 1;
            this.maximization.TabStop = true;
            this.maximization.Text = "Maximization problem";
            this.maximization.UseVisualStyleBackColor = true;
            // 
            // btn_solve
            // 
            this.btn_solve.BackColor = System.Drawing.Color.Plum;
            this.btn_solve.Location = new System.Drawing.Point(257, 398);
            this.btn_solve.Name = "btn_solve";
            this.btn_solve.Size = new System.Drawing.Size(138, 51);
            this.btn_solve.TabIndex = 2;
            this.btn_solve.Text = "Solve";
            this.btn_solve.UseVisualStyleBackColor = false;
            this.btn_solve.Click += new System.EventHandler(this.buttonSolve);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fill matrix";
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.Plum;
            this.btn_back.Location = new System.Drawing.Point(12, 398);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(138, 51);
            this.btn_back.TabIndex = 4;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.buttonBack);
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.Color.Plum;
            this.btn_reset.Location = new System.Drawing.Point(505, 398);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(138, 51);
            this.btn_reset.TabIndex = 5;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.buttonReset);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Chocolate;
            this.button1.Location = new System.Drawing.Point(18, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 43);
            this.button1.TabIndex = 6;
            this.button1.Text = "Quick tip";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GeneratedMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HungarianMethod.Properties.Resources.generatedMatrixBackground3x3;
            this.ClientSize = new System.Drawing.Size(654, 461);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_solve);
            this.Controls.Add(this.maximization);
            this.Controls.Add(this.minimization);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "GeneratedMatrix";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeneratedMatrix";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton minimization;
        private System.Windows.Forms.RadioButton maximization;
        private System.Windows.Forms.Button btn_solve;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button button1;
    }
}