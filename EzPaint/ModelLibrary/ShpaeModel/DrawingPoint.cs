using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    /// <summary>
    /// 用來儲存畫圖中的點
    /// </summary>
    public class DrawingPoint
    {
        //X座標
        int _x;
        //Y座標
        int _y;

        /// <summary>
        /// 初始化
        /// </summary>
        public DrawingPoint(int pointX, int pointY)
        {
            _x = pointX;
            _y = pointY;
        }

        /// <summary>
        /// 取得X軸座標
        /// </summary>
        public int GetXCoordinate()
        {
            return _x;
        }

        /// <summary>
        /// 取得Y軸座標
        /// </summary>
        public int GetYCoordinate()
        {
            return _y;
        }
    }
}
