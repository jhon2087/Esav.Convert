using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FormDocuments_MainConvert : BasePageMaster
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUser = (WebSession)Session["SessionUser"];
        
    }
}
