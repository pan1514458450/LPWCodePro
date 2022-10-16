using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpCode.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ModelTableName : Attribute
    {
        public string TableName;
        public ModelTableName(string tableName)
        {
            TableName = tableName;
        }

    }
}
