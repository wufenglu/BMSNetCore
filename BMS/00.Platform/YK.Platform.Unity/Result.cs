using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Platform.Unity
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
