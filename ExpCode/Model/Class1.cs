using ExpCode.DynamicProxy;
using ExpCode.StaticFiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpCode.Model
{
    internal class Class1: IClass1
    {
        [ActionClass]
        public List<string> MyProperty(string d)
        {
            return default;
        }
    }
    internal interface IClass1:ics
    {
         List<string> MyProperty(string d);
    }
    internal interface ics
    {

    }
}
