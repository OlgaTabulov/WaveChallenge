using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities.CommonEntities
{
    public class ResultView
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Value { get; set; }
    }

    public class ResultObject
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }
    }

}