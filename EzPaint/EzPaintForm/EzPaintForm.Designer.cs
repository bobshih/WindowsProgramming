namespace EzPaintForm
{
    partial class PaintForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaintForm));
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._unDoReDoButtonSet = new System.Windows.Forms.ToolStripDropDownButton();
            this._unDoButton = new System.Windows.Forms.ToolStripMenuItem();
            this._reDoButton = new System.Windows.Forms.ToolStripMenuItem();
            this._shapeButtonSet = new System.Windows.Forms.ToolStripDropDownButton();
            this._triangleButton = new System.Windows.Forms.ToolStripMenuItem();
            this._rectangleButton = new System.Windows.Forms.ToolStripMenuItem();
            this._ellipseButton = new System.Windows.Forms.ToolStripMenuItem();
            this._lineButton = new System.Windows.Forms.ToolStripMenuItem();
            this._pointerButton = new System.Windows.Forms.ToolStripMenuItem();
            this._deleteButton = new System.Windows.Forms.ToolStripButton();
            this._clearButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._stateLabel = new System.Windows.Forms.ToolStripLabel();
            this._upLoadButton = new System.Windows.Forms.ToolStripButton();
            this._drawingPanel = new EzPaintForm.DoubleBufferPanel();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._unDoReDoButtonSet,
            this._shapeButtonSet,
            this._deleteButton,
            this._clearButton,
            this._closeButton,
            this._upLoadButton,
            this._stateLabel
            });
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(534, 25);
            this._toolStrip.TabIndex = 0;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _unDoReDoButtonSet
            // 
            this._unDoReDoButtonSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._unDoReDoButtonSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._unDoButton,
            this._reDoButton
            });
            this._unDoReDoButtonSet.Image = ((System.Drawing.Image)(resources.GetObject("_unDoReDoButtonSet.Image")));
            this._unDoReDoButtonSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._unDoReDoButtonSet.Name = "_unDoReDoButtonSet";
            this._unDoReDoButtonSet.Size = new System.Drawing.Size(88, 22);
            this._unDoReDoButtonSet.Text = "UnDo&&ReDo";
            // 
            // _unDoButton
            // 
            this._unDoButton.Name = "_unDoButton";
            this._unDoButton.Size = new System.Drawing.Size(104, 22);
            this._unDoButton.Text = "UnDo";
            this._unDoButton.Click += new System.EventHandler(this.ClickUnDoButton);
            // 
            // _reDoButton
            // 
            this._reDoButton.Name = "_reDoButton";
            this._reDoButton.Size = new System.Drawing.Size(104, 22);
            this._reDoButton.Text = "ReDo";
            this._reDoButton.Click += new System.EventHandler(this.ClickReDoButton);
            // 
            // _shapeButtonSet
            // 
            this._shapeButtonSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._shapeButtonSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._triangleButton,
            this._rectangleButton,
            this._ellipseButton,
            this._lineButton,
            this._pointerButton
            });
            this._shapeButtonSet.Image = ((System.Drawing.Image)(resources.GetObject("_shapeButtonSet.Image")));
            this._shapeButtonSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._shapeButtonSet.Name = "_shapeButtonSet";
            this._shapeButtonSet.Size = new System.Drawing.Size(89, 22);
            this._shapeButtonSet.Text = "ShapeChoice";
            // 
            // _triangleButton
            // 
            this._triangleButton.Name = "_triangleButton";
            this._triangleButton.Size = new System.Drawing.Size(126, 22);
            this._triangleButton.Text = "Triangle";
            this._triangleButton.Click += new System.EventHandler(this.ClickTriangleButton);
            // 
            // _rectangleButton
            // 
            this._rectangleButton.Name = "_rectangleButton";
            this._rectangleButton.Size = new System.Drawing.Size(126, 22);
            this._rectangleButton.Text = "Rectangle";
            this._rectangleButton.Click += new System.EventHandler(this.ClickRectangleButton);
            // 
            // _ellipseButton
            // 
            this._ellipseButton.Name = "_ellipseButton";
            this._ellipseButton.Size = new System.Drawing.Size(126, 22);
            this._ellipseButton.Text = "Ellipse";
            this._ellipseButton.Click += new System.EventHandler(this.ClickEllipseButton);
            // 
            // _lineButton
            // 
            this._lineButton.Name = "_lineButton";
            this._lineButton.Size = new System.Drawing.Size(126, 22);
            this._lineButton.Text = "Line";
            this._lineButton.Click += new System.EventHandler(this.ClickLineButton);
            // 
            // _pointerButton
            // 
            this._pointerButton.Name = "_pointerButton";
            this._pointerButton.Size = new System.Drawing.Size(126, 22);
            this._pointerButton.Text = "Pointer";
            this._pointerButton.Click += new System.EventHandler(this.ClickPointerButton);
            // 
            // _deleteButton
            // 
            this._deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("_deleteButton.Image")));
            this._deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._deleteButton.Name = "_deleteButton";
            this._deleteButton.Size = new System.Drawing.Size(44, 22);
            this._deleteButton.Text = "Delete";
            this._deleteButton.Click += new System.EventHandler(this.ClickDeleteButton);
            // 
            // _clearButton
            // 
            this._clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._clearButton.Image = ((System.Drawing.Image)(resources.GetObject("_clearButton.Image")));
            this._clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(38, 22);
            this._clearButton.Text = "Clear";
            this._clearButton.Click += new System.EventHandler(this.ClickClearButton);
            // 
            // _closeButton
            // 
            this._closeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._closeButton.Image = ((System.Drawing.Image)(resources.GetObject("_closeButton.Image")));
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(40, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this.ClickCloseButton);
            // 
            // _stateLabel
            // 
            this._stateLabel.Name = "_stateLabel";
            this._stateLabel.Size = new System.Drawing.Size(33, 22);
            this._stateLabel.Text = "State";
            // 
            // _upLoadButton
            // 
            this._upLoadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._upLoadButton.Image = ((System.Drawing.Image)(resources.GetObject("_upLoadButton.Image")));
            this._upLoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._upLoadButton.Name = "_upLoadButton";
            this._upLoadButton.Size = new System.Drawing.Size(52, 22);
            this._upLoadButton.Text = "UpLoad";
            this._upLoadButton.Click += new System.EventHandler(this.ClickUpLoadButton);
            // 
            // _drawingPanel
            // 
            this._drawingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._drawingPanel.Location = new System.Drawing.Point(0, 25);
            this._drawingPanel.Name = "_drawingPanel";
            this._drawingPanel.Size = new System.Drawing.Size(534, 386);
            this._drawingPanel.TabIndex = 1;
            this._drawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPanel);
            this._drawingPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PressDownOnDrawingPanel);
            this._drawingPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOnDrawingPanel);
            this._drawingPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PressUpOnDrawingPanel);
            // 
            // PaintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 411);
            this.Controls.Add(this._drawingPanel);
            this.Controls.Add(this._toolStrip);
            this.MinimumSize = new System.Drawing.Size(550, 450);
            this.Name = "PaintForm";
            this.Text = "EzPaintForm";
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferPanel _drawingPanel;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripLabel _stateLabel;
        private System.Windows.Forms.ToolStripDropDownButton _unDoReDoButtonSet;
        private System.Windows.Forms.ToolStripMenuItem _unDoButton;
        private System.Windows.Forms.ToolStripMenuItem _reDoButton;
        private System.Windows.Forms.ToolStripDropDownButton _shapeButtonSet;
        private System.Windows.Forms.ToolStripMenuItem _triangleButton;
        private System.Windows.Forms.ToolStripMenuItem _rectangleButton;
        private System.Windows.Forms.ToolStripMenuItem _ellipseButton;
        private System.Windows.Forms.ToolStripMenuItem _lineButton;
        private System.Windows.Forms.ToolStripButton _clearButton;
        private System.Windows.Forms.ToolStripMenuItem _pointerButton;
        private System.Windows.Forms.ToolStripButton _deleteButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.ToolStripButton _upLoadButton;
    }
}

