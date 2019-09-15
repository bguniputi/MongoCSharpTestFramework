using System;

namespace NextGenTestLibrary.Attributes
{
    /// <summary>
    /// Specifies that a test method should be rerun on failure/Error up to the specified 
    /// maximum number of times.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false,Inherited = false)]
    public class RetryByAttribute : Attribute
    {
        public int _tryCount { get; private set; }

        /// <summary>
        /// Construct a <see cref="RetryByAttribute"/>
        /// </summary>
        /// <param name="tryCount">The maximum number of times the test should be run if it fails/erros</param>
        public RetryByAttribute(int tryCount)
        {
            _tryCount = tryCount;
        }

    }
}
