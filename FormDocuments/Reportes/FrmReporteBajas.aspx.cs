using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FormDocuments_Reportes_ReporteBajas : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("Jquery")) { Page.ClientScript.RegisterClientScriptInclude("Jquery", "../../gogdn/library/jquery/jquery-1.4.2.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("JqueryBlock")) { Page.ClientScript.RegisterClientScriptInclude("JqueryBlock", "../../gogdn/library/jquery/jquery.blockUI.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript2")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript2", "../../gogdn/library/jquery/jquery-ui.min.js"); }


        if (!Page.IsPostBack)
        {
        }
    }

     protected void btngenera_Click(object sender, EventArgs e)
    {

    }

     protected void rptListado_ItemCommand(object source, RepeaterCommandEventArgs e)
     {

     }

     protected void rptListado_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

}