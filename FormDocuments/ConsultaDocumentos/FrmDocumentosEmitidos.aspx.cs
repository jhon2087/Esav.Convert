using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;

public partial class FormDocuments_ConsultaDocumentos_FrmDocumentosEmitidos : BasePage
{
    private int curPage = 1;
    private int totalRecords = 1;
    private int totalPages = 1;
    private int pageSize = 7;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.ClientScript.IsClientScriptIncludeRegistered("Jquery")) { Page.ClientScript.RegisterClientScriptInclude("Jquery", "../../gogdn/library/jquery/jquery-1.4.2.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("JqueryBlock")) { Page.ClientScript.RegisterClientScriptInclude("JqueryBlock", "../../gogdn/library/jquery/jquery.blockUI.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript2")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript2", "../../gogdn/library/jquery/jquery-ui.min.js"); }


        if (!Page.IsPostBack)
        {

            string urlCancelar = string.Format(WebHelper.getProtocoloUrl() + "/FormDocuments/Comun/FrmInicio.aspx", 0);
            btnSalir.Attributes.Add("onClick", string.Format("lnkCancelar_onClick('{0}');", urlCancelar));

            cargarGrilla(UsuarioId, "", "", "", 0, DateTime.MinValue.AddYears(1900), DateTime.MinValue.AddYears(1900), "");
        }


    }

    private void cargarGrilla(string CodUsuario, string RazonSocial, string NroFactura, string serie, decimal monto, DateTime _FechIni, DateTime _FechFin, string tipodoc)
    {

        ReporteEmitido servicioDocument = new ReporteEmitido();
      

        totalRecords = servicioDocument.obtenerRepDocumentoCriteriosFilasDA(CodUsuario, RazonSocial, NroFactura, serie, monto, _FechIni, _FechFin, tipodoc);
        totalPages = totalRecords / pageSize;
        if ((pageSize * totalPages) != totalRecords)
        {
            totalPages++;
        }


        List<ReporteEmitido> lRepDocumento = servicioDocument.ListarRepDocumentoCriteriosDA(CodUsuario, RazonSocial, NroFactura, serie, monto, pageSize, curPage - 1, _FechIni, _FechFin, tipodoc).ToList();
        rptListado.DataSource = lRepDocumento;
        rptListado.DataBind();

        
    }
    public void cargarGrillas(string RazonSocial, string NroDocumento, string Serie, string idx, string FECHINI, string FECHFIN, string tipodoc)
    {
        try
        {
            curPage = Convert.ToInt32(idx);
            DateTime fechaini = Convert.ToDateTime(FECHINI);
            DateTime fechafin = Convert.ToDateTime(FECHFIN);
            cargarGrilla(CodigoEmisor, RazonSocial, NroDocumento, Serie, 0, fechaini, fechafin, tipodoc);

            AddCallbackValue("0");
            AddCallbackControl(rptListado);

        }
        catch (FaultException ex)
        {
            AddCallbackValue("-1");
            AddCallbackValue("Service :" + ex.Message);
        }
        catch (Exception ex)
        {
            AddCallbackValue("-1");
            AddCallbackValue("Application :" + ex.Message);
        }
    }

    protected void rptListado_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            //  wswsDocument.AgenciaBE objAgencia = new wswsDocument.AgenciaBE();
            ReporteEmitido objRepDocumento = (ReporteEmitido)e.Item.DataItem;
/*
            HyperLink lnkVerFactura = (HyperLink)e.Item.FindControl("lnkVerFactura");
            lnkVerFactura.Text = Resources.label.lblVer;
            lnkVerFactura.CssClass = "Linked";
            string url = String.Format(WebHelper.getProtocoloUrl() + "/FormDocuments/Mantenimientos/FrmVerPdf.aspx?hdCodFactura={0}&idemisor={1}", objRepDocumento.IdDocumento, objRepDocumento.IdEmisor.Trim());
            lnkVerFactura.Attributes.Add("onclick", String.Format("openGUIDialogIframe('{0}', '{1}', '" + Resources.label.lblVer + "', 851, 950, false)", lnkVerFactura.ClientID, url));

            HyperLink lnkdescargar = (HyperLink)e.Item.FindControl("lnkDescargar");
            lnkdescargar.Text = Resources.label.lblDescargar;
            //lnkdescargar.CssClass = "Linked";

            string urlCancelar = string.Format(WebHelper.getProtocoloUrl() + "/FormDocuments/Mantenimientos/FrmDescargar.aspx?hdCodFactura={0}", objRepDocumento.IdDocumento);
            lnkdescargar.Attributes.Add("onClick", string.Format("lnkDescargar_onClick('{0}');", urlCancelar));
*/

        }
        else
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {

                HyperLink lnkFirst = (HyperLink)e.Item.FindControl("lnkFirst");
                HyperLink lnkPrev = (HyperLink)e.Item.FindControl("lnkPrev");
                HyperLink lnkLast = (HyperLink)e.Item.FindControl("lnkLast");
                HyperLink lnkNext = (HyperLink)e.Item.FindControl("lnkNext");

                Label lblPages = (Label)e.Item.FindControl("lblPages");
                Label lblCurrpage = (Label)e.Item.FindControl("lblCurrpage");
                lblPages.Text = String.Format(Resources.label.lblPagina, curPage.ToString(), totalPages.ToString(), totalRecords.ToString());
                int inicio = 0;
                int fin = 0;
                if (curPage > (pageSize / 2))
                {
                    inicio = curPage - 4;
                    if ((curPage + 5) < totalPages)
                    {
                        fin = curPage + 5;
                    }
                    else
                    {
                        fin = totalPages;
                    }
                }
                else
                {
                    inicio = 1;
                    if (pageSize < totalPages)
                    {
                        fin = pageSize;
                    }
                    else
                    {
                        fin = totalPages;
                    }
                }

                for (int i = inicio; i <= fin; i++)
                {
                    if (i == curPage)
                        lblCurrpage.Text = lblCurrpage.Text + String.Format("&nbsp;<label class='CurrentPageNumber_'>[{0}]</label>", i.ToString());
                    else
                        lblCurrpage.Text = lblCurrpage.Text + String.Format("&nbsp; <a href='javascript:goPage({0})' class='PageNumber_'>{0}</a>", i.ToString()) + ((i == fin) ? "&nbsp;" : "");


                }
                if (curPage == 1)
                {
                    lnkFirst.CssClass = "firstdisable";
                    lnkPrev.CssClass = "prevdisable";
                }
                else
                {
                    lnkFirst.CssClass = "first";
                    lnkPrev.CssClass = "prev";

                    lnkFirst.NavigateUrl = "javascript:goPage(1)";
                    lnkPrev.NavigateUrl = String.Format("javascript:goPage({0})", (curPage - 1).ToString());
                }

                if (curPage == totalPages)
                {
                    lnkLast.CssClass = "lastdisable";
                    lnkNext.CssClass = "nextdisable";
                }
                else
                {
                    lnkLast.CssClass = "last";
                    lnkNext.CssClass = "next";

                    lnkLast.NavigateUrl = String.Format("javascript:goPage({0})", totalPages.ToString());
                    lnkNext.NavigateUrl = String.Format("javascript:goPage({0})", (curPage + 1).ToString());
                }

            }
        }
    }
    protected void rptListado_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void txtNombres_TextChanged(object sender, EventArgs e)
    {

    }

   
}