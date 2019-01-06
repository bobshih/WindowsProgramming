using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    class Ellipse : Shape
    {
        //橢圓的兩個焦點
        double[] _focus1, _focus2;
        //a 是橢圓的半長軸，b 是橢圓的半短軸，c 是橢圓的半焦距(這絕對不是我亂命名，從高中開始都是用a, b, c來當作代表)
        double _a, _b, _c;
        //儲存該橢圓是不是橫的橢圓
        bool _cross; 

        /// <summary>
        /// 初始化
        /// </summary>
        public Ellipse(DrawingPoint firstPoint) : base(firstPoint)
        {
            _focus1 = new double[2];
            _focus2 = new double[2];
        }

        /// <summary>
        /// 設定第二個點，，這時候兩個焦點就決定好了，所以在這兒計算兩焦點的位置
        /// </summary>
        public override void SetSecondPoint(DrawingPoint secondPoint)
        {
            base.SetSecondPoint(secondPoint);
            int x1 = GetFirstPoint().GetXCoordinate();
            int y1 = GetFirstPoint().GetYCoordinate();
            int x2 = GetSecondPoint().GetXCoordinate();
            int y2 = GetSecondPoint().GetYCoordinate();
            DrawingPoint midPoint = new DrawingPoint((x1 + x2), (y1 + y2));
            //判斷橢圓往哪邊長
            if (Math.Abs(x1 - x2) >= Math.Abs(y1 - y2))
            {
                _cross = true;
            }
            else
            {
                _cross = false;
            }
            //計算a, b, c(半長軸、半短軸、半焦距)
            if (_cross)
            {
                _a = (double)Math.Abs(x1 - x2) / 2;
                _b = (double)Math.Abs(y1 - y2) / 2;
            }
            else
            {
                _a = (double)Math.Abs(y1 - y2) / 2;
                _b = (double)Math.Abs(x1 - x2) / 2;
            }
            _c = (double)Math.Sqrt((_a * _a - _b * _b));
            //求焦點
            if (_cross)
            {
                _focus1[0] = (double)midPoint.GetXCoordinate() / 2.0 + _c;
                _focus1[1] = (double)midPoint.GetYCoordinate() / 2.0;
                _focus2[0] = (double)midPoint.GetXCoordinate() / 2.0 - _c;
                _focus2[1] = (double)midPoint.GetYCoordinate() / 2.0;
            }
            else
            {
                _focus1[0] = (double)midPoint.GetXCoordinate() / 2.0;
                _focus1[1] = (double)midPoint.GetYCoordinate() / 2.0 + _c;
                _focus2[0] = (double)midPoint.GetXCoordinate() / 2.0;
                _focus2[1] = (double)midPoint.GetYCoordinate() / 2.0 - _c;
            }
        }

        /// <summary>
        /// 畫填滿的橢圓
        /// </summary>
        public override void DrawFillShape(IGraphics g)
        {
            g.DrawFillEllipse(GetFirstPoint().GetXCoordinate(), GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetXCoordinate(), GetSecondPoint().GetYCoordinate());          //畫圖
        }

        /// <summary>
        /// 畫橢圓的外框
        /// </summary>
        public override void DrawSketchShape(IGraphics g)
        {
            g.DrawSketchEllipse(GetFirstPoint().GetXCoordinate(), GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetXCoordinate(), GetSecondPoint().GetYCoordinate());
        }

        /// <summary>
        /// 畫橢圓的虛線邊框
        /// </summary>
        //public override void DrawSketchShapeInDash(IGraphics g)
        //{
        //    g.DrawDashRectangle(GetFirstPoint().GetXCoordinate(), GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetXCoordinate(), GetSecondPoint().GetYCoordinate());
        //}

        /// <summary>
        /// 檢查某點有沒有在圖形內
        /// </summary>
        public override bool IsPointInside(int x, int y)
        {
            //與兩焦點的距離
            double distance1 = (x - _focus1[0]) * (x - _focus1[0]) + (y - _focus1[1]) * (y - _focus1[1]);
            double distance2 = (x - _focus2[0]) * (x - _focus2[0]) + (y - _focus2[1]) * (y - _focus2[1]);
            //如果與焦點的距離總和小於長軸，表示在橢圓裡面，回傳true
            if (Math.Sqrt(distance1) + Math.Sqrt(distance2) <= 2 * _a)
            {
                return true;
            }
            //反之回傳false
            return false;
        }
    }
}
