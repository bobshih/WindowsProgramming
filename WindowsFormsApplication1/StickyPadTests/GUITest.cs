using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace StickyPadTests
{
    /// <summary>
    /// CodedUITest1 的摘要描述
    /// </summary>
    [CodedUITest]
    public class GUITest
    {
        const string FILE_PATH = "WindowsFormsApplication.exe";
        const string TITLE = "StickyPad";

        public GUITest()
        {
        }

        [TestInitialize()]
        public void Initialize()
        {
            Robot.Initialize(FILE_PATH, TITLE);
        }

        [TestMethod]
        public void TestCategoryName()      //測試類別名稱
        {
            Robot.AssertEdit("_categoryBox", "");
            this.UIMap.SetCategoryName();
            Robot.AssertEdit("_categoryBox", "1234");
        }

        [TestMethod()]
        public void TestCancelButtonEnable()        //測試取消按鈕狀態
        {
            this.UIMap.AssertCancelButtonUnable();      //一開始Unable
            this.UIMap.SetCategoryName();               //輸入類別名稱
            this.UIMap.AssertCancelButton();            //取消按鈕Enable
            this.UIMap.ClickCancelButton();             //點擊取消按鈕
            this.UIMap.AssertCancelButtonUnable();      //取消按鈕又不能按了
            this.UIMap.ClickColorTextAndSetColor();     //設定類別顏色
            this.UIMap.AssertCancelButton();            //取消按鈕又可以按了
        }

        [TestMethod()]
        public void TestNewCateogryButton()     //測試新增類別按鈕
        {
            this.UIMap.AssertNewCategoryButtonUnable();     //一開始Unable
            this.UIMap.SetCategoryName();                   //設定類別名稱
            this.UIMap.AssertNewCategoryButtonUnable();     //只有名稱還是不能新增類別
            this.UIMap.ClickColorTextAndSetColor();         //設定類別顏色
            this.UIMap.AssertNewCategoryButtonEnable();     //變成可以按的了
        }

        [TestMethod()]
        public void TestCategoryData()      //測試類別資料輸入
        {
            this.UIMap.AssertEmptyCategoryDataGridView();   //一開始資料是空的
            this.UIMap.SetCategoryName();                   //輸入類別資料
            this.UIMap.ClickColorTextAndSetColor();
            this.UIMap.ClickNewCategoryButton();
            this.UIMap.AssertCategoryDataGridView();        //簡查是不是正確
        }

        [TestMethod()]
        public void TestTabPageControl()        //測試轉換tab頁面之後有取消的效果
        {
            this.UIMap.SetCategoryName();
            this.UIMap.ClickColorTextAndSetColor();
            Robot.AssertEdit("_categoryBox", "1234");
            this.UIMap.ChangeTabPageAndBack();
            Robot.AssertEdit("_categoryBox", "");
        }

        [TestMethod()]
        public void TestDeleteButton()      //測試刪除類別
        {
            this.UIMap.AssertEmptyCategoryDataGridView();   //檢查一開始沒有資料
            this.UIMap.SetCategoryName();       //新增一個類別
            this.UIMap.ClickColorTextAndSetColor();
            this.UIMap.ClickNewCategoryButton();
            this.UIMap.AssertCategoryDataGridView();        //檢查datagridview有沒有類別
            this.UIMap.ClickDeleteAndCancelButton();        //按Delete按鈕但是取消
            this.UIMap.AssertCategoryDataGridView();        //檢查資料還在不在
            this.UIMap.ClickDeleteAndYesButton();           //刪除資料
            this.UIMap.AssertEmptyCategoryDataGridView();   //檢查資料有沒有消失
        }

        [TestMethod()]
        public void TestEditButton()        //測是修改模式
        {
            this.UIMap.SetCategoryName();           //輸入一筆資料
            this.UIMap.ClickColorTextAndSetColor();
            this.UIMap.ClickNewCategoryButton();
            this.UIMap.AssertCategoryDataGridView();//檢查資料有沒有輸入
            this.UIMap.ClickEditButtonInCategoryPage();
            Robot.AssertEdit("_categoryBox", "1234");
            this.UIMap.ClickCancelButton();     //取消修改
            this.UIMap.AssertCategoryDataGridView();    //檢查資料有沒有仍一樣
            this.UIMap.ClickEditButtonInCategoryPage(); //在進行修改一次
            this.UIMap.SetModifyCategoryName();         //修改類別名稱為asdf
            this.UIMap.ClickEditButton();               //確認修改
            this.UIMap.AssertModifyCategoryDataGridView();  //檢查修改的類別資料有沒有輸入到datagridview裡
        }

        [TestMethod()]
        public void TestAddStickyPadButton()            //檢查新增便條清單按鈕的狀態
        {

            this.UIMap.ClcikStickyPadTab();         //到便條頁面
            Robot.AssertButtonEnable("_messageAddition", false);     //檢查新增便條按鈕的狀態
            this.UIMap.ClickCategoryTabPage();      //到類別頁面
            this.UIMap.SetCategoryName();           //輸入一個類別
            this.UIMap.ClickColorTextAndSetColor();
            this.UIMap.ClickNewCategoryButton();
            Robot.AssertButtonEnable("_messageAddition", true);      //再次檢查新增便條按鈕的狀態
        }

        [TestMethod()]
        public void TestAddStickyPad()      //測試新增便條
        {
            this.UIMap.ClcikStickyPadTab();
            this.UIMap.AssertEmptyStickyPadDataGridView();      //檢查一開始便條是沒有資料的
            this.UIMap.ClickCategoryTabPage();
            SetNewStickyPad();      //新增一個便條
            this.UIMap.AssertStickyPadDatagridView();
        }

        [TestMethod()]
        public void TestDeleteStickyPad()       //測試便條的刪除按鈕
        {
            SetNewStickyPad();      //設定一個便條
            this.UIMap.ClickDeleteButtonInStickyPadTabPage();   //刪除便條資料
            this.UIMap.AssertEmptyCategoryDataGridView();       //判斷資料不存在
        }

        [TestMethod()]
        public void TestStickyPadEditButtonThatModifyStickyPadContent()       //測試修改便條內容的修改功能
        {
            SetNewStickyPad();      //設定一個便條
            this.UIMap.AssertStickyPadDatagridView();                   //檢查一開始的便條狀態
            this.UIMap.ClickEditButtonAndModifyStickyPadContent();      //修改便條內容
            this.UIMap.AssertStickyPadDatagridViewThatChangesContent(); //檢查便條內容有沒有修改
        }

        [TestMethod()]
        public void TsetStickyPadEditButtonThatModifyStickyPadCategory()        //測試修改便條類別的修改功能
        {
            SetNewStickyPad();      //設定一個便條
            this.UIMap.AssertStickyPadDatagridView();                   //檢查一開始的便條狀態
            this.UIMap.ClickCategoryTabPage();                          //到類別頁面
            this.UIMap.AddNewCategory();                                //設定新類別
            this.UIMap.ClcikStickyPadTab();                             //回到便條頁面
            this.UIMap.ClickEditButtonAndModifyStickyPadCategory();     //修改便條類別
            this.UIMap.AssertStickyPadDatagridViewThatChangesCategory();//檢查便條類別有沒有被修改
        }

        [TestMethod()]
        public void TestContentWindows()        //測試便條視窗
        {
            SetNewStickyPad();      //設定一個便條
            this.UIMap.ClickStickyPadContentCell();     //點選便條內容
            Robot.AssertWindow("便條");
            this.UIMap.CloseStickyPad();
            this.UIMap.ClickStickyPadCategoryCell();
            Robot.AssertWindow("便條");
            this.UIMap.CloseStickyPad();
        }

        [TestMethod()]
        public void TestExitMenu()      //測試離開的menu
        {
            Robot.AssertWindow("StickyPad");
            this.UIMap.ClickExitInMenu();
            Robot.AssertWindow("");
        }

        [TestMethod()]
        public void TestColorTextBox()      //測試在顏色TextBox中輸入資料
        {
            Robot.AssertEdit("_colorTextBox", "");
            this.UIMap.SetValueInColorTextBox();
            Robot.AssertEdit("_colorTextBox", "");
        }

        private void SetNewStickyPad()      //設定新的便條
        {
            //新增便條
            this.UIMap.SetCategoryName();       //新增一個類別
            this.UIMap.ClickColorTextAndSetColor();
            this.UIMap.ClickNewCategoryButton();
            this.UIMap.ClcikStickyPadTab();     //到便條頁面
            SetStickyPad();
        }

        private void SetStickyPad()     //設定一個便條
        {
            this.UIMap.ClickAdditionButton();   //點選新增便條按鈕
            this.UIMap.SetComboBox();
            this.UIMap.SetStickyPadContent();
            this.UIMap.ConfirmStickyPadAddition();      //確認便條新增
        }

        #region 其他測試屬性

        // 您可以使用下列其他屬性撰寫測試: 

        ////在每項測試執行前先使用 TestInitialize 執行程式碼 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // 若要為這個測試產生程式碼，請在捷徑功能表上選取 [產生自動程式碼 UI 測試的程式碼]，並選取其中一個功能表項目。
        //}

        ////在每項測試執行後使用 TestCleanup 執行程式碼
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // 若要為這個測試產生程式碼，請在捷徑功能表上選取 [產生自動程式碼 UI 測試的程式碼]，並選取其中一個功能表項目。
        //}

        #endregion

        /// <summary>
        ///取得或設定提供目前測試回合
        ///相關資訊與功能的測試內容。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
