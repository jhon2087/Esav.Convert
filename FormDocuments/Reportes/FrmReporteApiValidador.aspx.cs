using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.Threading;
using System.IO;

public partial class FormDocuments_Reportes_FrmReporteApiValidador : BasePage
{
    string emisor;
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
        /* cargar id emisor de la pagina anterior*/
        emisor = CodigoEmisor;// dato a modificar
        hdemisor.Value = emisor;

        if (!Page.IsPostBack)
        {

            wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
            List<wswsDocument.CargarDatosBE> DDLlistarsede = servicioDocument.ListarSedesSE(emisor).ToList();
            DDLsede.DataSource = DDLlistarsede;
            DDLsede.DataTextField = "denominacion";
            DDLsede.DataValueField = "codigo";
            DDLsede.DataBind();

            List<wswsDocument.CargarDatosBE> DDLlistarseries = servicioDocument.ListarSeriesSE(emisor, "0").ToList();
            DDLserie.DataSource = DDLlistarseries;
            DDLserie.DataTextField = "serie";
            DDLserie.DataValueField = "codigorango";
            DDLserie.DataBind();

            btnActualizarload_Click();
        }


    }
    /*cargado inicial de la pagina*/
    protected void btnActualizarload_Click()
    {

        string fecha = txtFechabusqueda.Value;
        string sede = DDLsede.SelectedValue;
        string serie = DDLserie.SelectedValue;
        wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
        ReporteRegularizarBE servicioDocument2 = new ReporteRegularizarBE();

        string tablarespuesta = "<table class=table width=100%>";
        tablarespuesta = tablarespuesta + " <thead style=background-color:#3F5367 >";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>sucursal</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Facturas Rechazadas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Boletas Rechazadas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Total de Rechazos</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%> </th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Facturas Aceptadas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Boletas Aceptadas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Total de Aceptados</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%> </th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Facturas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Boletas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Total Recibidas</th>";
        tablarespuesta = tablarespuesta + " </thead>";
        //tablarespuesta = tablarespuesta + " <tbody><tr><td class=nopad colspan=1>";

        List<wswsDocument.ReporteGeneralAPIBE> lExterno = servicioDocument.obtenerreportegeneralapiSE(emisor, fecha, sede, serie).ToList();

        foreach (var repgeneral in lExterno)
        {
            tablarespuesta = tablarespuesta + " <tr>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " width=15%> " + repgeneral.sede + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Facturasrechazadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Boletasrechazadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Totalrechazadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " ></th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.FacturasAceptadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.BoletasAceptadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.TotalAceptadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " ></th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Facturas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Boletas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Totalrecibidas + "</th></tr>";

        }
        tablarespuesta = tablarespuesta + "</table>";

        ltrtablatotales.Text = tablarespuesta;

        string tablarespuesta2 = "<table class=table width=70%  align=" + "center" + ">";
        tablarespuesta2 = tablarespuesta2 + " <thead style=background-color:#3F5367 >";
        tablarespuesta2 = tablarespuesta2 + " <th style=color:white class=headerGlass  width=3%>sucursal</th>";
        tablarespuesta2 = tablarespuesta2 + " <th style=color:white class=headerGlass  width=3%>Facturas Saltos</th>";
        tablarespuesta2 = tablarespuesta2 + " <th style=color:white class=headerGlass  width=3%>Boletas Saltos</th>";
        tablarespuesta2 = tablarespuesta2 + " <th style=color:white class=headerGlass  width=3%>Total de Saltos</th>";
        tablarespuesta2 = tablarespuesta2 + " </thead>";
        // tablarespuesta2 = tablarespuesta2 + " <tbody><tr><td class=nopad colspan=1>";

        foreach (var repgeneral in lExterno)
        {
            tablarespuesta2 = tablarespuesta2 + " <tr>";
            tablarespuesta2 = tablarespuesta2 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.sede + "</th>";
            tablarespuesta2 = tablarespuesta2 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.FacturasSaltos + "</th>";
            tablarespuesta2 = tablarespuesta2 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.BoletasSaltos + "</th>";
            tablarespuesta2 = tablarespuesta2 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.TotalSaltos + "</th></tr>";

        }

        tablarespuesta2 = tablarespuesta2 + "</table>";

        ltrtablatotales2.Text = tablarespuesta2;

        string tablarespuesta5 = "<table class=table width=100%>";
        tablarespuesta5 = tablarespuesta5 + " <thead style=background-color:#3F5367 >";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Nombre Archivo Recibido</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>sucursal</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Emitido</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Tipo Documento</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Serie</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Nro Documento</th>";

        tablarespuesta5 = tablarespuesta5 + " </thead>";

        List<ReporteRegularizarBE> lExterno4 = servicioDocument2.obtenerreporteRegularizarpi2DA(emisor, fecha, sede, serie).ToList();

        foreach (var repregularizar in lExterno4)
        {
            tablarespuesta5 = tablarespuesta5 + " <tr>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.NombTxt.Trim() + "</th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Sede + "</th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.FEmision + "</th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.TipoDocumento + " </th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Serie + "</th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Correlativo + "</th>";
            tablarespuesta5 = tablarespuesta5 + "</tr>";

        }
        tablarespuesta5 = tablarespuesta5 + "</table>";

        ltrtablatotales5.Text = tablarespuesta5;




        string tablarespuesta4 = "<table class=table width=100%>";
        tablarespuesta4 = tablarespuesta4 + " <thead style=background-color:#3F5367 >";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Nombre Archivo Recibido</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>sucursal</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Emitido</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Tipo Documento</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Serie</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Nro Documento</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Detalle</th>";

        tablarespuesta4 = tablarespuesta4 + " </thead>";

        List<wswsDocument.ReporteRegularizarBE> lExterno3 = servicioDocument.obtenerreporteRegularizarpiSE(emisor, fecha, sede, serie).ToList();

        foreach (var repregularizar in lExterno3)
        {
            tablarespuesta4 = tablarespuesta4 + " <tr>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.NombTxt.Trim() + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Sede + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.FEmision + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.TipoDocumento + " </th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Serie + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Correlativo + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.DetalleError + "</th> </tr>";

        }
        tablarespuesta4 = tablarespuesta4 + "</table>";

        ltrtablatotales4.Text = tablarespuesta4;



    }
    /*actualizar carga segun la busqueda solicitada*/
    protected void btnActualizar_Click(object sender, EventArgs e)
    {

        string fecha = txtFechabusqueda.Value;
        string sede = DDLsede.SelectedValue;
        string serie = DDLserie.SelectedValue;
        wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
        ReporteRegularizarBE servicioDocument2 = new ReporteRegularizarBE();

        string tablarespuesta = "<table class=table width=100%>";
        tablarespuesta = tablarespuesta + " <thead style=background-color:#3F5367 >";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>sucursal</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Facturas Rechazadas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Boletas Rechazadas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Total de Rechazos</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%> </th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Facturas Aceptadas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Boletas Aceptadas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Total de Aceptados</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%> </th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Facturas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Boletas</th>";
        tablarespuesta = tablarespuesta + " <th style=color:white class=headerGlass  width=3%>Total Recibidas</th>";
        tablarespuesta = tablarespuesta + " </thead>";
        //tablarespuesta = tablarespuesta + " <tbody><tr><td class=nopad colspan=1>";

        List<wswsDocument.ReporteGeneralAPIBE> lExterno = servicioDocument.obtenerreportegeneralapiSE(emisor, fecha, sede, serie).ToList();

        foreach (var repgeneral in lExterno)
        {
            tablarespuesta = tablarespuesta + " <tr>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " width=15%> " + repgeneral.sede + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Facturasrechazadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Boletasrechazadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Totalrechazadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " ></th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.FacturasAceptadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.BoletasAceptadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.TotalAceptadas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " ></th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Facturas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Boletas + "</th>";
            tablarespuesta = tablarespuesta + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.Totalrecibidas + "</th></tr>";

        }
        tablarespuesta = tablarespuesta + "</table>";

        ltrtablatotales.Text = tablarespuesta;

        string tablarespuesta2 = "<table class=table width=70%  align=" + "center" + ">";
        tablarespuesta2 = tablarespuesta2 + " <thead style=background-color:#3F5367 >";
        tablarespuesta2 = tablarespuesta2 + " <th style=color:white class=headerGlass  width=3%>sucursal</th>";
        tablarespuesta2 = tablarespuesta2 + " <th style=color:white class=headerGlass  width=3%>Facturas Saltos</th>";
        tablarespuesta2 = tablarespuesta2 + " <th style=color:white class=headerGlass  width=3%>Boletas Saltos</th>";
        tablarespuesta2 = tablarespuesta2 + " <th style=color:white class=headerGlass  width=3%>Total de Saltos</th>";
        tablarespuesta2 = tablarespuesta2 + " </thead>";
        // tablarespuesta2 = tablarespuesta2 + " <tbody><tr><td class=nopad colspan=1>";

        foreach (var repgeneral in lExterno)
        {
            tablarespuesta2 = tablarespuesta2 + " <tr>";
            tablarespuesta2 = tablarespuesta2 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.sede + "</th>";
            tablarespuesta2 = tablarespuesta2 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.FacturasSaltos + "</th>";
            tablarespuesta2 = tablarespuesta2 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.BoletasSaltos + "</th>";
            tablarespuesta2 = tablarespuesta2 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repgeneral.TotalSaltos + "</th></tr>";

        }

        tablarespuesta2 = tablarespuesta2 + "</table>";

        ltrtablatotales2.Text = tablarespuesta2;

        string tablarespuesta5 = "<table class=table width=100%>";
        tablarespuesta5 = tablarespuesta5 + " <thead style=background-color:#3F5367 >";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Nombre Archivo Recibido</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>sucursal</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Emitido</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Tipo Documento</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Serie</th>";
        tablarespuesta5 = tablarespuesta5 + " <th style=color:white class=headerGlass  width=3%>Nro Documento</th>";

        tablarespuesta5 = tablarespuesta5 + " </thead>";

        List<ReporteRegularizarBE> lExterno4 = servicioDocument2.obtenerreporteRegularizarpi2DA(emisor, fecha, sede, serie).ToList();

        foreach (var repregularizar in lExterno4)
        {
            tablarespuesta5 = tablarespuesta5 + " <tr>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.NombTxt.Trim() + "</th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Sede + "</th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.FEmision + "</th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.TipoDocumento + " </th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Serie + "</th>";
            tablarespuesta5 = tablarespuesta5 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Correlativo + "</th>";
            tablarespuesta5 = tablarespuesta5 + "</tr>";

        }
        tablarespuesta5 = tablarespuesta5 + "</table>";

        ltrtablatotales5.Text = tablarespuesta5;




        string tablarespuesta4 = "<table class=table width=100%>";
        tablarespuesta4 = tablarespuesta4 + " <thead style=background-color:#3F5367 >";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Nombre Archivo Recibido</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>sucursal</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Emitido</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Tipo Documento</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Serie</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Nro Documento</th>";
        tablarespuesta4 = tablarespuesta4 + " <th style=color:white class=headerGlass  width=3%>Detalle</th>";

        tablarespuesta4 = tablarespuesta4 + " </thead>";

        List<wswsDocument.ReporteRegularizarBE> lExterno3 = servicioDocument.obtenerreporteRegularizarpiSE(emisor, fecha, sede, serie).ToList();

        foreach (var repregularizar in lExterno3)
        {
            tablarespuesta4 = tablarespuesta4 + " <tr>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.NombTxt.Trim() + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Sede + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.FEmision + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.TipoDocumento + " </th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Serie + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.Correlativo + "</th>";
            tablarespuesta4 = tablarespuesta4 + "<th  class=" + "dataItem" + " valign=" + "middle" + " > " + repregularizar.DetalleError + "</th> </tr>";

        }
        tablarespuesta4 = tablarespuesta4 + "</table>";

        ltrtablatotales4.Text = tablarespuesta4;


    }

    /*boton para exportar a excel*/
    protected void btnrepgeneral_Click(object sender, EventArgs e)
    {
        string fecha = txtFechabusqueda.Value;
        string sede = DDLsede.SelectedValue;
        string serie = DDLserie.SelectedValue;

        String fileName = "Reporte procesados por Auditor.xls";
        HttpContext.Current.Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.Default;

        wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();

        List<wswsDocument.ReporteGeneralAPIBE> lExterno = servicioDocument.obtenerreportegeneralapiSE(emisor, fecha, sede, serie).ToList();
        List<wswsDocument.ReporteRegularizarBE> lExterno3 = servicioDocument.obtenerreporteRegularizarpiSE(emisor, fecha, sede, serie).ToList();

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
                dato = String.Format("<img src='{0}'/><div style='font-weight: bold; font-size: 30pt; text-align: center;'>{1}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{2}</p></div>", WebHelper.getProtocoloUrl() + "/gogdn/library/img/" + logo, "Reporte API Validador del " + fecha, "Reporte EsavDoc R2.3");
                /*
                if (CodigoEmisor == "AG0000000002")
                {
                    dato = String.Format("<img src='{0}'/><div style='font-weight: bold; font-size: 30pt; text-align: center;'>{1}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{2}</p></div>", WebHelper.getProtocoloUrl() + "/gogdn/library/img/" + logo, "Reporte API Validador del " + fecha, "Reporte EsavDoc R2.3");
                }
                if (CodigoEmisor == "OT0000000005")
                {
                    dato = String.Format("<img src='{0}'/><div style='font-weight: bold; font-size: 30pt; text-align: center;'><br><br><br><br><br>{1}<br><p style='font-weight: bold; font-size: 10pt; text-align: right;'>{2}</p></div>", WebHelper.getProtocoloUrl() + "/gogdn/library/img/rokylogo.jpg", "Reporte API Validador del " + fecha, "Reporte EsavDoc R2.3");
                }
                 */
                String xlsTitle = dato; Literal lt = new Literal();
                lt.Text = "&nbsp;<br>" + xlsTitle + "<br>";
                lt.RenderControl(htw);

                ////////// Write Header
                table.Rows.Add(WriteHeaderreportegeneral());
                WriteItemreportegeneral(lExterno, table);

                table.RenderControl(htw);
                /////////////////////////////////////////////////////////////

            }

            using (HtmlTextWriter htw3 = new HtmlTextWriter(sw))
            {
                Table table3 = new Table();
                table3.Font.Name = "Arial";
                table3.Font.Size = 8;
                table3.CellPadding = 0;
                table3.CellSpacing = 0;

                String xlsTitle3 = String.Format("<div style='font-weight: bold; font-size: 24pt; text-align: center;'>REPORTE DOCUMENTOS SALTADOS </div>");
                Literal lt3 = new Literal();
                lt3.Text = "&nbsp;<br>" + xlsTitle3 + "<br>";
                lt3.RenderControl(htw3);

                ////////// Write Header
                table3.Rows.Add(WriteHeaderreportesaltos());
                WriteItemreportesaltos(lExterno, table3);

                table3.RenderControl(htw3);
            }

            using (HtmlTextWriter htw2 = new HtmlTextWriter(sw))
            {
                Table table2 = new Table();
                table2.Font.Name = "Arial";
                table2.Font.Size = 8;
                table2.CellPadding = 0;
                table2.CellSpacing = 0;

                String xlsTitle2 = String.Format("<div style='font-weight: bold; font-size: 24pt; text-align: center;'>Reporte Documentos a Regularizar</div>");
                Literal lt2 = new Literal();
                lt2.Text = "&nbsp;<br>" + xlsTitle2 + "<br>";
                lt2.RenderControl(htw2);

                ////////// Write Header
                table2.Rows.Add(WriteHeaderreporteregularizacion());
                WriteItemreporteregularizacion(lExterno3, table2);

                table2.RenderControl(htw2);
            }
            Response.Write(sw.ToString());
            Response.End();
        }


        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    /* armamos las cabeceras de las tablas en excel */
    public TableRow WriteHeaderreporteregularizacion()
    {
        TableRow rowHeader = new TableRow();
        dicStyle.Clear();
        dicStyle.Add("color", "#fff");
        dicStyle.Add("text-align", "center");
        dicStyle.Add("background-color", "#00008B");
        dicStyle.Add("font-weight", "bold");
        dicStyle.Add("border", "solid thin #000");
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblnombrearchivo, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblSucursal, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFechaEmision, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTipoDocumento, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblSerie, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblNumero, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblDescripcion, 100, dicStyle));
        return rowHeader;
    }
    public TableRow WriteHeaderreportesaltos()
    {
        TableRow rowHeader = new TableRow();
        dicStyle.Clear();
        dicStyle.Add("color", "#fff");
        dicStyle.Add("text-align", "center");
        dicStyle.Add("background-color", "#00008B");
        dicStyle.Add("font-weight", "bold");
        dicStyle.Add("border", "solid thin #000");
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblSucursal, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFacturassaltos, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblBoletasaltos, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTotal, 110, dicStyle));
        return rowHeader;
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

        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblSucursal, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFacturasRechazadas, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblBoletassRechazadas, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTotal, 110, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFacturasAceptadas, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblBoletasAceptadas, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTotal, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblFactura, 100, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblBoletaapi, 120, dicStyle));
        rowHeader.Cells.Add(WriteCellStyle(Resources.label.lblTotal, 100, dicStyle));

        return rowHeader;
    }

    /* armar cuerpo de las tablas en el excel*/

    private void WriteItemreporteregularizacion(List<wswsDocument.ReporteRegularizarBE> lreportes, Table table)
    {
        TableRow rowItem = new TableRow();
        int cont = 1;

        foreach (var item in lreportes)
        {

            rowItem = new TableRow();
            dicStyle.Clear();
            dicStyle.Add("text-align", "center");
            dicStyle.Add("background-color", "#D3D3D3");
            dicStyle.Add("border", "solid thin #000");
            rowItem.Cells.Add(WriteCellStyle(item.NombTxt, 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Sede), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.FEmision), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TipoDocumento), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Serie), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Correlativo), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.DetalleError), 100, dicStyle));

            _isBackground = !_isBackground;
            table.Rows.Add(rowItem);
            cont = cont + 1;
        }
        // rowItem.Cells.Add(WriteCellStyleColumnSpan("", dicStyle, 3));
        table.Rows.Add(rowItem);
    }
    private void WriteItemreportesaltos(List<wswsDocument.ReporteGeneralAPIBE> lreportes, Table table)
    {
        TableRow rowItem = new TableRow();
        int cont = 1;

        foreach (var item in lreportes)
        {

            rowItem = new TableRow();
            dicStyle.Clear();
            dicStyle12.Clear();
            dicStyle13.Clear();
            dicStyle.Add("text-align", "center");
            dicStyle.Add("color", "#000000");
            dicStyle.Add("background-color", "#D3D3D3");
            dicStyle.Add("border", "solid thin #000");
            dicStyle.Add("font-weight", "bold");
            //SUCURSAL
            dicStyle12.Add("background-color", "#F0F8FF");
            dicStyle12.Add("color", "#000000");
            dicStyle12.Add("text-align", "center");
            dicStyle12.Add("font-weight", "bold");
            dicStyle12.Add("border", "solid thin #000");
            //TOTALES
            dicStyle13.Add("background-color", "#8B0000");
            dicStyle13.Add("color", "#fff");
            dicStyle13.Add("text-align", "center");
            dicStyle13.Add("font-weight", "bold");
            dicStyle13.Add("border", "solid thin #000");

            rowItem.Cells.Add(WriteCellStyle(item.sede, 100, dicStyle12));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.FacturasSaltos), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.BoletasSaltos), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalSaltos), 100, dicStyle13));

            _isBackground = !_isBackground;
            table.Rows.Add(rowItem);
            cont = cont + 1;
        }
        // rowItem.Cells.Add(WriteCellStyleColumnSpan("", dicStyle, 3));
        table.Rows.Add(rowItem);
    }
    private void WriteItemreportegeneral(List<wswsDocument.ReporteGeneralAPIBE> lreportes, Table table)
    {
        TableRow rowItem = new TableRow();
        int cont = 1;

        foreach (var item in lreportes)
        {

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
            dicStyle13.Add("background-color", "#8B0000");
            dicStyle13.Add("color", "#fff");
            dicStyle13.Add("text-align", "center");
            dicStyle13.Add("font-weight", "bold");
            dicStyle13.Add("border", "solid thin #000");

            rowItem.Cells.Add(WriteCellStyle(item.sede, 100, dicStyle12));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Facturasrechazadas), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Boletasrechazadas), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Totalrechazadas), 100, dicStyle13));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.FacturasAceptadas), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.BoletasAceptadas), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.TotalAceptadas), 100, dicStyle13));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Facturas), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Boletas), 100, dicStyle));
            rowItem.Cells.Add(WriteCellStyle(Convert.ToString(item.Totalrecibidas), 100, dicStyle13));

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