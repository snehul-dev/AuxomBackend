using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Exceptions
{
    public class BadRequestException:Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}
