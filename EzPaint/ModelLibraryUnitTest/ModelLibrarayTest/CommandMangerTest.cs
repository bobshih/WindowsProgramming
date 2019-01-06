using System;
using ModelLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelLibraryUnitTest.ModelLibrarayTest
{
    [TestClass]
    public class CommandMangerTest
    {
        Model _testModel;
        Triangle _testTriagle;
        Rectangle _testRectangle;
        DrawingCommand _testCommandOne;
        DrawingCommand _testCommandTwo;
        const int CENTER_X_ONE = 30;
        const int CENTER_Y_ONE = 30;
        const int CENTER_X_TWO = 40;
        const int CENTER_Y_TWO = 10;
        CommandManger _testCommandManger;
        PrivateObject _commamdMangerObject;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _testModel = new Model();
            _testTriagle = new Triangle(new DrawingPoint(CENTER_X_ONE, CENTER_Y_ONE));
            _testRectangle = new Rectangle(new DrawingPoint(CENTER_X_TWO, CENTER_Y_TWO));
            _testCommandOne = new DrawingCommand(_testModel, _testTriagle);
            _testCommandTwo = new DrawingCommand(_testModel, _testRectangle);
            _testCommandManger = new CommandManger();
            _commamdMangerObject = new PrivateObject(_testCommandManger);
        }

        /// <summary>
        /// 測試PushCommand()
        /// </summary>
        [TestMethod]
        public void TestPushCommand()
        {
            //一開始List是空的，所以按鈕也是unabled的
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, false);
            _testCommandManger.PushCommand(_testCommandOne);
            //PushCommand之後，按鈕被啟動了
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, true);
            //再初始化一次，再來測試CommandTwo
            Initialize();
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, false);
            _testCommandManger.PushCommand(_testCommandTwo);
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, true);
        }

        /// <summary>
        /// 測試ReDo的例外
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestsUnDoException()
        {
            Assert.AreEqual(_testCommandManger._IsReDoButtonEnable, false);
            //因為會丟出例外，所以不用assert
            _testCommandManger.UnDo();
        }

        /// <summary>
        /// 測試UnDo
        /// </summary>
        [TestMethod]
        public void TestUnDo()
        {
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, false);
            Assert.AreEqual(_testCommandManger._IsReDoButtonEnable, false);
            _testCommandManger.PushCommand(_testCommandOne);
            Assert.AreEqual(_testCommandManger._IsReDoButtonEnable, false);
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, true);
            _testCommandManger.UnDo();
            Assert.AreEqual(_testCommandManger._IsReDoButtonEnable, true);
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, false);
        }

        /// <summary>
        /// 測試ReDo的例外
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestsReDoException()
        {
            Assert.AreEqual(_testCommandManger._IsReDoButtonEnable, false);
            //因為會丟出例外，所以不用assert
            _testCommandManger.ReDo();
        }

        /// <summary>
        /// 測試ReDo()
        /// </summary>
        [TestMethod]
        public void TestReDo()
        {
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, false);
            Assert.AreEqual(_testCommandManger._IsReDoButtonEnable, false);
            _testCommandManger.PushCommand(_testCommandTwo);
            Assert.AreEqual(_testCommandManger._IsReDoButtonEnable, false);
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, true);
            _testCommandManger.UnDo();
            Assert.AreEqual(_testCommandManger._IsReDoButtonEnable, true);
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, false);
            _testCommandManger.ReDo();
            Assert.AreEqual(_testCommandManger._IsReDoButtonEnable, false);
            Assert.AreEqual(_testCommandManger._IsUnDoButtonEnable, true);
        }
    }
}
