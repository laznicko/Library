using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Exceptions
{
    /// <summary>
    /// Exception if user not found
    /// </summary>
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User does not exist") { }
    }
}
