using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controles_ucLoginEsavConvert : BaseUserControl
{
    #region "Propiedades"
    public bool isRedirect { set; get; }
    public bool goHome { set; get; }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("Jquery")) { Page.ClientScript.RegisterClientScriptInclude("Jquery", "../gogdn/library/jquery/jquery-1.4.2.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("JqueryBlock")) { Page.ClientScript.RegisterClientScriptInclude("JqueryBlock", "../gogdn/library/jquery/jquery.blockUI.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript", "../gogdn/library/js/global.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript2")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript2", "../gogdn/library/jquery/jquery-ui.min.js"); }
     
    }



    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //   imgLoader.Src = WebHelper.getProtocoloUrl() + "/gogdn/library/img/loading.gif";

            lnkOlvidoContrasenha.Attributes.Add("href", "#");
            lnkOlvidoContrasenha.Attributes.Add("onClick", string.Format("lnkOlvidoContrasenha_onClick('{0}')", WebHelper.getProtocoloUrl() + "/system/seguridad/solicitudCambioPassword.aspx"));
            // String urlRedireccion = WebHelper.getProtocoloUrl() + "/FormDocuments/Comun/FrmInicio.aspx";
            // Response.Redirect(urlRedireccion, true);
            string usuario = "";
            string Pass = "";
            usuario = txtUserName.Text;
            Pass = txtPassword.Text;
            AutenticarUsuario(usuario, Pass);


        }
        else
        {
            String urlRedireccion = WebHelper.getProtocoloUrl() + "/FormDocuments/Mantenimientos/FrmAgencia.aspx";
            Response.Redirect(urlRedireccion, true);
        }

    }



    protected void AutenticarUsuario(String _cUsuario, String _cPassword)
    {
        wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
        var dat = servicioDocument.ObtenerUsuarioLoginSE(_cUsuario, _cPassword);
        if (dat.CodigoEmisor == null)
        {
            dvMensajeError.Visible = true;
            ltErrorMensaje.Text = "Datos Incorrectos";
            txtUserName.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtUserName.Focus();
            return;
        }
        else
        {
            WebSession wSession = new WebSession();
            wSession.CodigoEmisor = dat.CodigoEmisor;
            Session["emisor"] = dat.CodigoEmisor;
            wSession.UsuarioId = dat.UsuarioId;
            wSession.Nombres = dat.Nombres;
            wSession.Ruc = dat.Ruc;
            wSession.Sucursal = dat.Sucursal;
            wSession.RazonSocial = dat.RazonSocial;
            wSession.Direccion = dat.Direccion;
            wSession.Email = dat.Email;
            HttpContext.Current.Session["isLogin"] = 1;
            HttpContext.Current.Session["SessionUser"] = wSession;
            HttpContext.Current.Session["currentCultureID"] = "1";
            HttpContext.Current.Session["currentCultureKey"] = "es-PE";
            wSession.mGuil = Guid.NewGuid().ToString();

            String urlRedireccion = WebHelper.getProtocoloUrl() + "/FormDocuments/Comun/FrmInicio.aspx";
            Response.Redirect(urlRedireccion, true);


        }


    }


}