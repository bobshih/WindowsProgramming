using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    class ClearCommand : ICommand
    {
        Model _model;
        //儲存被移除掉的形狀
        List<Shape> _shapes;

        /// <summary>
        /// 建構子
        /// </summary>
        public ClearCommand(Model model)
        {
            _model = model;
            _shapes = new List<Shape>();
        }

        /// <summary>
        /// 執行清除的命令
        /// </summary>
        public void Excute()
        {
            List<Shape> tempList = _model.GetAllShapes();
            //儲存每個形狀
            foreach (Shape shape in tempList)
            {
                _shapes.Add(shape);
            }
            //再刪除
            _model.DeleteAll();
        }

        /// <summary>
        /// 反執行
        /// </summary>
        public void UnExcute()
        {
            foreach (Shape shape in _shapes)
            {
                _model.AddShape(shape);
            }
        }
    }
}
