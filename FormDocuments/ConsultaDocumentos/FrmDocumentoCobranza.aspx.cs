using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.Threading;
using System.IO;

public partial class FormDocuments_ConsultaDocumentos_FrmDocumentoCobranza : BasePage
{
    private int curPage = 1;
    private int totalRecords = 1;
    private int totalPages = 1;
    private int pageSize = 20;
    Dictionary<String, String> dicStyle = new Dictionary<string, string>();
    Dictionary<String, String> dicStyle12 = new Dictionary<string, string>();
    Dictionary<String, String> dicStyle13 = new Dictionary<string, string>();
    Boolean _isBackground = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("Jquery")) { Page.ClientScript.RegisterClientScriptInclude("Jquery", "../../gogdn/library/jquery/jquery-1.4.2.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("JqueryBlock")) { Page.ClientScript.RegisterClientScriptInclude("JqueryBlock", "../../gogdn/library/jquery/jquery.blockUI.js"); }
        // if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript", "../../gogdn/library/js/global.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript2")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript2", "../../gogdn/library/jquery/jquery-ui.min.js"); }

        string idemisor = CodigoEmisor;
        if (!Page.IsPostBack)
        {
            string urlCancelar = string.Format(WebHelper.getProtocoloUrl() + "/FormDocuments/Comun/FrmInicio.aspx", 0);
            btnSalir.Attributes.Add("onClick", string.Format("lnkCancelar_onClick('{0}');", urlCancelar));

            cargarGrilla(DateTime.MinValue.AddYears(1900), DateTime.MinValue.AddYears(1900),  1);
        }
    }

    private void cargarGrilla(DateTime _fechaInicial, DateTime _FechaFinal,  int idx)
    {
        RepConcBE servicioDocument = new RepConcBE();

        string FInicial = _fechaInicial.Year + "-" + string.Format("{0:00}", _fechaInicial.Month) + "-" + string.Format("{0:00}", _fechaInicial.Day);
        string FFinal = _FechaFinal.Year + "-" + string.Format("{0:00}", _FechaFinal.Month) + "-" + string.Format("{0:00}", _FechaFinal.Day);

        totalRecords = servicioDocument.obtenerRepDocCobranzaFilasDA(CodigoEmisor, FInicial, FFinal);
        totalPages = totalRecords / pageSize;
        if ((pageSize * totalPages) != totalRecords)
        {
            totalPages++;
        }


        List<RepConcBE> lRepDocumento = servicioDocument.ListarRepDocCobranzaDA(CodigoEmisor, FInicial, FFinal, pageSize, idx).ToList();
        rptListado.DataSource = lRepDocumento;
        rptListado.DataBind();

    }

    public void cargarGrillas(string _fechaInicial, string _FechaFinal,  string idx)
    {
        try
        {
            curPage = Convert.ToInt32(idx);
            DateTime fechaini = Convert.ToDateTime(_fechaInicial);
            DateTime fechafin = Convert.ToDateTime(_FechaFinal);
            cargarGrilla(fechaini, fechafin,  curPage);

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

    public void btngenera_Click(object sender, EventArgs e)
    {
        string _fechaInicial = txtFechaInicial.Text;
        string _FechaFinal = txtFechaFinal.Text;
       
        string idemisor = CodigoEmisor;



        RepConcBE servicioDocument = new RepConcBE();
        List<RepConcBE> lRepDocumento = servicioDocument.ObtenerreportedocCobranzaexcelDA(CodigoEmisor, _fechaInicial, _FechaFinal).ToList();
        List<RepConcBE> lRepDocumentoresumen = servicioDocument.ObtenerreportedocCobranzaresumenexcelDA(CodigoEmisor, _fechaInicial, _FechaFinal).ToList();

        String fileName = "Reporte Contable del " + _fechaInicial + " hasta " + _FechaFinal + ".xls";
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
                String dato = "";
                ConfiguracionBE servicioDocument2 = new ConfiguracionBE();
                string logo = servicioDocument2.obtenerconfiguracionlogoDA(CodigoEmisor);

                dato = String.Format("<img src='{0}'/><br><br><br><br><br><div style='font-weight: bold; font-size: 26pt; text-align: center;'>{1}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{2}</p></div>", WebHelper.getProtocoloUrl() + "/gogdn/library/img/" + logo, "Reporte Documentos Emitidos del " + _fechaInicial + " hasta " + _FechaFinal, "Reporte EsavDoc R2.1");
               
                String xlsTitle = dato;
                Literal lt = new Literal();
                lt.Text = "&nbsp;<br>" + xlsTitle + "<br>";
                lt.RenderControl(htw);

                // Write Header
                table.Rows.Add(WriteHeader());
                WriteItem(lRepDocumento, table);

                table.RenderControl(htw);

            }



            using (HtmlTextWriter htw1 = new HtmlTextWriter(sw))
            {
                Table table1 = new Table();
                table1.Font.Name = "Arial";
                table1.Font.Size = 8;
                table1.CellPadding = 0;
                table1.CellSpacing = 0;
                String dato = "";

                dato = String.Format("<div style='font-weight: bold; font-size: 30pt; text-align: left;'>{0}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{1}</p></div>", "Resumen Totales", "                    Reporte EsavDoc R2.1");

                String xlsTitle = dato;
                Literal lt = new Literal();
                lt.Text = "&nbsp;<br>" + xlsTitle + "<br>";
                lt.RenderControl(htw1);

                // Write Header
                table1.Rows.Add(WriteHeaderresumen());
                WriteIteRESUMEN(lRepDocumentoresumen, table1);

                table1.RenderControl(htw1);
            }
            Response.Write(sw.ToString());
            Response.End();
        }
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    #region "Exportar Excel"

    public TableRow WriteHeader()
    {
        TableRow rowHeader = new TableRow();
        dicStyle.Clear();
        dicStyle.Add("color", "#fff");
        dicStyle.Add("text-align", "center");
        dicStyle.Add("background-color", "#00008B");
        dicStyle.Add("font-weight", "bold");
        dicStyle.Add("border", "solid thin #000");

        rowHeader.Cells.Add(WriteCellStyle("RUC", 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("RAZON SOCIAL", 800, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("FECHA DE EMISION", 90, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("TIPO DOCUMENTO", 90, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("Nro. DOCUMENTO", 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("SUCURSAL", 170, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("MONEDA", 80, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("BASE IMPONIBLE", 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTotalIgv, 60, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("ISC", 60, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("OTROS CARGOS", 80, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("OTROS TRIBUTOS", 80, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("IMPORTE EN S/.", 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("IMPORTE EN $", 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("TC", 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("BASE GRAVADO", 75, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("BASE EXONERADO", 75, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("BASE INAFECTO", 75, dicStyle));

        return rowHeader;
    }


    public TableRow WriteHeaderresumen()
    {
        TableRow rowHeader = new TableRow();
        dicStyle.Clear();
        dicStyle.Add("color", "#fff");
        dicStyle.Add("text-align", "center");
        dicStyle.Add("background-color", "#00008B");
        dicStyle.Add("font-weight", "bold");
        dicStyle.Add("border", "solid thin #000");


        rowHeader.Cells.Add(WriteCellStyle("TIPO DOCUMENTO", 90, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("SUCURSAL", 800, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("DOC. EMITIDOS", 80, dicStyle));


        return rowHeader;
    }


    private void WriteItem(List<RepConcBE> lreportes, Table table)
    {
        TableRow rowItem = new TableRow();
        int cant = lreportes.Count;
        int corre = 0;
        foreach (var item in lreportes)
        {
            corre = corre + 1;
            rowItem = new TableRow();
            dicStyle.Clear();
            dicStyle12.Clear();
            dicStyle13.Clear();
            dicStyle.Add("text-align", "center");
            dicStyle.Add("background-color", "#D3D3D3");
            dicStyle.Add("border", "solid thin #000");
            dicStyle.Add("color", "#000000");
            dicStyle.Add("font-weight", "bold");
            dicStyle12.Add("text-align", "left");
            dicStyle12.Add("background-color", "#D3D3D3");
            dicStyle12.Add("border", "solid thin #000");
            dicStyle12.Add("color", "#000000");
            dicStyle12.Add("font-weight", "bold");
            dicStyle13.Add("text-align", "center");
            dicStyle13.Add("background-color", "#FFB6C1");
            dicStyle13.Add("border", "solid thin #000");
            dicStyle13.Add("color", "#000000");
            dicStyle13.Add("font-weight", "bold");

            if (corre != cant)
            {
                rowItem.Cells.Add(WriteCellStyle(item.RucAgencia, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(item.NombreAgencia, dicStyle12));
                rowItem.Cells.Add(WriteCellStyle(item.FechaEmisionREP, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(item.TipoDocumento, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(item.NroDocumento, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(item.Sucuresal, dicStyle12));
                rowItem.Cells.Add(WriteCellStyle(item.Moneda, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ValorVenta), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalIgv), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalIsc), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalOtros), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalOtrostributos), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ImporteTotal), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ImporteTotaldolares), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TipoCambio), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Gravado), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Exonerado), dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Inafecto), dicStyle));
            }
            else
            {
                rowItem.Cells.Add(WriteCellStyle(item.RucAgencia, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(item.NombreAgencia, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(item.FechaEmisionREP, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(item.TipoDocumento, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle("--", dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(item.Sucuresal, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(item.Moneda, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ValorVenta), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalIgv), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalIsc), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalOtros), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalOtrostributos), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ImporteTotal), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ImporteTotaldolares), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(0.00), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Gravado), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Exonerado), dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Inafecto), dicStyle13));
            }
            _isBackground = !_isBackground;
            table.Rows.Add(rowItem);

        }
        // rowItem.Cells.Add(WriteCellStyleColumnSpan("", dicStyle, 3));
        table.Rows.Add(rowItem);
    }


    private void WriteIteRESUMEN(List<RepConcBE> lreportes, Table table)
    {
        TableRow rowItem = new TableRow();
        int cant = lreportes.Count;
        int corre = 0;
        foreach (var item in lreportes)
        {
            corre = corre + 1;
            rowItem = new TableRow();
            dicStyle.Clear();
            dicStyle12.Clear();
            dicStyle13.Clear();
            dicStyle.Add("text-align", "center");
            dicStyle.Add("background-color", "#D3D3D3");
            dicStyle.Add("border", "solid thin #000");
            dicStyle.Add("color", "#000000");
            dicStyle.Add("font-weight", "bold");
            dicStyle12.Add("text-align", "left");
            dicStyle12.Add("background-color", "#D3D3D3");
            dicStyle12.Add("border", "solid thin #000");
            dicStyle12.Add("color", "#000000");
            dicStyle12.Add("font-weight", "bold");
            dicStyle13.Add("text-align", "center");
            dicStyle13.Add("background-color", "#FFB6C1");
            dicStyle13.Add("border", "solid thin #000");
            dicStyle13.Add("color", "#000000");
            dicStyle13.Add("font-weight", "bold");

            if (corre != cant)
            {

                rowItem.Cells.Add(WriteCellStyle(item.TipoDocumento, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(item.Sucuresal, dicStyle12));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Fila), dicStyle));
            }
            else
            {

                rowItem.Cells.Add(WriteCellStyle(item.TipoDocumento, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(item.Sucuresal, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Fila), dicStyle13));
            }
            _isBackground = !_isBackground;
            table.Rows.Add(rowItem);

        }
        // rowItem.Cells.Add(WriteCellStyleColumnSpan("", dicStyle, 3));
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

 
}