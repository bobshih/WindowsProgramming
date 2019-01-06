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

namespace StickyPadForm.Model
{
    public class ToDoListFormCollectionModel        //管理待辦事項視窗的model
    {
        private List<ToDoListWindows> _toDoFormList;
        public int _Count        //回傳現在List的大小
        {
            get
            {
                return _toDoFormList.Count;
            }
        }

        public ToDoListFormCollectionModel()        //建構子，new 一個List
        {
            _toDoFormList = new List<ToDoListWindows>();
            _toDoFormList.Clear();
        }

        public void Add(ToDoListWindows newForm)        //新增把視窗丟到List裡
        {
            if (FindToDoListWindows(newForm) == null)       //如果沒有在List中找到視窗，就加進來
            {
                _toDoFormList.Add(newForm);
                newForm._closeForm += Remove;       //當視窗被關閉的時候，移除掉List中對應的元素
                //顯示視窗
                newForm.Show();
                newForm.Activate();
            }
            else        //視窗是已經開啟的狀態
            {
                FindToDoListWindows(newForm).Activate();        //顯示視窗到最上層
            }
        }

        public ToDoListWindows FindToDoListWindows(ToDoListWindows findWindows)     //尋找視窗有沒有開啟的函式
        {
            for (int i = 0; i < _toDoFormList.Count; i++)       //for迴圈搜尋
            {
                if (_toDoFormList[i].GetToDo() == findWindows.GetToDo())
                {
                    return _toDoFormList[i];
                }
            }
            return null;        //沒有找到，回傳null
        }

        public void Remove(ToDoListWindows deleteForm)      //當視窗被關閉的時候，呼叫這個function移除List中的東西
        {
            _toDoFormList.Remove(deleteForm);
        }

        public void WriteData(StreamWriter formWriter)      //把資料寫進檔案中
        {
            const string SEPARATOR = " \n ";
            for (int i = 0; i < _toDoFormList.Count; i++)   //寫入所有資料
            {
                ToDo formToDo = _toDoFormList[i].GetToDo();
                formWriter.Write(formToDo._CategoryText + SEPARATOR + formToDo._Content + SEPARATOR);
            }
        }

        public void DeleteToDoForm(ToDoListWindows deleteToDo)      //當刪除待辦事項的按鈕按下去之後，刪除該對應的視窗
        {
            if (FindToDoListWindows(deleteToDo) == null)        //如果視窗沒有開啟的話，就別理他
            {
                return;
            }
            FindToDoListWindows(deleteToDo).Close();
        }

        public void DeleteCategory(string categoryName)     //移除某一類別的所有待辦事項是窗
        {
            for (int i = 0; i < _toDoFormList.Count; i++)
            {
                ToDo toDo = _toDoFormList[i].GetToDo();     //暫時儲存在form裡面的待辦事項
                if (toDo._CategoryText == categoryName)     //如果待辦事項的類別是要刪除的類別就刪掉
                {
                    _toDoFormList[i].Close();
                    i--;        //因為少一項了，所以要減1
                }
            }
        }
    }
}
