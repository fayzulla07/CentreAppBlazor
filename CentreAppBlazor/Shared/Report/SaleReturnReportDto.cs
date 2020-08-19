using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Report
{
    public class SaleReturnReportDto
    {
        public List<SaleReportDto> sale { get; set; }
        public List<ReturnReportDto> returns { get; set; }
    }
}
