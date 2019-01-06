using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    class CommandManger
    {
        List<ICommand> _unDo;
        List<ICommand> _reDo;

        /// <summary>
        /// 初始化一開始的list
        /// </summary>
        public CommandManger()
        {
            _unDo = new List<ICommand>();
            _unDo.Clear();
            _reDo = new List<ICommand>();
            _reDo.Clear();
        }

        /// <summary>
        /// 執行command，然後push到list中
        /// </summary>
        public void PushCommand(ICommand command)
        {
            command.Excute();
            _unDo.Add(command);
            //這時候要把redo裡面的東西清空
            _reDo.Clear();
        }

        /// <summary>
        /// 執行UnDo
        /// </summary>
        public void UnDo()
        {
            if (_unDo.Count <= 0)
                throw new Exception("UnDo的List中已經沒有任何元素了");
            //暫時儲存丟出來的命令
            ICommand tempCommand = _unDo[_unDo.Count - 1];
            //移除剛剛丟出來的命令
            _unDo.Remove(tempCommand);
            //移除剛剛丟出來的命令   
            _reDo.Add(tempCommand);
            //反執行
            tempCommand.UnExcute();
        }

        /// <summary>
        /// 執行ReDo
        /// </summary>
        public void ReDo()
        {
            if (_reDo.Count <= 0)
                throw new Exception("ReDo的List中已經沒有任何元素了");
            //儲存暫時丟出來的redo命令
            ICommand tempCommand = _reDo[_reDo.Count - 1];
            //移除剛剛丟出來的命令
            _reDo.Remove(tempCommand);
            _unDo.Add(tempCommand);
            //執行命令
            tempCommand.Excute();
        }

        #region 按鈕狀態
        /// <summary>
        /// 判斷redo的狀態
        /// </summary>
        public bool _IsReDoButtonEnable
        {
            get
            {
                return !_reDo.Count.Equals(0);
            }
        }

        /// <summary>
        /// 判斷undo按鈕的狀態
        /// </summary>
        public bool _IsUnDoButtonEnable
        {
            get
            {
                return !_unDo.Count.Equals(0);
            }
        }
        #endregion
    }
}
