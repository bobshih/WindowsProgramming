using System;
using ModelLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class DrawingPointTest
    {
        const int CENTER_X = 40;
        const int CENTER_Y = 50;
        DrawingPoint _testDrawingPoint;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _testDrawingPoint = new DrawingPoint(CENTER_X, CENTER_Y);
        }

        /// <summary>
        /// 測試GetXCoordinate()
        /// </summary>
        [TestMethod]
        public void TestGetXCoordinate()
        {
            Assert.AreEqual(_testDrawingPoint.GetXCoordinate(), CENTER_X);
        }

        /// <summary>
        /// 測試GetYCoordinate()
        /// </summary>
        [TestMethod]
        public void TestGetYcoordinate()
        {
            Assert.AreEqual(_testDrawingPoint.GetYCoordinate(), CENTER_Y);
        }
    }
}
