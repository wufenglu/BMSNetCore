using System;
using System.Collections.Generic;
using System.Text;

namespace YK.Platform.Entitys.Enum
{
    /// <summary>
    /// 字段类型枚举
    /// </summary>
    public enum FieldTypeEnum
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// 加密文本
        /// </summary>
        PasswordText,
        /// <summary>
        /// 文本区
        /// </summary>
        TextArea,
        /// <summary>
        /// 日期
        /// </summary>
        Date,
        /// <summary>
        /// 时间
        /// </summary>
        DateTime,
        /// <summary>
        /// 邮件
        /// </summary>
        Email,
        /// <summary>
        /// 附件
        /// </summary>
        Attachment,
        /// <summary>
        /// 图片
        /// </summary>
        Picture,
        /// <summary>
        /// 单选
        /// </summary>
        Radio,
        /// <summary>
        /// 复选
        /// </summary>
        Checkbox,
        /// <summary>
        /// 逻辑选项
        /// </summary>
        LogicOption,
        /// <summary>
        /// 选项列表
        /// </summary>
        Option,
        /// <summary>
        /// 选项列表(多选)
        /// </summary>
        Options,
        /// <summary>
        /// 地理位置
        /// </summary>
        Address
    }
}
