using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyPadForm;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StickyPadTests;
using StickyPadForm.Model;

namespace StickyPadForm.Tests
{
    [TestClass()]
    public class ToDoListFormCollectionModelTests
    {
        const string FORM_CATEGORY_NAME_ONE = "Category1";
        const string FORM_CATEGORY_NAME_TWO = "Category2";
        const string FORM_CATEGORY_NAME_TEST = "Test";
        Color FORM_CATEGORY_COLOR_ONE = Color.FromArgb(255, 0, 0, 160);
        Color FORM_CATEGORY_COLOR_TWO = Color.FromArgb(255, 0, 255, 64);
        Color FORM_CATEGORY_COLOR_TEST = Color.FromArgb(255, 255, 0, 0);
        const string FORM_CONTENT_ONE = "Content1";
        const string FORM_CONTENT_TWO = "Content2";
        const string FORM_CONTENT_TEST = "Test";
        ToDoListWindows _formOne;
        ToDoListWindows _formTwo;
        ToDoListWindows _formTest;
        ToDoListFormCollectionModel _formCollection;

        [TestInitialize]
        public void Initialize()        //初始化
        {
            _formCollection = new ToDoListFormCollectionModel();
            _formOne = new ToDoListWindows(new ToDo(new Category(FORM_CATEGORY_NAME_ONE, FORM_CATEGORY_COLOR_ONE), FORM_CONTENT_ONE));
            _formTwo = new ToDoListWindows(new ToDo(new Category(FORM_CATEGORY_NAME_TWO, FORM_CATEGORY_COLOR_TWO), FORM_CONTENT_TWO));
            _formCollection.Add(_formOne);
            _formCollection.Add(_formTwo);
            _formTest = new ToDoListWindows(new ToDo(new Category(FORM_CATEGORY_NAME_TEST, FORM_CATEGORY_COLOR_TEST), FORM_CONTENT_TEST));
        }

        [TestMethod()]
        public void TestAdd()       //測試Add的功能
        {
            Assert.AreEqual(_formCollection._Count, 2);
            Assert.IsNull(_formCollection.FindToDoListWindows(_formTest));
            _formCollection.Add(_formTest);
            Assert.AreEqual(_formCollection._Count, 3);
            Assert.IsNotNull(_formCollection.FindToDoListWindows(_formTest));
            //加入已經有的視窗
            _formCollection.Add(_formOne);
            Assert.AreEqual(_formCollection._Count, 3);
        }

        [TestMethod()]
        public void TestCount()     //檢查count的回傳值正確嗎?
        {
            Assert.AreEqual(_formCollection._Count, 2);
            _formCollection.Add(_formTest);
            Assert.AreEqual(_formCollection._Count, 3);
            _formCollection.Remove(_formOne);
            _formCollection.Remove(_formTest);
            Assert.AreEqual(_formCollection._Count, 1);
        }

        [TestMethod()]
        public void TestFindToDoListWindows()       //測試尋找的功能
        {
            Assert.AreEqual(_formOne, _formCollection.FindToDoListWindows(_formOne));
            Assert.AreEqual(_formTwo, _formCollection.FindToDoListWindows(_formTwo));
            Assert.IsNull(_formCollection.FindToDoListWindows(_formTest));
        }

        [TestMethod()]
        public void TestRemove()        //測試移除的功能
        {
            Assert.AreEqual(_formOne, _formCollection.FindToDoListWindows(_formOne));
            Assert.AreEqual(_formCollection._Count, 2);
            _formCollection.Remove(_formOne);
            Assert.IsNull(_formCollection.FindToDoListWindows(_formOne));
            Assert.AreEqual(_formCollection._Count, 1);
        }

        [TestMethod()]
        public void TestWriteData()
        {
            string FAKE_PATH = "fake_path";
            SubStreamWriter formWriter = new SubStreamWriter(FAKE_PATH);
            Assert.AreEqual(string.Empty, formWriter._Writer);
            _formCollection.WriteData(formWriter);
            //檢查有沒有資料寫進去
            Assert.AreNotEqual(string.Empty, formWriter._Writer);
            formWriter.Close();
        }

        [TestMethod()]
        public void TestDeleteToDoForm()
        {
            Assert.AreEqual(_formCollection._Count, 2);
            _formCollection.DeleteToDoForm(_formOne);
            Assert.AreEqual(_formCollection._Count, 1);
            Assert.IsNull(_formCollection.FindToDoListWindows(_formOne));
            //另外移除List中沒有的東西
            _formCollection.DeleteToDoForm(_formTest);
            Assert.IsNull(_formCollection.FindToDoListWindows(_formTest));
            Assert.AreEqual(_formCollection._Count, 1);
        }

        [TestMethod()]
        public void TestDeleteCategory()
        {
            Assert.AreEqual(_formCollection._Count, 2);
            _formCollection.DeleteCategory(FORM_CATEGORY_NAME_TWO);
            Assert.AreEqual(_formCollection._Count, 1);     //數量少了一個
            //移除不存在的類別
            _formCollection.DeleteCategory(FORM_CATEGORY_NAME_TEST);
            Assert.AreEqual(_formCollection._Count, 1);     //數量沒有改變
        }
    }
}
