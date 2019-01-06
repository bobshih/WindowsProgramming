using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    class Line : Shape
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public Line(DrawingPoint firstPoint)
            : base(firstPoint)
        {
        }

        /// <summary>
        /// 畫出一條線
        /// </summary>
        public override void DrawSketchShape(IGraphics g)
        {
            g.DrawLine(GetFirstPoint().GetXCoordinate(), GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetXCoordinate(), GetSecondPoint().GetYCoordinate());
        }

        /// <summary>
        /// 畫出一條線
        /// </summary>
        public override void DrawFillShape(IGraphics g)
        {
            g.DrawLine(GetFirstPoint().GetXCoordinate(), GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetXCoordinate(), GetSecondPoint().GetYCoordinate());
        }

        /// <summary>
        /// 檢查某點是否在圖形中
        /// </summary>
        public override bool IsPointInside(int x, int y)
        {
            DrawingPoint point1, point2;
            point1 = GetFirstPoint();
            point2 = GetSecondPoint();
            //判斷延長線的情況
            if (x > Math.Max(point1.GetXCoordinate(), point2.GetXCoordinate()) || x < Math.Min(point1.GetXCoordinate(), point2.GetXCoordinate()))
            {
                return false;
            }
            else if (y > Math.Max(point1.GetYCoordinate(), point2.GetYCoordinate()) || y < Math.Min(point1.GetYCoordinate(), point2.GetYCoordinate()))
            {
                return false;
            }
            //計算直線方程式，a*x + b*y + c = 0;
            double a, b, c;
            a = -1 * point1.GetYCoordinate() + point2.GetYCoordinate();
            b = point1.GetXCoordinate() - point2.GetXCoordinate();
            c = -a * point1.GetXCoordinate() - b * point1.GetYCoordinate();
            //計算點到直線的距離
            double distance = Math.Abs((double)(x * a + y * b + c)) / Math.Sqrt(a * a + b * b);
            if (distance < 2.0)
            {
                return true;
            }
            //點和線的距離過遠，不再線上，回傳false
            return false;
        }
    }
}
