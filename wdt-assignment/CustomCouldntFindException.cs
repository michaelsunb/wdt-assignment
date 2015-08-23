using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wdt_assignment
{
    /// <summary>Custom Exception to distinguish program failure was set by methods throw this class.</summary>
    class CustomCouldntFindException : Exception
    {
        /// <summary>Constructor to only take an input message for a capture to print out.</summary>
        /// <param name="message"> parameter takes a message.</param>
        /// <returns>Returns the Custom exception class.</returns>
        public CustomCouldntFindException(string message)
            : base(message) { }
    }
}
