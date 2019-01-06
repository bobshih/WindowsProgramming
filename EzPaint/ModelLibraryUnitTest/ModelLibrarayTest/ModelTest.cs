using System;
using ModelLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class ModelTest
    {
        const int CENTER_X_ONE = 40;
        const int CENTER_Y_ONE = 43;
        const int CENTER_X_TWO = 80;
        const int CENTER_Y_TWO = 94;
        DrawingPoint _drawingPointOne;
        DrawingPoint _drawingPointTwo;
        Triangle _triangle;
        Rectangle _rectangle;
        Model _model;
        PrivateObject _privateModel;
        FakeIGraphic _iGraphic;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _drawingPointOne = new DrawingPoint(CENTER_X_ONE, CENTER_Y_ONE);
            _drawingPointTwo = new DrawingPoint(CENTER_X_TWO, CENTER_Y_TWO);
            _triangle = new Triangle(_drawingPointOne);
            _triangle.SetSecondPoint(_drawingPointTwo);
            _rectangle = new Rectangle(_drawingPointTwo);
            _rectangle.SetSecondPoint(_drawingPointOne);
            _model = new Model();
            _privateModel = new PrivateObject(_model);
            _iGraphic = new FakeIGraphic();
        }

        /// <summary>
        /// 測試GetShapeListSize()
        /// </summary>
        [TestMethod]
        public void TestGetShapeListSize()
        {
            Assert.AreEqual(_model.GetShapeListSize(), 0);
            DrawAPicture(shapes.Rectangle, _drawingPointTwo, _drawingPointOne);
            Assert.AreEqual(_model.GetShapeListSize(), 1);
        }

        /// <summary>
        /// 測試ReleaseSelectRectangle()
        /// </summary>
        [TestMethod]
        public void TestReleaseSelectRectangle()
        {
            DrawAPicture(shapes.Line, _drawingPointOne, _drawingPointTwo);
            DrawAPicture(shapes.Rectangle, _drawingPointTwo, _drawingPointOne);
            _model.AddSelectRectangle(new DrawingPoint(0, 0));
            _model.SetSecondPointOfSelectRectangle(new DrawingPoint(1, 33));
            _model.ReleaseSelectRectangle();
            List<Shape> selectedList = (List<Shape>)_privateModel.GetFieldOrProperty("_selectedShapes");
            Assert.AreEqual(selectedList.Count, 0);
            selectedList = (List<Shape>)_privateModel.GetFieldOrProperty("_selectedShapes");
            Assert.AreEqual(selectedList.Count, 0);
            _model.AddSelectRectangle(_drawingPointOne);
            _model.SetSecondPointOfSelectRectangle(_drawingPointTwo);
            _model.ReleaseSelectRectangle();
            selectedList = (List<Shape>)_privateModel.GetFieldOrProperty("_selectedShapes");
            Assert.AreEqual(selectedList.Count, 2);
        }

        /// <summary>
        /// 測試GetHint()
        /// </summary>
        [TestMethod]
        public void TestGetHint()
        {
            Assert.AreEqual(_model.GetHint(), null);
            DrawAPicture(shapes.Triangle, _drawingPointOne, _drawingPointTwo);
            Assert.AreEqual(_model.GetHint().GetType(), typeof(Triangle));
        }

        /// <summary>
        /// 測試IsUnDoButtonEnable()
        /// </summary>
        [TestMethod]
        public void TestIsUnDButtonEnable()
        {
            Assert.AreEqual(_model.IsUnDoButtonEnable(), false);
            DrawAPicture(shapes.Triangle, _drawingPointTwo, _drawingPointOne);
            Assert.AreEqual(_model.IsUnDoButtonEnable(), true);
        }

        /// <summary>
        /// 測試IsReDoButtonEnable()
        /// </summary>
        [TestMethod]
        public void TestIsReDoButtonEnable()
        {
            Assert.AreEqual(_model.IsReDoButtonEnable(), false);
            DrawAPicture(shapes.Rectangle, _drawingPointOne, _drawingPointTwo);
            _model.ExcuteUnDo();
            Assert.AreEqual(_model.IsReDoButtonEnable(), true);
        }

        /// <summary>
        /// 測試PressDown()
        /// </summary>
        [TestMethod]
        public void TestPressDown()
        {
            Assert.AreEqual(_model.GetHint(), null);
            _model.PressDown(shapes.Triangle, _drawingPointOne);
            Assert.AreNotEqual(_model.GetHint(), null);
            Initialize();           //在初始化一次
            Assert.AreEqual(_model.GetHint(), null);
            _model.PressDown(shapes.Rectangle, _drawingPointOne);
            Assert.AreNotEqual(_model.GetHint(), null);
        }

        /// <summary>
        /// 測試MoveMouse()
        /// </summary>
        [TestMethod]
        public void TestMoveMouse()
        {
            _model.PressDown(shapes.Triangle, _drawingPointOne);
            DrawingPoint tempPoint = _model.GetHint().GetSecondPoint();
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(tempPoint, _drawingPointOne), true);
            _model.MoveMouse(_drawingPointTwo);
            tempPoint = _model.GetHint().GetSecondPoint();
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(tempPoint, _drawingPointTwo), true);
        }

        /// <summary>
        /// 測試PressUp()
        /// </summary>
        [TestMethod]
        public void TestPressUp()
        {
            _model.PressDown(shapes.Triangle, _drawingPointTwo);
            Assert.AreEqual(_model.IsUnDoButtonEnable(), false);
            _model.PressUp();
            Assert.AreEqual(_model.IsUnDoButtonEnable(), true);
        }

        /// <summary>
        /// 測試DeleteShape()
        /// </summary>
        [TestMethod]
        public void TestDeleteShape()
        {
            DrawAPicture(shapes.Triangle, _drawingPointTwo, _drawingPointTwo);
            //圖形List中存有一筆資料
            Assert.AreEqual(_model.GetShapeListSize(), 1);
            //刪除List中的最後一個元素，List大小會變成0
            _model.DeleteShape();
            Assert.AreEqual(_model.GetShapeListSize(), 0);
        }

        /// <summary>
        /// 測試AddShape()
        /// </summary>
        [TestMethod]
        public void TestAddShape()
        {
            Assert.AreEqual(_model.GetShapeListSize(), 0);
            _model.AddShape(_triangle);
            Assert.AreEqual(_model.GetShapeListSize(), 1);
            _model.AddShape(_rectangle);
            Assert.AreEqual(_model.GetShapeListSize(), 2);
        }

        /// <summary>
        /// 測試ExcuteUnDo()
        /// </summary>
        [TestMethod]
        public void TestExcuteUnDo()
        {
            Assert.AreEqual(_model.IsUnDoButtonEnable(), false);
            DrawAPicture(shapes.Rectangle, _drawingPointTwo, _drawingPointOne);
            Assert.AreEqual(_model.IsUnDoButtonEnable(), true);
            _model.ExcuteUnDo();
            //因為執行undo了，所以button被disable
            Assert.AreEqual(_model.IsUnDoButtonEnable(), false);
        }

        /// <summary>
        /// 測試ExcuteReDo()
        /// </summary>
        [TestMethod]
        public void TestExcuteReDo()
        {
            DrawAPicture(shapes.Triangle, _drawingPointOne, _drawingPointTwo);
            Assert.AreEqual(_model.IsReDoButtonEnable(), false);
            //執行UnDo，會讓ReDo按鈕Enable
            _model.ExcuteUnDo();
            Assert.AreEqual(_model.IsReDoButtonEnable(), true);
            //執行ReDo之後，因為沒有多的可以執行，所以被disable
            _model.ExcuteReDo();
            Assert.AreEqual(_model.IsReDoButtonEnable(), false);
        }

        /// <summary>
        /// 測試OnPaint()
        /// </summary>
        [TestMethod]
        public void TestOnPaint()
        {
            Assert.AreEqual(_iGraphic.GetRectangleFirstPointX(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleFirstPointY(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleSecondPointY(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointOne(), new DrawingPoint(0, 0)), true);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointTwo(), new DrawingPoint(0, 0)), true);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointThree(), new DrawingPoint(0, 0)), true);
            _model.AddShape(_triangle);
            _model.DrawPanel(_iGraphic);
            Assert.AreEqual(_iGraphic.GetRectangleFirstPointX(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleFirstPointY(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleSecondPointY(), 0);
            Assert.AreEqual(_iGraphic.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointOne(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointTwo(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointThree(), new DrawingPoint(0, 0)), false);
            _model.AddShape(_rectangle);
            _model.DrawPanel(_iGraphic);
            Assert.AreNotEqual(_iGraphic.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointOne(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointTwo(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_iGraphic.GetTrianglePointThree(), new DrawingPoint(0, 0)), false);
            //測試pointer模式下
            _model.AddSelectRectangle(_drawingPointTwo);
            _model.SetSecondPointOfSelectRectangle(_drawingPointOne);
            _model.DrawPanel(_iGraphic);
            Assert.AreNotEqual(_iGraphic.GetDrawingType(), DrawingType.Dash);
            Assert.AreNotEqual(_iGraphic.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleSecondPointX(), 0);
            _model.ReleaseSelectRectangle();
            _model.DrawPanel(_iGraphic);
            Assert.AreNotEqual(_iGraphic.GetDrawingType(), DrawingType.Dash);
            Assert.AreNotEqual(_iGraphic.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphic.GetRectangleSecondPointX(), 0);
        }

        /// <summary>
        /// 測試DeleteAll()
        /// </summary>
        [TestMethod]
        public void TestDeleteAll()
        {
            DrawAPicture(shapes.Line, _drawingPointOne, _drawingPointTwo);
            Assert.AreEqual(_model.GetShapeListSize(), 1);
            _model.DeleteAll();
            Assert.AreEqual(_model.GetShapeListSize(), 0);
        }

        /// <summary>
        /// 測試ExcuteClearCommand()
        /// </summary>
        [TestMethod]
        public void TestExcuteClearCommand()
        {
            DrawAPicture(shapes.Triangle, _drawingPointOne, _drawingPointTwo);
            Assert.AreEqual(_model.GetShapeListSize(), 1);
            _model.ExcuteClearCommand();
            Assert.AreEqual(_model.GetShapeListSize(), 0);
        }

        /// <summary>
        /// 測試GetAllShapes()
        /// </summary>
        [TestMethod]
        public void TestGetAllShapes()
        {
            DrawAPicture(shapes.Rectangle, _drawingPointTwo, _drawingPointOne);
            List<Shape> tempList = new List<Shape>();
            Assert.AreEqual(tempList.Count, 0);
            tempList = _model.GetAllShapes();
            Assert.AreEqual(tempList.Count, 1);
            Assert.AreEqual(tempList[0].GetType(), typeof(Rectangle));
        }

        /// <summary>
        /// 測試AddSomeShape的例外
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddSomeShapeException()
        {
            List<Shape> addList = new List<Shape>();
            List<int> indexList = new List<int>();
            indexList.Add(1);
            _model.AddSomeShape(addList, indexList);
        }

        [TestMethod]
        public void TestExcuteDeleteCommand()
        {
            DrawAPicture(shapes.Rectangle, _drawingPointOne, _drawingPointTwo);
            _model.AddSelectRectangle(_drawingPointTwo);
            _model.SetSecondPointOfSelectRectangle(_drawingPointOne);
            _model.ReleaseSelectRectangle();
            Assert.AreEqual(_model.GetSelectListSize(), 1);
            _model.ExcuteDeleteCommand();
            Assert.AreEqual(_model.GetSelectListSize(), 0);
        }

        /// <summary>
        /// 畫一張圖在畫布上
        /// </summary>
        private void DrawAPicture(shapes drawingShape, DrawingPoint startPoint, DrawingPoint endPoint)
        {
            //完成一次的繪圖
            _model.PressDown(drawingShape, startPoint);
            _model.MoveMouse(endPoint);
            _model.PressUp();
        }
    }
}
