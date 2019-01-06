using System;
using System.Drawing;

namespace ModelLibrary
{
    public class FormGraphicAdaptor : IGraphics
    {
        Graphics _graphics;
        //畫直線的筆的寬度
        const float PEN_WIDTH = 3.0f;
        //虛線的寬度
        const float DASH_PEN_WIDTH = 2.0f;

        /// <summary>
        /// 初始化
        /// </summary>
        public FormGraphicAdaptor(Graphics g)
        {
            this._graphics = g;
        }

        /// <summary>
        /// 清除內容
        /// </summary>
        public void ClearAll()
        {
        }

        /// <summary>
        /// 畫填滿的三角形
        /// </summary>
        public void DrawFillTriangle(DrawingPoint[] points)
        {
            _graphics.FillPolygon(Brushes.Yellow, TranslateToPoint(points));
        }

        /// <summary>
        /// 畫填滿的矩形
        /// </summary>
        public void DrawFillRectangle(int x1, int y1, int x2, int y2)
        {
            _graphics.FillRectangle(Brushes.Green, ProduceRectangle(x1, y1, x2, y2));
        }

        /// <summary>
        /// 畫填滿的橢圓
        /// </summary>
        public void DrawFillEllipse(int x1, int y1, int x2, int y2)
        {
            _graphics.FillEllipse(Brushes.Red, ProduceRectangle(x1, y1, x2, y2));
        }

        /// <summary>
        /// 畫三角形的形狀
        /// </summary>
        public void DrawSketchTriangle(DrawingPoint[] points)
        {
            _graphics.DrawPolygon(Pens.Yellow, TranslateToPoint(points));
        }

        /// <summary>
        /// 畫矩形的形狀
        /// </summary>
        public void DrawSketchRectangle(int x1, int y1, int x2, int y2)
        {
            _graphics.DrawRectangle(Pens.Green, ProduceRectangle(x1, y1, x2, y2));
        }

        /// <summary>
        /// 畫橢圓的形狀
        /// </summary>
        public void DrawSketchEllipse(int x1, int y1, int x2, int y2)
        {
            _graphics.DrawEllipse(Pens.Red, ProduceRectangle(x1, y1, x2, y2));
        }

        /// <summary>
        /// 畫直線
        /// </summary>
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            _graphics.DrawLine(new Pen(Color.Orange, PEN_WIDTH), new Point(x1, y1), new Point(x2, y2));
        }

        /// <summary>
        /// 用虛線畫矩形外框表示被選取，或是用來選取的範圍
        /// </summary>
        public void DrawDashRectangle(int x1, int y1, int x2, int y2)
        {
            //設定一支畫虛線的筆
            Pen dashPen = new Pen(Color.Blue);
            dashPen.Width = DASH_PEN_WIDTH;
            dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            _graphics.DrawRectangle(dashPen, ProduceRectangle(x1, y1, x2, y2));
        }

        #region 相關轉換函式，包含三角形、矩形和虛線筆的生成函式
        /// <summary>
        /// 把三角形的點轉換成point結構
        /// </summary>
        private Point[] TranslateToPoint(DrawingPoint[] points)
        {
            Point[] point = new Point[3];
            point[0] = new Point(points[0].GetXCoordinate(), points[0].GetYCoordinate());
            point[1] = new Point(points[1].GetXCoordinate(), points[1].GetYCoordinate());
            point[2] = new Point(points[2].GetXCoordinate(), points[2].GetYCoordinate());
            return point;
        }

        /// <summary>
        /// 生成矩形
        /// </summary>
        private static System.Drawing.Rectangle ProduceRectangle(int x1, int y1, int x2, int y2)
        {
            //取矩形左上和右下座標
            int top = Math.Min(y1, y2);
            int left = Math.Min(x1, x2);
            int down = Math.Max(y1, y2);
            int right = Math.Max(x1, x2);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(left, top, right - left, down - top);
            return rectangle;
        }
        #endregion
    }
}
