using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Platform.Core.Enums
{
    /// <summary>
    /// 表达式枚举
    /// </summary>
    public enum ConditionEnum
    {
        /// <summary>
        /// 包含
        /// </summary>
        Like,
        /// <summary>
        /// 左Like
        /// </summary>
        LeftLike,
        /// <summary>
        /// 右Like
        /// </summary>
        RightLike,
        /// <summary>
        /// 不包含
        /// </summary>
        NotLike,
        /// <summary>
        /// 在集合里面
        /// </summary>
        In,
        /// <summary>
        /// 不在集合里面
        /// </summary>
        NotIn,
        /// <summary>
        /// 等于
        /// </summary>
        Eq,
        /// <summary>
        /// 不等于
        /// </summary>
        Ne,
        /// <summary>
        /// 大于
        /// </summary>
        Gt,
        /// <summary>
        /// 大于等于
        /// </summary>
        Ge,
        /// <summary>
        /// 小于
        /// </summary>
        Lt,
        /// <summary>
        /// 小于等于
        /// </summary>
        Le
    }
}
