using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DynamicProxy
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class ActionClass : Attribute
    {
        public Type type;
        //public ActionClass(Type type)
        //{
        //    this.type = type;
        //}
    }
}
