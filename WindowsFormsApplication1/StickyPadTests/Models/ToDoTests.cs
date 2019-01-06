using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyPadForm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.ComponentModel;
using StickyPadForm.Model;

namespace StickyPadForm.Tests
{
    [TestClass()]
    public class ToDoTests
    {
        const string TO_DO_CATEGORRY_NAME = "CategoryName";
        Color TO_DO_COLOR = Color.FromArgb(123, 123, 123, 123);
        const string TO_DO_CONTENT = "Content";
        const string TEST_TO_DO_CATEGORY_NAME = "TestCategoryName";
        Color TEST_TO_DO_COLOR = Color.FromArgb(222, 222, 222, 222);
        const string TEST_TO_DO_CONTENT = "TestContent";
        ToDo _toDo;

        [TestInitialize()]
        [DeploymentItem("StickyPad.exe")]
        public void Initialize()        //初始化
        {
            _toDo = new ToDo(new Category(TO_DO_CATEGORRY_NAME, TO_DO_COLOR), TO_DO_CONTENT);
        }

        [TestMethod()]
        public void TestCategory()      //測試待辦是像的類別和他能不能正常呼叫事件
        {
            string testString = string.Empty;
            const string TO_DO_CHANGED = "To_Do_Changed";
            _toDo._toDoChanged += delegate()
            {
                testString = TO_DO_CHANGED;
            };
            _toDo._Category = new Category(TEST_TO_DO_CATEGORY_NAME, TEST_TO_DO_COLOR);
            Assert.AreNotEqual(testString, string.Empty);
            Assert.AreNotEqual(_toDo._Category, TO_DO_CATEGORRY_NAME);
        }

        [TestMethod()]
        public void NotifyPropertyChangedTest()     //測試Notify的功能和Content的回傳
        {
            string testString = string.Empty;
            _toDo.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                testString = e.PropertyName;
            };
            //測試content的回傳
            Assert.AreSame(_toDo._Content, TO_DO_CONTENT);
            //測試類別改變之後
            Assert.AreEqual(testString, string.Empty);
            _toDo._Category = new Category(TEST_TO_DO_CATEGORY_NAME, TEST_TO_DO_COLOR);
            Assert.AreNotEqual(testString, string.Empty);
            //測試內容改變之後
            testString = string.Empty;
            _toDo._Content = TEST_TO_DO_CONTENT;
            Assert.AreNotEqual(testString, string.Empty);
        }

        [TestMethod()]
        public void TestCategoryColor()     //測試類別顏色地回傳
        {
            Assert.AreEqual(_toDo._CategoryColor, TO_DO_COLOR);
        }

        [TestMethod()]
        public void TestCategoryText()      //測試類別名稱的回傳
        {
            Assert.AreEqual(_toDo._CategoryText, TO_DO_CATEGORRY_NAME);
        }
    }
}
