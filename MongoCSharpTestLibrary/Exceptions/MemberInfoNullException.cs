using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Exceptions
{
    public class MemberInfoNullException : Exception
    {
        /// <summary>
        /// Memberinfo exception with message
        /// </summary>
        /// <param name="message"></param>
        public MemberInfoNullException(string message)
            :base(message)
        {

        }
        /// <summary>
        /// MemberInfo exception with inner and message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public MemberInfoNullException(string message,Exception inner)
            :base(message,inner)
        {

        }
    }
}
