using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Dto
{
    public class ApiQuery
    {
        public int? Skip { get; set; }
        public int? Top { get; set; }
        public string OrderBy { get; set; }
        public string Filter { get; set; }
    }
}
