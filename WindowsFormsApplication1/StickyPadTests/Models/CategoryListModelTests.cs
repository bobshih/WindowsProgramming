using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.ComponentModel;
using StickyPadForm;
using StickyPadForm.Model;

namespace StickyPadForm.Tests
{
    [TestClass()]
    public class CategoryListModelTests
    {
        const string CATEGORY_NAME_ONE = "Category1";
        const int CATEGORY_ID_ONE = 0;
        Color CATEGORY_COLOR_ONE = Color.FromArgb(100, 100, 100, 100);
        const string CATEGORY_NAME_TWO = "Category2";
        const int CATEGORY_ID_TWO = 1;
        Color CATEGORY_COLOR_TWO = Color.FromArgb(220, 220, 220, 220);
        const string WRONG_CATEGORY_NAME = "WrongName";
        Color WRONG_CATEGORY_COLOR = Color.FromArgb(7, 7, 7, 7);
        const string TEST_CATEGORY_NAME = "TestCategory";
        Color TEST_CATEGORY_COLOR = Color.FromArgb(50, 50, 50, 50);
        CategoryListModel _categoryModel;

        [TestInitialize()]
        [DeploymentItem("StickyPad.exe")]
        public void Initalize()     //初始化
        {
            _categoryModel = new CategoryListModel();
            _categoryModel.SetCategory(CATEGORY_NAME_ONE, CATEGORY_COLOR_ONE);      //測資
            _categoryModel.SetCategory(CATEGORY_NAME_TWO, CATEGORY_COLOR_TWO);
        }

        [TestMethod()]
        public void TestGetCategory()       //檢查回傳的類別是不是相對應的類別
        {
            Assert.IsTrue(TestEqual(_categoryModel.GetCategory(CATEGORY_NAME_ONE), new Category(CATEGORY_NAME_ONE, CATEGORY_COLOR_ONE)));
            Assert.IsTrue(TestEqual(_categoryModel.GetCategory(CATEGORY_NAME_TWO), new Category(CATEGORY_NAME_TWO, CATEGORY_COLOR_TWO)));
        }

        [TestMethod()]
        public void TestGetMappingColor()       //檢查回傳的資料是不是相對應的類別顏色
        {
            Assert.AreEqual(_categoryModel.GetMappingColor(CATEGORY_NAME_ONE), CATEGORY_COLOR_ONE);
            Assert.AreEqual(_categoryModel.GetMappingColor(CATEGORY_NAME_TWO), CATEGORY_COLOR_TWO);
        }

        [TestMethod()]
        public void TestFindCategory()      //檢查類別有沒有存在在資料中
        {
            Assert.IsTrue(_categoryModel.FindCategory(CATEGORY_NAME_ONE));
            Assert.IsTrue(_categoryModel.FindCategory(CATEGORY_NAME_TWO));
        }

        [TestMethod()]
        public void TestRemoveCategory()        //檢查類別有沒有被移除
        {
            //先判斷類別是否存在
            Assert.IsTrue(_categoryModel.FindCategory(CATEGORY_NAME_ONE));
            Assert.IsTrue(_categoryModel.FindCategory(CATEGORY_NAME_TWO));
            //移除類別，在判斷該類別存不存在
            _categoryModel.RemoveCategory(CATEGORY_NAME_ONE);
            Assert.IsFalse(_categoryModel.FindCategory(CATEGORY_NAME_ONE));
            Initalize();        //再次初始化
            _categoryModel.RemoveCategory(CATEGORY_NAME_TWO);
            Assert.IsFalse(_categoryModel.FindCategory(CATEGORY_NAME_TWO));
        }


        [TestMethod()]       //先檢查不存在的類別是不是沒有在List中，然後丟類別進去，檢查有沒有設定到List中
        public void TestSetCategory()
        {
            Assert.IsFalse(_categoryModel.FindCategory(TEST_CATEGORY_NAME));
            //把資料丟到model中
            _categoryModel.SetCategory(TEST_CATEGORY_NAME, TEST_CATEGORY_COLOR);
            Assert.IsTrue(_categoryModel.FindCategory(TEST_CATEGORY_NAME));
        }

        [TestMethod()]
        public void TestSetCategoryOverload()       //測試SetCategory的另一個有三個參數的多載
        {
            Assert.IsTrue(_categoryModel.FindCategory(CATEGORY_NAME_ONE));
            _categoryModel.SetCategory(CATEGORY_ID_ONE, TEST_CATEGORY_NAME, TEST_CATEGORY_COLOR);       //修改類別內容
            Assert.IsFalse(_categoryModel.FindCategory(CATEGORY_NAME_ONE));
        }

        [TestMethod()]
        public void TestGetCategoryList()       //測試GetCategoryList
        {
            BindingList<Category> testCategoryList = _categoryModel.GetCategoryList();
            Assert.AreEqual(testCategoryList, _categoryModel.GetCategoryList());
        }

        public bool TestEqual(Category inputOne, Category inputTwo)     //檢查兩個類別有沒有一樣
        {
            return inputOne._CategoryColor == inputTwo._CategoryColor && inputOne._CategoryName == inputTwo._CategoryName;
        }
    }
}
