using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyPadForm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using StickyPadTests;
using StickyPadForm.Model;

namespace StickyPadForm.Tests
{
    [TestClass()]
    public class StickyPadPresentionModelTests
    {
        const string CATEGORY_NAME_ONE = "Category1";
        const string CATEGORY_NAME_TWO = "Category2";
        const string CATEGORY_NAME_TEST = "Test";
        Color CATEGORY_COLOR_ONE = Color.FromArgb(255, 0, 0, 160);
        Color CATEGORY_COLOR_TWO = Color.FromArgb(255, 0, 255, 64);
        Color CATEGORY_COLOR_TEST = Color.FromArgb(255, 255, 0, 0);
        const string CONTENT_ONE = "Content1";
        const string CONTENT_TWO = "Content2";
        const string CONTENT_TEST = "Test";
        const int CATEGORY_ID_ONE = 0;
        const int CATEGORY_ID_TWO = 1;
        const int CATEGORY_ID_TEST = 2;
        Category _categoryOne;
        Category _categoryTwo;
        Category _categoryTest;
        ToDo _toDoOne;
        ToDo _toDoTwo;
        ToDo _toDoTest;
        ToDoListWindows _toDoFormOne;
        ToDoListWindows _toDoFormTwo;
        ToDoListWindows _toDoFormTest;
        StickyPadPresentionModel _presentionModel;
        PrivateObject _target;

        [TestInitialize()]
        public void Initialize()        //初始化
        {
            _presentionModel = new StickyPadPresentionModel();
            _categoryOne = new Category(CATEGORY_NAME_ONE, CATEGORY_COLOR_ONE);
            _categoryTwo = new Category(CATEGORY_NAME_TWO, CATEGORY_COLOR_TWO);
            _categoryTest = new Category(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST);
            _toDoOne = new ToDo(_categoryOne, CONTENT_ONE);
            _toDoTwo = new ToDo(_categoryTwo, CONTENT_TWO);
            _toDoTest = new ToDo(_categoryTest, CONTENT_TEST);
            _toDoFormOne = new ToDoListWindows(_toDoOne);
            _toDoFormTwo = new ToDoListWindows(_toDoTwo);
            _toDoFormTest = new ToDoListWindows(_toDoTest);
            _target = new PrivateObject(_presentionModel);
            //加入資料
            _presentionModel.SetCategoryList(CATEGORY_NAME_ONE, CATEGORY_COLOR_ONE);
            _presentionModel.SetCategoryList(CATEGORY_NAME_TWO, CATEGORY_COLOR_TWO);
            _presentionModel.SetToDo(DialogResult.OK, CATEGORY_NAME_ONE, CONTENT_ONE);
            _presentionModel.SetToDo(DialogResult.OK, CATEGORY_NAME_TWO, CONTENT_TWO);
            _presentionModel.AddToDoListForm(_toDoFormOne);
            _presentionModel.AddToDoListForm(_toDoFormTwo);
        }

