using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Dto
{
    public class ModelCountDto<T>
    {
        public T Items { get; set; }
        public int Count { get; set; }
    }
}
