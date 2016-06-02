using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.Threading;
using System.IO;

public partial class FormDocuments_Reportes_FrmReporteEmitidos : BasePage
{
    private int curPage = 1;
    private int totalRecords = 1;
    private int totalRegistros = 1;
    private int totalPages = 1;
    private int pageSize = 5;
    Dictionary<String, String> dicStyle = new Dictionary<string, string>();
    Boolean _isBackground = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("Jquery")) { Page.ClientScript.RegisterClientScriptInclude("Jquery", "../../gogdn/library/jquery/jquery-1.4.2.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("JqueryBlock")) { Page.ClientScript.RegisterClientScriptInclude("JqueryBlock", "../../gogdn/library/jquery/jquery.blockUI.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript2")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript2", "../../gogdn/library/jquery/jquery-ui.min.js"); }


        if (!Page.IsPostBack)
        {
            string urlCancelar = string.Format(WebHelper.getProtocoloUrl() + "/FormDocuments/Comun/FrmInicio.aspx", 0);
            btnSalir.Attributes.Add("onClick", string.Format("lnkCancelar_onClick('{0}');", urlCancelar));

            cargarGrilla(CodigoEmisor, "", "", "", 0, DateTime.MinValue.AddYears(1900), DateTime.MinValue.AddYears(1900), "");
        }
    }

    private void cargarGrilla(string _CodUsuario, string _RazonSocial, string _NroFactura, string _serie, decimal _monto, DateTime _FechIni, DateTime _FechFin, string _tipodoc)
    {
        ClGenerarCass objGenerar = new ClGenerarCass();
        //wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
        //    List<wswsDocument.EmisorBE> lEmisor = new List<wswsDocument.EmisorBE>();

        totalRecords = objGenerar.obtenerTotalesReporteEmitido(_CodUsuario, _RazonSocial, _NroFactura, _serie, _monto, _FechIni, _FechFin, _tipodoc);
        //totalRecords = servicioDocument.obtenerRepDocumentoCriteriosFilasSE(CodUsuario, RazonSocial, NroFactura, serie, monto, _FechIni, _FechFin, tipodoc);

        
        
        totalPages = totalRecords / pageSize;
        if ((pageSize * totalPages) != totalRecords)
        {
            totalPages++;
        }

        List<ReporteEmitido> lRepEmitido = objGenerar.ListarReporteEmitido(_CodUsuario, _RazonSocial, _NroFactura, _serie, _monto, pageSize, curPage - 1, _FechIni, _FechFin, _tipodoc);
        //List<wswsDocument.RepDocumentoBE> lRepDocumento = servicioDocument.ListarRepDocumentoCriteriosSE(CodUsuario, RazonSocial, NroFactura, serie, monto, pageSize, curPage - 1, _FechIni, _FechFin, tipodoc).ToList();
        rptListado.DataSource = lRepEmitido;
        rptListado.DataBind();

            //servicioDocument.Close();
        
        
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

    protected void txtNombres_TextChanged(object sender, EventArgs e)
    {

    }

    protected void rptListado_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void rptListado_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            ReporteEmitido objReporteEmitido = (ReporteEmitido)e.Item.DataItem;
            //if (objReporteEmitido.CodigoRes.Equals("0"))
            //{

            //}

        }

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

    public bool IsDate(string sdate)
    {
        DateTime dt;
        bool isDate = true;
        try
        {
            dt = DateTime.Parse(sdate);
        }
        catch
        {
            isDate = false;
        }
        return isDate;
    }

    public String FormatDateTime(Object mDate)
    {
        DateTime mDateTime;

        if (IsDate(mDate.ToString()))
        {
            mDateTime = DateTime.Parse(mDate.ToString());
            return mDateTime.ToString(GetFormatDate() + " HH:mm");
        }
        else
        {
            return "";
        }
    }

    #region "Exportar Excel"

    public void MovimientosReport(string _CodUsuario, string _RazonSocial, string _NroFactura, string _serie, decimal _monto, DateTime _FechIni, DateTime _FechFin, string _tipodoc)
    {
        //DateTime fechaini = Convert.ToDateTime(_fechaInicial);
        //DateTime fechafin = Convert.ToDateTime(_FechaFinal);
        ClGenerarCass objGenerar = new ClGenerarCass();
        //wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
        totalRegistros = objGenerar.obtenerTotalesReporteEmitido(_CodUsuario, _RazonSocial, _NroFactura, _serie, _monto, _FechIni, _FechFin, _tipodoc);

        if (totalRegistros > 3000)
        {
            Response.Write("<script language=javascript>alert('El rango de fecha ingresado, supera el limite de registros. Ingrese un rango de fecha menor o de lo contrario el numero de serie.');</script>");
        }
        else
        {
            List<ReporteEmitido> lRepEmitido = objGenerar.ListarReporteEmitido(_CodUsuario, _RazonSocial, _NroFactura, _serie, _monto, pageSize, curPage - 1, _FechIni, _FechFin, _tipodoc);

            //List<wswsDocument.ReporteConcarBE> lRepDocumento = servicioDocument.ListarRepConcarCriteriosSE(CodigoEmisor, fechaini, fechafin, tipodoc, 10000, 0).ToList();
            //  _isBackground = false;

            String fileName = "ARCHIVO_GENERADO_REPORTE_EMITIDO.xls";
            HttpContext.Current.Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Default;

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    Table table = new Table();
                    table.Font.Name = "Arial";
                    table.Font.Size = 8;
                    table.CellPadding = 0;
                    table.CellSpacing = 0;

                    //    String _img = String.Format("<img src='{0}'/>", ApplicationUrl("library/img/logoes-es.gif"));
                    String xlsTitle = String.Format("<div style='font-weight: bold; font-size: 14pt; text-align: center;'>{0}</div>", "Reporte Emitido");
                    Literal lt = new Literal();
                    lt.Text = "&nbsp;<br>" + xlsTitle + "<br>";
                    lt.RenderControl(htw);

                    // Write Header
                    table.Rows.Add(WriteHeader());
                    WriteItem(lRepEmitido, table);

                    table.RenderControl(htw);

                    Response.Write(sw.ToString());
                    Response.End();
                    //  Response.Flush();
                    // Response.Close();

                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    /*
                                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                                    Response.Write(sw.ToString());
                                  */
                }
            }
        }
        
    }



    public TableRow WriteHeader()
    {
        TableRow rowHeader = new TableRow();
        dicStyle.Clear();
        dicStyle.Add("color", "#fff");
        dicStyle.Add("text-align", "center");
        dicStyle.Add("background-color", "#a6caf0");
        dicStyle.Add("font-weight", "bold");
        dicStyle.Add("border", "solid thin #000");

        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblDocumento, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblEmisorDocumento, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblRango, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblSerie, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblNroDocumento, 100, dicStyle));

        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblRazonSocial, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFechaEmision, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblSucursal, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblMoneda, 120, dicStyle));

        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblCantidad, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblValorUnitario, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblDescuento, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("Base Imponible", 100, dicStyle));

        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblIgv, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblImporteSoles, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblOtrosCargos, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblOtrosTributos, 120, dicStyle));

        
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblImporteTotal, 100, dicStyle));
       

        return rowHeader;
    }



    private void WriteItem(List<ReporteEmitido> lreportes, Table table)
    {
        TableRow rowItem = new TableRow();
        foreach (var item in lreportes)
        {
            rowItem = new TableRow();
            dicStyle.Clear();
            dicStyle.Add("text-align", "center");

            
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.IdDocumento), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.IdEmisor, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.IdRango), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.Serie, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.NroDocumento, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.RazonSocial, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(FormatDateTime(item.FechaEmision), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.Sede, dicStyle));

            rowItem.Cells.Add(WriteCellStyle(item.Moneda, dicStyle));

            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Cantidad), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ValorUnitario), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Descuento), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.BaseImponible), dicStyle));

            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.IGV), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ISC), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.OtrosCargos), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.OtrosTributos), dicStyle));
            
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ImporteTotal), dicStyle));
          
            _isBackground = !_isBackground;
            table.Rows.Add(rowItem);
        }
        rowItem.Cells.Add(WriteCellStyleColumnSpan("", dicStyle, 3));
        table.Rows.Add(rowItem);
    }

    public static string ToTitleCase(String sentence)
    {
        return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(sentence);
    }

    private TableCell WriteCellStyle(String title, Int32 width, Dictionary<String, String> dicStyles)
    {
        TableCell output = new TableCell();
        output.Text = title;
        output.Width = width;
        foreach (String key in dicStyles.Keys)
        {
            output.Style.Add(key, dicStyles[key]);
        }
        return output;
    }

    private TableCell WriteCellStyle(String title, Dictionary<String, String> dicStyles)
    {
        TableCell output = new TableCell();
        output.Text = title;
        foreach (String key in dicStyles.Keys)
            output.Style.Add(key, dicStyles[key]);
        return output;
    }

    private TableCell WriteCellStyleColumnSpan(String title, Dictionary<String, String> dicStyles, Int32 columnSpan)
    {
        TableCell output = new TableCell();
        output.Text = title;
        output.ColumnSpan = columnSpan;
        foreach (String key in dicStyles.Keys)
            output.Style.Add(key, dicStyles[key]);
        return output;
    }

    #endregion



    protected void btngenera_Click(object sender, EventArgs e)
    {

        DateTime FECHAINI = Convert.ToDateTime(txtFechaInicial.Text);
        DateTime FECHAFIN = Convert.ToDateTime(txtFechaFinal.Text);
        string tipodoc = ddlTipoDocumento.Value;
        string RazonSocial = txtNombres.Text;
        string NroFactura = txtNroDocumento.Text;
        string serie = txtNroDocumento0.Text;

        MovimientosReport(CodigoEmisor, RazonSocial, NroFactura, serie, 0, FECHAINI, FECHAFIN, tipodoc); 

    }

}