using Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
namespace Business.Common
{
    public class Util
    {
        public static string GetResult(bool success, string message, string value)
        {
            var result = new ResultView();
            result.Success = success;
            result.Message = message;
            result.Value = value;

            var resultString = new JavaScriptSerializer().Serialize(result);
            return resultString;
        }
    }
    
}