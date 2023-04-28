using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.DAL.Exceptions
{
    public class DALException : Exception
    {

        public DALException() { }
        
        public DALException(string message) : base(message) { }

        public DALException(string message, Exception inner)  : base(message, inner) { }
    }
}
