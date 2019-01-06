using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class ShapeFactoryTest
    {
        const int POINT_X = 123;
        const int POINT_Y = 67;
        DrawingPoint _point;
        Factory _factory;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _point = new DrawingPoint(POINT_X, POINT_Y);
            _factory = new Factory();
        }

        /// <summary>
        /// 測試GetShape()
        /// </summary>
        [TestMethod]
        public void TestGetShape()
        {
            Shape tempShape = _factory.GetShape(shapes.Triangle, _point);
            Assert.AreEqual(tempShape.GetType(), typeof(Triangle));
            tempShape = _factory.GetShape(shapes.Rectangle, _point);
            Assert.AreEqual(tempShape.GetType(), typeof(Rectangle));
            tempShape = _factory.GetShape(shapes.Ellipse, _point);
            Assert.AreEqual(tempShape.GetType(), typeof(Ellipse));
            tempShape = _factory.GetShape(shapes.Line, _point);
            Assert.AreEqual(tempShape.GetType(), typeof(Line));
            tempShape = _factory.GetShape((shapes)123, _point);
            Assert.AreEqual(tempShape, null);
        }
    }
}
