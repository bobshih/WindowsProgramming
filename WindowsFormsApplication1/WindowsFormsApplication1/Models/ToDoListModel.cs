using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StickyPadForm.Model
{
    public class ToDoListModel
    {
        private BindingList<ToDo> _toDoList;      //儲存待辦事項
        //private ToDoListFormCollectionModel _formModel;     //管理待辦事像視窗的Model

        public ToDoListModel(/*ToDoListFormCollectionModel formModel*/)      //default constructor
        {
            //新增List
            _toDoList = new BindingList<ToDo>();
            _toDoList.Clear();
            //設定管理視窗Model的reference
            //_formModel = formModel;
        }

        public string SetToDoList(ToDo input)        //輸入待辦事項
        {
            //檢查是否已經有一樣的類別和內容的待辦事項
            for (int i = 0; i < _toDoList.Count; i++)
            {
                if (_toDoList[i]._Content == input._Content && _toDoList[i]._CategoryText == input._CategoryText)
                {
                    const string REPEAT_ERROR = "已經有一個相同類別和相同內容的待辦事項囉，不需要重複輸入喔！";
                    return REPEAT_ERROR;
                }
            }
            //no repeat, then add both of them
            _toDoList.Add(input);
            return string.Empty;
        }

        public BindingList<ToDo> GetToDoList()     //回傳待辦事項清單
        {
            return _toDoList;
        }

        public void RemoveCategory(string category)     //移除某一類別的待辦事項，利用for迴圈尋找有相同類別名稱的待辦事項
        {
            for (int i = 0; i < _toDoList.Count; i++)
            {
                if (_toDoList[i]._Category._CategoryName == category)     //如果是要移除的名稱相符合，就進行移除
                {
                    _toDoList.Remove(_toDoList[i]);
                    i--;        //因為少了一項，所以做為index的i要減1，回到前一項
                }
            }
        }

        public ToDo GetToDo(string category, string content)        //回傳對應的待辦事項，在有類別和內容的情況下
        {
            int i = 0;      //做為迴圈索引值
            for (i = 0; i < _toDoList.Count; i++)       //尋找對應的清單事項
            {
                if (_toDoList[i]._Category._CategoryName == category && _toDoList[i]._Content == content)
                {
                    break;
                }
            }
            return _toDoList[i];        //回傳找到的值
        }

        public void EditToDo(ToDo beEditedOne, Category cate, string content)      //修改待辦事項的內容
        {
            beEditedOne._Category = cate;
            beEditedOne._Content = content;
        }

        public void RemoveToDo(string toDoListCategory, string toDoListContent)      //移除待辦事項清單
        {
            for (int i = 0; i < _toDoList.Count; i++)       //尋找相符合的待辦事項
            {
                //相同的類別名稱和清單內容，進行刪除
                if (_toDoList[i]._Category._CategoryName == toDoListCategory && _toDoList[i]._Content == toDoListContent)
                {
                    _toDoList.Remove(_toDoList[i]);
                    break;
                }
            }
        }

        public bool FindToDo(string toDoCategory, string content)     //尋找List中有沒有對應的待辦事項
        {
            for (int i = 0; i < _toDoList.Count; i++)
            {
                if (_toDoList[i]._CategoryText == toDoCategory && _toDoList[i]._Content == content)
                {
                    return true;        //找到了
                }
            }
            return false;       //沒找到...
        }
    }
}
