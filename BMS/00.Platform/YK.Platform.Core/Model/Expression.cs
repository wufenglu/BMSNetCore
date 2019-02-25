using YK.Platform.Core.Enums;

namespace YK.Platform.Core.Model
{
    //public enum Join { like,in,or,(,) }
    /// <summary>
    /// 表达式
    /// </summary>
    public class Expression
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public Expression()
        { 
        
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="condition">条件</param>
        /// <param name="value">值</param>
        /// <param name="join"></param>
        public Expression(string fieldName, ConditionEnum condition, object value, JoinEnum join = JoinEnum.And)
        {
            FieldName = fieldName;
            Condition = condition;
            Value = value.ToString();
            Join = join;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="join">连接符: (、)、and 、 or</param>
        /// <param name="value">值</param>
        public Expression(JoinEnum join)
        {
            Join = join;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public ConditionEnum Condition { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 连接符: (、)、and 、 or
        /// </summary>
        public JoinEnum Join { get; set; }
    }
}
