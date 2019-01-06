using System;
using System.Collections.Generic;

namespace ModelLibrary
{
    public class Model
    {
        //儲存形狀
        List<Shape> _shapeList;
        //用來選取的矩形
        Rectangle _selectRectangle;
        //被選擇的圖形
        List<Shape> _selectedShapes;
        //管理命令
        CommandManger _commandManger;
        //製作形狀的工廠
        Factory _factory;
        //判斷是不是按下的狀態
        bool _isPressed;
        //提示的形狀
        Shape _hint;

        /// <summary>
        /// 初始化
        /// </summary>
        public Model()
        {
            _shapeList = new List<Shape>();
            _selectedShapes = new List<Shape>();
            _selectRectangle = new Rectangle(new DrawingPoint(-1, -1));
            _shapeList.Clear();
            _selectedShapes.Clear();
            _commandManger = new CommandManger();
            _factory = new Factory();
        }

        #region 取得部分變數
        /// <summary>
        /// 取得_hint，單元測試用
        /// </summary>
        public Shape GetHint()
        {
            return _hint;
        }
        #endregion

        #region 狀態Function
        /// <summary>
        /// 回傳UnDoButton的狀態
        /// </summary>
        public bool IsUnDoButtonEnable()
        {
            return _commandManger._IsUnDoButtonEnable;
        }

        /// <summary>
        /// 回傳ReDoButton的狀態
        /// </summary>
        public bool IsReDoButtonEnable()
        {
            return _commandManger._IsReDoButtonEnable;
        }

        /// <summary>
        /// 拿到shape list的大小
        /// </summary>
        public int GetShapeListSize()
        {
            return _shapeList.Count;
        }

        /// <summary>
        /// 拿到select list的大小
        /// </summary>
        public int GetSelectListSize()
        {
            return _selectedShapes.Count;
        }
        #endregion

        /// <summary>
        /// 滑鼠壓下去之後，確定位置，然後新增一個提示的形狀
        /// </summary>
        public void PressDown(shapes type, DrawingPoint point)
        {
            _selectedShapes.Clear();
            _hint = _factory.GetShape(type, point);
            _hint.SetSecondPoint(point);
            _isPressed = true;
        }

        /// <summary>
        /// 加入一個選擇用的矩形，設定第一個點
        /// </summary>
        public void AddSelectRectangle(DrawingPoint firstPoint)
        {
            _selectedShapes.Clear();
            _selectRectangle.SetFirstPoint(firstPoint);
        }

        /// <summary>
        /// 設定選擇用舉行的第二個點
        /// </summary>
        public void SetSecondPointOfSelectRectangle(DrawingPoint secondPoint)
        {
            _selectRectangle.SetSecondPoint(secondPoint);
        }

        /// <summary>
        /// 放開選擇模式，要決定那些圖形被選取
        /// </summary>
        public void ReleaseSelectRectangle()
        {
            //選取的矩形之上下左右限
            int top = Math.Max(_selectRectangle.GetFirstPoint().GetYCoordinate(), _selectRectangle.GetSecondPoint().GetYCoordinate());
            int button = Math.Min(_selectRectangle.GetFirstPoint().GetYCoordinate(), _selectRectangle.GetSecondPoint().GetYCoordinate());
            int left = Math.Min(_selectRectangle.GetFirstPoint().GetXCoordinate(), _selectRectangle.GetSecondPoint().GetXCoordinate());
            int right = Math.Max(_selectRectangle.GetFirstPoint().GetXCoordinate(), _selectRectangle.GetSecondPoint().GetXCoordinate());
            //紀錄那些shape被選到
            List<Shape> tempList = new List<Shape>();
            tempList.Clear();
            //紀錄有沒有某一點已經確認在選取的矩形中
            bool flag;
            foreach (Shape shape in _shapeList)
            {
                flag = false;
                for (int i = left; i <= right; i++)
                {
                    for (int j = button; j <= top; j++)
                    {
                        if (shape.IsPointInside(i, j))
                        {
                            flag = true;
                            tempList.Add(shape);
                        }
                        //flag為true表示有一點已經在矩形當中，那就不用繼續判斷其他點
                        if (flag)
                            break;
                    }
                    if (flag)
                        break;
                }
            }
            //如果有形狀被選取，才做SelectCommand
            if (tempList.Count != 0)
            {
                //_commandManger.PushCommand(new SelectCommand(this, tempList));
                SelectShape(tempList);
            }
            _selectRectangle.SetFirstPoint(new DrawingPoint(-1, -1));
        }

