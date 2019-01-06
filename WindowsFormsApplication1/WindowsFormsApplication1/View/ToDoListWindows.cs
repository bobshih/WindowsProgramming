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
    public partial class ToDoListWindows : Form     //顯示待辦事項內容的視窗
    {
        public event CloseFormEventHandler _closeForm;       //當有視窗背關閉的時候
        public delegate void CloseFormEventHandler(ToDoListWindows closeForm);
        private ToDo _toDo;     //儲存待辦事項的reference
        private Category _cate;     //該待辦事項的參照

        public ToDoListWindows(ToDo toDoList)     //建構子，設定是否開啟該待辦清單內容
        {
            InitializeComponent();
            _toDo = toDoList;
            _cate = _toDo._Category;
            //綁定backcolor和清單內容
            this.DataBindings.Add("BackColor", _cate, "_categoryColor");
            _contentTextBox.DataBindings.Add("Text", _toDo, "_content");
            //設定待辦事項內容被變更時的呼叫函式
            _toDo._toDoChanged += CheckBackColor;
        }

        private void CheckBackColor()       //檢查背景顏色
        {
            if (_toDo._CategoryColor != BackColor)      //如果背景顏色和待辦事項的類別顏色不一樣，進行update
            {
                BackColor = _toDo._CategoryColor;
                _cate = _toDo._Category;
                //清除databinding，重新綁定backcolor
                this.DataBindings.Clear();
                this.DataBindings.Add("BackColor", _cate, "_categoryColor");
            }
        }

        public ToDo GetToDo()       //回傳待辦事項
        {
            return _toDo;
        }

        private void CloseToDoListWindows(object sender, FormClosedEventArgs e)     //偵測視窗是不是要關閉了
        {
            //呼叫關閉程式的function
            if (_closeForm != null) 
            {
                _closeForm(this);
            }
        }
    }
}
