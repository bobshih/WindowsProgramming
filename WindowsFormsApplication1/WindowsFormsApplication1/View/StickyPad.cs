using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using StickyPadForm.Model;

namespace StickyPadForm
{
    public partial class StickyPad : Form
    {
        private StickyPadPresentionModel _stickyPadData;        //資料的presentation model
        
        public StickyPad()           //default constructor
        {
            //new a set of data
            this._stickyPadData = new StickyPadPresentionModel();
            InitializeComponent();
            //綁定類別datagridview的datasource
            _categoryDataGridView.AutoGenerateColumns = false;
            _categoryDataGridView.DataSource = _stickyPadData.GetCategoryList();
            _categoryDataGridView.ClearSelection();     //清除選取
            //綁定待辦清單datagridview的datasource
            _toDoListDataGridView.AutoGenerateColumns = false;
            _toDoListDataGridView.DataSource = _stickyPadData.GetToDo();
            //綁定新增按鈕上的文字
            const string CATEGORY_BUTTON_TEXT = "Text";
            const string CATEGORY_BUTTON_TEXT_IN_MODEL = "_newCategoryButtonText";
            _newCategory.DataBindings.Add(CATEGORY_BUTTON_TEXT, _stickyPadData, CATEGORY_BUTTON_TEXT_IN_MODEL, true);
            //綁定textbox上的內容(文字和顏色)
            const string CATEGORY_BOX_TEXT = "Text";
            const string CATEGORY_BOX_TEXT_IN_MODEL = "_categoryTextBoxText";
            _categoryBox.DataBindings.Add(CATEGORY_BOX_TEXT, _stickyPadData, CATEGORY_BOX_TEXT_IN_MODEL, true);
            const string COLOR_TEXT_BOX_BACKCOLOR = "Backcolor";
            const string CATEGORY_COLOR_IN_MODEL = "_categoryColor";
            _colorTextBox.DataBindings.Add(COLOR_TEXT_BOX_BACKCOLOR, _stickyPadData, CATEGORY_COLOR_IN_MODEL, true);
            //綁定新增待辦清單按鈕的狀態
            const string MESSAGE_ADDITION_ENABLE = "Enabled";
            const string NEW_STICKY_PAD_ENABLE = "_newStickyPadButtonEnable";
            _messageAddition.DataBindings.Add(MESSAGE_ADDITION_ENABLE, _stickyPadData, NEW_STICKY_PAD_ENABLE);
            //綁定事件
            _stickyPadData._callMessageBoxEvent += ConfirmDeleteCategory;
        }

        private void ClickExit(object sender, EventArgs e)      //當Exit被按下去之後
        {
            Application.Exit();                                               //關閉程式
        }

        private void ClickNewCategory(object sender, EventArgs e)       //按下新增類別按鈕的事件發生
        {
            SetCategory();      //設定類別
        }

        private void SetCategory()        //點擊新增或修改類別的按鈕
        {
            _stickyPadData.SetCategoryList(_categoryBox.Text, _colorTextBox.BackColor);     //利用model 的function把資料存到List中
            ClearData();                                                         //清除textbox中的資料
            SetCategoryDataGridViewColor();     //設定在datagridview中顯示顏色
        }

        void SetCategoryDataGridViewColor()     //顯示類別顏色
        {
            //顯示各類別的顏色
            for (int i = 0; i < _stickyPadData.GetCategoryList().Count; i++)
            {
                _categoryDataGridView.Rows[i].Cells[0].Style.BackColor = _stickyPadData.GetCategoryList()[i]._CategoryColor;
            }
            _categoryDataGridView.ClearSelection();     //清除選取
        }

        private void ChangeTextOfCategoryBox(object sender, EventArgs e)        //當有類別名稱輸入改變的時候，讓新增按建和取消按建enable
        {
            _textError.Clear();         //清除錯誤
            _textError.SetError(_categoryBox, _stickyPadData.CheckRepeat(_categoryBox.Text));
            CheckButtonsInCategoryPage();       //檢查讓類別頁面按鈕Enable的條件有沒有符合
        }

