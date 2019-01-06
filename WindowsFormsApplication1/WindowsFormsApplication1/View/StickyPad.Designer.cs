namespace StickyPadForm
{
    partial class StickyPad
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
            this.components = new System.ComponentModel.Container();
            this._stickyPadMenu = new System.Windows.Forms.MenuStrip();
            this._fileInMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._exitInFileInMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._categoryTabPage = new System.Windows.Forms.TabPage();
            this._tableLayoutInCategoryPage = new System.Windows.Forms.TableLayoutPanel();
            this._categoryDataGridView = new System.Windows.Forms.DataGridView();
            this._colorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._deleteColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this._editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this._tableLayoutInUpperCategoryPage = new System.Windows.Forms.TableLayoutPanel();
            this._newCategory = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._colorLabal = new System.Windows.Forms.Label();
            this._colorTextBox = new System.Windows.Forms.TextBox();
            this._categoryBox = new System.Windows.Forms.TextBox();
            this._nameInCategory = new System.Windows.Forms.Label();
            this._formTabControl = new System.Windows.Forms.TabControl();
            this._messageTabPage = new System.Windows.Forms.TabPage();
            this._tableLayoutInToDoPage = new System.Windows.Forms.TableLayoutPanel();
            this._messageAddition = new System.Windows.Forms.Button();
            this._toDoListDataGridView = new System.Windows.Forms.DataGridView();
            this._categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._contentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._deleteStickyPadColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this._editButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this._textError = new System.Windows.Forms.ErrorProvider(this.components);
            this._colorError = new System.Windows.Forms.ErrorProvider(this.components);
            this._toDoError = new System.Windows.Forms.ErrorProvider(this.components);
            this._stickyPadMenu.SuspendLayout();
            this._categoryTabPage.SuspendLayout();
            this._tableLayoutInCategoryPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._categoryDataGridView)).BeginInit();
            this._tableLayoutInUpperCategoryPage.SuspendLayout();
            this._formTabControl.SuspendLayout();
            this._messageTabPage.SuspendLayout();
            this._tableLayoutInToDoPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._toDoListDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._textError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._colorError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._toDoError)).BeginInit();
            this.SuspendLayout();
            // 
            // _stickyPadMenu
            // 
            this._stickyPadMenu.AccessibleName = "_stickyPadMenu";
            this._stickyPadMenu.AutoSize = false;
            this._stickyPadMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileInMenu});
            this._stickyPadMenu.Location = new System.Drawing.Point(0, 0);
            this._stickyPadMenu.Name = "_stickyPadMenu";
            this._stickyPadMenu.Size = new System.Drawing.Size(375, 24);
            this._stickyPadMenu.TabIndex = 0;
            this._stickyPadMenu.Text = "menuStrip1";
            // 
            // _fileInMenu
            // 
            this._fileInMenu.AccessibleName = "_fileMenu";
            this._fileInMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._exitInFileInMenu});
            this._fileInMenu.Name = "_fileInMenu";
            this._fileInMenu.Size = new System.Drawing.Size(37, 20);
            this._fileInMenu.Text = "File";
            // 
            // _exitInFileInMenu
            // 
            this._exitInFileInMenu.AccessibleName = "_exitInFileMenu";
            this._exitInFileInMenu.Name = "_exitInFileInMenu";
            this._exitInFileInMenu.Size = new System.Drawing.Size(92, 22);
            this._exitInFileInMenu.Text = "Exit";
            this._exitInFileInMenu.Click += new System.EventHandler(this.ClickExit);
            // 
            // _categoryTabPage
            // 
            this._categoryTabPage.AccessibleName = "_categoryTabPage";
            this._categoryTabPage.Controls.Add(this._tableLayoutInCategoryPage);
            this._categoryTabPage.Location = new System.Drawing.Point(4, 22);
            this._categoryTabPage.Name = "_categoryTabPage";
            this._categoryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._categoryTabPage.Size = new System.Drawing.Size(367, 348);
            this._categoryTabPage.TabIndex = 0;
            this._categoryTabPage.Text = "類別";
            this._categoryTabPage.UseVisualStyleBackColor = true;
            // 
            // _tableLayoutInCategoryPage
            // 
            this._tableLayoutInCategoryPage.ColumnCount = 1;
            this._tableLayoutInCategoryPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutInCategoryPage.Controls.Add(this._categoryDataGridView, 0, 1);
            this._tableLayoutInCategoryPage.Controls.Add(this._tableLayoutInUpperCategoryPage, 0, 0);
            this._tableLayoutInCategoryPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutInCategoryPage.Location = new System.Drawing.Point(3, 3);
            this._tableLayoutInCategoryPage.Name = "_tableLayoutInCategoryPage";
            this._tableLayoutInCategoryPage.RowCount = 2;
            this._tableLayoutInCategoryPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._tableLayoutInCategoryPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this._tableLayoutInCategoryPage.Size = new System.Drawing.Size(361, 342);
            this._tableLayoutInCategoryPage.TabIndex = 8;
            // 
            // _categoryDataGridView
            // 
            this._categoryDataGridView.AccessibleName = "_categoryDataGridView";
            this._categoryDataGridView.AllowUserToAddRows = false;
            this._categoryDataGridView.AllowUserToResizeColumns = false;
            this._categoryDataGridView.AllowUserToResizeRows = false;
            this._categoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._categoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._colorColumn,
            this._nameColumn,
            this._deleteColumn,
            this._editColumn});
            this._categoryDataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this._categoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._categoryDataGridView.Location = new System.Drawing.Point(3, 71);
            this._categoryDataGridView.Name = "_categoryDataGridView";
            this._categoryDataGridView.ReadOnly = true;
            this._categoryDataGridView.RowHeadersVisible = false;
            this._categoryDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._categoryDataGridView.RowTemplate.Height = 24;
            this._categoryDataGridView.Size = new System.Drawing.Size(355, 268);
            this._categoryDataGridView.TabIndex = 3;
            this._categoryDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClickCategoryDataGridViewButton);
            // 
            // _colorColumn
            // 
            this._colorColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._colorColumn.HeaderText = "Color";
            this._colorColumn.MinimumWidth = 70;
            this._colorColumn.Name = "_colorColumn";
            this._colorColumn.ReadOnly = true;
            this._colorColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _nameColumn
            // 
            this._nameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._nameColumn.DataPropertyName = "_CategoryName";
            this._nameColumn.HeaderText = "Name";
            this._nameColumn.MinimumWidth = 70;
            this._nameColumn.Name = "_nameColumn";
            this._nameColumn.ReadOnly = true;
            this._nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _deleteColumn
            // 
            this._deleteColumn.HeaderText = "";
            this._deleteColumn.Name = "_deleteColumn";
            this._deleteColumn.ReadOnly = true;
            this._deleteColumn.Text = "Delete";
            this._deleteColumn.UseColumnTextForButtonValue = true;
            this._deleteColumn.Width = 66;
            // 
            // _editColumn
            // 
            this._editColumn.HeaderText = "";
            this._editColumn.Name = "_editColumn";
            this._editColumn.ReadOnly = true;
            this._editColumn.Text = "Edit";
            this._editColumn.UseColumnTextForButtonValue = true;
            this._editColumn.Width = 66;
            // 
            // _tableLayoutInUpperCategoryPage
            // 
            this._tableLayoutInUpperCategoryPage.ColumnCount = 4;
            this._tableLayoutInUpperCategoryPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this._tableLayoutInUpperCategoryPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutInUpperCategoryPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this._tableLayoutInUpperCategoryPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this._tableLayoutInUpperCategoryPage.Controls.Add(this._newCategory, 2, 1);
            this._tableLayoutInUpperCategoryPage.Controls.Add(this._cancelButton, 3, 1);
            this._tableLayoutInUpperCategoryPage.Controls.Add(this._colorLabal, 0, 1);
            this._tableLayoutInUpperCategoryPage.Controls.Add(this._colorTextBox, 1, 1);
            this._tableLayoutInUpperCategoryPage.Controls.Add(this._categoryBox, 1, 0);
            this._tableLayoutInUpperCategoryPage.Controls.Add(this._nameInCategory, 0, 0);
            this._tableLayoutInUpperCategoryPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutInUpperCategoryPage.Location = new System.Drawing.Point(3, 3);
            this._tableLayoutInUpperCategoryPage.Name = "_tableLayoutInUpperCategoryPage";
            this._tableLayoutInUpperCategoryPage.RowCount = 2;
            this._tableLayoutInUpperCategoryPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.54054F));
            this._tableLayoutInUpperCategoryPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.45946F));
            this._tableLayoutInUpperCategoryPage.Size = new System.Drawing.Size(355, 62);
            this._tableLayoutInUpperCategoryPage.TabIndex = 7;
            // 
            // _newCategory
            // 
            this._newCategory.AccessibleName = "_newCategory";
            this._newCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._newCategory.Enabled = false;
            this._newCategory.Location = new System.Drawing.Point(242, 33);
            this._newCategory.Name = "_newCategory";
            this._newCategory.Size = new System.Drawing.Size(52, 20);
            this._newCategory.TabIndex = 2;
            this._newCategory.Text = "新增";
            this._newCategory.UseVisualStyleBackColor = true;
            this._newCategory.Click += new System.EventHandler(this.ClickNewCategory);
            // 
            // _cancelButton
            // 
            this._cancelButton.AccessibleName = "_cancelButton";
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.Enabled = false;
            this._cancelButton.Location = new System.Drawing.Point(300, 33);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(52, 20);
            this._cancelButton.TabIndex = 6;
            this._cancelButton.Text = "取消";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this.ClickCancelButton);
            // 
            // _colorLabal
            // 
            this._colorLabal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._colorLabal.AutoSize = true;
            this._colorLabal.Location = new System.Drawing.Point(38, 37);
            this._colorLabal.Name = "_colorLabal";
            this._colorLabal.Size = new System.Drawing.Size(29, 12);
            this._colorLabal.TabIndex = 4;
            this._colorLabal.Text = "顏色";
            // 
            // _colorTextBox
            // 
            this._colorTextBox.AccessibleName = "_colorTextBox";
            this._colorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._colorTextBox.BackColor = System.Drawing.SystemColors.Control;
            this._colorTextBox.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._colorTextBox.HideSelection = false;
            this._colorTextBox.Location = new System.Drawing.Point(73, 32);
            this._colorTextBox.Name = "_colorTextBox";
            this._colorTextBox.Size = new System.Drawing.Size(163, 22);
            this._colorTextBox.TabIndex = 5;
            this._colorTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClickColorTextBox);
            this._colorTextBox.BackColorChanged += new System.EventHandler(this.ChangedColorTextBox);
            this._colorTextBox.TextChanged += new System.EventHandler(this.ClearColorTextBox);
            // 
            // _categoryBox
            // 
            this._categoryBox.AccessibleName = "_categoryBox";
            this._categoryBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._categoryBox.Location = new System.Drawing.Point(73, 3);
            this._categoryBox.Name = "_categoryBox";
            this._categoryBox.Size = new System.Drawing.Size(163, 22);
            this._categoryBox.TabIndex = 1;
            this._categoryBox.TextChanged += new System.EventHandler(this.ChangeTextOfCategoryBox);
            // 
            // _nameInCategory
            // 
            this._nameInCategory.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._nameInCategory.AutoSize = true;
            this._nameInCategory.Location = new System.Drawing.Point(14, 6);
            this._nameInCategory.Name = "_nameInCategory";
            this._nameInCategory.Size = new System.Drawing.Size(53, 12);
            this._nameInCategory.TabIndex = 0;
            this._nameInCategory.Text = "類別名稱";
            // 
            // _formTabControl
            // 
            this._formTabControl.AccessibleName = "_formTabControl";
            this._formTabControl.Controls.Add(this._categoryTabPage);
            this._formTabControl.Controls.Add(this._messageTabPage);
            this._formTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formTabControl.Location = new System.Drawing.Point(0, 24);
            this._formTabControl.Name = "_formTabControl";
            this._formTabControl.SelectedIndex = 0;
            this._formTabControl.Size = new System.Drawing.Size(375, 374);
            this._formTabControl.TabIndex = 1;
            this._formTabControl.SelectedIndexChanged += new System.EventHandler(this.SelectTabPageIndex);
            // 
            // _messageTabPage
            // 
            this._messageTabPage.AccessibleName = "_messageTabPage";
            this._messageTabPage.Controls.Add(this._tableLayoutInToDoPage);
            this._messageTabPage.Location = new System.Drawing.Point(4, 22);
            this._messageTabPage.Name = "_messageTabPage";
            this._messageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._messageTabPage.Size = new System.Drawing.Size(367, 348);
            this._messageTabPage.TabIndex = 1;
            this._messageTabPage.Text = "便條清單";
            this._messageTabPage.UseVisualStyleBackColor = true;
            // 
            // _tableLayoutInToDoPage
            // 
            this._tableLayoutInToDoPage.ColumnCount = 1;
            this._tableLayoutInToDoPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutInToDoPage.Controls.Add(this._messageAddition, 0, 0);
            this._tableLayoutInToDoPage.Controls.Add(this._toDoListDataGridView, 0, 1);
            this._tableLayoutInToDoPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutInToDoPage.Location = new System.Drawing.Point(3, 3);
            this._tableLayoutInToDoPage.Name = "_tableLayoutInToDoPage";
            this._tableLayoutInToDoPage.RowCount = 2;
            this._tableLayoutInToDoPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._tableLayoutInToDoPage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutInToDoPage.Size = new System.Drawing.Size(361, 342);
            this._tableLayoutInToDoPage.TabIndex = 2;
            // 
            // _messageAddition
            // 
            this._messageAddition.AccessibleName = "_messageAddition";
            this._messageAddition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._messageAddition.Enabled = false;
            this._messageAddition.Location = new System.Drawing.Point(3, 3);
            this._messageAddition.Name = "_messageAddition";
            this._messageAddition.Size = new System.Drawing.Size(88, 34);
            this._messageAddition.TabIndex = 0;
            this._messageAddition.Text = "新增便條";
            this._messageAddition.UseVisualStyleBackColor = true;
            this._messageAddition.Click += new System.EventHandler(this.ClickAddToDoList);
            // 
            // _toDoListDataGridView
            // 
            this._toDoListDataGridView.AccessibleName = "_toDoListDataGridView";
            this._toDoListDataGridView.AllowUserToAddRows = false;
            this._toDoListDataGridView.AllowUserToResizeColumns = false;
            this._toDoListDataGridView.AllowUserToResizeRows = false;
            this._toDoListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._toDoListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._categoryColumn,
            this._contentColumn,
            this._deleteStickyPadColumn,
            this._editButtonColumn});
            this._toDoListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toDoListDataGridView.Location = new System.Drawing.Point(3, 43);
            this._toDoListDataGridView.Name = "_toDoListDataGridView";
            this._toDoListDataGridView.RowHeadersVisible = false;
            this._toDoListDataGridView.RowTemplate.Height = 24;
            this._toDoListDataGridView.Size = new System.Drawing.Size(355, 296);
            this._toDoListDataGridView.TabIndex = 1;
            this._toDoListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClickToDoListDataGridViewCell);
            this._toDoListDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ClickToDoListColumnHeader);
            // 
            // _categoryColumn
            // 
            this._categoryColumn.DataPropertyName = "_CategoryText";
            this._categoryColumn.HeaderText = "Category";
            this._categoryColumn.Name = "_categoryColumn";
            this._categoryColumn.ReadOnly = true;
            this._categoryColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._categoryColumn.Width = 80;
            // 
            // _contentColumn
            // 
            this._contentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._contentColumn.DataPropertyName = "_Content";
            this._contentColumn.HeaderText = "Content";
            this._contentColumn.Name = "_contentColumn";
            this._contentColumn.ReadOnly = true;
            this._contentColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _deleteStickyPadColumn
            // 
            this._deleteStickyPadColumn.HeaderText = "";
            this._deleteStickyPadColumn.Name = "_deleteStickyPadColumn";
            this._deleteStickyPadColumn.Text = "delete";
            this._deleteStickyPadColumn.UseColumnTextForButtonValue = true;
            this._deleteStickyPadColumn.Width = 80;
            // 
            // _editButtonColumn
            // 
            this._editButtonColumn.HeaderText = "";
            this._editButtonColumn.Name = "_editButtonColumn";
            this._editButtonColumn.Text = "edit";
            this._editButtonColumn.UseColumnTextForButtonValue = true;
            this._editButtonColumn.Width = 80;
            // 
            // _textError
            // 
            this._textError.ContainerControl = this;
            // 
            // _colorError
            // 
            this._colorError.ContainerControl = this;
            // 
            // _toDoError
            // 
            this._toDoError.ContainerControl = this;
            // 
            // StickyPad
            // 
            this.AccessibleName = "StickyPad";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(375, 398);
            this.Controls.Add(this._formTabControl);
            this.Controls.Add(this._stickyPadMenu);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.HelpButton = true;
            this.MainMenuStrip = this._stickyPadMenu;
            this.MinimumSize = new System.Drawing.Size(391, 436);
            this.Name = "StickyPad";
            this.Text = "StickyPad";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClosingStickyPad);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClosedStickyPad);
            this.Load += new System.EventHandler(this.LoadDataFromTxt);
            this._stickyPadMenu.ResumeLayout(false);
            this._stickyPadMenu.PerformLayout();
            this._categoryTabPage.ResumeLayout(false);
            this._tableLayoutInCategoryPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._categoryDataGridView)).EndInit();
            this._tableLayoutInUpperCategoryPage.ResumeLayout(false);
            this._tableLayoutInUpperCategoryPage.PerformLayout();
            this._formTabControl.ResumeLayout(false);
            this._messageTabPage.ResumeLayout(false);
            this._tableLayoutInToDoPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._toDoListDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._textError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._colorError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._toDoError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip _stickyPadMenu;
        private System.Windows.Forms.ToolStripMenuItem _fileInMenu;
        private System.Windows.Forms.ToolStripMenuItem _exitInFileInMenu;
        private System.Windows.Forms.TabPage _categoryTabPage;
        private System.Windows.Forms.TabControl _formTabControl;
        private System.Windows.Forms.Label _nameInCategory;
        private System.Windows.Forms.TextBox _categoryBox;
        private System.Windows.Forms.Button _newCategory;
        private System.Windows.Forms.Label _colorLabal;
        private System.Windows.Forms.TextBox _colorTextBox;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.TabPage _messageTabPage;
        private System.Windows.Forms.DataGridView _toDoListDataGridView;
        private System.Windows.Forms.Button _messageAddition;
        private System.Windows.Forms.DataGridView _categoryDataGridView;
        private System.Windows.Forms.ErrorProvider _textError;
        private System.Windows.Forms.ErrorProvider _colorError;
        private System.Windows.Forms.DataGridViewTextBoxColumn _categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _contentColumn;
        private System.Windows.Forms.DataGridViewButtonColumn _deleteStickyPadColumn;
        private System.Windows.Forms.DataGridViewButtonColumn _editButtonColumn;
        private System.Windows.Forms.ErrorProvider _toDoError;
        private System.Windows.Forms.TableLayoutPanel _tableLayoutInUpperCategoryPage;
        private System.Windows.Forms.TableLayoutPanel _tableLayoutInCategoryPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _nameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn _deleteColumn;
        private System.Windows.Forms.DataGridViewButtonColumn _editColumn;
        private System.Windows.Forms.TableLayoutPanel _tableLayoutInToDoPage;
    }
}

