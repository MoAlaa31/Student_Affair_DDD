using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Affair_DDD.Modules.Students.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"\"{name}\" ({key}) was not found.")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
