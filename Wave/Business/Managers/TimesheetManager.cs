using DataAccess;
using Entities.CommonEntities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Enums;

namespace Business.Managers
{
    public class TimesheetManager
    {
        public static List<TimesheetReportItemView> GetTimesheetReportItems()
        {
            var timesheetReportItems = new List<TimesheetReportItemView>();
            var sqlReportList = TimesheetDA.GetReport();
            if (sqlReportList.Success)
            {
                foreach(TimesheetReportItemSql row in ((List<TimesheetReportItemSql>)sqlReportList.Value))
                {
                    timesheetReportItems.Add(new TimesheetReportItemView()
                    {
                        EmployeeId = row.EmployeeId,
                        PayPeriod = GetPeriodString(row.PayPeriodStart),
                        AmountPaid = GetCurrency(row.AmountPaid)
                    });
                }
            }

            //TODO: we are loosing the db error return here, consider logging
            return timesheetReportItems;
        }

        //expecting 1st or 15th of the month
        public static string GetPeriodString(DateTime startDate)
        {
            //TODO: implement user culture
            if (startDate.Day == 1)
            {
                return startDate.ToString("dd/mm/yyyy") + " - " + startDate.AddDays(14).ToString("dd/mm/yyyy");
            }
            else
            {
                return startDate.ToString("dd/mm/yyyy") + " - " + startDate.AddMonths(1).AddDays(-16).ToString("dd/mm/yyyy");
            }
        }
        public static string GetCurrency(Decimal amount)
        {
            return string.Format("{0:c}", amount);
        }

        public static ResultView InsertRows(List<TimesheetUploadItem> timesheetUploadList, int reportId)
        {
            foreach (var item in timesheetUploadList)
            {
                item.AmountPaid = (decimal)item.Hours * GetHourlySalary(item.JobGroup);
                item.PayPeriodStart = GetPayPeriodStart(item.TimeEntry);
                item.ReportId = reportId;
            }
            return TimesheetDA.InsertTimesheetBatch(timesheetUploadList);
        }

        private static DateTime GetPayPeriodStart(DateTime timeEntry)
        {
            return new DateTime(timeEntry.Year, timeEntry.Month,
                timeEntry.Day < 16 ? 1 : 16);            
        }

        private static int GetHourlySalary(string jobGroup)
        {
            JobGroupRate rate = (JobGroupRate)Enum.Parse(typeof(JobGroupRate), jobGroup);
            //in realistic case, check fail cases, but the assumption of assignment is the values are correct
            return (int)rate;
        }

    }
}
