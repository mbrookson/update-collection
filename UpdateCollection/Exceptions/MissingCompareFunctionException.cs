using System;

namespace UpdateCollection.Exceptions
{
    public class MissingCompareFunctionException : Exception
    {
        public MissingCompareFunctionException() : base("You must provide a compare function. Use CompareWith()")
        {
        }
    }
}