        public DialogResult ConfirmDeleteCategory(string category)      //確認是否要刪除類別
        {
            //messagebox的字串內容
            const string DELETE_WORD = "Are you sure to delete the category, ";
            const string QUESTION_MARK = " ?";
            const string WARN = "Warning";
            //確定使用者是否要刪除該類別
            return MessageBox.Show(DELETE_WORD + category + QUESTION_MARK, WARN, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        private void ClickColorTextBox(object sender, MouseEventArgs e)     //點擊_colorTextBox的事件
        {
            ColorDialog categoryColor = new ColorDialog();      //新增一個顏色選擇的視窗
            //點擊框框之後，可以修改顏色
            if (categoryColor.ShowDialog() == DialogResult.OK)
            {
                //回傳選擇顏色，並檢查讓新增取消按鈕enable的條件有沒有符合
                _colorTextBox.BackColor = categoryColor.Color;
                CheckButtonsInCategoryPage();
            }
        }

        private void CheckButtonsInCategoryPage()       //檢查類別頁面的按鈕狀態有無達到Enable的狀態
        {
            //讓PModel設定按鈕的狀態
            _stickyPadData.SetNewCategoryButtonAndCancelCategoryButton(_categoryBox.Text, _colorTextBox.BackColor, _textError.GetError(_categoryBox), _colorError.GetError(_colorTextBox));
            //讓View和Model同步
            _newCategory.Enabled = _stickyPadData.IsNewCategoryButtonEnable();
            _cancelButton.Enabled = _stickyPadData.IsCancelButtonEnable();
        }

        private void ClearColorTextBox(object sender, EventArgs e)      //禁止在_colorTestBox中輸入資料
        {
            _colorTextBox.Clear();
        }

        private void ClickCancelButton(object sender, EventArgs e)      //點擊取消類別輸入的按鈕，所以清除輸入的類別資料(包含顏色和文字)
        {
            ClearData();
        }

        private void ClearData()        //清除_categoryBox和_colorTextBox中的資料
        {
            _stickyPadData.LeaveEditMode();
            _categoryBox.Clear();
            _colorTextBox.BackColor = TextBox.DefaultBackColor;
            CheckButtonsInCategoryPage();       //檢查按鈕Eabled的條件有沒有達成
        }

        private void ClickAddToDoList(object sender, EventArgs e)       //點擊新增便條之後的觸發事件
        {
            _toDoError.Clear();     //先清掉之前的記錄
            //新增一個form，用來輸入待辦事項
            ToDoListAdditionWindows toDoListWindows = new ToDoListAdditionWindows(_stickyPadData.GetCategoryList());
            //新增到model中，看showdialog的結果，在model中判斷要不要新增便條內容
            string errorText = _stickyPadData.SetToDo(toDoListWindows.ShowDialog(), toDoListWindows.GetCategoryComboBox(), toDoListWindows.GetContentMessage());
            _toDoError.SetError(_messageAddition, errorText);
        }

        private void ClickToDoListDataGridViewCell(object sender, DataGridViewCellEventArgs e)      //點擊待辦清單的DataGridView所觸發事件
        {
            if (e.RowIndex == -1)       //如果點到標頭，不要執行function
            {
                return;
            }
            const int NEW_STICKY_PAD_FORM = 1;
            const int DELETE_STICKY_PAD = 2;
            const int EDIT_STICKY_PAD = 3;
            //暫存待辦事項的內容和類別
            string toDoListContent = _toDoListDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            string toDoListCategory = _toDoListDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            ToDo tempToDo = _stickyPadData.GetToDo(toDoListCategory, toDoListContent);      //暫時儲存待辦事項
            ToDoListWindows tempToDoForm = new ToDoListWindows(tempToDo);                   //暫時儲存待辦的視窗
            //藉由column index來判斷要做什麼
            if (e.ColumnIndex <= NEW_STICKY_PAD_FORM)     //如果是第0列或第1列，顯示待辦事項視窗
            {
                _stickyPadData.AddToDoListForm(tempToDoForm);
            }
            else if (e.ColumnIndex == DELETE_STICKY_PAD)        //如果是第二列，那就要刪除那待辦事項
            {
                _stickyPadData.DeleteToDo(toDoListCategory, toDoListContent, tempToDoForm);       //刪除待辦事項的資料和關閉視窗
            }
            else if (e.ColumnIndex == EDIT_STICKY_PAD)        //如果是第三列，那就要編輯那待辦事項
            {
                //新增一個form，用來輸入待辦事項
                ToDoListAdditionWindows _toDoListWindows = new ToDoListAdditionWindows(_stickyPadData.GetCategoryList(), _stickyPadData.GetToDo(toDoListCategory, toDoListContent));
                //新增到model中，看showdialog的結果，在model中判斷要不要新增便條內容
                _stickyPadData.EditToDo(_toDoListWindows.ShowDialog(), tempToDo, _stickyPadData.GetCategory(_toDoListWindows.GetCategoryComboBox()), _toDoListWindows.GetContentMessage());
            }
        }

        private void ClickCategoryDataGridViewButton(object sender, DataGridViewCellEventArgs e)      //點擊類別的DataGridView，刪除或修改該項類別
        {
            //把類別名稱暫存起來
            string tempCategory = _categoryDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            //呼叫Pmodel中的函式來進行刪除，讓model判斷是不是點到可以刪除的位置
            _stickyPadData.DeleteCategory(e.ColumnIndex, tempCategory);
            ClearData();        //清除資料欄的資料
            //呼教Pmodel中的函式來進行修改，讓Pmodel判斷是不是進入修改模式
            _stickyPadData.EditCategory(e.ColumnIndex, e.RowIndex);
            CheckButtonsInCategoryPage();       //檢查新增和取消按鈕狀態
            //重新設定類別的datagridview、待辦事項的datagridview和新增待辦事項的按鈕狀態
            SetCategoryDataGridViewColor();
        }

        //點到待辦清單的column header的時候，跳出錯誤視窗
        private void ClickToDoListColumnHeader(object sender, DataGridViewCellMouseEventArgs e)
        {
            //設定錯誤視窗要顯示的內容字串
            const string ERROR_STRING = "Sorry！You have clicked the header, where is banned to be clicked. \nPlease don't click the column header.";
            const string ERROR_HEADER = "Error";
            MessageBox.Show(ERROR_STRING, ERROR_HEADER, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SelectTabPageIndex(object sender, EventArgs e)     //選取不一樣的tabpage，把輸入模式改為新增模式
        {
            ClearData();
        }

        private void ChangedColorTextBox(object sender, EventArgs e)        //colorbox的背景顏色換的時候檢查有沒有重複的顏色
        {
            //檢查重複的顏色，有的話就顯示錯誤
            _colorError.Clear();
            _colorError.SetError(_colorTextBox, _stickyPadData.CheckRepeat(_colorTextBox.BackColor));
            CheckButtonsInCategoryPage();       //檢查讓類別頁面按鈕Enable的條件有沒有符合
        }

        private void ClosedStickyPad(object sender, FormClosedEventArgs e)       //最後要關閉視窗時，儲存文字資料
        {
            const string CATEGORY_PATH = "../../Data/Category.txt";
            StreamWriter categoryWriter = new StreamWriter(CATEGORY_PATH, false);       //類別資料的輸出
            _stickyPadData.WriteCategoryData(categoryWriter);     //輸入資料到檔案中
            categoryWriter.Close();
            const string CONTENT_PATH = "../../Data/Content.txt";
            StreamWriter contentWriter = new StreamWriter(CONTENT_PATH, false);     //開啟檔案
            _stickyPadData.WriteContentData(contentWriter);      //輸入資料到檔案中
            contentWriter.Close();
        }

        private void LoadDataFromTxt(object sender, EventArgs e)        //載入資料
        {
            const string PATH_CATEOGRY = "../../Data/Category.txt";     //類別資料的路徑
            _stickyPadData.ReadCategoryData(PATH_CATEOGRY, false, string.Empty);      //載入類別資料和顯示顏色
            SetCategoryDataGridViewColor();
            //輸入待辦清單資料
            const string PATH_TO_DO = "../../Data/Content.txt";         //清單資料的路徑
            _stickyPadData.ReadContentData(PATH_TO_DO, false, string.Empty);
            //讀取視窗資料
            const string FORM_PATH = "../../Data/Form.txt";
            if (!File.Exists(FORM_PATH))     //如果路徑不存在，直接回傳
            {
                return;
            }
            StreamReader formReader = new StreamReader(FORM_PATH);       //讀取檔案，並進行切割
            string data = formReader.ReadToEnd();
            const string SEPARATOR = " \n ";
            string[] seperator = { SEPARATOR };                        //切割的字串
            string[] formInformation = data.Split(seperator, StringSplitOptions.RemoveEmptyEntries);        //切割好的字串集合
            for (int i = 0; i < formInformation.Length; )        //讀取每一筆資料
            {
                //開啟視窗
                ToDo tempToDo = _stickyPadData.GetToDo(formInformation[i++], formInformation[i++]);
                ToDoListWindows toDoWindows = new ToDoListWindows(tempToDo);
                _stickyPadData.AddToDoListForm(toDoWindows);        //新增視窗到管理視窗的model
            }
            formReader.Close();
        }

        private void ClosingStickyPad(object sender, FormClosingEventArgs e)       //即將關閉程式，儲存視窗資料
        {
            const string FORM_PATH = "../../Data/Form.txt";
            StreamWriter formWriter = new StreamWriter(FORM_PATH, false);       //開啟檔案
            //寫資料進去
            _stickyPadData.WriteFormData(formWriter);
            formWriter.Close();
        }
    }
}
