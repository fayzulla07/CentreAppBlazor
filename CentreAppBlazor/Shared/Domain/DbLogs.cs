using System;
using System.Collections.Generic;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class DbLogs
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime? RegDt { get; set; }
    }
}
