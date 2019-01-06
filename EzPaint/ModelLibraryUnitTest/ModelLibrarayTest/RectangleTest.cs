using System;
using ModelLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class RectangleTest
    {
        const int POINT_X_ONE = 50;
        const int POINT_Y_ONE = 20;
        const int POINT_X_TWO = 34;
        const int POINT_Y_TWO = 788;
        DrawingPoint _firstPoint;
        DrawingPoint _secondPoint;
        FakeIGraphic _iGraphics;
        Rectangle _rectangle;
        PrivateObject _privateIGraphics;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _firstPoint = new DrawingPoint(POINT_X_ONE, POINT_Y_ONE);
            _secondPoint = new DrawingPoint(POINT_X_TWO, POINT_Y_TWO);
            _rectangle = new Rectangle(_firstPoint);
            _rectangle.SetSecondPoint(_secondPoint);
            _iGraphics = new FakeIGraphic();
            _privateIGraphics = new PrivateObject(_iGraphics);
        }

        /// <summary>
        /// 測試DrawFillShape()
        /// </summary>
        [TestMethod]
        public void TestDrawFillShape()
        {
            Assert.AreEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), (DrawingType)0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), (shapes)0);
            _rectangle.DrawFillShape(_iGraphics);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), DrawingType.Fill);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), shapes.Rectangle);
        }

        /// <summary>
        /// 測試DrawSketchShape()
        /// </summary>
        [TestMethod]
        public void TestDrawSketchShape()
        {
            Assert.AreEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), (DrawingType)0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), (shapes)0);
            _rectangle.DrawFillShape(_iGraphics);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), DrawingType.Fill);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), shapes.Rectangle);
        }

        /// <summary>
        /// 測試IsPointInside()
        /// </summary>
        [TestMethod]
        public void TestIsPointInside()
        {
            Assert.AreEqual(_rectangle.IsPointInside(40, 40), true);
            Assert.AreEqual(_rectangle.IsPointInside(10, 10), false);
        }
    }
}
