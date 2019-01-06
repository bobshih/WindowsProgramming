using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using System.Collections.Generic;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class DeleteCommandTest
    {
        DeleteCommand _testDeleteCommand;
        Model _testModel;
        Shape _testShape;
        PrivateObject _privateModel;
        const int POINT_ONE_X = 34;
        const int POINT_ONE_Y = 67;
        const int POINT_TWO_X = 865;
        const int POINT_TWO_Y = 3;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _testModel = new Model();
            _testShape = new Line(new DrawingPoint(POINT_ONE_X, POINT_ONE_Y));
            _testShape.SetSecondPoint(new DrawingPoint(POINT_TWO_X, POINT_TWO_Y));
            _testModel.AddShape(_testShape);
            _testModel.AddSelectRectangle(new DrawingPoint(POINT_ONE_X, POINT_ONE_Y));
            _testModel.SetSecondPointOfSelectRectangle(new DrawingPoint(POINT_TWO_X, POINT_TWO_Y));
            _testModel.ReleaseSelectRectangle();
            _privateModel = new PrivateObject(_testModel);
            _testDeleteCommand = new DeleteCommand(_testModel, (List<Shape>)_privateModel.GetFieldOrProperty("_selectedShapes"));
        }

        /// <summary>
        /// 測試Excute()
        /// </summary>
        [TestMethod]
        public void TestExcute()
        {
            Assert.AreEqual(_testModel.GetSelectListSize(), 1);
            Assert.AreEqual(_testModel.GetShapeListSize(), 1);
            _testDeleteCommand.Excute();
            Assert.AreEqual(_testModel.GetSelectListSize(), 0);
            Assert.AreEqual(_testModel.GetShapeListSize(), 0);
        }

        /// <summary>
        /// 測試UnExcute()
        /// </summary>
        [TestMethod]
        public void TestUnExcute()
        {
            _testDeleteCommand.Excute();
            Assert.AreEqual(_testModel.GetSelectListSize(), 0);
            Assert.AreEqual(_testModel.GetShapeListSize(), 0);
            _testDeleteCommand.UnExcute();
            Assert.AreEqual(_testModel.GetSelectListSize(), 0);
            Assert.AreEqual(_testModel.GetShapeListSize(), 1);
        }
    }
}
