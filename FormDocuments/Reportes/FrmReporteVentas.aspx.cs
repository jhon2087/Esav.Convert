using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.Threading;
using System.IO;


public partial class FormDocuments_Reportes_FrmReporteVentas : BasePage
{
    private int curPage = 1;
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

       

        if (!Page.IsPostBack)
        {
            string idemisor = CodigoEmisor;
            wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
            List<wswsDocument.CargarDatosBE> DDLlistarseries = servicioDocument.ListarSeriesSE(idemisor, "0").ToList();
            DDLserie.DataSource = DDLlistarseries;
            DDLserie.DataTextField = "serie";
            DDLserie.DataValueField = "codigorango";
            DDLserie.DataBind();
            string urlCancelar = string.Format(WebHelper.getProtocoloUrl() + "/FormDocuments/Comun/FrmInicio.aspx", 0);
            btnSalir.Attributes.Add("onClick", string.Format("lnkCancelar_onClick('{0}');", urlCancelar));

            cargarGrilla(DateTime.MinValue.AddYears(1900), 1,"0");
        }
    }

    private void cargarGrilla(DateTime _fechaInicial, int idx,string _serie)
    {


        RepConcBE servicioDocument = new RepConcBE();
       // string _serie = DDLserie.Text;

        string FInicial = _fechaInicial.Year + "-" + string.Format("{0:00}", _fechaInicial.Month) + "-" + string.Format("{0:00}", _fechaInicial.Day);

        List<RepConcBE> lRepDocumento = servicioDocument.ListarRepVentasConsolidadoDA(CodigoEmisor, FInicial, _serie).ToList();
        rptListado.DataSource = lRepDocumento;
        rptListado.DataBind();

        
    }

    public void cargarGrillas(string _fechaInicial, string idx,string _serie)
    {
        try
        {
            curPage = Convert.ToInt32(idx);
            DateTime fechaini = Convert.ToDateTime(_fechaInicial);

            cargarGrilla(fechaini, curPage, _serie);

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
            RepConcBE objRepDocumento = (RepConcBE)e.Item.DataItem;

        }
        else
        {

            /* if (e.Item.ItemType == ListItemType.Footer)
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
                 }*/

        }
    }

    protected void rptListado_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    /*  public bool IsDate(string sdate)
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
      }*/

    protected void btngenera_Click(object sender, EventArgs e)
    {

        RepConcBE servicioDocument = new RepConcBE();
        string _serie = DDLserie.Text;
        string FInicial = txtFechaInicial.Text;
        
        String fileName = "Reporte consolidado de ventas del "+FInicial+".xls";
        HttpContext.Current.Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.Default;

        List<RepConcBE> lRepDocumento = servicioDocument.exportarrepconsolidadoventasDA(CodigoEmisor, FInicial, _serie).ToList();
        
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
                dato = String.Format("<img src='{0}'/><div style='font-weight: bold; font-size: 30pt; text-align: center;'>{1}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{2}</p></div>", WebHelper.getProtocoloUrl() + "/gogdn/library/img/" + logo, "Reporte Consolidado del " + FInicial, "Reporte EsavDoc R2.2");
                /*
                if (CodigoEmisor == "AG0000000002")
                {
                    dato = String.Format("<img src='{0}'/><div style='font-weight: bold; font-size: 30pt; text-align: center;'>{1}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{2}</p></div>", WebHelper.getProtocoloUrl() + "/gogdn/library/img/carlsonlogo1.jpg", "Reporte Consolidado del " + FInicial  , "Reporte EsavDoc R2.2");
                }
                if (CodigoEmisor == "OT0000000005")
                {
                    dato = String.Format("<img src='{0}'/><div style='font-weight: bold; font-size: 30pt; text-align: center;'><br><br><br><br><br>{1}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{2}</p></div>", WebHelper.getProtocoloUrl() + "/gogdn/library/img/rokylogo.jpg", "Reporte Consolidado del " + FInicial, "Reporte EsavDoc R2.2");
                }
                 */
                String xlsTitle = dato;
                Literal lt = new Literal();
                lt.Text = "&nbsp;<br>" + xlsTitle + "<br>";
                lt.RenderControl(htw);

                ////////// Write Header
                table.Rows.Add(WriteHeaderreportegeneral());
                WriteItemreportegeneral(lRepDocumento, table);

                table.RenderControl(htw);
                /////////////////////////////////////////////////////////////

            }
            Response.Write(sw.ToString());
            Response.End();
        }
                 HttpContext.Current.ApplicationInstance.CompleteRequest();
 
    }

    public TableRow WriteHeaderreportegeneral()
    {
        TableRow rowHeader = new TableRow();
        dicStyle.Clear();
        dicStyle.Add("color", "#fff");
        dicStyle.Add("text-align", "center");
        dicStyle.Add("background-color", "#00008B");
        dicStyle.Add("font-weight", "bold");
        dicStyle.Add("border", "solid thin #000");

        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFechaEmision, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblcTipoDocumento, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblSerie, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("SUCURSAL", 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblRango, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("CANTIDAD DOCUMENTOS", 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblMoneda, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("BASE IMPONIBLE", 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTotalIgv, 60, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("ISC", 60, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("OTROS CARGOS", 80, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("OTROS TRIBUTOS", 80, dicStyle));
       /* rowHeader.Cells.Add(WriteCellStyle("GRAVADO", 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("EXONERADO", 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("INAFECTO", 110, dicStyle));*/
        rowHeader.Cells.Add(WriteCellStyle("IMPORTE EN S/.", 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("IMPORTE EN $", 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("TC", 100, dicStyle));
   
        return rowHeader;
    }

    private void WriteItemreportegeneral(List<RepConcBE> lreportes, Table table)
    {
        TableRow rowItem = new TableRow();
        int cont = 1;
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
            //SUCURSAL
            dicStyle12.Add("background-color", "#F0F8FF");
            dicStyle12.Add("color", "#000000");
            dicStyle12.Add("text-align", "center");
            dicStyle12.Add("font-weight", "bold");
            dicStyle12.Add("border", "solid thin #000");
            //TOTALES
            dicStyle13.Add("text-align", "center");
            dicStyle13.Add("background-color", "#FFB6C1");
            dicStyle13.Add("border", "solid thin #000");
            dicStyle13.Add("color", "#000000");
            dicStyle13.Add("font-weight", "bold");

            if (corre != cant)
            {
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.FechaEmisionREP), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TipoDocumento), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Serie), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Sucuresal), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Rango), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Fila), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Moneda), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ValorVenta), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalIgv), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalIsc), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalOtros), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalOtrostributos), 100, dicStyle));
               /* rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Gravado), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Exonerado), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Inafecto), 100, dicStyle));*/
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ImporteTotal), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ImporteTotaldolares), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TipoCambio), 100, dicStyle));
            }
            else
            {
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.FechaEmisionREP), 100, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TipoDocumento), 100, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Serie), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Sucuresal), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Rango), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Fila), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Moneda), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ValorVenta), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalIgv), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalIsc), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalOtros), 100, dicStyle));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalOtrostributos), 100, dicStyle));
                /* rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Gravado), 100, dicStyle));
                 rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Exonerado), 100, dicStyle));
                 rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Inafecto), 100, dicStyle));*/
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ImporteTotal), 100, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.ImporteTotaldolares), 100, dicStyle13));
                rowItem.Cells.Add(WriteCellStyle(Convert.ToString(""), 100, dicStyle13));
            }
            _isBackground = !_isBackground;
            table.Rows.Add(rowItem);
            cont = cont + 1;
        }
        // rowItem.Cells.Add(WriteCellStyleColumnSpan("", dicStyle, 3));
        table.Rows.Add(rowItem);
    }

    /*ordena los estylos que enviamos para el excel*/
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
    private TableCell WriteCellStyleColumnSpan(String title, Dictionary<String, String> dicStyles, Int32 columnSpan)
    {
        TableCell output = new TableCell();
        output.Text = title;
        output.ColumnSpan = columnSpan;
        foreach (String key in dicStyles.Keys)
            output.Style.Add(key, dicStyles[key]);
        return output;
    }

}