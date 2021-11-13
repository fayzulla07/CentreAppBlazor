using CentreAppBlazor.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Text;


namespace CentreAppBlazor.Shared.Report
{
   public class RemainReportDto
    {
        [EpplusIgnore]
        public int ProductId { get; set; }


        [EpplusDisplay(Name = "Имя")]
        public string Name { get; set; }

        [EpplusDisplay(Name = "Код")]
        public string Code { get; set; }

        [EpplusDisplay(Name = "Цена прихода")]
        public double? IncomeCost { get; set; }

        [EpplusDisplay(Name = "Цена продажи")]
        public double? SaleCost { get; set; }

        [EpplusDisplay(Name = "Остаток")]
        public double RemainCount { get; set; }

        [EpplusDisplay(Name = "Лимит")]
        public int Limit { get; set; }
       
    }
}
