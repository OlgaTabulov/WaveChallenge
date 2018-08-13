using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class TimesheetReportItemView
    {
        public int EmployeeId { get; set; }
        public string PayPeriod { get; set; }
        public string AmountPaid { get; set; }
    }
}
