using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.Threading;
using System.IO;

public partial class FormDocuments_InterfaceContable_FrmEspecializado : BasePage
{
    private int curPage = 1;
    private int totalRecords = 1;
    private int totalPages = 1;
    private int pageSize = 10;
    Dictionary<String, String> dicStyle = new Dictionary<string, string>();
    Boolean _isBackground = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("Jquery")) { Page.ClientScript.RegisterClientScriptInclude("Jquery", "../../gogdn/library/jquery/jquery-1.4.2.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("JqueryBlock")) { Page.ClientScript.RegisterClientScriptInclude("JqueryBlock", "../../gogdn/library/jquery/jquery.blockUI.js"); }
        // if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript", "../../gogdn/library/js/global.js"); }
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("GlobalScript2")) { Page.ClientScript.RegisterClientScriptInclude("GlobalScript2", "../../gogdn/library/jquery/jquery-ui.min.js"); }




        if (!Page.IsPostBack)
        {


            string urlCancelar = string.Format(WebHelper.getProtocoloUrl() + "/FormDocuments/Comun/FrmInicio.aspx", 0);
            btnSalir.Attributes.Add("onClick", string.Format("lnkCancelar_onClick('{0}');", urlCancelar));

            cargarGrilla(DateTime.MinValue.AddYears(1900), DateTime.MinValue.AddYears(1900), "", 0);
        }
    }


    private void cargarGrilla(DateTime _fechaInicial, DateTime _FechaFinal, string tipodoc, int idx)
    {

        wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
        //    List<wswsDocument.EmisorBE> lEmisor = new List<wswsDocument.EmisorBE>();

        totalRecords = servicioDocument.obtenerRepConcarCriteriosFilasSE(CodigoEmisor, _fechaInicial, _FechaFinal, tipodoc);
        totalPages = totalRecords / pageSize;
        if ((pageSize * totalPages) != totalRecords)
        {
            totalPages++;
        }


        List<wswsDocument.ReporteConcarBE> lRepDocumento = servicioDocument.ListarRepConcarCriteriosSE(CodigoEmisor, _fechaInicial, _FechaFinal, tipodoc, pageSize, curPage - 1).ToList();
        rptListado.DataSource = lRepDocumento;
        rptListado.DataBind();

       // servicioDocument.Close();
    }

    public void cargarGrillas(string _fechaInicial, string _FechaFinal, string tipodoc, string idx)
    {
        try
        {
            curPage = Convert.ToInt32(idx);
            DateTime fechaini = Convert.ToDateTime(_fechaInicial);
            DateTime fechafin = Convert.ToDateTime(_FechaFinal);
            cargarGrilla(fechaini, fechafin, tipodoc, curPage);

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
            wswsDocument.ReporteConcarBE objRepDocumento = (wswsDocument.ReporteConcarBE)e.Item.DataItem;

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

    #region "Exportar Excel"

    public void MovimientosReport(string _fechaInicial, string _FechaFinal, string tipodoc)
    {
        DateTime fechaini = Convert.ToDateTime(_fechaInicial);
        DateTime fechafin = Convert.ToDateTime(_FechaFinal);
       // wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
       // List<wswsDocument.ReporteConcarBSPBE> lRepDocumento = servicioDocument.ListarRepConcarCriteriosBSPSE(CodigoEmisor, fechaini, fechafin, tipodoc).ToList();

        List<ReporteConcarBSPBE> lRepDocumento = WebHelper.ListarRepConcarCriteriosBSPDA(CodigoEmisor, fechaini, fechafin, tipodoc).ToList();
        
        
        
        //  _isBackground = false;

        String fileName = "REPORTE CONTABLE DEL " + _fechaInicial + " HASTA " + _FechaFinal+".xls";
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

                if (CodigoEmisor == "AG0000000002")
                {
                    dato = String.Format("<img src='{0}'/><div style='font-weight: bold; font-size: 35pt; text-align: center;'>{1}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{2}</p></div>", WebHelper.getProtocoloUrl() + "/gogdn/library/img/carlsonlogo1.jpg", "REPORTE CONTABLE DEL " + _fechaInicial + " HASTA " + _FechaFinal, "Reporte EsavDoc R2.3");
                }
                if (CodigoEmisor == "OT0000000005")
                {
                    dato = String.Format("<img src='{0}'/><div style='font-weight: bold; font-size: 35pt; text-align: center;'>{1}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{2}</p></div>", WebHelper.getProtocoloUrl() + "/gogdn/library/img/rokylogo.jpg","REPORTE CONTABLE DEL "+_fechaInicial +" HASTA "+ _FechaFinal, "Reporte EsavDoc R2.3");
                }

                String xlsTitle = dato;
                
               
                Literal lt = new Literal();
                lt.Text = "&nbsp;<br>" + xlsTitle + "<br>";
                lt.RenderControl(htw);

                // Write Header
                table.Rows.Add(WriteHeader());
                WriteItem(lRepDocumento, table);

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

    public TableRow WriteHeader()
    {
        TableRow rowHeader = new TableRow();
        dicStyle.Clear();
        dicStyle.Add("color", "#fff");
        dicStyle.Add("text-align", "center");
        dicStyle.Add("background-color", "#00008B");
        dicStyle.Add("font-weight", "bold");
        dicStyle.Add("border", "solid thin #000");
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblSubdiario, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblNroComprobante, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFechaEmision, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblMoneda, 80, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle("DESCRIPCION", 160, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTipoCambio, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTipoConversion, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFlagConversion ,100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFechaTipoCambio, 80, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTipoCuenta, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblCodigoAnexo, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblCodCentroCosto, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblDebeHab, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblImporteOriginal, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblImporteDolares, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblImporteSoles, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTipoDocumento, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblNroDocumento, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFechaDocumento, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFechaVencimiento, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblCodigoArea, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblGlosa, 160, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblCodigoAuxiliar, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblMedioPago, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTipoDocReferencia, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblNroDocReferencia, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFechaDocReferencia, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblBaseDocReferencia, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblIgvProvicion, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTipoRefMQ, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblNroSerieCajaRegistra, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFechaOperacion, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTipoDetAsa, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTasaDetraccion, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblImporteDetraccionUsd, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblImporteDetraccionSol, 100, dicStyle));

        return rowHeader;
    }



    private void WriteItem(List<ReporteConcarBSPBE> lreportes, Table table)
    {
        TableRow rowItem = new TableRow();
        int cont = 0;
        int indice = 1;
        string nrocomprobantefinal = "";
        string comprobante = "";
        string nrodocumento = "";

        foreach (var item in lreportes)
        {
            cont = cont + 1;
           
            if (cont == 1)
            {
                 comprobante = item.NroComprobante;
                 nrodocumento = item.NroDocumento;
              //  nrocomprobantefinal = comprobante + "0" + cont;
            }
            else
            {
                if (nrodocumento == item.NroDocumento)
                {
                    nrocomprobantefinal = comprobante; //+ "0";//+ indice;


                }
                else
                {
                    indice = indice + 1;
                    nrocomprobantefinal = comprobante ;//+ "0"; //+ indice;
                    nrodocumento = item.NroDocumento;
                   
                }
            }

            if (cont == 1)
                nrocomprobantefinal = comprobante + "000" + cont;
            else
            {
                if (indice < 10)

                {
                    nrocomprobantefinal = nrocomprobantefinal + "000" + indice;
                
                }
                else
                    if (indice < 100)
                    {
                        nrocomprobantefinal = nrocomprobantefinal + "00" + indice;

                    }

                    else
                        if (indice < 1000)
                        {
                            nrocomprobantefinal = nrocomprobantefinal + "0" + indice;

                        }
                        else
                            if (indice < 1000)
                            {
                                nrocomprobantefinal = nrocomprobantefinal + indice;

                            }
            }

            decimal impd=0;
            decimal imps=0;
           /* if(item.Moneda=="MN")
            {*/
            	 impd=Convert.ToDecimal(item.ImporteOriginal)/Convert.ToDecimal(item.Tipocambio);
            	 imps=Convert.ToDecimal(item.ImporteOriginal);
            /*}
            else
            {
            	 imps=Convert.ToDecimal(item.ImporteOriginal)*Convert.ToDecimal(item.Tipocambio);
            	 impd=Convert.ToDecimal(item.ImporteOriginal);
            }*/

            
            rowItem = new TableRow();
            dicStyle.Clear();
           // dicStyle12.Clear();
            //dicStyle13.Clear();
            dicStyle.Add("text-align", "center");
            dicStyle.Add("background-color", "#D3D3D3");
            dicStyle.Add("border", "solid thin #000");
            dicStyle.Add("color", "#000000");
            dicStyle.Add("font-weight", "bold");
            rowItem.Cells.Add(WriteCellStyle(String.Format("{0,2:00}", item.Subdiario.ToString().Trim()), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(String.Format("{0,18:00000000}", nrocomprobantefinal.ToString().Trim()), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(WebHelper.getFechaFormatoNormal(item.FechaEmision), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.Moneda, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.Periodo, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Tipocambio), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TipoConversion), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.FlagConversion), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(WebHelper.getFechaFormatoNormal(item.FechaTipoCambio), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TipoCuenta), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.CodigoAnexo, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.CodCentroCosto, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.DebeHab, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.ImporteOriginal, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(Math.Round(impd,2)), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(Math.Round(imps,2)), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.TipoDocumento, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.NroDocumento, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(WebHelper.getFechaFormatoNormal(item.FechaDocumento), dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.FechaVencimiento, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.CodigoArea, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.Glosa, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.CodigoAuxiliar, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.MedioPago, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.TipoDocReferencia, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.NroDocReferencia, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.FechaDocReferencia, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.BaseDocReferencia, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.IgvProvicion, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.TipoRefMQ, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.NroSerieCajaRegistra, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.FechaOperacion, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.TipoDetAsa, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.TasaDetraccion, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.ImporteDetraccionUsd, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(item.ImporteDetraccionSol, dicStyle));
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



    protected void btngenera_Click(object sender, EventArgs e)
    {
        string FECHAINI = txtFechaInicial.Text;
        string FECHAFIN = txtFechaFinal.Text;
        string tipodoc = ddltipodocumento.Value;
        MovimientosReport(FECHAINI, FECHAFIN, tipodoc);

    }
}