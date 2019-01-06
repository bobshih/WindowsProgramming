using ModelLibrary;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

// 空白頁項目範本已記錄在 http://go.microsoft.com/fwlink/?LinkId=234238

namespace EzPaintApp
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ModelLibrary.PresentationModel _presentationModel;
        WindowsStoreAppGraphicAdaptor _appAdaptor;
        const string UPLOADING_TEXT = "UpLoading...";
        const string NO_UPLOADING_TEXT = "Upload";

        /// <summary>
        /// 初始化
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            _appAdaptor = new WindowsStoreAppGraphicAdaptor(_canvas);
            _presentationModel = new ModelLibrary.PresentationModel(new ModelLibrary.Model());
            _presentationModel._presentationModelChanged += HandlePresentationModelChanged;
            //檢查按鈕狀態
            CheckState();
            _uploadButton.Content = (object)NO_UPLOADING_TEXT;
        }

        #region 按鈕的點擊事件
        /// <summary>
        /// 點三角形按鈕
        /// </summary>
        private void ClickTriangleButton(object sender, RoutedEventArgs e)
        {
            _presentationModel.ChangeDrawingType(shapes.Triangle);
            CheckState();
        }

        /// <summary>
        /// 點矩形按鈕
        /// </summary>
        private void ClickRectangleButton(object sender, RoutedEventArgs e)
        {
            _presentationModel.ChangeDrawingType(shapes.Rectangle);
            CheckState();
        }
        /// <summary>
        /// 點橢圓按鈕
        /// </summary>
        public void ClickEllipseButton(object sender, RoutedEventArgs e)
        {
            _presentationModel.ChangeDrawingType(shapes.Ellipse);
            CheckState();
        }

        /// <summary>
        /// 點直線按鈕
        /// </summary>
        public void ClickLineButton(object sender, RoutedEventArgs e)
        {
            _presentationModel.ChangeDrawingType(shapes.Line);
            CheckState();
        }

        /// <summary>
        /// 點指標按鈕
        /// </summary>
        private void ClickPointerButton(object sender, RoutedEventArgs e)
        {
            _presentationModel.ChangeDrawingType(shapes.Pointer);
            CheckState();
        }

        /// <summary>
        /// 點Delete按鈕
        /// </summary>
        public void ClickDeleteButton(object sender, RoutedEventArgs e)
        {
            _presentationModel.DeleteShape();
            CheckState();
        }

        /// <summary>
        /// 點Clear按鈕
        /// </summary>
        public void ClickClearButton(object sender, RoutedEventArgs e)
        {
            _presentationModel.Clear();
            CheckState();
        }

        /// <summary>
        /// 點UnDo按鈕
        /// </summary>
        private void ClickReDoButton(object sender, RoutedEventArgs e)
        {
            _presentationModel.ClickReDo();
            CheckState();
        }

        /// <summary>
        /// 點ReDo按鈕
        /// </summary>
        private void ClickUnDoButton(object sender, RoutedEventArgs e)
        {
            _presentationModel.ClickUnDo();
            CheckState();
        }

        /// <summary>
        /// 點UpLoad按鈕
        /// </summary>
        public async void ClickUploadButton(object sender, RoutedEventArgs e)
        {
            _uploadButton.IsEnabled = false;
            _uploadButton.Content = UPLOADING_TEXT;
            await UploadCanvas();
            _uploadButton.IsEnabled = _clearButton.IsEnabled;
            _uploadButton.Content = NO_UPLOADING_TEXT;
        }
        #endregion

        /// <summary>
        /// 檢查程式狀態
        /// </summary>
        private void CheckState()
        {
            //圖形按鈕狀態檢查
            _rectangleButton.IsEnabled = _presentationModel.IsRectangleButtonEnable();
            _triangleButton.IsEnabled = _presentationModel.IsTriangleButtonEnable();
            _ellipseButton.IsEnabled = _presentationModel.IsEllipseButtonEnable();
            _lineButton.IsEnabled = _presentationModel.IsLineButtonEnable();
            _pointerButton.IsEnabled = _presentationModel.IsPointerButtonEnable();
            //狀態標籤的說明文字
            _stateDescription.Content = _presentationModel.GetStateLabelText();
            //redo、undo、clear、upload和delete按鈕檢查
            _deleteButton.IsEnabled = _presentationModel.IsDeleteButtonEnable();
            _clearButton.IsEnabled = _presentationModel.IsClearButtonEnable();
            _reDoButton.IsEnabled = _presentationModel.IsReDoButtonEnable();
            _unDoButton.IsEnabled = _presentationModel.IsUnDoButtonEnable();
            _uploadButton.IsEnabled = _presentationModel.IsUploadButtonEnable(_uploadButton.Content.ToString());
        }

        /// <summary>
        /// 在canvas上點了一下
        /// </summary>
        private void PressDownOnCanvas(object sender, PointerRoutedEventArgs e)
        {
            _presentationModel.PressDownOnPanel((int)e.GetCurrentPoint(_canvas).Position.X, (int)e.GetCurrentPoint(_canvas).Position.Y);
            CheckState();
        }

        /// <summary>
        /// 在canvas上移動
        /// </summary>
        private void MoveMouseOnCanvas(object sender, PointerRoutedEventArgs e)
        {
            _presentationModel.MoveMouseOnPanel((int)e.GetCurrentPoint(_canvas).Position.X, (int)e.GetCurrentPoint(_canvas).Position.Y);
            CheckState();
        }

        /// <summary>
        /// 在canvas上放開滑鼠
        /// </summary>
        private void PressUpOnCanvas(object sender, PointerRoutedEventArgs e)
        {
            _presentationModel.PressUpOnPanel();
            CheckState();
        }

        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <returns></returns>
        private async Task UploadCanvas()
        {
            const string URL_STRING = "http://ntut-csie.twbbs.org:9000/user/108937c176d10d172568f86fd78de63d/upload";
            //產生圖片
            RenderTargetBitmap Bitmap = new RenderTargetBitmap();
            await Bitmap.RenderAsync(_canvas);
            //開啟檔案
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            const string FILE_NAME = "picture.bmp";
            StorageFile file = await localFolder.CreateFileAsync(FILE_NAME, CreationCollisionOption.OpenIfExists);
            //把圖片丟到檔案裡
            var pixels = await Bitmap.GetPixelsAsync();
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                byte[] bytes = pixels.ToArray();
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)_canvas.ActualWidth, (uint)_canvas.ActualHeight, 96, 96, bytes);
                await encoder.FlushAsync();
            }
            //傳資料上去
            var client = new HttpClient();
            const string UPLOAD_CONTENT = "Upload----";
            MultipartFormDataContent content = new MultipartFormDataContent(UPLOAD_CONTENT + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            const string FILE = "\"file\"";
            const string SLASH = "\"";
            const string NAME = "upload.jpg";
            content.Add(new StreamContent(await file.OpenStreamForReadAsync()), FILE, SLASH + NAME + SLASH);
            var message = await client.PostAsync(URL_STRING, content);
        }

        /// <summary>
        /// 處理PresentationModel變更的事件，重新繪圖
        /// </summary>
        private void HandlePresentationModelChanged()
        {
            _presentationModel.PaintPanel(_appAdaptor);
        }
    }
}
