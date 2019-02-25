using YK.Platform.Core.Enums;

namespace YK.Platform.Core.Model
{
    //public enum Join { like,in,or,(,) }
    /// <summary>
    /// ���ʽ
    /// </summary>
    public class Expression
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public Expression()
        { 
        
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="fieldName">�ֶ���</param>
        /// <param name="condition">����</param>
        /// <param name="value">ֵ</param>
        /// <param name="join"></param>
        public Expression(string fieldName, ConditionEnum condition, object value, JoinEnum join = JoinEnum.And)
        {
            FieldName = fieldName;
            Condition = condition;
            Value = value.ToString();
            Join = join;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="join">���ӷ�: (��)��and �� or</param>
        /// <param name="value">ֵ</param>
        public Expression(JoinEnum join)
        {
            Join = join;
        }

        /// <summary>
        /// �ֶ�����
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public ConditionEnum Condition { get; set; }

        /// <summary>
        /// ֵ
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// ���ӷ�: (��)��and �� or
        /// </summary>
        public JoinEnum Join { get; set; }
    }
}
