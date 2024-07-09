using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Exceptions
{
    /// <summary>
    /// Exception if book not found
    /// </summary>
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException() : base("Book does not exist") { }
    }
}
