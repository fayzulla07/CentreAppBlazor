using CentreAppBlazor.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Report
{
   public class ReturnReportDto
    {
        [EpplusIgnore]
        public int Id { get; set; }

        [EpplusDisplay(Name="Имя")]
        public string Name { get; set; }

        [EpplusDisplay(Name = "Цена возврата")]
        public double ReturnCost { get; set; }

        [EpplusDisplay(Name = "Коль-во")]
        public int Amount { get; set; }

        [EpplusDisplay(Name = "Дата рег")]
        public DateTime RegDt { get; set; }

        [EpplusDisplay(Name = "Имя пользователя")]
        public string CustomerName { get; set; }
    }
}
