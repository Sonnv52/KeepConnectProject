using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Exceptions
{
    public class UnAuthozireException : ApplicationException
    {
        public UnAuthozireException(string message) : base(message) { }
    }
}
