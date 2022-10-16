using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpCode.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ModelTableKey : Attribute
    {
        public string key;
        public string filed;
        public ModelTableKey(string key)
        {
            this.key = key;
        }
    }
}
