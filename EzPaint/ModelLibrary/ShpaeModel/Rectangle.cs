using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Rectangle : Shape
    {
        public Rectangle(DrawingPoint firstPoint) : base(firstPoint)
        {
        }

        /// <summary>
        /// 實作DrawFillShape
        /// </summary>
        public override void DrawFillShape(IGraphics g)
        {
            g.DrawFillRectangle(GetFirstPoint().GetXCoordinate(), GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetXCoordinate(), GetSecondPoint().GetYCoordinate());
        }

        /// <summary>
        /// 實作DrawSketchShape
        /// </summary>
        public override void DrawSketchShape(IGraphics g)
        {
            g.DrawSketchRectangle(GetFirstPoint().GetXCoordinate(), GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetXCoordinate(), GetSecondPoint().GetYCoordinate());
        }

        /// <summary>
        /// 檢查某點是否在圖形中
        /// </summary>
        public override bool IsPointInside(int x, int y)
        {
            int top = Math.Max(GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetYCoordinate());
            int button = Math.Min(GetFirstPoint().GetYCoordinate(), GetSecondPoint().GetYCoordinate());
            int left = Math.Max(GetFirstPoint().GetXCoordinate(), GetSecondPoint().GetXCoordinate());
            int right = Math.Min(GetFirstPoint().GetXCoordinate(), GetSecondPoint().GetXCoordinate());
            //如果在範圍之內，就回傳true
            if (y <= top && y >= button && x >= right && x <= left)
            {
                return true;
            }
            //反之，回傳false
            return false;
        }
    }
}
