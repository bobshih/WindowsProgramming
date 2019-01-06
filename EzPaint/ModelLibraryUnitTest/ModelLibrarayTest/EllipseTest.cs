using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class EllipseTest
    {
        const int POINT_ONE_X = 12;
        const int POINT_ONE_Y = 43;
        const int POINT_TWO_X = 885;
        const int POINT_TWO_Y = 21;
        DrawingPoint _drawingPointOne;
        DrawingPoint _drawingPointTwo;
        Ellipse _ellipse;
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
            _ellipse = new Ellipse(_drawingPointOne);
            _ellipse.SetSecondPoint(_drawingPointTwo);
            _iGraphics = new FakeIGraphic();
            _privateIGraphics = new PrivateObject(_iGraphics);
        }

        /// <summary>
        /// 測試DrawFillEllipse()
        /// </summary>
        [TestMethod]
        public void TestDrawFillEllipse()
        {
            Assert.AreEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), (DrawingType)0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), (shapes)0);
            _ellipse.DrawFillShape(_iGraphics);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), DrawingType.Fill);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), shapes.Ellipse);
        }

        /// <summary>
        /// 測試DrawSketchEllipse()
        /// </summary>
        [TestMethod]
        public void TestDrawSketchEllipse()
        {
            Assert.AreEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), (DrawingType)0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), (shapes)0);
            _ellipse.DrawSketchShape(_iGraphics);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointX(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleFirstPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointY(), 0);
            Assert.AreNotEqual(_iGraphics.GetRectangleSecondPointX(), 0);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawType"), DrawingType.Sketch);
            Assert.AreEqual(_privateIGraphics.GetFieldOrProperty("_drawingShape"), shapes.Ellipse);
        }

        /// <summary>
        /// 測試IsPointInside()
        /// </summary>
        [TestMethod]
        public void TestIsPointInside()
        {
            Assert.AreEqual(_ellipse.IsPointInside(499, 26), true);
            Assert.AreEqual(_ellipse.IsPointInside(12, 12), false);
        }
    }
}
