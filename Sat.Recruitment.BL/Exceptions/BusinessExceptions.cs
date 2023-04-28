using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.BL.Exceptions
{
    public class BusinessException : Exception
    {
        public string Errors { get; set; }
        public BusinessException() { }

        public BusinessException(string message) : base (message) { }

        public BusinessException(string message, Exception inner) : base(message, inner) { }
    }
}
