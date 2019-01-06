using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Factory
    {
        public Factory()
        {
        }

        /// <summary>
        /// 回傳產生的形狀
        /// </summary>
        public Shape GetShape(shapes shapeType, DrawingPoint firstPoint)
        {
            switch (shapeType)
            {
                case shapes.Triangle:
                    return new Triangle(firstPoint);
                case shapes.Rectangle:
                    return new Rectangle(firstPoint);
                case shapes.Ellipse:
                    return new Ellipse(firstPoint);
                case shapes.Line:
                    return new Line(firstPoint);
                default:
                    return null;
            }
        }
    }
}
