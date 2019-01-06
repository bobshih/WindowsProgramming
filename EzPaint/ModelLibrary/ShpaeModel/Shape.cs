using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public abstract class Shape
    {
        //第一點的座標
        DrawingPoint _firstPoint;
        //第二點的座標
        DrawingPoint _secondPoint;

        /// <summary>
        /// 初始化
        /// </summary>
        public Shape(DrawingPoint firstPoint)
        {
            SetFirstPoint(firstPoint);
            _secondPoint = new DrawingPoint(0, 0);
        }

        /// <summary>
        /// 畫填滿的圖
        /// </summary>
        public abstract void DrawFillShape(IGraphics g);

        /// <summary>
        /// 畫形狀
        /// </summary>
        public abstract void DrawSketchShape(IGraphics g);

        /// <summary>
        /// 畫虛線的框框
        /// </summary>
        public virtual void DrawSketchShapeInDash(IGraphics g)
        {
            g.DrawDashRectangle(GetFirstPoint().GetXCoordinate(), GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetXCoordinate(), GetSecondPoint().GetYCoordinate());
        }

        /// <summary>
        /// 檢查某點是否在圖形中
        /// </summary>
        public abstract bool IsPointInside(int x, int y);

        /// <summary>
        /// 設定第二點座標
        /// </summary>
        public virtual void SetSecondPoint(DrawingPoint secondPoint)
        {
            _secondPoint = secondPoint;
        }

        /// <summary>
        /// 設定第一點座標
        /// </summary>
        public void SetFirstPoint(DrawingPoint firstPoint)
        {
            _firstPoint = firstPoint;
        }

        /// <summary>
        /// 取得右下角座標
        /// </summary>
        public DrawingPoint GetSecondPoint()
        {
            return _secondPoint;
        }

        /// <summary>
        /// 取得左上角座標
        /// </summary>
        public DrawingPoint GetFirstPoint()
        {
            return _firstPoint;
        }
    }
}
