using Entities.Entities;
using Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entities.CommonEntities;
using System.IO;
using System.Globalization;

namespace Wave.WebServices
{
    /// <summary>
    /// Summary description for TimeSheetService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TimeSheetService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<TimesheetReportItemView> GetTimeSheetsReport()
        {
            var timesheets = TimesheetManager.GetTimesheetReportItems();
            return timesheets;
        }
        [WebMethod]
        public ResultView UploadTimesheetFile()
        {
            ResultView result = new ResultView() { Success = true };
            //            var files = HttpContext.Current.Request.Files["FileUpload"];

            if (HttpContext.Current.Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFile file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" ||
                            HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        string newFilename = AddSuffix(fname, String.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss")));
                        file.SaveAs(newFilename);

                        var fileParseResult = ParseTimesheetFile(file);
                        if (!fileParseResult.Success)
                        {
                            result.Success = false;
                            result.Message += "<br/>" + fileParseResult.Message;
                        }
                    }
                    if (result.Success)
                        result.Message = "File Uploaded Successfully!";
                }
                catch (Exception ex)
                {
                    result.Message = "Error occurred. Error details: " + ex.Message;
                    result.Success = false;
                }
            }
            else
            {
                result.Message = "No files selected.";
                result.Success = false;
            }
            return result;
        }

        private ResultView ParseTimesheetFile(HttpPostedFile file)
        {
            var streamReader = new StreamReader(file.InputStream);
            ResultView result = new ResultView();
            List<TimesheetUploadItem> timesheetUploadList = new List<TimesheetUploadItem>();
            var index = 1;
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                
                string[] columns = line.Split(',');
                if (columns[0] == "report id")
                {
                    int reportId = int.Parse(columns[1]);
                    //TODO: check if it is already in the DB
                    result = TimesheetManager.InsertRows(timesheetUploadList, reportId);
                }
                else if(columns[0] == "date") { }//skip the header
                else
                {
                    TimesheetUploadItem rowItem = new TimesheetUploadItem()
                    {
                        TimeEntry = DateTime.ParseExact(columns[0], "d/M/yyyy", CultureInfo.InvariantCulture),
                        Hours = double.Parse(columns[1]),
                        EmployeeId = int.Parse(columns[2]),
                        JobGroup = columns[3],
                        InFileLineNumber = index
                    };
                    timesheetUploadList.Add(rowItem);
                    index++;
                }
            }
            return result;
        }

        public static string AddSuffix(string filename, string suffix)
        {
            string fDir = Path.GetDirectoryName(filename);
            string fName = Path.GetFileNameWithoutExtension(filename);
            string fExt = Path.GetExtension(filename);
            return Path.Combine(fDir, String.Concat(fName, suffix, fExt));
        }

    }
}
