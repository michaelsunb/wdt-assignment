using System;
namespace wdt_assignment
{
    interface IOption
    {
        /// <summary>Returns a option string set by child class</summary>
        /// <returns>Returns string</returns>
        string GetOption();

        /// <summary>Selected method to display functionality.</summary>
        void Selected();
    }
}
