namespace StickyPadForm
{
    partial class ToDoListAdditionWindows
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
            this._categoryComboBox = new System.Windows.Forms.ComboBox();
            this._categorySeletion = new System.Windows.Forms.Label();
            this._contentTextBox = new System.Windows.Forms.TextBox();
            this._addButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _categoryComboBox
            // 
            this._categoryComboBox.AccessibleName = "_categoryComboBox";
            this._categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._categoryComboBox.FormattingEnabled = true;
            this._categoryComboBox.Location = new System.Drawing.Point(45, 12);
            this._categoryComboBox.Name = "_categoryComboBox";
            this._categoryComboBox.Size = new System.Drawing.Size(138, 20);
            this._categoryComboBox.TabIndex = 0;
            this._categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.ChangeCategoryComboBox);
            // 
            // _categorySeletion
            // 
            this._categorySeletion.AutoSize = true;
            this._categorySeletion.Location = new System.Drawing.Point(12, 16);
            this._categorySeletion.Name = "_categorySeletion";
            this._categorySeletion.Size = new System.Drawing.Size(29, 12);
            this._categorySeletion.TabIndex = 1;
            this._categorySeletion.Text = "類別";
            // 
            // _contentTextBox
            // 
            this._contentTextBox.AccessibleName = "_contentTextBox";
            this._contentTextBox.Location = new System.Drawing.Point(8, 37);
            this._contentTextBox.Multiline = true;
            this._contentTextBox.Name = "_contentTextBox";
            this._contentTextBox.Size = new System.Drawing.Size(295, 221);
            this._contentTextBox.TabIndex = 2;
            this._contentTextBox.TextChanged += new System.EventHandler(this.ChangeContentTextBox);
            // 
            // _addButton
            // 
            this._addButton.AccessibleName = "_addButton";
            this._addButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._addButton.Enabled = false;
            this._addButton.Location = new System.Drawing.Point(218, 264);
            this._addButton.Name = "_addButton";
            this._addButton.Size = new System.Drawing.Size(81, 22);
            this._addButton.TabIndex = 3;
            this._addButton.Text = "確定";
            this._addButton.UseVisualStyleBackColor = true;
            this._addButton.Click += new System.EventHandler(this.ClickAddButton);
            // 
            // _cancelButton
            // 
            this._cancelButton.AccessibleName = "_cancelButton";
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(131, 264);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(81, 22);
            this._cancelButton.TabIndex = 4;
            this._cancelButton.Text = "取消";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // ToDoListAdditionWindows
            // 
            this.AccessibleName = "ToDoListAdditionWindows";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(311, 292);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._addButton);
            this.Controls.Add(this._contentTextBox);
            this.Controls.Add(this._categorySeletion);
            this.Controls.Add(this._categoryComboBox);
            this.Name = "ToDoListAdditionWindows";
            this.Text = "ToDoListAdditionWindows";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _categoryComboBox;
        private System.Windows.Forms.Label _categorySeletion;
        private System.Windows.Forms.TextBox _contentTextBox;
        private System.Windows.Forms.Button _addButton;
        private System.Windows.Forms.Button _cancelButton;


    }
}