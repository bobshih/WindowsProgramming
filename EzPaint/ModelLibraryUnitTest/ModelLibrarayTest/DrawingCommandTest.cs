using System;
using ModelLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class DrawingCommandTest
    {
        DrawingCommand _testDrawingCommand;
        Model _testModel;
        Shape _testShape;
        const int CENTER_X = 30;
        const int CENTER_Y = 23;
        const string SHAPE_LIST_NAME = "_shapeList";

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _testModel = new Model();
            _testShape = new Triangle(new DrawingPoint(CENTER_X, CENTER_Y));
            _testDrawingCommand = new DrawingCommand(_testModel, _testShape);
        }

        /// <summary>
        /// 測試Excute()
        /// </summary>
        [TestMethod]
        public void TestExcute()
        {
            Assert.AreEqual(_testModel.GetShapeListSize(), 0);
            _testDrawingCommand.Excute();
            Assert.AreEqual(_testModel.GetShapeListSize(), 1);
        }

        /// <summary>
        /// 測試UnExcute()
        /// </summary>
        [TestMethod]
        public void TestUnExcute()
        {
            Assert.AreEqual(_testModel.GetShapeListSize(), 0);
            _testDrawingCommand.Excute();
            Assert.AreEqual(_testModel.GetShapeListSize(), 1);
            _testDrawingCommand.UnExcute();
            Assert.AreEqual(_testModel.GetShapeListSize(), 0);
        }
    }
}
