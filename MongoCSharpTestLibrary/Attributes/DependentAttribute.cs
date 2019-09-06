using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Attributes
{
    /// <summary>
    /// Dependent Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false,Inherited = false)]
    public class DependentAttribute : Attribute
    {
        public string TestMethodName { get; private set; }
        public DependentAttribute
            (string testMethodName)
        {
            TestMethodName = testMethodName;
        }
    }
}
