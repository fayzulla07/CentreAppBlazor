using CentreAppBlazor.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Report
{
    public class SaleReportDto
    {
        [EpplusIgnore]
        public int Id { get; set; }

        [EpplusDisplay(Name="Имя")]
        public string Name { get; set; }
        [EpplusDisplay(Name = "Коль-во")]
        public int Amount { get; set; }

        [EpplusDisplay(Name = "Цена продажи")]
        public double SaleCost { get; set; }

        [EpplusDisplay(Name = "Сумма")]
        public double TotalSaleCost { get; set; }

        [EpplusDisplay(Name = "Клиент")]
        public string Client { get; set; }

        [EpplusDisplay(Name = "Оптом")]
        public string OptCost { get; set; }

        [EpplusDisplay(Name = "Дата рег")]
        public DateTime RegDt { get; set; }

        [EpplusDisplay(Name = "Номер заказа")]
        public int OrderNumber { get; set; }

        [EpplusDisplay(Name = "Комментарии")]
        public string Comments { get; set; }

        [EpplusDisplay(Name = "Банк")]
        public bool IsBank { get; set; }
    }
}
