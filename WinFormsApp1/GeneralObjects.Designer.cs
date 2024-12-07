namespace WinFormsApp1
{
    partial class GeneralObjects
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
            First = new Button();
            Next = new Button();
            Previous = new Button();
            Last = new Button();
            textBoxGen = new TextBox();
            SuspendLayout();
            // 
            // First
            // 
            First.Location = new Point(12, 27);
            First.Name = "First";
            First.Size = new Size(43, 29);
            First.TabIndex = 0;
            First.Text = "<<";
            First.UseVisualStyleBackColor = true;
            First.Click += First_Click;
            // 
            // Next
            // 
            Next.Location = new Point(327, 27);
            Next.Name = "Next";
            Next.Size = new Size(43, 29);
            Next.TabIndex = 1;
            Next.Text = ">";
            Next.UseVisualStyleBackColor = true;
            Next.Click += Next_Click;
            // 
            // Previous
            // 
            Previous.Location = new Point(61, 27);
            Previous.Name = "Previous";
            Previous.Size = new Size(43, 29);
            Previous.TabIndex = 2;
            Previous.Text = "<";
            Previous.UseVisualStyleBackColor = true;
            Previous.Click += Previous_Click;
            // 
            // Last
            // 
            Last.Location = new Point(376, 27);
            Last.Name = "Last";
            Last.Size = new Size(43, 29);
            Last.TabIndex = 3;
            Last.Text = ">>";
            Last.UseVisualStyleBackColor = true;
            Last.Click += Last_Click;
            // 
            // textBoxGen
            // 
            textBoxGen.Location = new Point(110, 27);
            textBoxGen.Name = "textBoxGen";
            textBoxGen.Size = new Size(211, 27);
            textBoxGen.TabIndex = 4;
            // 
            // GeneralObjects
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(431, 75);
            Controls.Add(textBoxGen);
            Controls.Add(Last);
            Controls.Add(Previous);
            Controls.Add(Next);
            Controls.Add(First);
            Name = "GeneralObjects";
            Text = "GeneralObjects";
            Load += GeneralObjects_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button First;
        private Button Next;
        private Button Previous;
        private Button Last;
        private TextBox textBoxGen;
    }
}