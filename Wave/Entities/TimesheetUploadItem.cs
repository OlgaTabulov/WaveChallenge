using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class TimesheetUploadItem
    {
        public DateTime TimeEntry { get; set; }
        public int EmployeeId { get; set; }
        public double Hours { get; set; }
        public string JobGroup { get; set; }

        public decimal AmountPaid { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public int ReportId { get; set; }
        public int InFileLineNumber { get; set; }
    }
}
