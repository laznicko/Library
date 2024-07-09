using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Exceptions
{
    /// <summary>
    /// Exception if book is alredy borrowed
    /// </summary>
    public class BookAlreadyBorrowedException : Exception
    {
        public BookAlreadyBorrowedException(string message) : base(message) { }
    }
}
