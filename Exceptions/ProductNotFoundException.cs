using System;

namespace Pokladna.Exceptions
{
    internal class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}
