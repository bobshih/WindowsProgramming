using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    class Triangle : Shape
    {
        //三角形的頂點
        DrawingPoint[] _trianglePoint;

        public Triangle(DrawingPoint firstPoint) : base(firstPoint)
        {
            _trianglePoint = new DrawingPoint[3];
        }

        /// <summary>
        /// 實作DrawFillShape
        /// </summary>
        public override void DrawFillShape(IGraphics g)
        {
            g.DrawFillTriangle(_trianglePoint);
        }

        /// <summary>
        /// 實作DrawSketchShape
        /// </summary>
        public override void DrawSketchShape(IGraphics g)
        {
            g.DrawSketchTriangle(_trianglePoint);
        }

        /// <summary>
        /// 畫虛線三角形
        /// </summary>
        /*public override void DrawSketchShapeInDash(IGraphics g)
        {
            g.DrawDashTriangle(_trianglePoint);
        }*/

        /// <summary>
        /// 設定右下角座標
        /// </summary>
        public override void SetSecondPoint(DrawingPoint rightDown)
        {
            base.SetSecondPoint(rightDown);
            CalculateTriangelPoint(_trianglePoint);
        }

        /// <summary>
        /// 計算三角形的三頂點
        /// </summary>
        public void CalculateTriangelPoint(DrawingPoint[] points)
        {
            int leftPoint = base.GetFirstPoint().GetXCoordinate();
            int topPoint = base.GetFirstPoint().GetYCoordinate();
            int rightPoint = base.GetSecondPoint().GetXCoordinate();
            int downPoint = base.GetSecondPoint().GetYCoordinate();
            //計算過程
            //x1是頂點，另外兩個構成底邊，其中x2固定座標
            int x3, x2, x1, y1, y2, y3;
            x2 = leftPoint;
            y2 = y3 = topPoint;
            x3 = rightPoint;
            y1 = downPoint;
            x1 = (x2 + x3) / 2;
            //assign給陣列
            points[0] = new DrawingPoint(x1, y1);
            points[1] = new DrawingPoint(x2, y2);
            points[2] = new DrawingPoint(x3, y3);
        }

        /// <summary>
        /// 檢查某點是否在圖形中
        /// </summary>
        public override bool IsPointInside(int x, int y)
        {
            //先取得三頂點構成的方程式係數，分別命名為a1, a2, a3, b1, b2, b3, c1, c2, c3
            int a1, a2, a3, b1, b2, b3, c1, c2, c3;
            //第一組方程式，a1*x + b1*y + c1 = 0
            a1 = -1 * _trianglePoint[0].GetYCoordinate() + _trianglePoint[1].GetYCoordinate();
            b1 = _trianglePoint[0].GetXCoordinate() - _trianglePoint[1].GetXCoordinate();
            c1 = -a1 * _trianglePoint[0].GetXCoordinate() - b1 * _trianglePoint[0].GetYCoordinate();
            //第二組方程式，a2*x + b2*y + c2 = 0
            a2 = -1 * _trianglePoint[1].GetYCoordinate() + _trianglePoint[2].GetYCoordinate();
            b2 = _trianglePoint[1].GetXCoordinate() - _trianglePoint[2].GetXCoordinate();
            c2 = -a2 * _trianglePoint[1].GetXCoordinate() - b2 * _trianglePoint[1].GetYCoordinate();
            //第三組方程式，a3*x + b3*y + c3 = 0
            a3 = -1 * _trianglePoint[2].GetYCoordinate() + _trianglePoint[0].GetYCoordinate();
            b3 = _trianglePoint[2].GetXCoordinate() - _trianglePoint[0].GetXCoordinate();
            c3 = -a3 * _trianglePoint[2].GetXCoordinate() - b3 * _trianglePoint[2].GetYCoordinate();
            //把要檢測的X和Y分別丟到三個方程式中
            int mappingNumber1, mappingNumber2, mappingNumber3;
            mappingNumber1 = a1 * x + b1 * y + c1;
            mappingNumber2 = a2 * x + b2 * y + c2;
            mappingNumber3 = a3 * x + b3 * y + c3;
            if (mappingNumber1 >= 0 && mappingNumber2 >= 0 && mappingNumber3 >= 0)
            {
                //都大於零，表示在三角形裡面，回傳true
                return true;
            }
            if (mappingNumber1 <= 0 && mappingNumber2 <= 0 && mappingNumber3 <= 0)
            {
                //都小於零，也表示在三角形裡面(理由是看的方向不同，造成正負號全部反號)
                return true;
            }
            //都不符合，回傳false
            return false;
        }
    }
}