        [TestMethod()]
        public void TestCheckRepeat()       //檢查類別名稱的重覆
        {
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_NAME_ONE), "類別名稱重複了喔~~");
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_NAME_TWO), "類別名稱重複了喔~~");
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_NAME_TEST), "");
            //修改模式下
            const int EDIT_COLUMN_INDEX = 3;
            _presentionModel.EditCategory(EDIT_COLUMN_INDEX, CATEGORY_ID_ONE);
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_NAME_ONE), "");
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_NAME_TWO), "類別名稱重複了喔~~");
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_NAME_TEST), "");
        }

        [TestMethod()]
        public void TestCheckRepeatOverride()
        {
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_COLOR_ONE), "類別顏色重複了喔~~");
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_COLOR_TWO), "類別顏色重複了喔~~");
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_COLOR_TEST), "");
            //修改模式下
            const int EDIT_COLUMN_INDEX = 3;
            _presentionModel.EditCategory(EDIT_COLUMN_INDEX, CATEGORY_ID_TWO);
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_COLOR_ONE), "類別顏色重複了喔~~");
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_COLOR_TWO), "");
            Assert.AreEqual(_presentionModel.CheckRepeat(CATEGORY_COLOR_TEST), "");
        }

        [TestMethod()]
        public void TestSetCategoryList()       //檢查設定類別的功能
        {
            //如果在新增模式之下
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            Assert.AreEqual(_presentionModel.GetCategoryList().Count, 2);       //現在有兩個類別
            _presentionModel.SetCategoryList(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST);      //新增一個test類別
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            Assert.AreEqual(_presentionModel.GetCategoryList().Count, 3);
            //如果在修改模式之下
            Initialize();       //回到原本的狀態
            const int EDIT_BUTTON_COLUMN_INDEX = 3;
            _presentionModel.EditCategory(EDIT_BUTTON_COLUMN_INDEX, CATEGORY_ID_ONE);
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "修改");
            Assert.AreEqual(_target.GetFieldOrProperty("_editCategoryID"), CATEGORY_ID_ONE);
            //修改類別
            _presentionModel.SetCategoryList(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST);
            BindingList<Category> categoryList = _presentionModel.GetCategoryList();
            Assert.AreNotEqual(categoryList[CATEGORY_ID_ONE]._CategoryName, CATEGORY_NAME_ONE);
            Assert.AreNotEqual(categoryList[CATEGORY_ID_ONE]._CategoryColor, CATEGORY_COLOR_ONE);
        }

        [TestMethod()]
        public void TestGetCategoryList()
        {
            BindingList<Category> categoryList = new BindingList<Category>();
            Assert.AreNotEqual(categoryList, _presentionModel.GetCategoryList());
            categoryList = _presentionModel.GetCategoryList();
            Assert.AreEqual(categoryList, _presentionModel.GetCategoryList());
        }

        [TestMethod()]
        public void TestLeaveEditMode()     //檢查離開修改模式的功能
        {
            //確認在新增模式下
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            //進入修改模式
            const int EDIT_BUTTON_COLUMN_INDEX = 3;
            _presentionModel.EditCategory(EDIT_BUTTON_COLUMN_INDEX, CATEGORY_ID_TWO);
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "修改");
            Assert.AreEqual(_presentionModel._CategoryTextBoxText, CATEGORY_NAME_TWO);
            Assert.AreEqual(_presentionModel._CategoryColor, CATEGORY_COLOR_TWO);
            //離開修改模式
            _presentionModel.LeaveEditMode();
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            Assert.AreNotEqual(_presentionModel._CategoryColor, CATEGORY_COLOR_TWO);
            Assert.AreNotEqual(_presentionModel._CategoryTextBoxText, CATEGORY_NAME_TWO);
        }

        [TestMethod()]
        public void TestEditCategory()      //檢查能不能進入修改模式
        {
            const int EDIT_BUTTON_COLUMN_INDEX = 3;
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            //確認顏色沒有對應到第一組的類別
            Assert.AreNotEqual(_presentionModel._CategoryTextBoxText, CATEGORY_NAME_ONE);
            Assert.AreNotEqual(_presentionModel._CategoryColor, CATEGORY_COLOR_ONE);
            //沒進入修改模式
            _presentionModel.EditCategory(0, CATEGORY_ID_ONE);
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            _presentionModel.EditCategory(1, CATEGORY_ID_TWO);
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            _presentionModel.EditCategory(2, CATEGORY_ID_TEST);
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            //進入修改模式
            _presentionModel.EditCategory(EDIT_BUTTON_COLUMN_INDEX, CATEGORY_ID_ONE);
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "修改");
            //確認顏色和名稱相對應
            Assert.AreEqual(_presentionModel._CategoryTextBoxText, CATEGORY_NAME_ONE);
            Assert.AreEqual(_presentionModel._CategoryColor, CATEGORY_COLOR_ONE);
        }

        [TestMethod()]
        public void TestDeleteCategory()        //測試刪除檔案的功能
        {
            const int DELETE_COLUMN_INDEX = 2;
            const int OTHER_COLUMN_INDEX = 0;
            Assert.AreEqual(_presentionModel.GetCategoryList().Count, 2);       //確認Model中有兩個類別
            _presentionModel.DeleteCategory(OTHER_COLUMN_INDEX, CATEGORY_NAME_ONE);
            Assert.AreEqual(_presentionModel.GetCategoryList().Count, 2);       //Model仍有兩個類別
            _presentionModel.DeleteCategory(DELETE_COLUMN_INDEX, CATEGORY_NAME_TWO);
            Assert.AreEqual(_presentionModel.GetCategoryList().Count, 2);       //Model仍有兩個類別
            //賦予事件的接受者
            _presentionModel._callMessageBoxEvent += delegate(string categroy)
            {
                return DialogResult.OK;
            };
            _presentionModel.DeleteCategory(DELETE_COLUMN_INDEX, CATEGORY_NAME_ONE);
            Assert.AreEqual(_presentionModel.GetCategoryList().Count, 2);       //Model仍有兩個類別，因為沒有正確刪除
            _presentionModel._callMessageBoxEvent += delegate(string category)
            {
                return DialogResult.Yes;
            };
            _presentionModel.DeleteCategory(DELETE_COLUMN_INDEX, CATEGORY_NAME_TWO);
            Assert.AreEqual(_presentionModel.GetCategoryList().Count, 1);       //Model中只剩1個類別
        }

        [TestMethod()]
        public void TestAddToDoListForm()       //測試新增視窗至管理視窗資料的class有沒有問題
        {
            Assert.AreEqual(_presentionModel.GetToDoListFormCount(), 2);        //一開始是2
            _presentionModel.AddToDoListForm(_toDoFormTest);
            Assert.AreEqual(_presentionModel.GetToDoListFormCount(), 3);        //加一個之後，變3
        }

        [TestMethod()]
        public void TestGetToDoListFormCount()      //測試拿到視窗數量的功能有沒有問題
        {
            int testGetToDoListFormCount = new int();
            Assert.AreNotEqual(_presentionModel.GetToDoListFormCount(), testGetToDoListFormCount);
            testGetToDoListFormCount = _presentionModel.GetToDoListFormCount();
            Assert.AreEqual(_presentionModel.GetToDoListFormCount(), testGetToDoListFormCount);
        }

        [TestMethod()]
        public void TestWriteFormData()     //測試寫視窗資料的功能
        {
            const string FAKE_PATH = "Fake Path";
            SubStreamWriter fakeWriter = new SubStreamWriter(FAKE_PATH);
            Assert.AreEqual(fakeWriter._Writer, string.Empty);      //一開始是空的字串
            _presentionModel.WriteFormData(fakeWriter);
            Assert.AreNotEqual(fakeWriter._Writer, string.Empty);       //寫入資料之後，不是空的字串
            fakeWriter.Close();
        }

        [TestMethod()]
        public void TestSetToDo()       //測試SetToDo的功能
        {
            Assert.AreEqual(_presentionModel.SetToDo(DialogResult.Yes, CATEGORY_NAME_ONE, CONTENT_ONE), "");
            Assert.AreEqual(_presentionModel.SetToDo(DialogResult.Yes, CATEGORY_NAME_ONE, CONTENT_ONE), "");
            //丟一個重覆的待辦事項進去，回傳錯誤字串
            Assert.AreEqual(_presentionModel.SetToDo(DialogResult.OK, CATEGORY_NAME_ONE, CONTENT_ONE), "已經有一個相同類別和相同內容的待辦事項囉，不需要重複輸入喔！");
            Assert.AreEqual(_presentionModel.SetToDo(DialogResult.OK, CATEGORY_NAME_TWO, CONTENT_TWO), "已經有一個相同類別和相同內容的待辦事項囉，不需要重複輸入喔！");
            //丟沒有的進去
            Assert.AreEqual(_presentionModel.SetToDo(DialogResult.OK, CATEGORY_NAME_TEST, CONTENT_TEST), "");
        }

        [TestMethod()]
        public void TestGetToDo()       //檢查回傳的陣列有沒有相等
        {
            BindingList<ToDo> toDoList = new BindingList<ToDo>();
            Assert.AreNotEqual(toDoList, _presentionModel.GetToDo());
            toDoList = _presentionModel.GetToDo();
            Assert.AreEqual(toDoList, _presentionModel.GetToDo());
        }

        [TestMethod()]
        public void TestSetNewCategoryButtonAndCancelCategoryButton()       //檢查設定新增和取消按鈕的功能
        {
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            //狀況1，新增模式下，類別名稱有內容，類別顏色不是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), true);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況2，新增模式下，類別名稱有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, "ERROR", string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況3，新增模式下，類別名稱有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, string.Empty, "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況4，新增模式下，類別名稱有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況5，新增模式下，類別名稱沒有內容，類別顏色不是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, CATEGORY_COLOR_TEST, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況6，新增模式下，類別名稱沒有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, CATEGORY_COLOR_TEST, "ERROR", string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況7，新增模式下，類別名稱沒有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, CATEGORY_COLOR_TEST, string.Empty, "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況8，新增模式下，類別名稱沒有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, CATEGORY_COLOR_TEST, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況9，新增模式下，類別名稱有內容，類別顏色是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, TextBox.DefaultBackColor, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況10，新增模式下，類別名稱有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, TextBox.DefaultBackColor, "ERROR", string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況11，新增模式下，類別名稱有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, TextBox.DefaultBackColor, string.Empty, "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況12，新增模式下，類別名稱有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, TextBox.DefaultBackColor, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況13，新增模式下，類別名稱沒有內容，類別顏色是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), false);
            //狀況14，新增模式下，類別名稱沒有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, "ERROR", string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), false);
            //狀況15，新增模式下，類別名稱沒有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, string.Empty, "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), false);
            //狀況16，新增模式下，類別名稱沒有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), false);
            
            //在修改模式之下
            const int EDIT_COLUMN_INDEX = 3;
            _presentionModel.EditCategory(EDIT_COLUMN_INDEX, CATEGORY_ID_ONE);
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "修改");
            //狀況1，修改模式下，類別名稱有內容，類別顏色不是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), true);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況2，修改模式下，類別名稱有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, "ERROR", string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況3，修改模式下，類別名稱有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, string.Empty, "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況4，修改模式下，類別名稱有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況5，修改模式下，類別名稱沒有內容，類別顏色不是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, CATEGORY_COLOR_TEST, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況6，修改模式下，類別名稱沒有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, CATEGORY_COLOR_TEST, "ERROR", string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況7，修改模式下，類別名稱沒有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, CATEGORY_COLOR_TEST, string.Empty, "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況8，修改模式下，類別名稱沒有內容，類別顏色不是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, CATEGORY_COLOR_TEST, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況9，修改模式下，類別名稱有內容，類別顏色是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, TextBox.DefaultBackColor, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況10，修改模式下，類別名稱有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, TextBox.DefaultBackColor, "ERROR", string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況11，修改模式下，類別名稱有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, TextBox.DefaultBackColor, string.Empty, "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況12，修改模式下，類別名稱有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, TextBox.DefaultBackColor, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), true);
            //狀況13，修改模式下，類別名稱沒有內容，類別顏色是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), false);
            //狀況14，修改模式下，類別名稱沒有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, "ERROR", string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), false);
            //狀況15，修改模式下，類別名稱沒有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, string.Empty, "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), false);
            //狀況16，修改模式下，類別名稱沒有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), false);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), false);
            
        }

        [TestMethod()]
        public void TestGetCategory()       //測試拿到類別的功能
        {
            Assert.AreEqual(_presentionModel.GetCategory(CATEGORY_NAME_ONE)._CategoryColor, CATEGORY_COLOR_ONE);
            Assert.AreEqual(_presentionModel.GetCategory(CATEGORY_NAME_ONE)._CategoryName, CATEGORY_NAME_ONE);
            Assert.AreEqual(_presentionModel.GetCategory(CATEGORY_NAME_TWO)._CategoryColor, CATEGORY_COLOR_TWO);
            Assert.AreEqual(_presentionModel.GetCategory(CATEGORY_NAME_TWO)._CategoryName, CATEGORY_NAME_TWO);
        }

        [TestMethod()]
        public void TestIsNewCategoryButtonEnable()         //測試回傳新增按鈕Enable的功能，引用設定按鈕狀態
        {
            //在新增模式之下
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            //狀況1，新增模式下，類別名稱有內容，類別顏色不是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), _presentionModel.IsNewCategoryButtonEnable());
            //在修改模式之下
            const int EDIT_COLUMN_INDEX = 3;
            _presentionModel.EditCategory(EDIT_COLUMN_INDEX, CATEGORY_ID_ONE);
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "修改");
            //狀況16，修改模式下，類別名稱沒有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_newCategotyButtonEnable"), _presentionModel.IsNewCategoryButtonEnable());
        }

        [TestMethod()]
        public void TestIsCancelButtonEnable()         //測試回傳取消按鈕Enable的功能，引用設定按鈕狀態
        {
            //在新增模式之下
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "新增");
            //狀況1，新增模式下，類別名稱有內容，類別顏色不是default color，沒有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(CATEGORY_NAME_TEST, CATEGORY_COLOR_TEST, string.Empty, string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), _presentionModel.IsCancelButtonEnable());
            //在修改模式之下
            const int EDIT_COLUMN_INDEX = 3;
            _presentionModel.EditCategory(EDIT_COLUMN_INDEX, CATEGORY_ID_ONE);
            Assert.AreEqual(_presentionModel._NewCategoryButtonText, "修改");
            //狀況16，修改模式下，類別名稱沒有內容，類別顏色是default color，有錯誤訊息
            _presentionModel.SetNewCategoryButtonAndCancelCategoryButton(string.Empty, TextBox.DefaultBackColor, "ERROR", "ERROR");
            Assert.AreEqual(_target.GetFieldOrProperty("_cancelCategoryButtonEbable"), _presentionModel.IsCancelButtonEnable());
        }

        [TestMethod()]
        public void TestGetToDo1()      //測試回傳帶辦事項的功能
        {
            ToDo toDo = _presentionModel.GetToDo(CATEGORY_NAME_TWO, CONTENT_TWO);
            Assert.AreEqual(toDo._Category, _presentionModel.GetToDo(CATEGORY_NAME_TWO, CONTENT_TWO)._Category);
            Assert.AreEqual(toDo._CategoryColor, _presentionModel.GetToDo(CATEGORY_NAME_TWO, CONTENT_TWO)._CategoryColor);
            Assert.AreEqual(toDo._CategoryText, _presentionModel.GetToDo(CATEGORY_NAME_TWO, CONTENT_TWO)._CategoryText);
            Assert.AreEqual(toDo._Content, _presentionModel.GetToDo(CATEGORY_NAME_TWO, CONTENT_TWO)._Content);
        }

        [TestMethod()]
        public void TestDeleteToDo()        //測試刪除待辦事項的功能
        {
            Assert.AreEqual(_presentionModel.GetToDo().Count, 2);       //一開始只有兩個元素
            _presentionModel.DeleteToDo(CATEGORY_NAME_ONE, CONTENT_ONE, new ToDoListWindows(_toDoOne));
            Assert.AreEqual(_presentionModel.GetToDo().Count, 1);
        }

        [TestMethod()]
        public void TestEditToDo()      //測試修改待辦事項的功能
        {
            ToDo toDo = _presentionModel.GetToDo(CATEGORY_NAME_TWO, CONTENT_TWO);
            Assert.AreEqual(toDo._CategoryText, CATEGORY_NAME_TWO);
            Assert.AreEqual(toDo._Content, CONTENT_TWO);
            _presentionModel.EditToDo(DialogResult.OK, toDo, _categoryTest, CONTENT_TEST);
            ToDo changedToDO = _presentionModel.GetToDo()[CATEGORY_ID_TWO];
            Assert.AreNotEqual(changedToDO._CategoryText, CATEGORY_NAME_TWO);
            Assert.AreNotEqual(changedToDO._Content, CONTENT_TWO);
        }

        [TestMethod()]
        public void TestWriteContentData()      //測試有沒有寫入待辦事項的資料
        {
            SubStreamWriter contentWriter = new SubStreamWriter("FakePath");
            Assert.AreEqual(contentWriter._Writer, "");
            _presentionModel.WriteContentData(contentWriter);
            Assert.AreNotEqual(contentWriter._Writer, "");
            contentWriter.Close();
        }

        [TestMethod()]
        public void TestWriteCategoryData()
        {
            SubStreamWriter categoryWriter = new SubStreamWriter("FakePath");
            Assert.AreEqual(categoryWriter._Writer, "");
            _presentionModel.WriteCategoryData(categoryWriter);
            Assert.AreNotEqual(categoryWriter._Writer, "");
            categoryWriter.Close();
        }

        [TestMethod()]
        public void TestReadCategoryData()      //測試讀類別的檔案
        {
            //寫入檔案
            const string PATH_CATEGORY = "FakePath";
            SubStreamWriter categoryWriter = new SubStreamWriter(PATH_CATEGORY);
            _presentionModel.WriteCategoryData(categoryWriter);
            string categoryData = categoryWriter._Writer;
            categoryWriter.Close();
            //生成空的Model，在丟資料進去
            StickyPadPresentionModel readingData = new StickyPadPresentionModel();
            Assert.AreEqual(readingData.GetCategoryList().Count, 0);
            //測試檔案
            readingData.ReadCategoryData("cataegory.txt", false, string.Empty);
            readingData.ReadCategoryData(PATH_CATEGORY, true, string.Empty);
            readingData.ReadCategoryData(PATH_CATEGORY, false, string.Empty);
            Assert.AreEqual(readingData.GetCategoryList().Count, 0);
            readingData.ReadCategoryData(PATH_CATEGORY, true, categoryData);
            Assert.AreEqual(readingData.GetCategoryList().Count, 2);
        }

        [TestMethod()]
        public void TestReadContentData()       //測試讀待辦事項的資料
        {
            //寫入待辦事項檔案
            const string PATH_CONTENT = "FakePath";
            SubStreamWriter contentWriter = new SubStreamWriter(PATH_CONTENT);
            _presentionModel.WriteContentData(contentWriter);
            string contentData = contentWriter._Writer;
            contentWriter.Close();
            //寫入類別檔案
            SubStreamWriter categoryWriter = new SubStreamWriter("Fake Path");
            _presentionModel.WriteCategoryData(categoryWriter);
            string categoryData = categoryWriter._Writer;
            categoryWriter.Close();
            //生成空的PModel，再丟資料進去
            StickyPadPresentionModel readingData = new StickyPadPresentionModel();
            //測試檔案不存在
            readingData.ReadContentData("content.txt", false, string.Empty);
            //先丟類別資料進去
            readingData.ReadCategoryData("Fake Path", true, categoryData);
            //開始測試待辦事項
            Assert.AreEqual(readingData.GetToDo().Count, 0);
            readingData.ReadContentData(PATH_CONTENT, true, string.Empty);
            readingData.ReadContentData(PATH_CONTENT, false, string.Empty);
            Assert.AreEqual(readingData.GetToDo().Count, 0);
            readingData.ReadContentData(PATH_CONTENT, true, contentWriter._Writer);
            Assert.AreEqual(readingData.GetToDo().Count, 2);
        }

        [TestMethod()]
        public void TestNewStickyPadButtonEnable()      //測試類別數量的回傳
        {
            Assert.AreEqual(true, _presentionModel._NewStickyPadButtonEnable);
            StickyPadPresentionModel test = new StickyPadPresentionModel();
            Assert.AreEqual(false, test._NewStickyPadButtonEnable);
        }
        [TestMethod()]
        public void TestNotify()        //測試通知函式
        {
            string property = string.Empty;
            Assert.AreEqual(property, string.Empty);
            _presentionModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                property = e.PropertyName;
            };
            const int EDIT_COLUMN_INDEX = 3;
            _presentionModel.EditCategory(EDIT_COLUMN_INDEX, CATEGORY_ID_ONE);
            Assert.AreEqual(property, "_categoryColor");
        }
    }
}
