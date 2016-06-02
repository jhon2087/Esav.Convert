using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FormDocuments_Comun_FrmNuevoTipoCambio : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("Jquery")) { Page.ClientScript.RegisterClientScriptInclude("Jquery", "../../gogdn/library/jquery/jquery-1.4.2.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("JqueryBlock")) { Page.ClientScript.RegisterClientScriptInclude("JqueryBlock", "../../gogdn/library/jquery/jquery.blockUI.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript2")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript2", "../../gogdn/library/jquery/jquery-ui.min.js"); }
        //   if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript", "../../gogdn/library/js/global.js"); }

    }

    public void InsertarTipoCambio(string fecha, string monto)
    {

        // mGuill = Guid.NewGuid().ToString();
        wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
        wswsDocument.TipoCambioBE objtipocambio = new wswsDocument.TipoCambioBE();
        objtipocambio.FechaTipoCambio = Convert.ToDateTime(fecha);
        objtipocambio.Monto = Convert.ToDecimal(monto);
        objtipocambio.Registro = 1;
        objtipocambio.UsuarioCreo = UsuarioId;
        objtipocambio.IpCreacion = UsuarioIP;

        String respuesta = servicioDocument.registrarTipoCambioSE(objtipocambio);
        if (respuesta.Trim().Equals("OK"))
        {


            AddCallbackValue("1");
            AddCallbackValue("ok");
        }
        else
            AddCallbackValue("-1");
        AddCallbackValue(respuesta);
    }


}