using CentreAppBlazor.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Report
{
    public class IncomeReportDto
    {
        [EpplusIgnore]
        public int Id { get; set; }

        [EpplusIgnore]
        public int ProductId { get; set; }

        [EpplusDisplay(Name="Имя")]
        public string Name { get; set; }

        [EpplusDisplay(Name = "Код")]
        public string Code { get; set; }

        [EpplusDisplay(Name = "Поставщик")]
        public string SupplierName { get; set; }

        [EpplusDisplay(Name = "Цена прихода")]
        public double IncomeCost { get; set; }

        [EpplusDisplay(Name = "Цена продажи")]
        public double SaleCost { get; set; }

        [EpplusDisplay(Name = "Оптовая цена")]
        public double OptCost { get; set; }

        [EpplusDisplay(Name = "Дата рег")]
        public DateTime? RegDt { get; set; }

        [EpplusDisplay(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [EpplusDisplay(Name = "Коль-во")]
        public double Amount { get; set; }
    }
}
