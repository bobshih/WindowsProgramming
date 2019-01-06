using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    class DrawingCommand : ICommand
    {
        Shape _drawingShape;
        Model _model;

        /// <summary>
        /// 初始化
        /// </summary>
        public DrawingCommand(Model model, Shape shape)
        {
            _drawingShape = shape;
            _model = model;
        }

        /// <summary>
        /// 執行命令
        /// </summary>
        public void Excute()
        {
            _model.AddShape(_drawingShape);
        }

        /// <summary>
        /// 反執行命令
        /// </summary>
        public void UnExcute()
        {
            _model.DeleteShape();
        }
    }
}
