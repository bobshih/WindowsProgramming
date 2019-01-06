using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;

namespace StickyPadForm.Model
{
    public class ToDo : INotifyPropertyChanged      //單一待辦事項的class
    {
        public event PropertyChangedEventHandler PropertyChanged;       //屬性變更
        public event ToDoChangedEventHandler _toDoChanged;              //自己發生變更的事件
        public delegate void ToDoChangedEventHandler();                 //委派任務
        Category _category;        //類別的索引
        string _content;        //待辦清單的內容

        public ToDo(Category category, string content)       //有參數的建構子
        {
            _category = category;
            _content = content;
        }

        public Category _Category     //對外類別的設定和回傳
        {
            set
            {
                _category = value;
                //本身發生改變，所以呼叫事件
                if (_toDoChanged != null)
                {
                    _toDoChanged();
                }
                //因為類別改變，所以類別名稱和顏色也都跟著改變，因此也要呼叫另外兩個屬性變更
                const string CATEGORY = "_Category";
                NotifyPropertyChanged(CATEGORY);
                const string CATEGORY_TEXT = "_CategoryText";
                NotifyPropertyChanged(CATEGORY_TEXT);
                const string CATEGORY_COLOR_FORM = "_CategoryColorForm";
                NotifyPropertyChanged(CATEGORY_COLOR_FORM);
            }
            get
            {
                return _category;
            }
        }

        public Color _CategoryColor     //對外回傳類別的顏色
        {
            get
            {
                return _category._CategoryColor;
            }
        }

        public string _CategoryText     //對外回傳類別的名稱
        {
            get
            {
                return _category._CategoryName;
            }
        }

        public string _Content     //對外清單內容的設定和回傳
        {
            set
            {
                _content = value;
                const string CONTENT = "_Content";
                NotifyPropertyChanged(CONTENT);
            }
            get
            {
                return _content;
            }
        }

        void NotifyPropertyChanged(string property)     //通知屬性變更
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
