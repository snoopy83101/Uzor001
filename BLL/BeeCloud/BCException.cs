using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud
{
    public class BCException: ApplicationException
    {
        public BCException() { }
        public BCException(string message)  
            : base(message) { }  
    }
}
