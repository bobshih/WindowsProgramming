using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyPadForm.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using StickyPadForm.Model;

namespace StickyPadForm.Models.Tests
{
    [TestClass()]
    public class AdditionWindowModelTests
    {
        const string COMBOBOX_STRING_ONE = "ComboBoxOne";
        const string COMBOBOX_STRING_TEST = "ComboBoxTest";
        const string CONTENT_STRING_ONE = "ContentOne";
        const string CONTENT_STRING_TEST = "ContentTest";
        AdditionWindowModel _testModel;
        PrivateObject _target;

        [TestInitialize()]
        public void Initialize()        //初始化
        {
            _testModel = new AdditionWindowModel();
            _target = new PrivateObject(_testModel);
        }

        [TestMethod()]
        public void TestAdditionWindowModel()       //測試建構子
        {
            Assert.AreEqual(_target.GetFieldOrProperty("_comboBoxString"), string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_contentString"), string.Empty);
        }

        [TestMethod()]
        public void TestSetContentString()
        {
            Assert.AreEqual(_target.GetFieldOrProperty("_comboBoxString"), string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_contentString"), string.Empty);
            _testModel.SetContentString(CONTENT_STRING_ONE);
            Assert.AreEqual(_target.GetFieldOrProperty("_contentString"), CONTENT_STRING_ONE);
            Assert.AreEqual(_target.GetFieldOrProperty("_comboBoxString"), string.Empty);
            _testModel.SetContentString(CONTENT_STRING_TEST);
            Assert.AreEqual(_target.GetFieldOrProperty("_contentString"), CONTENT_STRING_TEST);
            Assert.AreEqual(_target.GetFieldOrProperty("_comboBoxString"), string.Empty);
        }

        [TestMethod()]
        public void TestSetComboBoxString()
        {
            Assert.AreEqual(_target.GetFieldOrProperty("_comboBoxString"), string.Empty);
            Assert.AreEqual(_target.GetFieldOrProperty("_contentString"), string.Empty);
            _testModel.SetComboBoxString(COMBOBOX_STRING_ONE);
            Assert.AreEqual(_target.GetFieldOrProperty("_comboBoxString"), COMBOBOX_STRING_ONE);
            Assert.AreEqual(_target.GetFieldOrProperty("_contentString"), string.Empty);
            _testModel.SetComboBoxString(COMBOBOX_STRING_TEST);
            Assert.AreEqual(_target.GetFieldOrProperty("_comboBoxString"), COMBOBOX_STRING_TEST);
            Assert.AreEqual(_target.GetFieldOrProperty("_contentString"), string.Empty);
        }

        [TestMethod()]
        public void TestAdditionButtonEnable()
        {
            Assert.IsFalse(_testModel._AdditionButtonEnable);
            _testModel.SetComboBoxString(COMBOBOX_STRING_ONE);
            Assert.IsFalse(_testModel._AdditionButtonEnable);
            _testModel.SetComboBoxString(string.Empty);
            Assert.IsFalse(_testModel._AdditionButtonEnable);
            _testModel.SetContentString(CONTENT_STRING_ONE);
            Assert.IsFalse(_testModel._AdditionButtonEnable);
            _testModel.SetContentString(string.Empty);
            Assert.IsFalse(_testModel._AdditionButtonEnable);
            _testModel.SetComboBoxString(COMBOBOX_STRING_TEST);
            _testModel.SetContentString(CONTENT_STRING_TEST);
            Assert.IsTrue(_testModel._AdditionButtonEnable);
        }

        [TestMethod()]
        public void TestNotify()
        {
            string propertyName = string.Empty;
            Assert.AreEqual(propertyName, string.Empty);
            _testModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                propertyName = e.PropertyName;
            };
            _testModel.SetComboBoxString(COMBOBOX_STRING_ONE);
            Assert.AreEqual(propertyName, "_AdditionButtonEnable");
        }
    }
}
