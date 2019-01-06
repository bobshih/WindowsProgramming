using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace EzPaintApp
{
    /// <summary>
    /// 這個partial class主要收集IGraphics之外的設定Function
    /// </summary>
    partial class WindowsStoreAppGraphicAdaptor : IGraphics
    {
        /// <summary>
        /// 設定矩形
        /// </summary>
        private Windows.UI.Xaml.Shapes.Rectangle SetRectangle(int x1, int y1, int x2, int y2)
        {
            //取矩形左上和右下座標
            int top = Math.Min(y1, y2);
            int left = Math.Min(x1, x2);
            int down = Math.Max(y1, y2);
            int right = Math.Max(x1, x2);
            //設定矩形屬性
            Windows.UI.Xaml.Shapes.Rectangle rectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            rectangle.Width = right - left;
            rectangle.Height = down - top;
            //設定矩形左上角位置
            Canvas.SetLeft(rectangle, (double)left);
            Canvas.SetTop(rectangle, (double)top);
            return rectangle;
        }

        /// <summary>
        /// 設定三角形
        /// </summary>
        private Polygon SetTriangle(DrawingPoint[] points)
        {
            Windows.UI.Xaml.Shapes.Polygon triangle = new Polygon();
            //新增頂點的集合
            PointCollection triaglePoints = new PointCollection();
            triaglePoints.Add(new Windows.Foundation.Point(points[0].GetXCoordinate(), points[0].GetYCoordinate()));
            triaglePoints.Add(new Windows.Foundation.Point(points[1].GetXCoordinate(), points[1].GetYCoordinate()));
            triaglePoints.Add(new Windows.Foundation.Point(points[2].GetXCoordinate(), points[2].GetYCoordinate()));
            triangle.Points = triaglePoints;
            return triangle;
        }

        /// <summary>
        /// 設定橢圓
        /// </summary>
        private static Windows.UI.Xaml.Shapes.Ellipse SetEllipse(int x1, int y1, int x2, int y2)
        {
            Windows.UI.Xaml.Shapes.Ellipse ellipse = new Windows.UI.Xaml.Shapes.Ellipse();
            //取矩形左上和右下座標
            int top = Math.Min(y1, y2);
            int left = Math.Min(x1, x2);
            int down = Math.Max(y1, y2);
            int right = Math.Max(x1, x2);
            ellipse.Margin = new Windows.UI.Xaml.Thickness(left, top, 0, 0);
            ellipse.Width = right - left;
            ellipse.Height = down - top;
            return ellipse;
        }

        /// <summary>
        /// 設定直線
        /// </summary>
        private Windows.UI.Xaml.Shapes.Line SetLine(int x1, int y1, int x2, int y2)
        {
            Windows.UI.Xaml.Shapes.Line line = new Windows.UI.Xaml.Shapes.Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.StrokeThickness = PEN_WIDTH;
            return line;
        }
    }
}
