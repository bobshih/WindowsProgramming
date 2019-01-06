using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace StickyPadForm.Model
{
    public class StickyPadPresentionModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;       //實作INotifyPropertyChanged
        public event CallMessageBoxEventHandler _callMessageBoxEvent;   //呼叫mseeagebox的事件
        public delegate DialogResult CallMessageBoxEventHandler(string categoryName);

        #region private variables

        private CategoryListModel _category;                //類別的資料
        private ToDoListModel _toDo;                        //待辦事項的資料
        private ToDoListFormCollectionModel _formList;      //待辦事項視窗的資料
        private bool _newCategotyButtonEnable;              //儲存新增類別按鈕的狀態
        private bool _cancelCategoryButtonEbable;           //儲存取消類別新增按鈕的狀態
        private string _newCategoryButtonText;              //儲存新增類別按鈕上的text
        private string _categoryTextBoxText;                //儲存累textbox上的內容
        private Color _categoryColor;                       //儲存在color textbox上的背景顏色
        private int _editCategoryID;                        //儲存要被修改的類別索引值
        const string EDIT_CATEGORY_TEXT = "修改";           //修改模式下的按紐文字
        const string NEW_CATEGORY_TEXT = "新增";            //新增模式下的按紐文字

        #endregion

        #region public variables that are used on the databind

        public string _NewCategoryButtonText        //對外的回傳變數
        {
            get
            {
                return _newCategoryButtonText;
            }
        }

        public string _CategoryTextBoxText          //對外的回傳變數
        {
            get
            {
                return _categoryTextBoxText;
            }
        }

        public Color _CategoryColor                 //對外的回傳變數
        {
            get
            {
                return _categoryColor;
            }
        }

        public bool _NewStickyPadButtonEnable       //對外的回傳變數，這變數用在檢查新增便條的按鈕是否啟用
        {
            get
            {
                return _category.GetCategoryList().Count != 0;      //如果類別List中的數量不是 0 就回傳true，反之回傳false
            }
        }

        #endregion

        public StickyPadPresentionModel()      //default constcuctor
        {
            _category = new CategoryListModel();
            _toDo = new ToDoListModel();
            _formList = new ToDoListFormCollectionModel();
            _newCategotyButtonEnable = _cancelCategoryButtonEbable = false;     //按鈕屬性初始化為unable
            _newCategoryButtonText = NEW_CATEGORY_TEXT;    //一開始按鈕上是新增的字
            _editCategoryID = 0;                           //初始為 0
            _categoryColor = TextBox.DefaultBackColor;     //初始為 default color
        }

        public string CheckRepeat(string category)     //檢查有沒有重複的類別名稱
        {
            BindingList<Category> tempList = _category.GetCategoryList();       //載入類別的List
            for (int i = 0; i < tempList.Count; i++)        //用for迴圈去跑，檢查有沒有重複的項目
            {
                if (tempList[i]._CategoryName == category)      //檢查一樣的類別名稱，有就回傳錯誤訊息
                {
                    if (_newCategoryButtonText == "修改" && i == _editCategoryID)     //如果在修改模式之下，而且索引值i和要修改的類別ID相同，表示可以忽略這個相同
                    {
                        continue;
                    }
                    const string REPEAT_ERROR = "類別名稱重複了喔~~";
                    return REPEAT_ERROR;
                }
            }
            return string.Empty;       //沒有任何重複，回傳空字串
        }

        public string CheckRepeat(Color categorColor)     //檢查類別顏色有沒有重複
        {
            BindingList<Category> tempList = _category.GetCategoryList();       //載入類別的List
            for (int i = 0; i < tempList.Count; i++)        //用for迴圈去跑，檢查有沒有重複的項目
            {
                if (tempList[i]._CategoryColor.ToArgb() == categorColor.ToArgb())      //檢查一樣的類別顏色，有就回傳錯誤訊息
                {
                    if (_newCategoryButtonText == "修改" && i == _editCategoryID)     //如果在修改模式之下，而且索引值i和要修改的類別ID相同，表示可以忽略這個相同
                    {
                        continue;
                    }
                    const string REPEAT_ERROR = "類別顏色重複了喔~~";
                    return REPEAT_ERROR;
                }
            }
            return string.Empty;       //沒有任何重複，回傳""
        }

        public void SetCategoryList(string category, Color categoryColor)       //把字串加到類別的list中
        {
            if (/*_modeInCategoryTabPage == true*/_newCategoryButtonText == NEW_CATEGORY_TEXT)     //在新增的模式下
            {
                _category.SetCategory(category, categoryColor);
            }
            else            //在修改模式之下
            {
                //修改回新增模式
                LeaveEditMode();
                //修改內容
                _category.SetCategory(_editCategoryID, category, categoryColor);
            }
            const string NOTIFY_NEW_STICKY_PAD_BUTTON_ENABLE = "_NewStickyPadButtonEnable";
            Notify(NOTIFY_NEW_STICKY_PAD_BUTTON_ENABLE);        //新增便條按鈕的狀態可能已經改變
        }

        public BindingList<Category> GetCategoryList()             //回傳類別名稱的list給view
        {
            return _category.GetCategoryList();
        }

        public void LeaveEditMode()     //離開修改模式，回到新增模式
        {
            if (/*_modeInCategoryTabPage == false*/_newCategoryButtonText == EDIT_CATEGORY_TEXT)
            {
                //設定模式和按鈕上的文字
                //_modeInCategoryTabPage = true;
                _newCategoryButtonText = NEW_CATEGORY_TEXT;
                const string NEW_CATEGORY_BUTTON = "_newCategoryButtonText";
                Notify(NEW_CATEGORY_BUTTON);
                //清除之前記錄
                _categoryTextBoxText = string.Empty;
                _categoryColor = TextBox.DefaultBackColor;
            }
        }

        public void EditCategory(int columnIndex, int rowIndex)       //進入修改類別模式
        {
            const int EDIT_COLUMN_INDEX = 3;
            if (columnIndex != EDIT_COLUMN_INDEX)       //判斷點擊的Column Index是不是edit button 的那一列
            {
                return;
            }
            //設定模式和按鈕上的文字
            _newCategoryButtonText = EDIT_CATEGORY_TEXT;
            const string NEW_CATEGORY_BUTTON = "_newCategoryButtonText";
            Notify(NEW_CATEGORY_BUTTON);
            //把類別內容丟到textbox上顯示，等著被修改
            BindingList<Category> tempCategoryList = _category.GetCategoryList();       //暫存類別List
            _editCategoryID = rowIndex;             //設定修改的類別ID
            _categoryTextBoxText = tempCategoryList[rowIndex]._CategoryName;        //設定綁定view的屬性
            const string CATEGORY_TEXT_BOX_TEXT = "_categoryTextBoxText";
            Notify(CATEGORY_TEXT_BOX_TEXT);         //通知變更
            _categoryColor = tempCategoryList[rowIndex]._CategoryColor;             //設定綁定view的屬性
            const string CATEGORY_COLOR = "_categoryColor";
            Notify(CATEGORY_COLOR);               //通知變更
        }

        public void DeleteCategory(int columnIndex, string deleteName)      //刪除選中的類別
        {
            const int DELETE_COLUMN_INDEX = 2;
            //判斷點擊的Column Index是不是delete button的那一列
            if (columnIndex != DELETE_COLUMN_INDEX)
            {
                return;
            }
            //呼叫call messagebox的事件繼承者，確認使用者的要求
            if (_callMessageBoxEvent != null && _callMessageBoxEvent(deleteName) == DialogResult.Yes)
            {
                //先刪除待辦事項清單中相同類別的待辦事項
                _toDo.RemoveCategory(deleteName);
                //然後刪除視窗中相同類別的待辦事項
                _formList.DeleteCategory(deleteName);
                //接著刪除類別清單中的類別
                _category.RemoveCategory(deleteName);
            }
            const string NOTIFY_NEW_STICKY_PAD_BUTTON_ENABLE = "_NewStickyPadButtonEnable";
            Notify(NOTIFY_NEW_STICKY_PAD_BUTTON_ENABLE);        //新增便條按鈕的狀態可能已經改變
        }

        public void AddToDoListForm(ToDoListWindows newForm)        //新增待辦事項的視窗
        {
            _formList.Add(newForm);
        }

        public int GetToDoListFormCount()       //回傳視窗的數量
        {
            return _formList._Count;
        }

        public string SetToDo(DialogResult result, string category, string content)        //設定待辦清單
        {
            if (result == DialogResult.OK)      //如果回傳的result是OK，就新增過去
            {
                BindingList<Category> tempCategoryList = _category.GetCategoryList();       //把類別List暫存起來比較方便使用
                for (int i = 0; i < tempCategoryList.Count; i++)        //i是類別清單中的索引值
                {
                    if (tempCategoryList[i]._CategoryName == category)      //尋找該類別名稱的類別(包含名稱和顏色)
                    {
                        return _toDo.SetToDoList(new ToDo(tempCategoryList[i], content));        //設定參照reference
                    }
                }
            }
            return string.Empty;
        }

        public BindingList<ToDo> GetToDo()     //回傳待辦清單的List
        {
            return _toDo.GetToDoList();
        }

        //設定在類別頁面的新增取消按鈕
        public void SetNewCategoryButtonAndCancelCategoryButton(string categoryText, Color categoryColor, string textError, string colorError)
        {
            bool error = textError.Equals("") && colorError.Equals("");      //檢查是不是兩個錯誤訊息都是空字串
            //新增按鈕的啟動和類別名稱、類別顏色、與錯誤訊息的存在有關
            _newCategotyButtonEnable = !categoryText.Equals("") && !categoryColor.Equals(TextBox.DefaultBackColor) && error;
            //取消按鈕的啟動與否則是看類別名稱和類別顏色有沒有東西可以被清除
            _cancelCategoryButtonEbable = !categoryColor.Equals(TextBox.DefaultBackColor) || !categoryText.Equals("");
        }

        public Category GetCategory(string categoryName)        //回傳一個類別
        {
            BindingList<Category> tempCateoryList = _category.GetCategoryList();        //設定暫存的類別List
            int i;      //作為索引值
            for (i = 0; i < tempCateoryList.Count; i++)
            {
                if (tempCateoryList[i]._CategoryName == categoryName)
                    break;
            }
            return tempCateoryList[i];      //回傳那項類別
        }

        public bool IsNewCategoryButtonEnable()     //回傳新增類別按鈕的狀態
        {
            return _newCategotyButtonEnable;
        }

        public bool IsCancelButtonEnable()      //回傳取消類別新增按鈕的狀態
        {
            return _cancelCategoryButtonEbable;
        }

        public ToDo GetToDo(string toDoCategory, string toDoContent)        //回傳由書入類別名稱和待辦內容構成的待辦事項
        {
            return _toDo.GetToDo(toDoCategory, toDoContent);
        }

        public void DeleteToDo(string toDoCategory, string toDoContent, ToDoListWindows toDoForm)     //刪除待辦事項，和對應的視窗
        {
            _formList.DeleteToDoForm(toDoForm);     //刪除視窗
            _toDo.RemoveToDo(toDoCategory, toDoContent);        //刪除待辦事項
        }

        public void WriteFormData(StreamWriter formWriter)      //輸出form的資料
        {
            _formList.WriteData(formWriter);
        }

        public void EditToDo(DialogResult result, ToDo beEdited, Category category, string content)
        {
            if (result == DialogResult.OK)      //如果回傳的結果是OK，就修改資料
            {
                _toDo.EditToDo(beEdited, category, content);
            }
        }

        public void WriteContentData(StreamWriter contentWriter)      //寫入便條資料
        {
            const string SEPARATOR = " \n ";
            BindingList<ToDo> tempContent = _toDo.GetToDoList();        //暫時儲存資料
            for (int i = 0; i < tempContent.Count; i++)     //把便條資料輸入至檔案
            {
                string category = tempContent[i]._CategoryText;     //便條的類別名稱
                string content = tempContent[i]._Content;           //便條內容
                contentWriter.Write(category + SEPARATOR + content + SEPARATOR);      //加上區分符號，輸入
            }
        }

        public void WriteCategoryData(StreamWriter categoryWriter)     //寫入類別資料
        {
            const string SEPARATOR = "\n";
            BindingList<Category> tempCategory = _category.GetCategoryList();       //暫時儲存資料
            for (int i = 0; i < tempCategory.Count; i++)     //輸入類別資料
            {
                Color categoryColor = tempCategory[i]._CategoryColor;
                int red = categoryColor.R;      //儲存顏色的各項數值
                int green = categoryColor.G;
                int blue = categoryColor.B;
                int alpha = categoryColor.A;
                categoryWriter.Write(tempCategory[i]._CategoryName + SEPARATOR + alpha.ToString() + SEPARATOR + red.ToString() + SEPARATOR + green.ToString() + SEPARATOR + blue.ToString() + SEPARATOR);
            }
        }

        public void ReadCategoryData(string path, bool unitTest, string unitTestInput)      //讀取類別資料
        {
            if (!File.Exists(path) || (unitTest && unitTestInput.Equals("")))     //如果檔案不存在，直接回傳
            {
                return;
            }
            //檔案存在，檔案開啟，把資料存到string上
            if (unitTest == false)
            {
                StreamReader categoryReader = new StreamReader(path);
                LoadCategoryData(categoryReader.ReadToEnd());
                categoryReader.Close();
            }
            else        //單元測試用的
            {
                LoadCategoryData(unitTestInput);
            }
        }

        private void LoadCategoryData(string categoryData)      //處理類別內容
        {
            const char SEPARATOR = '\n';
            char[] separator = { SEPARATOR };     //分隔字元
            string[] cateInformation = categoryData.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cateInformation.Length; )       //讀資料
            {
                string category = cateInformation[i++];     //類別名稱
                int alpha = Convert.ToInt32(cateInformation[i++]);      //類別顏色的RGBA屬性
                int red = Convert.ToInt32(cateInformation[i++]);
                int green = Convert.ToInt32(cateInformation[i++]);
                int blue = Convert.ToInt32(cateInformation[i++]);
                Color categoryColor = Color.FromArgb(alpha, red, green, blue);      //產生顏色
                _category.SetCategory(category, categoryColor);     //輸入資料
            }
        }

        public void ReadContentData(string path, bool unitTest, string unitTestInput)       //讀取清單資料
        {
            if (!File.Exists(path) || (unitTest && unitTestInput.Equals("")))     //如果檔案不存在，直接回傳，結束函式
            {
                return;
            }
            //如果檔案存在，開啟檔案，並把資料存到string中
            if (unitTest == false)
            {
                StreamReader contentReader = new StreamReader(path);
                string data = contentReader.ReadToEnd();
                LoadContentData(data);
                contentReader.Close();
                //File.Copy(path,Path.GetDirectoryName( Application.ExecutablePath)+"/M.TXT", true);
            }
            else        //單元測試用h
            {
                LoadContentData(unitTestInput);
            }   
        }

        private void LoadContentData(string contentData)         //處理待辦事項的內容
        {
            const string SEPAEATOR = " \n ";
            string[] separator = new string[] { SEPAEATOR };     //字串形態的分隔
            string[] contentInformation = contentData.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < contentInformation.Length; )      //讀資料
            {   
                string category = contentInformation[i++];
                string content = contentInformation[i++];
                SetToDo(DialogResult.OK, category, content);        //輸入到視窗中
            }
        }

        void Notify(string property)        //屬性變更通知
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
