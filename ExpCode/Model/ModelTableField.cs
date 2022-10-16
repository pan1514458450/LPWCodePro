using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpCode.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ModelTableField : Attribute
    {
        public string Name;
        public string key;

        public ModelTableField(string name)
        {
            Name = name;
        }
        public ModelTableField(string tableName, string key = null)
        {
            Name = tableName;
            if (key != null)
                this.key = key;
        }
    }
}
