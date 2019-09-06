using System;

namespace NextGenTestLibrary.Attributes
{
    /// <summary>
    /// Module Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false,Inherited = false)]
    public class ModuleAttribute : Attribute
    {
        public string ModuleType { get; private set; }

        /// <param name="moduleType"></param>
        public ModuleAttribute
            (string moduleType)
        {
            ModuleType = moduleType;
        }
    }
}
