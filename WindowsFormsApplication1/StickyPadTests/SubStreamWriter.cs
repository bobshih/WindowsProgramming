using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace StickyPadTests
{
    class SubStreamWriter : StreamWriter        //模擬StreamWriter
    {
        string _writer;

        public string _Writer       //回傳_writer的資料
        {
            get
            {
                return _writer;
            }
        }

        public SubStreamWriter(string fakePath) : base(fakePath)        //constructor
        {
            _writer = string.Empty;
        }

        public override void Write(string input)     //寫入檔案
        {
            _writer += input;
        }
    }
}
