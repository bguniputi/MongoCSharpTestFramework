using System;

namespace NextGenTestLibrary.Attributes
{
    /// <summary>
    /// Test order attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false,Inherited = false)]
    public class TestOrderAttribute : Attribute
    {
        public int Order { get; private set; }

        /// <param name="Order"></param>
        public TestOrderAttribute(int Order)
        {
            this.Order = Order;
        }
    }
}
