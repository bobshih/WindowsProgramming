using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    /// <summary>
    /// 圖形介面的轉換
    /// </summary>
    public interface IGraphics
    {
        void ClearAll();

        void DrawFillTriangle(DrawingPoint[] points);

        void DrawFillRectangle(int x1, int y1, int x2, int y2);

        void DrawFillEllipse(int x1, int y1, int x2, int y2);

        void DrawLine(int x1, int y1, int x2, int y2);

        void DrawSketchTriangle(DrawingPoint[] points);

        void DrawSketchRectangle(int x1, int y1, int x2, int y2);

        void DrawSketchEllipse(int x1, int y1, int x2, int y2);

        void DrawDashRectangle(int x1, int y1, int x2, int y2);
    }
}
