using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StickyPadForm.Model
{
    public class AdditionWindowModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;       //實做介面

        string _comboBoxString;     //儲存combobox中的字串
        string _contentString;      //儲存便條內容的字串

        public bool _AdditionButtonEnable       //對外設定增加便條的按鈕是否能啟動
        {
            get{
                return !_comboBoxString.Equals("") && !_contentString.Equals("");
            }
        }

        public AdditionWindowModel()        //default constructor
        {
            _contentString = string.Empty;
            _comboBoxString = string.Empty;
        }

        public void SetContentString(string content)        //設定類別內容
        {       
            _contentString = content;
            const string PROPERTY_ADDITION_BUTTON_ENABLE = "_AdditionButtonEnable";
            Notify(PROPERTY_ADDITION_BUTTON_ENABLE);
        }

        public void SetComboBoxString(string comboBoxText)      //設定便條內容
        {
            _comboBoxString = comboBoxText;
            const string PROPERTY_ADDITION_BUTTON_ENABLE = "_AdditionButtonEnable";
            Notify(PROPERTY_ADDITION_BUTTON_ENABLE);
        }

        void Notify(string property)        //呼叫屬性便更
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
