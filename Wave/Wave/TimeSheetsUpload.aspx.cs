using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wave.Common;
using Business;

namespace Wave
{
    public partial class TimeSheetsUpload : BasePage
    {
        protected BaseMasterPage _master;
        protected void Page_Load(object sender, EventArgs e)
        {
            _master = (BaseMasterPage)Page.Master;
            _master.RegisterClientScriptVariable("WebServicesPath", "WebServices/TimeSheetService.asmx/");
            _master.RegisterClientScriptVariable("DuplicateFileErrorMessage",
                Entities.CommonEntities.Constants.ErrorMessages.DuplicateFileErrorMessage);
            _master.RegisterClientScriptVariable("UploadSuccessfulMessage",
                Entities.CommonEntities.Constants.SuccessMessages.UploadSuccessfulMessage);

        }
    }
}