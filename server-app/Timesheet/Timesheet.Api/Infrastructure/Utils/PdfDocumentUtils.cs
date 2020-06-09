using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Timesheet.Core.Models.Salaries;

namespace Timesheet.Api.Infrastructure.Utils
{
    public static class PdfDocumentUtils
    {
        public static string GetMonthlySalaryChartUrl(MonthlySalaryModel model)
        {
            var monthLabels = model.Days
                .Select(x => x.Date.Day > 9 ? x.Date.Day.ToString() : "0" + x.Date.Day.ToString())
                .Aggregate(string.Empty, (acc, val) => acc + $"'{val}',")
                .TrimEnd(',');

            var estimatedHoursDataList = new List<decimal>();
            var workedHoursDataList = new List<decimal>();
            foreach (var day in model.Days)
            {
                estimatedHoursDataList.Add(day.PredictedHours + estimatedHoursDataList.LastOrDefault());
                workedHoursDataList.Add(day.WorkedHours + workedHoursDataList.LastOrDefault());
            }

            var estimatedHoursData = estimatedHoursDataList.Aggregate(string.Empty, (acc, val) => acc + $"'{val.ToString(CultureInfo.InvariantCulture)}',").TrimEnd(',');
            var workedHoursData = workedHoursDataList.Aggregate(string.Empty, (acc, val) => acc + $"'{val.ToString(CultureInfo.InvariantCulture)}',").TrimEnd(',');

            return "https://quickchart.io/chart?c={type:'line',data:{labels:["
                + monthLabels
                + "], datasets:[{label:'Estimated hours', data: ["
                + estimatedHoursData
                + "], fill:false,borderColor:'blue'},{label:'Worked hours', data:["
                + workedHoursData
                + "], fill:false,borderColor:'green'}]}}";
        }

        public static string GetMonthlyProjectTasksTimeConsumingChartUrl(MonthlyTimeConsumingProjectModel model)
        {
            var taskNames = model.Tasks
                .Aggregate(string.Empty, (acc, val) => acc + $"'{val.Name}',")
                .TrimEnd(',');
            
            var dataSets = model.Tasks
                .Aggregate(string.Empty, (acc, val) => acc + $"'{val.ConsumedHours.ToString(CultureInfo.InvariantCulture)}',").TrimEnd(',');

            return "https://quickchart.io/chart?c={type:'doughnut',data:{labels:["
                + taskNames
                + "],datasets:[{data:["
                + dataSets
                + "]}]},options:{plugins:{doughnutlabel:{labels:[{text:'"
                + model.ConsumedHours
                + "',font:{size:20}},{text:'total'}]}}}}";
        }

        public static string GetMonthAndYearFromDateTime(DateTime dateTime)
        {
            return dateTime.ToString("MMMM yyyy", CultureInfo.InvariantCulture);
        }


        //https://quickchart.io/chart?c={type:'pie',data:{labels:['January','February', 'March','April', 'May'], datasets:[{data:[50,60,70,180,190]}]}}
    }
}
