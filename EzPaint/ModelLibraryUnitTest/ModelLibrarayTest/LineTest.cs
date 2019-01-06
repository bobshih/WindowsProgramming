using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class LineTest
    {
        const int POINT_ONE_X = 566;
        const int POINT_ONE_Y = 42;
        const int POINT_TWO_X = 98;
        const int POINT_TWO_Y = 2;
        DrawingPoint _drawingPointOne;
        DrawingPoint _drawingPointTwo;
        Line _line;
        FakeIGraphic _iGraphics;
        PrivateObject _privateIGraphics;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _drawingPointOne = new DrawingPoint(POINT_ONE_X, POINT_ONE_Y);
            _drawingPointTwo = new DrawingPoint(POINT_TWO_X, POINT_TWO_Y);
            _line = new Line(_drawingPointOne);
            _line.SetSecondPoint(_drawingPointTwo);
            _iGraphics = new FakeIGraphic();
            _privateIGraphics = new PrivateObject(_iGraphics);
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
            _line.DrawSketchShape(_iGraphics);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            //因為Line沒有所謂外框的部分，所以不論是DrawSketchShape或是DrawFillShape都是用一樣的Function
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), DrawingType.Fill);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), shapes.Line);
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
            _line.DrawFillShape(_iGraphics);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), DrawingType.Fill);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), shapes.Line);
        }

        /// <summary>
        /// 測試IsPointInside()
        /// </summary>
        [TestMethod]
        public void TestIsPointInside()
        {
            Assert.AreEqual(_line.IsPointInside(40, 1), false);
            Assert.AreEqual(_line.IsPointInside(600, 55), false);
            Assert.AreEqual(_line.IsPointInside(100, 32), false);
            Assert.AreEqual(_line.IsPointInside(103, 40), false);
            Assert.AreEqual(_line.IsPointInside(332, 22), true);
            Assert.AreEqual(_line.IsPointInside(103, 330), false);
        }
    }
}
