using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Affair_DDD.Modules.Students.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
