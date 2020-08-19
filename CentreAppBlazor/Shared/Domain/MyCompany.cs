using System;
using System.Collections.Generic;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class MyCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int? Level { get; set; }
    }
}