        /// <summary>
        /// 在畫布上移動時，更新第二點座標
        /// </summary>
        public void MoveMouse(DrawingPoint point)
        {
            if (_isPressed)
            {
                _hint.SetSecondPoint(point);
            }
        }

        /// <summary>
        /// 在畫布上放開滑鼠的時候新增一個畫圖的command
        /// </summary>
        public void PressUp()
        {
            if (_isPressed)
            {
                _isPressed = false;
                _commandManger.PushCommand(new DrawingCommand(this, _hint));
            }
        }

        /// <summary>
        /// 移除最後一個形狀
        /// </summary>
        public void DeleteShape()
        {
            _shapeList.RemoveAt(_shapeList.Count - 1);
        }

        /// <summary>
        /// 新增一個形狀
        /// </summary>
        public void AddShape(Shape newShape)
        {
            _shapeList.Add(newShape);
        }

        /// <summary>
        /// 著色
        /// </summary>
        public void DrawPanel(IGraphics graphic)
        {
            //清掉畫布上的東西
            graphic.ClearAll();
            //劃出所有的圖形
            for (int i = 0; i < _shapeList.Count; i++)
            {
                _shapeList[i].DrawFillShape(graphic);
            }
            //替被選到的圖形畫邊框
            foreach (Shape tempShape in _selectedShapes)
            {
                tempShape.DrawSketchShapeInDash(graphic);
            }
            //如果選擇用矩形第一點座標不是-1，表示現在在選擇模式之下
            if (_selectRectangle.GetFirstPoint().GetXCoordinate() != -1)
            {
                _selectRectangle.DrawSketchShapeInDash(graphic);
            }
            //如果在繪圖模式下，顯示_hint
            if (_isPressed)
            {
                _hint.DrawSketchShape(graphic);
            }
        }

        /// <summary>
        /// 取得所有的形狀
        /// </summary>
        public List<Shape> GetAllShapes()
        {
            return _shapeList;
        }

        /// <summary>
        /// 清除所有的形狀
        /// </summary>
        public void DeleteAll()
        {
            _shapeList.Clear();
            _selectedShapes.Clear();
        }

        /// <summary>
        /// 刪除部分形狀
        /// </summary>
        public void DeletePartOfShape(List<Shape> delete, List<int> deleteIndex)
        {
            _selectedShapes.Clear();
            deleteIndex.Clear();
            foreach (Shape tempShape in delete)
            {
                for (int i = 0; i < _shapeList.Count; i++)
                {
                    if (tempShape == _shapeList[i])
                    {
                        deleteIndex.Add(i);
                        _shapeList.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 把某些Shape加回去
        /// </summary>
        public void AddSomeShape(List<Shape> addList, List<int> indexList)
        {
            //檢查他們兩有沒有一樣大
            if (addList.Count != indexList.Count)
                throw new Exception("出現奇怪的問題啦，他們倆應該一樣大才對啊");
            for (int i = indexList.Count - 1; i >= 0; i--)
            {
                _shapeList.Insert(indexList[i], addList[i]);
            }
        }

        /// <summary>
        /// 執行DeleteCommand
        /// </summary>
        public void ExcuteDeleteCommand()
        {
            _commandManger.PushCommand(new DeleteCommand(this, _selectedShapes));
        }

        /// <summary>
        /// 執行選取
        /// </summary>
        public void SelectShape(List<Shape> selectShapes)
        {
            foreach (Shape shape in selectShapes)
            {
                _selectedShapes.Add(shape);
            }
        }

        /// <summary>
        /// 執行清除命令
        /// </summary>
        public void ExcuteClearCommand()
        {
            _commandManger.PushCommand(new ClearCommand(this));
        }

        /// <summary>
        /// 執行UnDo
        /// </summary>
        public void ExcuteUnDo()
        {
            _commandManger.UnDo();
        }

        /// <summary>
        /// 執行ReDo
        /// </summary>
        public void ExcuteReDo()
        {
            _commandManger.ReDo();
        }
    }
}
