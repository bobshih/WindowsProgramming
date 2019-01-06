using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ModelLibrary
{
    /// <summary>
    /// 形狀的集合
    /// </summary>
    public enum shapes
    {
        Triangle = 1,
        Rectangle,
        Ellipse,
        Line,
        Pointer
    };

    public class PresentationModel
    {
        public event PresentationModelChangedEventHandler _presentationModelChanged;
        public delegate void PresentationModelChangedEventHandler();

        //紀錄目前的圖形
        shapes _drawingType;
        //model
        Model _model;
        //選取的矩形
        Rectangle _selectedRectangle;
        //一些常數字串
        const string UPLOADING_TEXT = "UpLoading...";
        const string STATE = "Drawing State : ";

        /// <summary>
        /// 初始化
        /// </summary>
        public PresentationModel(Model model)
        {
            _model = model;
            _drawingType = shapes.Triangle;
            _selectedRectangle = new Rectangle(new DrawingPoint(0, 0));
        }

        #region 對外的狀態Function
        /// <summary>
        /// 回傳UnDo按鈕的狀態
        /// </summary>
        public bool IsUnDoButtonEnable()
        {
            return _model.IsUnDoButtonEnable();
        }

        /// <summary>
        /// 回傳ReDo按鈕的狀態
        /// </summary>
        public bool IsReDoButtonEnable()
        {
            return _model.IsReDoButtonEnable();
        }

        /// <summary>
        /// 回傳Triangle按鈕的狀態
        /// </summary>
        public bool IsTriangleButtonEnable()
        {
            return !_drawingType.Equals(shapes.Triangle);
        }

        /// <summary>
        /// 回傳Rectangle按鈕的狀態
        /// </summary>
        public bool IsRectangleButtonEnable()
        {
            return !_drawingType.Equals(shapes.Rectangle);
        }

        /// <summary>
        /// 回傳Ellipse按鈕的狀態
        /// </summary>
        public bool IsEllipseButtonEnable()
        {
            return !_drawingType.Equals(shapes.Ellipse);
        }

        /// <summary>
        /// 回傳Line按鈕的狀態
        /// </summary>
        public bool IsLineButtonEnable()
        {
            return !_drawingType.Equals(shapes.Line);
        }

        /// <summary>
        /// 回傳Pointer按鈕的狀態
        /// </summary>
        public bool IsPointerButtonEnable()
        {
            return !_drawingType.Equals(shapes.Pointer);
        }

        /// <summary>
        /// 取得狀態標籤的說明文字
        /// </summary>
        public string GetStateLabelText()
        {
            return STATE + _drawingType.ToString();
        }

        /// <summary>
        /// 回傳Clear按鈕的狀態
        /// </summary>
        public bool IsClearButtonEnable()
        {
            return !_model.GetShapeListSize().Equals(0);
        }

        /// <summary>
        /// 回傳Delete按鈕的狀態
        /// </summary>
        public bool IsDeleteButtonEnable()
        {
            return !_model.GetSelectListSize().Equals(0);
        }

        /// <summary>
        /// 回傳Upload的狀態
        /// </summary>
        /// <returns></returns>
        public bool IsUploadButtonEnable(string buttonText)
        {
            return IsClearButtonEnable() && !buttonText.Equals(UPLOADING_TEXT);
        }
        #endregion

        /// <summary>
        /// 當滑鼠在畫布上按下去之後
        /// </summary>
        public void PressDownOnPanel(int x, int y)
        {
            //如果不是在Pointer模式下
            if (_drawingType != shapes.Pointer)
            {
                _model.PressDown(_drawingType, new DrawingPoint(x, y));
            }
            //反之，就是選取模式下
            else
            {
                _model.AddSelectRectangle(new DrawingPoint(x, y));
            }
            NotifyPresentationModelChanged();
        }

        /// <summary>
        /// 當滑鼠在畫布上移動的時候
        /// </summary>
        public void MoveMouseOnPanel(int x, int y)
        {
            //如果不是在Pointer模式下
            if (_drawingType != shapes.Pointer)
            {
                _model.MoveMouse(new DrawingPoint(x, y));
            }
            else
            {
                _model.SetSecondPointOfSelectRectangle(new DrawingPoint(x, y));
            }
            NotifyPresentationModelChanged();
        }

        /// <summary>
        /// 當滑鼠在畫布上放開的時後
        /// </summary>
        public void PressUpOnPanel()
        {
            //如果不是在Pointer模式下
            if (_drawingType != shapes.Pointer)
            {
                _model.PressUp();
            }
            //在pointer模式下
            else
            {
                _model.ReleaseSelectRectangle();
            }
            NotifyPresentationModelChanged();
        }

        /// <summary>
        /// 改變畫圖狀態，輸入的type是1表示畫三角形，type是2表示畫的圖形是矩形
        /// </summary>
        public void ChangeDrawingType(shapes type)
        {
            _drawingType = type;
            NotifyPresentationModelChanged();
        }

        /// <summary>
        /// 在panel上繪圖
        /// </summary>
        public void PaintPanel(IGraphics graphic)
        {
            _model.DrawPanel(graphic);
        }

        /// <summary>
        /// 執行UnDo
        /// </summary>
        public void ClickUnDo()
        {
            _model.ExcuteUnDo();
            //因為UnDo，所以模型變更了
            NotifyPresentationModelChanged();
        }

        /// <summary>
        /// 執行ReDo
        /// </summary>
        public void ClickReDo()
        {
            _model.ExcuteReDo();
            //因為ReDo，所以模型變更了
            NotifyPresentationModelChanged();
        }

        /// <summary>
        /// 清除畫面
        /// </summary>
        public void Clear()
        {
            _model.ExcuteClearCommand();
            //因為清除畫面，model當然也就變更囉
            NotifyPresentationModelChanged();
        }

        /// <summary>
        /// 執行delete
        /// </summary>
        internal void DeleteShape()
        {
            _model.ExcuteDeleteCommand();
            NotifyPresentationModelChanged();
        }

        /// <summary>
        /// 呼叫事件通知
        /// </summary>
        void NotifyPresentationModelChanged()
        {
            if (_presentationModelChanged != null)
            {
                _presentationModelChanged();
            }
        }
    }
}
