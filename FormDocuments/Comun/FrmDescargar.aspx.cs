using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FormDocuments_Comun_FrmDescargar : System.Web.UI.Page
{
    string urlp = "";
    string idreporte = "";
    string idruc = "";
    string idemisor = "";
    string fechaconvertido="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["hdNomReporte"]))
            {

                idreporte = Request.QueryString["hdNomReporte"];
                idruc = Request.QueryString["hdruc"];
                idemisor = Request.QueryString["hdemisor"];
                fechaconvertido = Request.QueryString["hdfconvert"];

                string filename = string.Format("{0}.xls", idreporte);
                String rutaAplicacion = "T:";
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                String dlDir = @"\\" + idruc + "\\";
                String path = string.Format(rutaAplicacion + dlDir + "{0}.xls", idreporte);
               

                System.IO.FileInfo toDownload =
                             new System.IO.FileInfo(path);

                if (toDownload.Exists)
                {

                    Response.ClearContent();
                    Response.ClearHeaders();

                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.ContentType = "application/octet-stream";

                    Response.WriteFile(path);
                    Response.Flush();
                    Response.Close();

                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                }

            }

               CargarDatosBE serviciorep = new CargarDatosBE();
               serviciorep.actualizarestadodescargaexcelDA(idemisor, "R21", idreporte,fechaconvertido);


        }
    }
}