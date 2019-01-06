using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class ClearCommandTest
    {
        ClearCommand _testClearCommand;
        Model _model;
        Shape _inputShape;
        const int CENTER_X_ONE = 40;
        const int CENTER_Y_ONE = 43;
        const int CENTER_X_TWO = 80;
        const int CENTER_Y_TWO = 94;
        DrawingPoint _drawingPointOne;
        DrawingPoint _drawingPointTwo;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _model = new Model();
            _drawingPointOne = new DrawingPoint(CENTER_X_ONE, CENTER_Y_ONE);
            _inputShape = new Ellipse(_drawingPointOne);
            _drawingPointTwo = new DrawingPoint(CENTER_X_TWO, CENTER_Y_TWO);
            _inputShape.SetSecondPoint(_drawingPointTwo);
            _testClearCommand = new ClearCommand(_model);
        }

        /// <summary>
        /// 測試Excute()
        /// </summary>
        [TestMethod]
        public void TestExcute()
        {
            _model.AddShape(_inputShape);
            Assert.AreEqual(_model.GetShapeListSize(), 1);
            _testClearCommand.Excute();
            Assert.AreEqual(_model.GetShapeListSize(), 0);
        }

        /// <summary>
        /// 測試Unexcute()
        /// </summary>
        [TestMethod]
        public void TestUnexcute()
        {
            _model.AddShape(_inputShape);
            _testClearCommand.Excute();
            Assert.AreEqual(_model.GetShapeListSize(), 0);
            _testClearCommand.UnExcute();
            Assert.AreEqual(_model.GetShapeListSize(), 1);
        }
    }
}
