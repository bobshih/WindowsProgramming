using System;
using ModelLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class PresentationModelTest
    {
        const int POINT_X_ONE = 50;
        const int POINT_Y_ONE = 304;
        const int POINT_X_TWO = 403;
        const int POINT_Y_TWO = 3;
        DrawingPoint _drawingPointOne;
        DrawingPoint _drawingPointTwo;
        Triangle _triangle;
        Rectangle _rectangle;
        FakeIGraphic _iGraphic;
        Model _model;
        PrivateObject _privateModel;
        PresentationModel _presentationModel;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _drawingPointOne = new DrawingPoint(POINT_X_ONE, POINT_Y_ONE);
            _drawingPointTwo = new DrawingPoint(POINT_X_TWO, POINT_Y_TWO);
            _triangle = new Triangle(_drawingPointOne);
            _rectangle = new Rectangle(_drawingPointTwo);
            _iGraphic = new FakeIGraphic();
            _model = new Model();
            _privateModel = new PrivateObject(_model);
            _presentationModel = new PresentationModel(_model);
        }

        /// <summary>
        /// 測試PressDownOnPanel()
        /// </summary>
        [TestMethod]
        public void TestPressDownOnPanel()
        {
            Assert.AreEqual(_privateModel.GetFieldOrProperty("_isPressed"), false);
            _presentationModel.PressDownOnPanel(POINT_X_ONE, POINT_Y_ONE);
            Assert.AreEqual(_privateModel.GetFieldOrProperty("_isPressed"), true);
            _presentationModel.PressUpOnPanel();
            //測試pointer的部分
            _presentationModel.ChangeDrawingType(shapes.Pointer);
            _presentationModel.PressDownOnPanel(POINT_X_TWO, POINT_Y_TWO);
            Rectangle selectRectangle = (Rectangle)_privateModel.GetFieldOrProperty("_selectRectangle");
            Assert.AreNotEqual(selectRectangle.GetFirstPoint().GetXCoordinate(), -1);
            Assert.AreNotEqual(selectRectangle.GetFirstPoint().GetYCoordinate(), -1);
        }

        /// <summary>
        /// 測試MoveMouseOnPanel()
        /// </summary>
        [TestMethod]
        public void TestMoveMouseOnPanel()
        {
            _presentationModel.PressDownOnPanel(POINT_X_ONE, POINT_Y_ONE);
            //儲存_hint的右下座標
            DrawingPoint tempPoint = _model.GetHint().GetSecondPoint();
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(tempPoint, _drawingPointOne), true);
            _presentationModel.MoveMouseOnPanel(POINT_X_TWO, POINT_Y_TWO);
            tempPoint = _model.GetHint().GetSecondPoint();
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(tempPoint, _drawingPointTwo), true);
            _presentationModel.PressUpOnPanel();
            //測試pointer的部分
            _presentationModel.ChangeDrawingType(shapes.Pointer);
            _presentationModel.PressDownOnPanel(POINT_X_TWO, POINT_Y_TWO);
            Rectangle selectRectangle = (Rectangle)_privateModel.GetFieldOrProperty("_selectRectangle");
            Assert.AreEqual(selectRectangle.GetSecondPoint().GetXCoordinate(), 0);
            Assert.AreEqual(selectRectangle.GetSecondPoint().GetYCoordinate(), 0);
            _presentationModel.MoveMouseOnPanel(POINT_X_ONE, POINT_Y_ONE);
            Assert.AreNotEqual(selectRectangle.GetSecondPoint().GetXCoordinate(), 0);
            Assert.AreNotEqual(selectRectangle.GetSecondPoint().GetYCoordinate(), 0);
        }

        /// <summary>
        /// 測試PressUpOnPanel()
        /// </summary>
        [TestMethod]
        public void TestPressUpOnPanel()
        {
            _presentationModel.PressDownOnPanel(POINT_X_TWO, POINT_Y_TWO);
            Assert.AreEqual(_privateModel.GetFieldOrProperty("_isPressed"), true);
            _presentationModel.PressUpOnPanel();
            Assert.AreEqual(_privateModel.GetFieldOrProperty("_isPressed"), false);
            //測試Pointer的部分
            _presentationModel.ChangeDrawingType(shapes.Pointer);
            _presentationModel.PressDownOnPanel(POINT_X_TWO, POINT_Y_TWO);
            _presentationModel.MoveMouseOnPanel(POINT_X_ONE, POINT_Y_ONE);
            Rectangle selectRectangle = (Rectangle)_privateModel.GetFieldOrProperty("_selectRectangle");
            Assert.AreNotEqual(selectRectangle.GetFirstPoint().GetXCoordinate(), -1);
            Assert.AreNotEqual(selectRectangle.GetFirstPoint().GetYCoordinate(), -1);
            _presentationModel.PressUpOnPanel();
            selectRectangle = (Rectangle)_privateModel.GetFieldOrProperty("_selectRectangle");
            Assert.AreEqual(selectRectangle.GetFirstPoint().GetXCoordinate(), -1);
            Assert.AreEqual(selectRectangle.GetFirstPoint().GetYCoordinate(), -1);
        }

        /// <summary>
        /// 測試GetStateLabel()
        /// </summary>
        [TestMethod]
        public void TestGetStateLabelText()
        {
            Assert.AreEqual(_presentationModel.GetStateLabelText(), "Drawing State : " + shapes.Triangle.ToString());
            _presentationModel.ChangeDrawingType(shapes.Rectangle);
            Assert.AreEqual(_presentationModel.GetStateLabelText(), "Drawing State : " + shapes.Rectangle.ToString());
            _presentationModel.ChangeDrawingType(shapes.Ellipse);
            Assert.AreEqual(_presentationModel.GetStateLabelText(), "Drawing State : " + shapes.Ellipse.ToString());
            _presentationModel.ChangeDrawingType(shapes.Line);
            Assert.AreEqual(_presentationModel.GetStateLabelText(), "Drawing State : " + shapes.Line.ToString());
            _presentationModel.ChangeDrawingType(shapes.Triangle);
            Assert.AreEqual(_presentationModel.GetStateLabelText(), "Drawing State : " + shapes.Triangle.ToString());
        }

        /// <summary>
        /// 測試ChangeDrawingType()
        /// </summary>
        [TestMethod]
        public void TestChangeDrawingType()
        {
            Assert.AreEqual(_presentationModel.IsRectangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsTriangleButtonEnable(), false);
            //轉換成矩形模式
            _presentationModel.ChangeDrawingType(shapes.Rectangle);
            Assert.AreEqual(_presentationModel.IsRectangleButtonEnable(), false);
            Assert.AreEqual(_presentationModel.IsTriangleButtonEnable(), true);
            //轉換成三角形模式
            _presentationModel.ChangeDrawingType(shapes.Triangle);
            Assert.AreEqual(_presentationModel.IsRectangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsTriangleButtonEnable(), false);
        }

        /// <summary>
        /// 測試PaintPanel()
        /// </summary>
        [TestMethod]
        public void TestPaintPanel()
        {
            Assert.AreEqual(_iGraphic.GetRectangleFirstPointX(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleFirstPointY(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleSecondPointY(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointOne(), new DrawingPoint(0, 0)), true);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointTwo(), new DrawingPoint(0, 0)), true);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointThree(), new DrawingPoint(0, 0)), true);
            //因為內建一開始的畫圖模式是三角形，所以下面的assert檢查三角形部分
            _presentationModel.PressDownOnPanel(POINT_X_TWO, POINT_Y_TWO);
            //畫圖
            _presentationModel.PaintPanel(_iGraphic);
            Assert.AreEqual(_iGraphic.GetRectangleFirstPointX(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleFirstPointY(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleSecondPointY(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointOne(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointTwo(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointThree(), new DrawingPoint(0, 0)), false);
            //把畫圖模式轉成矩形模式，因為上面有輸入三角形了，所以下面的assert除了矩形，三角形的部分也要檢查
            _presentationModel.ChangeDrawingType(shapes.Rectangle);
            //輸入座標
            _presentationModel.PressDownOnPanel(POINT_X_ONE, POINT_Y_ONE);
            //畫圖
            _presentationModel.PaintPanel(_iGraphic);
            Assert.AreNotEqual(_iGraphic.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointOne(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointTwo(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointThree(), new DrawingPoint(0, 0)), false);
        }

        /// <summary>
        /// 測試ClickUnDo()
        /// </summary>
        [TestMethod]
        public void TestClickUnDo()
        {
            Assert.AreEqual(_presentationModel.IsUnDoButtonEnable(), false);
            DrawAPictureInPresentationModel(_drawingPointOne, _drawingPointTwo);
            Assert.AreEqual(_presentationModel.IsUnDoButtonEnable(), true);
            _presentationModel.ClickUnDo();
            Assert.AreEqual(_presentationModel.IsUnDoButtonEnable(), false);
        }

        /// <summary>
        /// 測試Clear()和IsClearButtonEnable
        /// </summary>
        [TestMethod]
        public void TestClearAndIsClearButtonEnabled()
        {
            Assert.AreEqual(_presentationModel.IsClearButtonEnable(), false);
            DrawAPictureInPresentationModel(_drawingPointOne, _drawingPointTwo);
            Assert.AreEqual(_presentationModel.IsClearButtonEnable(), true);
            _presentationModel.Clear();
            Assert.AreEqual(_presentationModel.IsClearButtonEnable(), false);
        }

        /// <summary>
        /// 測試ClickReDo()
        /// </summary>
        [TestMethod]
        public void TestClickReDo()
        {
            Assert.AreEqual(_presentationModel.IsReDoButtonEnable(), false);
            DrawAPictureInPresentationModel(_drawingPointTwo, _drawingPointOne);
            //點了UnDo，ReDo就可以點了
            _presentationModel.ClickUnDo();
            Assert.AreEqual(_presentationModel.IsReDoButtonEnable(), true);
            //點了ReDo，ReDo再度被disable
            _presentationModel.ClickReDo();
            Assert.AreEqual(_presentationModel.IsReDoButtonEnable(), false);
        }

        /// <summary>
        /// 測試Notify()
        /// </summary>
        [TestMethod]
        public void TestNotify()
        {
            string notify = string.Empty;
            _presentationModel._presentationModelChanged += delegate()
            {
                notify = "PresentationModelChaged";
            };
            Assert.AreEqual(notify, string.Empty);
            _presentationModel.PressDownOnPanel(POINT_X_TWO, POINT_Y_TWO);
            Assert.AreEqual(notify, "PresentationModelChaged");

        }

        /// <summary>
        /// 測試圖形ButtonEnable的狀態Function
        /// </summary>
        [TestMethod]
        public void TestButtonEnable()
        {
            Assert.AreEqual(_presentationModel.IsTriangleButtonEnable(), false);
            Assert.AreEqual(_presentationModel.IsRectangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsEllipseButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsLineButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsPointerButtonEnable(), true);
            _presentationModel.ChangeDrawingType(shapes.Rectangle);
            Assert.AreEqual(_presentationModel.IsTriangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsRectangleButtonEnable(), false);
            Assert.AreEqual(_presentationModel.IsEllipseButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsLineButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsPointerButtonEnable(), true);
            _presentationModel.ChangeDrawingType(shapes.Ellipse);
            Assert.AreEqual(_presentationModel.IsTriangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsRectangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsEllipseButtonEnable(), false);
            Assert.AreEqual(_presentationModel.IsLineButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsPointerButtonEnable(), true);
            _presentationModel.ChangeDrawingType(shapes.Line);
            Assert.AreEqual(_presentationModel.IsTriangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsRectangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsEllipseButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsLineButtonEnable(), false);
            Assert.AreEqual(_presentationModel.IsPointerButtonEnable(), true);
            _presentationModel.ChangeDrawingType(shapes.Pointer);
            Assert.AreEqual(_presentationModel.IsTriangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsRectangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsEllipseButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsLineButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsPointerButtonEnable(), false);
            _presentationModel.ChangeDrawingType(shapes.Triangle);
            Assert.AreEqual(_presentationModel.IsTriangleButtonEnable(), false);
            Assert.AreEqual(_presentationModel.IsRectangleButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsEllipseButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsLineButtonEnable(), true);
            Assert.AreEqual(_presentationModel.IsPointerButtonEnable(), true);
        }

        /// <summary>
        /// 測試DeleteShape()和IsDeleteButtonEnable()
        /// </summary>
        [TestMethod]
        public void TestDeleteShape()
        {
            DrawAPictureInPresentationModel(_drawingPointTwo, _drawingPointOne);
            Assert.AreEqual(_presentationModel.IsDeleteButtonEnable(), false);
            _presentationModel.ChangeDrawingType(shapes.Pointer);
            DrawAPictureInPresentationModel(_drawingPointOne, _drawingPointTwo);
            Assert.AreEqual(_presentationModel.IsDeleteButtonEnable(), true);
            Assert.AreEqual(_model.GetShapeListSize(), 1);
            Assert.AreEqual(_model.GetSelectListSize(), 1);
            _presentationModel.DeleteShape();
            Assert.AreEqual(_model.GetShapeListSize(), 0);
            Assert.AreEqual(_model.GetSelectListSize(), 0);
            Assert.AreEqual(_presentationModel.IsDeleteButtonEnable(), false);
        }

        /// <summary>
        /// 測試IsUploadButtonEnable()
        /// </summary>
        [TestMethod]
        public void TestIsUploadButtonEnable()
        {
            Assert.AreEqual(_presentationModel.IsUploadButtonEnable("UpLoad"), false);
            DrawAPictureInPresentationModel(_drawingPointOne, _drawingPointTwo);
            Assert.AreEqual(_presentationModel.IsUploadButtonEnable("UpLoad"), true);
            Assert.AreEqual(_presentationModel.IsUploadButtonEnable("UpLoading..."), false);
        }

        /// <summary>
        /// 用presentationModel的function來畫張圖
        /// </summary>
        private void DrawAPictureInPresentationModel(DrawingPoint startPoint, DrawingPoint endPoint)
        {
            _presentationModel.PressDownOnPanel(startPoint.GetXCoordinate(), startPoint.GetYCoordinate());
            _presentationModel.MoveMouseOnPanel(endPoint.GetXCoordinate(), endPoint.GetYCoordinate());
            _presentationModel.PressUpOnPanel();
        }
    }
}
