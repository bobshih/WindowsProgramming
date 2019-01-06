namespace StickyPadForm
{
    partial class ToDoListWindows
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
            this._contentTextBox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _contentTextBox
            // 
            this._contentTextBox.AutoSize = true;
            this._contentTextBox.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._contentTextBox.Location = new System.Drawing.Point(0, 0);
            this._contentTextBox.Name = "_contentTextBox";
            this._contentTextBox.Size = new System.Drawing.Size(0, 27);
            this._contentTextBox.TabIndex = 0;
            this._contentTextBox.AccessibleName = "_contentTextBox";
            // 
            // ToDoListWindows
            // 
            this.AccessibleName = "ToDoListWindows";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this._contentTextBox);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "ToDoListWindows";
            this.Text = "便條";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseToDoListWindows);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _contentTextBox;
    }
}