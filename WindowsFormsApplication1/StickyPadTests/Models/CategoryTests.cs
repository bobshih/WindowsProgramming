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
    public class CategoryTests
    {
        const string TEST_CATEGORY_NAME = "Category";
        Color TEST_CATEGORY_COLOR = Color.FromArgb(15, 15, 15, 15);
        const string TESTING_NAME = "TestingName";
        Color TESTING_COLOR = Color.FromArgb(44, 44, 44, 44);
        Category _category;

        [TestInitialize()]
        [DeploymentItem("StickyPad.exe")]
        public void Initialize()
        {
            _category = new Category(TEST_CATEGORY_NAME, TEST_CATEGORY_COLOR);
        }

        [TestMethod()]
        public void NotifyPropertyChangedTest()
        {
            string testString = string.Empty;
            //設定一個額外的事件，抓屬性變更的名稱
            _category.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                testString = e.PropertyName;
            };
            //類別名稱的屬性變更
            _category._CategoryName = TEST_CATEGORY_NAME;
            Assert.AreNotEqual(testString, string.Empty);
            Assert.AreEqual(testString, "_CategoryName");
            //類別顏色的屬性變更
            testString = string.Empty;
            _category._CategoryColor = TEST_CATEGORY_COLOR;
            Assert.AreNotEqual(testString, string.Empty);
            Assert.AreEqual(testString, "_CategoryColor");
        }
    }
}
