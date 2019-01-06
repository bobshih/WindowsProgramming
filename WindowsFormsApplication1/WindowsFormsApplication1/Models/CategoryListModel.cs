using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace StickyPadForm.Model
{
    public class CategoryListModel     //類別List的MODEL
    {
        private BindingList<Category> _categoryList = new BindingList<Category>();     //類別的名稱和顏色List

        public CategoryListModel()      //default constructor
        {
            _categoryList.Clear();
        }

        public void SetCategory(string categoryInput, Color categoryColor)       //輸入類別的名稱和顏色
        {
            //加到List
            _categoryList.Add(new Category(categoryInput, categoryColor));
        }

        public void SetCategory(int categoryID, string category, Color categoryColor)       //修改模式下的SetCategory，多一個數字的參數
        {
            //修改內容
            _categoryList[categoryID]._CategoryColor = categoryColor;
            _categoryList[categoryID]._CategoryName = category;
        }

        public Category GetCategory(string category)        //取得類別資料
        {
            int i = 0;      //迴圈索引值
            for (; i < _categoryList.Count; i++)        //尋找中
            {
                if (_categoryList[i]._CategoryName == category)
                {
                    break;
                }
            }
            return _categoryList[i];        //回傳找到的值
        }

        public BindingList<Category> GetCategoryList()       //回傳類別的List
        {
            return _categoryList;
        }

        public bool FindCategory(string categoryName)       //尋找輸入的類別存不存在
        {
            for (int i = 0; i < _categoryList.Count; i++)
            {
                if (_categoryList[i]._CategoryName == categoryName)     //找到對應的類別
                {
                    return true;
                }
            }
            return false;       //沒找到對應的類別，回傳false
        }

        public void RemoveCategory(string categoryName)     //移除某一類別
        {
            for (int i = 0; i < _categoryList.Count; i++)
            {
                if (_categoryList[i]._CategoryName.Equals(categoryName))      //找到要被刪除的類別，因為顏色的INDEX和類別名稱相同，所以一並刪除
                {
                    _categoryList.RemoveAt(i);
                    break;
                }
            }
        }

        public Color GetMappingColor(string categoryName)       //回傳對應類別名稱的顏色
        {
            int i;      //做為for迴圈的索引值
            for (i = 0; i < _categoryList.Count; i++)
            {
                //判斷categoryName有沒有和List中的名稱一樣
                if (categoryName == _categoryList[i]._CategoryName)
                {
                    break;
                }
            }
            return _categoryList[i]._CategoryColor;       //回傳顏色
        }
    }
}
