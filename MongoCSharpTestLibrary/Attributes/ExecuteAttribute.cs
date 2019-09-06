using NextGenTestLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Attributes
{
    /// <summary>
    /// Execute Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false,Inherited = false)]
    public class ExecuteAttribute : Attribute
    {
        public ExecuteType ExecuteType { get; private set; }

        /// <param name="executeType"></param>
        public ExecuteAttribute
            (ExecuteType executeType)
        {
            ExecuteType = executeType;
        }

    }
}
