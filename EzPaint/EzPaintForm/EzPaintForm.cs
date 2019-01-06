using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModelLibrary;
using System.Globalization;
using System.IO;
using System.Net.Http;

namespace EzPaintForm
{
    public partial class PaintForm : Form
    {
        PresentationModel _presentionModel;
        const string NO_UPLOADING_TEXT = "UpLoad";
        const string UPLOADING_TEXT = "UpLoading...";

        public PaintForm(PresentationModel pModel)
        {
            InitializeComponent();
            _presentionModel = pModel;
            //初始化時先檢查視窗狀態
            CheckState();
            //assign事件
            _presentionModel._presentationModelChanged += HandlePresentationModelChanged;
        }

        #region 畫圖狀態按鈕的Click Function
        /// <summary>
        /// 點了三角形按鈕
        /// </summary>
        private void ClickTriangleButton(object sender, EventArgs e)
        {
            _presentionModel.ChangeDrawingType(shapes.Triangle);
            CheckState();
        }

        /// <summary>
        /// 點了矩形按鈕
        /// </summary>
        private void ClickRectangleButton(object sender, EventArgs e)
        {
            _presentionModel.ChangeDrawingType(shapes.Rectangle);
            CheckState();
        }

        /// <summary>
        /// 點了橢圓按鈕
        /// </summary>
        private void ClickEllipseButton(object sender, EventArgs e)
        {
            _presentionModel.ChangeDrawingType(shapes.Ellipse);
            CheckState();
        }

        /// <summary>
        /// 點了直線按鈕
        /// </summary>
        private void ClickLineButton(object sender, EventArgs e)
        {
            _presentionModel.ChangeDrawingType(shapes.Line);
            CheckState();
        }

        /// <summary>
        /// 點了POINTER按鈕
        /// </summary>
        private void ClickPointerButton(object sender, EventArgs e)
        {
            _presentionModel.ChangeDrawingType(shapes.Pointer);
            CheckState();
        }
        #endregion

        /// <summary>
        /// 檢查視窗的狀態是否有變更，包含按鈕和狀態說明
        /// </summary>
        private void CheckState()
        {
            //redo、undo、Clear、Deletec和UpLoad按鈕的狀態
            _reDoButton.Enabled = _presentionModel.IsReDoButtonEnable();
            _unDoButton.Enabled = _presentionModel.IsUnDoButtonEnable();
            _clearButton.Enabled = _presentionModel.IsClearButtonEnable();
            _deleteButton.Enabled = _presentionModel.IsDeleteButtonEnable();
            _upLoadButton.Enabled = _presentionModel.IsUploadButtonEnable(_upLoadButton.Text);
            //形狀的按鈕狀態
            _ellipseButton.Enabled = _presentionModel.IsEllipseButtonEnable();
            _triangleButton.Enabled = _presentionModel.IsTriangleButtonEnable();
            _rectangleButton.Enabled = _presentionModel.IsRectangleButtonEnable();
            _lineButton.Enabled = _presentionModel.IsLineButtonEnable();
            _pointerButton.Enabled = _presentionModel.IsPointerButtonEnable();
            //狀態標籤的說明文字
            _stateLabel.Text = _presentionModel.GetStateLabelText();
        }

        /// <summary>
        /// 在畫布上點擊之後
        /// </summary>
        private void PressDownOnDrawingPanel(object sender, MouseEventArgs e)
        {
            _presentionModel.PressDownOnPanel(e.X, e.Y);
            CheckState();
        }

        /// <summary>
        /// 滑鼠在畫布上移動
        /// </summary>
        private void MoveOnDrawingPanel(object sender, MouseEventArgs e)
        {
            _presentionModel.MoveMouseOnPanel(e.X, e.Y);
            CheckState();
        }

        /// <summary>
        /// 滑鼠在畫布上放開的時候
        /// </summary>
        private void PressUpOnDrawingPanel(object sender, MouseEventArgs e)
        {
            _presentionModel.PressUpOnPanel();
            CheckState();
        }

        /// <summary>
        /// 重新繪圖
        /// </summary>
        private void HandlePresentationModelChanged()
        {
            Invalidate(true);
        }

        /// <summary>
        /// 在畫布上畫圖
        /// </summary>
        private void DrawPanel(object sender, PaintEventArgs e)
        {
            _presentionModel.PaintPanel(new FormGraphicAdaptor(e.Graphics));
        }

        /// <summary>
        /// 點UnDo按鈕
        /// </summary>
        private void ClickUnDoButton(object sender, EventArgs e)
        {
            _presentionModel.ClickUnDo();
            CheckState();
        }

        /// <summary>
        /// 點ReDo按鈕
        /// </summary>
        private void ClickReDoButton(object sender, EventArgs e)
        {
            _presentionModel.ClickReDo();
            CheckState();
        }

        /// <summary>
        /// 點Clear按鈕
        /// </summary>
        private void ClickClearButton(object sender, EventArgs e)
        {
            _presentionModel.Clear();
            CheckState();
        }

        /// <summary>
        /// 點Delete按鈕
        /// </summary>
        private void ClickDeleteButton(object sender, EventArgs e)
        {
            _presentionModel.DeleteShape();
            CheckState();
        }

        /// <summary>
        /// 點擊Close
        /// </summary>
        private void ClickCloseButton(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 點擊UpLoad按鈕
        /// </summary>
        private async void ClickUpLoadButton(object sender, EventArgs e)
        {
            _upLoadButton.Enabled = false;
            _upLoadButton.Text = UPLOADING_TEXT;
            const string PATH = "picture.jpg";
            const string NAME = "upload.jpg";
            await UpLoadAsync(PATH, NAME);
            _upLoadButton.Enabled = _clearButton.Enabled;
            _upLoadButton.Text = NO_UPLOADING_TEXT;
        }

        /// <summary>
        /// 上傳圖片
        /// </summary>
        public async Task UpLoadAsync(string path, string name)
        {
            const string URL_STRING = "http://ntut-csie.twbbs.org:9000/user/108937c176d10d172568f86fd78de63d/upload";
            //產生圖片
            Bitmap panel = new Bitmap(this.Bounds.Width, this.Bounds.Height);
            Graphics graphics = Graphics.FromImage(panel);
            _presentionModel.PaintPanel(new FormGraphicAdaptor(graphics));
            panel.Save(path);
            //傳資料上去
            byte[] image = File.ReadAllBytes(path);
            var client = new HttpClient();
            const string UPLOAD_CONTENT = "Upload----";
            MultipartFormDataContent content = new MultipartFormDataContent(UPLOAD_CONTENT + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            const string FILE = "\"file\"";
            const string SLASH = "\"";
            content.Add(new StreamContent(new MemoryStream(image)), FILE, SLASH + name + SLASH);
            var message = await client.PostAsync(URL_STRING, content);
            MessageBox.Show(await message.Content.ReadAsStringAsync());
        }
    }
}
