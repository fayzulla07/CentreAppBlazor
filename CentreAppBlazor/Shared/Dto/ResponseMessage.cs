using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Dto
{
    public class ResponseMessage<T>
    {
        public bool IsSuccessCode { get; set; } = true;
        public string ErrorMessage { get; set; }
        public int? TCount { get; set; }
        public T entity { get; set; }
    }
}
