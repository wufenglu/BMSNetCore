using System;
using System.Collections.Generic;
using System.Text;

namespace YK.Platform.Unity.Attributes
{
    public sealed class ColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsDbGenerated { get; set; }

    }
}
