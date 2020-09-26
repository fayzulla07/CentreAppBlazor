using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Server.ReportToExcel
{
    public interface IReportToFile
    {
        public MemoryStream GetFile(string title, object items);
    }

}
