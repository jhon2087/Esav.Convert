using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main : BasePageMaster
{
    protected void Page_Load(object sender, EventArgs e)
    {


        CurrentUser = (WebSession)Session["SessionUser"];

        if (CurrentUser != null)
        {
            lnkLogout.Visible = true;
            lblUsuario.Text = CurrentUser.RazonSocial;
            lblNombreUser.Text = CurrentUser.Nombres;

            ConfiguracionBE servicioDocument = new ConfiguracionBE();
            string logo = servicioDocument.obtenerconfiguracionlogoDA(CurrentUser.CodigoEmisor.Trim());

            Image1.ImageUrl = WebHelper.getProtocoloUrl() + "/gogdn/library/img/" + logo;

            /*
            if (CurrentUser.CodigoEmisor.Trim() == "AG0000000002")
                Image1.ImageUrl = WebHelper.getProtocoloUrl() + "/gogdn/library/img/carlsonlogo1.jpg";
            else
                if (CurrentUser.CodigoEmisor.Trim() == "OT0000000005")
                    Image1.ImageUrl = WebHelper.getProtocoloUrl() + "/gogdn/library/img/rokylogo.jpg";

            else
                if (CurrentUser.CodigoEmisor.Trim() == "AG0000000031")
                    Image1.ImageUrl = WebHelper.getProtocoloUrl() + "/gogdn/library/img/logotravel1.jpg";

           */
        }
        else
            System.Web.HttpContext.Current.Response.Redirect(WebApplication.getInstance().getUrlWebInicial);
    }


    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        System.Web.HttpContext.Current.Session.Abandon();
        //System.Web.HttpContext.Current.Response.Redirect("~/system/aerolinea/default.aspx");
        string IN = WebApplication.getInstance().getUrlWebInicial;
        System.Web.HttpContext.Current.Response.Redirect(WebApplication.getInstance().getUrlWebInicial);
    }




}
