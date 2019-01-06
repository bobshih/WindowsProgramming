using System;
using ModelLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class TriangleTest
    {
        const int POINT_X_ONE = 50;
        const int POINT_Y_ONE = 20;
        const int POINT_X_TWO = 432;
        const int POINT_Y_TWO = 12;
        DrawingPoint _firstPoint;
        DrawingPoint _secondPoint;
        Triangle _triangle;
        FakeIGraphic _fakeIGraphic;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _firstPoint = new DrawingPoint(POINT_X_ONE, POINT_Y_ONE);
            _secondPoint = new DrawingPoint(POINT_X_TWO, POINT_Y_TWO);
            _triangle = new Triangle(_firstPoint);
            _triangle.SetSecondPoint(_secondPoint);
            _fakeIGraphic = new FakeIGraphic();
        }

        /// <summary>
        /// 測試DrawShape()
        /// </summary>
        [TestMethod]
        public void TestDrawShape()
        {
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_fakeIGraphic.GetTrianglePointOne(), new DrawingPoint(0, 0)), true);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_fakeIGraphic.GetTrianglePointTwo(), new DrawingPoint(0, 0)), true);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_fakeIGraphic.GetTrianglePointThree(), new DrawingPoint(0, 0)), true);
            _triangle.DrawFillShape(_fakeIGraphic);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_fakeIGraphic.GetTrianglePointOne(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_fakeIGraphic.GetTrianglePointTwo(), new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(_fakeIGraphic.GetTrianglePointThree(), new DrawingPoint(0, 0)), false);
        }

        /// <summary>
        /// 測試CalculateTrianglePoint()
        /// </summary>
        [TestMethod]
        public void TestCalculateTrianglePoint()
        {
            DrawingPoint[] point = new DrawingPoint[3];
            point[0] = new DrawingPoint(0, 0);
            point[1] = new DrawingPoint(0, 0);
            point[2] = new DrawingPoint(0, 0);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(point[0], new DrawingPoint(0, 0)), true);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(point[1], new DrawingPoint(0, 0)), true);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(point[2], new DrawingPoint(0, 0)), true);
            _triangle.CalculateTriangelPoint(point);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(point[0], new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(point[1], new DrawingPoint(0, 0)), false);
            Assert.AreEqual(FakeIGraphic.CompareDrawingPoint(point[2], new DrawingPoint(0, 0)), false);
        }

        /// <summary>
        /// 測試IsPointInside()
        /// </summary>
        [TestMethod]
        public void TestIsPointInside()
        {
            Assert.AreEqual(_triangle.IsPointInside(241, 16), true);
            Assert.AreEqual(_triangle.IsPointInside(0, 0), false);
            //第二部分測資
            _firstPoint = new DrawingPoint(POINT_X_ONE, POINT_Y_TWO);
            _secondPoint = new DrawingPoint(POINT_X_TWO, POINT_Y_ONE);
            _triangle.SetFirstPoint(_firstPoint);
            _triangle.SetSecondPoint(_secondPoint);
            Assert.AreEqual(_triangle.IsPointInside(241, 16), true);
        }
    }
}
