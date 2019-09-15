using NextGenTestLibrary.Attributes;
using NextGenTestLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Attributes
{
    internal static class AttributeInfo
    {
        /// <summary>
        /// Get Execute Attribute info
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static ExecuteAttribute GetExecuteAttribute(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new MemberInfoNullException("Member info object is null");
            }
            ExecuteAttribute attribute  = Attribute.GetCustomAttribute(memberInfo, typeof(ExecuteAttribute))
                                                                            as ExecuteAttribute;
            return attribute;
        }
        /// <summary>
        /// Get Test Case Attribute info
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static TestInfoAttribute GetTestInfoAttribute(this MemberInfo memberInfo)
        {

            if(memberInfo == null)
            {
                throw new MemberInfoNullException("Member info object is null");
            }

            TestInfoAttribute attribute = Attribute.GetCustomAttribute(memberInfo, typeof(TestInfoAttribute))
                                                                            as TestInfoAttribute;
            return attribute;
        }

        /// <summary>
        /// Get Order Attribute info
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static TestOrderAttribute GetTestOrderAttribute(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new MemberInfoNullException("Member info object is null");
            }

            TestOrderAttribute attribute = Attribute.GetCustomAttribute(memberInfo, typeof(TestOrderAttribute))
                                                                              as TestOrderAttribute;

            return attribute;

        }

        /// <summary>
        /// Get dependent Attribute info
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static DependentAttribute GetDependentAttribute(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new MemberInfoNullException("Member info object is null");
            }

            DependentAttribute attribute = Attribute.GetCustomAttribute(memberInfo, typeof(DependentAttribute))
                                                                              as DependentAttribute;

            return attribute;

        }

        /// <summary>
        /// Get Retry count Attribute info
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static RetryByAttribute GetRetryByAttribute(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new MemberInfoNullException("Member info object is null");
            }

            RetryByAttribute retryByAttribute = Attribute.GetCustomAttribute(memberInfo, typeof(RetryByAttribute))
                                                                                as RetryByAttribute;
            return retryByAttribute;
        }
    }
}
