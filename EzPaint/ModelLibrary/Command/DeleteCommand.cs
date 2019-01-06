using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    class DeleteCommand : ICommand
    {
        Model _model;
        List<Shape> _deleteShape;
        //儲存各個圖形的index
        List<int> _indexList;

        /// <summary>
        /// 初始化
        /// </summary>
        public DeleteCommand(Model model, List<Shape> beSelected)
        {
            _model = model;
            _indexList = new List<int>();
            _indexList.Clear();
            _deleteShape = new List<Shape>(beSelected);
        }

        /// <summary>
        /// 執行DeleteCommand
        /// </summary>
        public void Excute()
        {
            _model.DeletePartOfShape(_deleteShape, _indexList);
        }

        /// <summary>
        /// 反執行
        /// </summary>
        public void UnExcute()
        {
            _model.AddSomeShape(_deleteShape, _indexList);
        }
    }
}
