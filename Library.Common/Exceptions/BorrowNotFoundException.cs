using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Exceptions
{
    public class BorrowNotFoundException : Exception
    {
        public BorrowNotFoundException() : base("Borrow does not exist") { }
    }
}
