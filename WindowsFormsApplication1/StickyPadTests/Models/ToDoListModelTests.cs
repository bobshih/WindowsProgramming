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
    public class ToDoListModelTests
    {
        const string TO_DO_CATEGORY_NAME_ONE = "Category1";
        const string TO_DO_CATEGORY_NAME_TWO = "Category2";
        const string TO_DO_CATEGORY_NAME_TEST = "Test";
        Color TO_DO_CATEGORY_COLOR_ONE = Color.FromArgb(111, 111, 111, 111);
        Color TO_DO_CATEGORY_COLOR_TWO = Color.FromArgb(222, 222, 222, 222);
        Color TO_DO_CATEGORY_COLOR_TEST = Color.FromArgb(33, 33, 33, 33);
        const string TO_DO_CONTENT_ONE = "Content1";
        const string TO_DO_CONTENT_TWO = "Content2";
        const string TO_DO_CONTENT_TEST = "Test";
        const string ERROR_TEXT = "已經有一個相同類別和相同內容的待辦事項囉，不需要重複輸入喔！";
        ToDo _toDoOne;
        ToDo _toDoTwo;
        ToDoListModel _toDoList;


        [TestInitialize()]
        [DeploymentItem("StickyPad.exe")]
        public void Initialize()        //初始化
        {
            ToDoListFormCollectionModel formCollection = new ToDoListFormCollectionModel();
            _toDoList = new ToDoListModel();
            _toDoOne = new ToDo(new Category(TO_DO_CATEGORY_NAME_ONE, TO_DO_CATEGORY_COLOR_ONE), TO_DO_CONTENT_ONE);
            _toDoTwo = new ToDo(new Category(TO_DO_CATEGORY_NAME_TWO, TO_DO_CATEGORY_COLOR_TWO), TO_DO_CONTENT_TWO);
            _toDoList.SetToDoList(_toDoOne);
            _toDoList.SetToDoList(_toDoTwo);
        }

        [TestMethod()]
        public void TestGetToDoList()       //測試回傳的List正確與否
        {
            BindingList<ToDo> testToDoList = _toDoList.GetToDoList();
            Assert.AreEqual(testToDoList, _toDoList.GetToDoList());
        }

        [TestMethod()]
        public void TestFindToDo()      //測試FindToDO的功能
        {
            Assert.IsTrue(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_ONE, TO_DO_CONTENT_ONE));
            Assert.IsTrue(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_TWO, TO_DO_CONTENT_TWO));
            Assert.IsFalse(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_TEST, TO_DO_CONTENT_TEST));       //不存在的待辦事項，回傳false
        }

        [TestMethod()]
        public void TestRemoveToDo()        //測試移除的功能
        {
            //先確定都在
            Assert.IsTrue(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_ONE, TO_DO_CONTENT_ONE));
            Assert.IsTrue(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_TWO, TO_DO_CONTENT_TWO));
            //移除
            _toDoList.RemoveToDo(TO_DO_CATEGORY_NAME_TWO, TO_DO_CONTENT_TWO);
            //確認不存在
            Assert.IsTrue(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_ONE, TO_DO_CONTENT_ONE));
            Assert.IsFalse(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_TWO, TO_DO_CONTENT_TWO));
        }

        [TestMethod()]
        public void TestEditToDo()      //測試修改的功能
        {
            Assert.AreEqual(_toDoOne._CategoryText, TO_DO_CATEGORY_NAME_ONE);
            Assert.AreEqual(_toDoTwo._Content, TO_DO_CONTENT_TWO);
            _toDoList.EditToDo(_toDoOne, new Category(TO_DO_CATEGORY_NAME_TEST, TO_DO_CATEGORY_COLOR_TEST), TO_DO_CONTENT_ONE);
            _toDoList.EditToDo(_toDoTwo, new Category(TO_DO_CATEGORY_NAME_TWO, TO_DO_CATEGORY_COLOR_TWO), TO_DO_CONTENT_TEST);
            Assert.IsFalse(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_ONE, TO_DO_CONTENT_ONE));
            Assert.IsFalse(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_TWO, TO_DO_CONTENT_TWO));
        }

        [TestMethod()]
        public void TestGetToDo()       //測試抓某一個待辦事項的功能
        {
            Assert.AreEqual(_toDoOne, _toDoList.GetToDo(TO_DO_CATEGORY_NAME_ONE, TO_DO_CONTENT_ONE));
            Assert.AreEqual(_toDoTwo, _toDoList.GetToDo(TO_DO_CATEGORY_NAME_TWO, TO_DO_CONTENT_TWO));
        }

        [TestMethod()]
        public void TestRemoveCategory()
        {
            Assert.IsTrue(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_ONE, TO_DO_CONTENT_ONE));
            Assert.IsTrue(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_TWO, TO_DO_CONTENT_TWO));
            _toDoList.RemoveCategory(TO_DO_CATEGORY_NAME_ONE);
            Assert.IsFalse(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_ONE, TO_DO_CONTENT_ONE));
            Assert.IsTrue(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_TWO, TO_DO_CONTENT_TWO));
        }

        [TestMethod()]
        public void TestSetToDoList()       //測試設定待辦事項的功能
        {
            Assert.IsFalse(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_TEST, TO_DO_CONTENT_TEST));
            ToDo testToDo = new ToDo(new Category(TO_DO_CATEGORY_NAME_TEST, TO_DO_CATEGORY_COLOR_TEST), TO_DO_CONTENT_TEST);
            _toDoList.SetToDoList(testToDo);
            Assert.IsTrue(_toDoList.FindToDo(TO_DO_CATEGORY_NAME_TEST, TO_DO_CONTENT_TEST));
            Assert.AreEqual(ERROR_TEXT, _toDoList.SetToDoList(_toDoOne));
            
        }
    }
}
