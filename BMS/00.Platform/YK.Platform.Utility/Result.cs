using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Platform.Utility
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
