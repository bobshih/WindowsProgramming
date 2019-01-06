using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StickyPadForm.Model;

namespace StickyPadForm
{
    public partial class ToDoListAdditionWindows : Form
    {
        AdditionWindowModel _presentionModel;

        public ToDoListAdditionWindows(BindingList<Category> categoryList)      //以類別List為參數的constructor
        {
            InitializeComponent();
            //新增PModel
            _presentionModel = new AdditionWindowModel();
            //輸入類別
            _categoryComboBox.Items.Clear();
            //把類別一一丟進combobox中
            for (int i = 0; i < categoryList.Count; i++)
            {
                _categoryComboBox.Items.Add(categoryList[i]._CategoryName);
            }
            //databinding
            const string BUTTON_ENABLE = "Enabled";
            const string BUTTON_ENABLE_IN_MODEL = "_AdditionButtonEnable";
            _addButton.DataBindings.Add(BUTTON_ENABLE, _presentionModel, BUTTON_ENABLE_IN_MODEL);
        }

        public ToDoListAdditionWindows(BindingList<Category> categoryList, ToDo editedToDo)     //進入修改模式的建構子
        {
            InitializeComponent();
            const string FORM_HEADER = "修改";
            this.Text = FORM_HEADER;       //設定標題
            //新增model
            _presentionModel = new AdditionWindowModel();
            //輸入類別
            _categoryComboBox.Items.Clear();
            //把類別一一丟進combobox中
            for (int i = 0; i < categoryList.Count; i++)
            {
                _categoryComboBox.Items.Add(categoryList[i]._CategoryName);
            }
            //databinding
            const string BUTTON_ENABLE = "Enabled";
            const string BUTTON_ENABLE_IN_MODEL = "_AdditionButtonEnable";
            _addButton.DataBindings.Add(BUTTON_ENABLE, _presentionModel, BUTTON_ENABLE_IN_MODEL);
            //輸入待辦清單的內容
            _categoryComboBox.Text = editedToDo._Category._CategoryName;
            _presentionModel.SetComboBoxString(_categoryComboBox.Text);
            _contentTextBox.Text = editedToDo._Content;
            _presentionModel.SetContentString(_contentTextBox.Text);
        }

        public string GetCategoryComboBox()     //回傳combobox的text
        {
            return _categoryComboBox.Text;
        }

        public string GetContentMessage()       //回傳便條的內容
        {
            return _contentTextBox.Text;
        }

        private void ClickAddButton(object sender, EventArgs e)     //點擊新增按鈕之後，關閉視窗
        {
            this.Close();
        }

        private void ChangeCategoryComboBox(object sender, EventArgs e)     //當選擇類別的選項不為空的時候，檢查能否讓確定按鈕設定為Enable
        {
            _presentionModel.SetComboBoxString(_categoryComboBox.Text);
        }

        private void ChangeContentTextBox(object sender, EventArgs e)       //改變contentTextBox的內容時，檢查能否讓確定按鈕設定為Enable
        {
            _presentionModel.SetContentString(_contentTextBox.Text);
        }
    }
}
