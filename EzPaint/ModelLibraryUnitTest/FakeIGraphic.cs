using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace ModelLibraryUnitTest
{
    enum DrawingType
    {
        Sketch = 1,
        Fill,
        Dash
    }

    /// <summary>
    /// 假的IGraphics，單純用在單元測試
    /// </summary>
    class FakeIGraphic : IGraphics
    {
        //判斷有沒有進行ClearAll
        bool _isClearAll;
        //儲存長方形的資訊
        int _rectangleFirstPointX;
        int _rectangleFirstPointY;
        int _rectangleSecondPointX;
        int _rectangleSecondPointY;
        //儲存三角形的資訊
        DrawingPoint _trianglePointOne;
        DrawingPoint _trianglePointTwo;
        DrawingPoint _trianglePointThree;
        //畫圖模式
        DrawingType _drawType;
        //畫出來的圖案
        shapes _drawingShape;

        #region 向外回傳變數資訊
        public int GetRectangleFirstPointX()
        {
            return _rectangleFirstPointX;
        }

        public int GetRectangleFirstPointY()
        {
            return _rectangleFirstPointY;
        }

        public int GetRectangleSecondPointY()
        {
            return _rectangleSecondPointY;
        }

        public int GetRectangleSecondPointX()
        {
            return _rectangleSecondPointX;
        }

        public DrawingPoint GetTrianglePointOne()
        {
            return _trianglePointOne;
        }

        public DrawingPoint GetTrianglePointTwo()
        {
            return _trianglePointTwo;
        }

        public DrawingPoint GetTrianglePointThree()
        {
            return _trianglePointThree;
        }

        public DrawingType GetDrawingType()
        {
            return _drawType;
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public FakeIGraphic()
        {
            _isClearAll = false;
            _rectangleFirstPointX = 0;
            _rectangleFirstPointY = 0;
            _rectangleSecondPointX = 0;
            _rectangleSecondPointY = 0;
            _trianglePointOne = new DrawingPoint(0, 0);
            _trianglePointTwo = new DrawingPoint(0, 0);
            _trianglePointThree = new DrawingPoint(0, 0);
            _drawType = 0;
            _drawingShape = 0;
        }

        /// <summary>
        /// 設定_IsClearAll的變數為true
        /// </summary>
        public void ClearAll()
        {
            _isClearAll = true;
        }

        /// <summary>
        /// 填滿長方形，把長方形的個資訊存進來
        /// </summary>
        public void DrawFillRectangle(int x1, int y1, int x2, int y2)
        {
            _drawType = DrawingType.Fill;
            _drawingShape = shapes.Rectangle;
            LoadRectangleInformation(x1, y1, x2, y2, DrawingType.Fill);
        }

        /// <summary>
        /// 填滿三角形，儲存三角形的資訊
        /// </summary>
        public void DrawFillTriangle(DrawingPoint[] points)
        {
            _drawType = DrawingType.Fill;
            _drawingShape = shapes.Triangle;
            LoadTriangleInformation(points, DrawingType.Fill);
        }

        /// <summary>
        /// 畫三角形的外框，儲存三角形的資訊
        /// </summary>
        public void DrawSketchTriangle(DrawingPoint[] points)
        {
            _drawType = DrawingType.Sketch;
            _drawingShape = shapes.Triangle;
            LoadTriangleInformation(points, DrawingType.Sketch);
        }

        /// <summary>
        /// 畫矩形外框，儲存三角形的資訊
        /// </summary>
        public void DrawSketchRectangle(int x1, int y1, int x2, int y2)
        {
            _drawType = DrawingType.Sketch;
            _drawingShape = shapes.Rectangle;
            LoadRectangleInformation(x1, y1, x2, y2, DrawingType.Sketch);
        }

        /// <summary>
        /// 畫填滿的橢圓
        /// </summary>
        public void DrawFillEllipse(int x1, int y1, int x2, int y2)
        {
            _drawType = DrawingType.Fill;
            _drawingShape = shapes.Ellipse;
            LoadRectangleInformation(x1, y1, x2, y2, DrawingType.Fill);
        }

        /// <summary>
        /// 畫橢圓的外框
        /// </summary>
        public void DrawSketchEllipse(int x1, int y1, int x2, int y2)
        {
            _drawType = DrawingType.Sketch;
            _drawingShape = shapes.Ellipse;
            LoadRectangleInformation(x1, y1, x2, y2, DrawingType.Sketch);
        }

        /// <summary>
        /// 畫直線
        /// </summary>
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            _drawType = DrawingType.Fill;
            _drawingShape = shapes.Line;
            LoadRectangleInformation(x1, y1, x2, y2, DrawingType.Fill);
        }

        /// <summary>
        /// 用虛線畫矩形
        /// </summary>
        public void DrawDashRectangle(int x1, int y1, int x2, int y2)
        {
            _drawType = DrawingType.Dash;
            _drawingShape = shapes.Rectangle;
            LoadRectangleInformation(x1, y1, x2, y2, DrawingType.Sketch);
        }

        /// <summary>
        /// 載入三角形資訊和繪畫模式
        /// </summary>
        private void LoadTriangleInformation(DrawingPoint[] points, DrawingType drawType)
        {
            _trianglePointOne = points[0];
            _trianglePointTwo = points[1];
            _trianglePointThree = points[2];
            _drawType = drawType;
        }

        /// <summary>
        /// 載入矩形資訊，和繪畫模式
        /// </summary>
        private void LoadRectangleInformation(int x1, int y1, int x2, int y2, DrawingType drawType)
        {
            _rectangleFirstPointX = x1;
            _rectangleFirstPointY = y1;
            _rectangleSecondPointY = y2;
            _rectangleSecondPointX = x2;
            _drawType = drawType;
        }

        /// <summary>
        /// 比較兩個Drawing Point
        /// </summary>
        public static bool CompareDrawingPoint(DrawingPoint pointOne, DrawingPoint pointTwo)
        {
            if (pointOne.GetXCoordinate() == pointTwo.GetXCoordinate() && pointOne.GetYCoordinate() == pointTwo.GetYCoordinate())
            {
                return true;
            }
            return false;
        }
    }
}
