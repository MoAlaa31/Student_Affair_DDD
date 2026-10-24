using Student_Affair_DDD.Modules.Students.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Affair_DDD.Modules.Students.Application.SpecParams
{
    public class StudentPaginationSpecParams : PaginationSpecParams
    {
        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public bool? isActive { get; set; }
        public string? sortBy { get; set; }
        public bool sortDescending { get; set; }
        public string? department { get; set; }
    }
}
