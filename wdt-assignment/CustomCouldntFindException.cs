using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wdt_assignment
{
    class CustomCouldntFindException : Exception
    {
        public CustomCouldntFindException()
            : base() { }

        public CustomCouldntFindException(string message)
            : base(message) { }

        public CustomCouldntFindException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public CustomCouldntFindException(string message, Exception innerException)
            : base(message, innerException) { }

        public CustomCouldntFindException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
