using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;

namespace EzPaintApp
{
    /// <summary>
    /// 這個partial class只有IGraphics的實作部分，其他設定Function在名為AppAdaptorSetting裡面
    /// </summary>
    partial class WindowsStoreAppGraphicAdaptor : IGraphics
    {
        //畫布
        Canvas _canvas;
        //畫直線的筆的寬度
        const float PEN_WIDTH = 3.0f;
        //虛線的寬度
        const float DASH_PEN_WIDTH = 2.0f;

        /// <summary>
        /// 初始化
        /// </summary>
        public WindowsStoreAppGraphicAdaptor(Canvas canvas)
        {
            _canvas = canvas;
        }

        /// <summary>
        /// 清除全部
        /// </summary>
        public void ClearAll()
        {
            _canvas.Children.Clear();
        }

        /// <summary>
        /// 畫填滿的三角形
        /// </summary>
        public void DrawFillTriangle(DrawingPoint[] points)
        {
            //設定三角形的頂點
            Windows.UI.Xaml.Shapes.Polygon triangle = SetTriangle(points);
            //設定顏色
            triangle.Fill = new SolidColorBrush(Colors.Yellow);
            //加到畫布上
            _canvas.Children.Add(triangle);
        }

        /// <summary>
        /// 畫填滿的矩形
        /// </summary>
        public void DrawFillRectangle(int x1, int y1, int x2, int y2)
        {
            //設定矩形
            Windows.UI.Xaml.Shapes.Rectangle rectangle = SetRectangle(x1, y1, x2, y2);
            //矩形顏色
            rectangle.Fill = new SolidColorBrush(Colors.Green);
            _canvas.Children.Add(rectangle);
        }

        /// <summary>
        /// 畫三角形的外框
        /// </summary>
        public void DrawSketchTriangle(DrawingPoint[] points)
        {
            //設定三角形
            Windows.UI.Xaml.Shapes.Polygon triangle = SetTriangle(points);
            //設定顏色
            triangle.Stroke = new SolidColorBrush(Colors.Yellow);
            _canvas.Children.Add(triangle);
        }

        /// <summary>
        /// 畫矩形的外框
        /// </summary>
        public void DrawSketchRectangle(int x1, int y1, int x2, int y2)
        {
            //設定矩形
            Windows.UI.Xaml.Shapes.Rectangle rectangle = SetRectangle(x1, y1, x2, y2);
            //設定顏色
            rectangle.Stroke = new SolidColorBrush(Colors.Green);
            _canvas.Children.Add(rectangle);
        }

        /// <summary>
        /// 畫填滿的橢圓
        /// </summary>
        public void DrawFillEllipse(int x1, int y1, int x2, int y2)
        {
            Windows.UI.Xaml.Shapes.Ellipse ellipse = SetEllipse(x1, y1, x2, y2);
            ellipse.Fill = new SolidColorBrush(Colors.Red);
            _canvas.Children.Add(ellipse);
        }

        /// <summary>
        /// 畫橢圓的外框
        /// </summary>
        public void DrawSketchEllipse(int x1, int y1, int x2, int y2)
        {
            Windows.UI.Xaml.Shapes.Ellipse ellipse = SetEllipse(x1, y1, x2, y2);
            ellipse.Stroke = new SolidColorBrush(Colors.Red);
            _canvas.Children.Add(ellipse);
        }

        /// <summary>
        /// 畫直線，不分外框或是填滿
        /// </summary>
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            Windows.UI.Xaml.Shapes.Line line = SetLine(x1, y1, x2, y2);
            line.Stroke = new SolidColorBrush(Colors.Orange);
            _canvas.Children.Add(line);
        }

        /// <summary>
        /// 用虛線畫矩形，表示被選取
        /// </summary>
        public void DrawDashRectangle(int x1, int y1, int x2, int y2)
        {
            Windows.UI.Xaml.Shapes.Rectangle rectangle = SetRectangle(x1, y1, x2, y2);
            rectangle.Stroke = new SolidColorBrush(Colors.Blue);
            rectangle.StrokeDashArray.Add(2);
            rectangle.StrokeDashArray.Add(2);
            rectangle.StrokeThickness = DASH_PEN_WIDTH;
            rectangle.StrokeDashCap = PenLineCap.Flat;
            rectangle.StrokeDashOffset = 0;
            _canvas.Children.Add(rectangle);
        }
    }
}
