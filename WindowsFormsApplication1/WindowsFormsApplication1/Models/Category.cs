using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace StickyPadForm.Model
{
    public class Category : INotifyPropertyChanged      //類別的Model，儲存名稱和顏色
    {
        public event PropertyChangedEventHandler PropertyChanged;       //實作INotifyPropertyChanged
        private string _categoryName;       //類別名稱
        private Color _categoryColor;       //類別的顏色

        public Category(string name, Color color)       //建構子，有參數的
        {
            _categoryName = name;
            _categoryColor = color;
        }

        public string _CategoryName     //對外類別名稱的設定和回傳
        {
            set
            {
                _categoryName = value;
                const string CATEGORY_NAME = "_CategoryName";
                NotifyPropertyChanged(CATEGORY_NAME);
            }
            get
            {
                return _categoryName;
            }
        }

        public Color _CategoryColor     //對外顏色屬性的設定和回傳
        {
            set
            {
                _categoryColor = value;
                const string CATEGORY_COLOR = "_CategoryColor";
                NotifyPropertyChanged(CATEGORY_COLOR);
            }
            get
            {
                return _categoryColor;
            }
        }

        public void NotifyPropertyChanged(string property)     //通知屬性變更
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
