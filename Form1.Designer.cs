namespace labyrinth
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
            this.solveMazeButton = new System.Windows.Forms.Button();
            this.mazeTrace = new System.Windows.Forms.RichTextBox();
            this.exitProgramButton = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // solveMazeButton
            // 
            this.solveMazeButton.Location = new System.Drawing.Point(570, 198);
            this.solveMazeButton.Name = "solveMazeButton";
            this.solveMazeButton.Size = new System.Drawing.Size(104, 75);
            this.solveMazeButton.TabIndex = 1;
            this.solveMazeButton.Text = "Draw and solve maze";
            this.solveMazeButton.UseVisualStyleBackColor = true;
            this.solveMazeButton.Click += new System.EventHandler(this.solveMazeButton_Click);
            // 
            // mazeTrace
            // 
            this.mazeTrace.Location = new System.Drawing.Point(784, 29);
            this.mazeTrace.Name = "mazeTrace";
            this.mazeTrace.ReadOnly = true;
            this.mazeTrace.Size = new System.Drawing.Size(463, 536);
            this.mazeTrace.TabIndex = 2;
            this.mazeTrace.Text = "Check maze trace...";
            // 
            // exitProgramButton
            // 
            this.exitProgramButton.Location = new System.Drawing.Point(570, 312);
            this.exitProgramButton.Name = "exitProgramButton";
            this.exitProgramButton.Size = new System.Drawing.Size(104, 72);
            this.exitProgramButton.TabIndex = 3;
            this.exitProgramButton.Text = "Exit";
            this.exitProgramButton.UseVisualStyleBackColor = true;
            this.exitProgramButton.Click += new System.EventHandler(this.exitProgramButton_Click);
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(12, 42);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(552, 523);
            this.canvas.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1272, 741);
            this.Controls.Add(this.exitProgramButton);
            this.Controls.Add(this.mazeTrace);
            this.Controls.Add(this.solveMazeButton);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button solveMazeButton;
        private System.Windows.Forms.RichTextBox mazeTrace;
        private System.Windows.Forms.Button exitProgramButton;
        private System.Windows.Forms.Panel canvas;
    }
}

