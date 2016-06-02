using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Globalization;
using System.Web.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de WebHelper
/// </summary>
public class WebHelper
{
    private static int __defaultCurrentCultureID = 1;
    private static string __defaultCurrentCultureKey = "es-PE";
    public string p_url;
    public string p_pdf;
    public static string p_key = "xe705fTl9/Xl8Ov15fb06/T36/z8/Pw=";
    public string p_file;

    public List<String> lp_url;
    public List<String> lp_pdf;
    public List<String> lp_key;
    public List<String> lp_file;
    /**********************************************************
     * Creado Por   : Ronald Ameri (16 Sep 2011)
     * Descripcion  : Verificar si ha inicio sesion.
    **********************************************************/
    /// <summary>
    /// Verificar si ha inicio sesion.
    /// </summary>
    /// <returns>bool</returns>
    public static bool isLogin()
    {
        WebSession CurrentUser = (WebSession)HttpContext.Current.Session["SessionUser"];
        if (CurrentUser != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**********************************************************
     * Creado Por   : Ronald Ameri (16 Sep 2011)
     * Descripcion  : Envia correo al los correos enviados.
    **********************************************************/
    /// <summary>
    /// Envia correo al los correos enviados.
    /// </summary>
    /// <param name="_To">Email Destino</param>
    /// <param name="_CC">Email a quien se va ha copiar el correo</param>
    /// <param name="_BCC">Copia oculta</param>
    /// <param name="_Subject">Asunto del Correo</param>
    /// <param name="_Body">Cuerpo del Correo</param>
    /// <param name="_mailPriority">Prioridad del Correo</param>
    public static void sendEmail(string _To, string _CC, string _BCC, string _Subject,
                                 string _Body, MailPriority _mailPriority)
    {

        string applicationMode = WebApplication.getInstance().getApplicationMode;
        string correoEsav = WebApplication.getInstance().getWebEmail;
        string nombreCorreo = WebApplication.getInstance().getNameEmail;
        string UsuarioCorreo = WebApplication.getInstance().getUsuarioEmail;
        string claveCorreo = WebApplication.getInstance().getPasswordEmailSMTP;
        string host = WebApplication.getInstance().getHostSMTP;
        string puerto = WebApplication.getInstance().getPortSMTP;

        if (applicationMode != "PRO")
        {
            _Subject = string.Format("[{0}]{1}", applicationMode, _Subject);
        }

        MailMessage MailObj = new MailMessage();

        Array arrayEmail = _To.Split(new char[] { ';' });

        for (int x = 0; x < arrayEmail.Length; x++)
        {
            MailObj.To.Add(arrayEmail.GetValue(x).ToString());
        }

        if (!String.IsNullOrEmpty(_CC))
        {
            arrayEmail = _CC.Split(new char[] { ';' });
            for (int m = 0; m < arrayEmail.Length; m++)
            {
                MailObj.CC.Add(arrayEmail.GetValue(m).ToString());
            }
        }

        if (!String.IsNullOrEmpty(_BCC))
        {
            arrayEmail = _BCC.Split(new char[] { ';' });
            for (int m = 0; m < arrayEmail.Length; m++)
            {
                MailObj.Bcc.Add(arrayEmail.GetValue(m).ToString());
            }
        }
        MailObj.From = new MailAddress(correoEsav, nombreCorreo);
        MailObj.Priority = _mailPriority;
        MailObj.Subject = _Subject;
        MailObj.Body = _Body;
        MailObj.IsBodyHtml = true;

        SmtpClient smtpcli = new SmtpClient(host, Convert.ToInt32(puerto));
        smtpcli.EnableSsl = false;
        smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;

        smtpcli.Credentials = new NetworkCredential(UsuarioCorreo, claveCorreo);

        try
        {
            smtpcli.Send(MailObj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /**********************************************************
     * Creado Por   : Henrry Cruzado (16 mayo 2015)
     * Descripcion  : Envia correo adjuntado documento.
    **********************************************************/
    /// <summary>
    /// Envia correo adjuntado documentos pdf y xml.
    /// </summary>
    /// <param name="_To">Email Destino</param>
    /// <param name="_CC">Email a quien se va ha copiar el correo</param>
    /// <param name="_BCC">Copia oculta</param>
    /// <param name="_Subject">Asunto del Correo</param>
    /// <param name="_Body">Cuerpo del Correo</param>
    /// <param name="_FilePath">Ubicacion del Archivo</param>    
    /// <param name="_FileName">Nombre del Archivo</param>    
    /// <param name="_mailPriority">Prioridad del Correo</param>
    public static void sendEmailXmlPdf(string _To, string _CC, string _BCC, string _Subject,
                                 string _Body, string _FilePathpdf, string _FileNamepdf, string _FilePathxml, string _FileNameXml, MailPriority _mailPriority)
    {

        string applicationMode = WebApplication.getInstance().getApplicationMode;
        string correoEsav = WebApplication.getInstance().getWebEmail;
        string nombreCorreo = WebApplication.getInstance().getNameEmail;
        string UsuarioCorreo = WebApplication.getInstance().getUsuarioEmail;
        string claveCorreo = WebApplication.getInstance().getPasswordEmailSMTP;
        string host = WebApplication.getInstance().getHostSMTP;
        string puerto = WebApplication.getInstance().getPortSMTP;

        if (applicationMode != "PRO")
        {
            _Subject = string.Format("[{0}]{1}", applicationMode, _Subject);
        }

        MailMessage MailObj = new MailMessage();

        Array arrayEmail = _To.Split(new char[] { ';' });

        for (int x = 0; x < arrayEmail.Length; x++)
        {
            MailObj.To.Add(arrayEmail.GetValue(x).ToString());
        }

        if (!String.IsNullOrEmpty(_CC))
        {
            arrayEmail = _CC.Split(new char[] { ';' });
            for (int m = 0; m < arrayEmail.Length; m++)
            {
                MailObj.CC.Add(arrayEmail.GetValue(m).ToString());
            }
        }

        if (!String.IsNullOrEmpty(_BCC))
        {
            arrayEmail = _BCC.Split(new char[] { ';' });
            for (int m = 0; m < arrayEmail.Length; m++)
            {
                MailObj.Bcc.Add(arrayEmail.GetValue(m).ToString());
            }
        }
        MailObj.From = new MailAddress(correoEsav, nombreCorreo);
        MailObj.Priority = _mailPriority;
        MailObj.Subject = _Subject;

        Attachment file = new Attachment(_FilePathpdf);
        file.Name = _FileNamepdf;
        MailObj.Attachments.Add(file);

        Attachment file1 = new Attachment(_FilePathxml);
        file1.Name = _FileNameXml;
        MailObj.Attachments.Add(file1);

        MailObj.Body = _Body;
        MailObj.IsBodyHtml = true;
        SmtpClient smtpcli = new SmtpClient(host, Convert.ToInt32(puerto));
        smtpcli.EnableSsl = false;
        smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;

        smtpcli.Credentials = new NetworkCredential(UsuarioCorreo, claveCorreo);

        try
        {
            smtpcli.Send(MailObj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }




    /**********************************************************
    * Creado Por   : Henrry Cruzado (16 mayo 2015)
    * Descripcion  : Envia correo adjuntado documento.
   **********************************************************/
    /// <summary>
    /// Envia correo adjuntado documento pdf .
    /// </summary>
    /// <param name="_To">Email Destino</param>
    /// <param name="_CC">Email a quien se va ha copiar el correo</param>
    /// <param name="_BCC">Copia oculta</param>
    /// <param name="_Subject">Asunto del Correo</param>
    /// <param name="_Body">Cuerpo del Correo</param>
    /// <param name="_FilePath">Ubicacion del Archivo</param>    
    /// <param name="_FileName">Nombre del Archivo</param>    
    /// <param name="_mailPriority">Prioridad del Correo</param>
    public static void sendEmailPdf(string _To, string _CC, string _BCC, string _Subject,
                                 string _Body, string _FilePathpdf, string _FileNamepdf, MailPriority _mailPriority)
    {

        string applicationMode = WebApplication.getInstance().getApplicationMode;
        string correoEsav = WebApplication.getInstance().getWebEmail;
        string nombreCorreo = WebApplication.getInstance().getNameEmail;
        string UsuarioCorreo = WebApplication.getInstance().getUsuarioEmail;
        string claveCorreo = WebApplication.getInstance().getPasswordEmailSMTP;
        string host = WebApplication.getInstance().getHostSMTP;
        string puerto = WebApplication.getInstance().getPortSMTP;

        if (applicationMode != "PRO")
        {
            _Subject = string.Format("[{0}]{1}", applicationMode, _Subject);
        }

        MailMessage MailObj = new MailMessage();

        Array arrayEmail = _To.Split(new char[] { ';' });

        for (int x = 0; x < arrayEmail.Length; x++)
        {
            MailObj.To.Add(arrayEmail.GetValue(x).ToString());
        }

        if (!String.IsNullOrEmpty(_CC))
        {
            arrayEmail = _CC.Split(new char[] { ';' });
            for (int m = 0; m < arrayEmail.Length; m++)
            {
                MailObj.CC.Add(arrayEmail.GetValue(m).ToString());
            }
        }

        if (!String.IsNullOrEmpty(_BCC))
        {
            arrayEmail = _BCC.Split(new char[] { ';' });
            for (int m = 0; m < arrayEmail.Length; m++)
            {
                MailObj.Bcc.Add(arrayEmail.GetValue(m).ToString());
            }
        }
        MailObj.From = new MailAddress(correoEsav, nombreCorreo);
        MailObj.Priority = _mailPriority;
        MailObj.Subject = _Subject;

        Attachment file = new Attachment(_FilePathpdf);
        file.Name = _FileNamepdf;
        MailObj.Attachments.Add(file);


        MailObj.Body = _Body;
        MailObj.IsBodyHtml = true;
        SmtpClient smtpcli = new SmtpClient(host, Convert.ToInt32(puerto));
        smtpcli.EnableSsl = false;
        smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;

        smtpcli.Credentials = new NetworkCredential(UsuarioCorreo, claveCorreo);

        try
        {
            smtpcli.Send(MailObj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }



  
    public static string getProtocoloUrl()
    {
        string url = "";
        url = ConfigurationManager.AppSettings["ApplicationUrlNormal"].ToString();
        return url;
    }

    public static string getProtocoloUrl(bool secure)
    {
        string url = "";
        url = ConfigurationManager.AppSettings["ApplicationUrlSecure"].ToString();
        return url;
    }

    public static string getFechaMonthNameDayAndYear(Object fecha)
    {
        if (fecha == null)
        {
            return "";
        }
        else
        {
            DateTime _fecha = (DateTime)fecha;
            return string.Format("{0} {1}, {2}",
                   CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_fecha.Month)),
                   _fecha.Day, _fecha.Year);
        }

    }

    public static string getFechaFormatoNormal(Object _fecha)
    {
        if (_fecha == null)
        {
            return "";
        }
        else
        {
            DateTime fecha = (DateTime)_fecha;
            return fecha.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
        }

    }

    public static string getFechaHoraFormatoNormal(Object _fecha)
    {
        if (_fecha == null)
        {
            return "";
        }
        else
        {
            DateTime fecha = (DateTime)_fecha;
            return fecha.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern) + " " + fecha.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
        }

    }

    public static string getHoraCorta(Object _fecha)
    {
        if (_fecha == null)
        {
            return "";
        }
        else
        {
            DateTime fecha = (DateTime)_fecha;
            return fecha.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern);
        }

    }

    public static string getHoraLarga(Object _fecha)
    {
        if (_fecha == null)
        {
            return "";
        }
        else
        {
            DateTime fecha = (DateTime)_fecha;
            return fecha.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
        }

    }

    public static int getIdiomaId
    {
        get
        {

            if (HttpContext.Current.Session["currentCultureID"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["currentCultureID"].ToString());
            }
            else return __defaultCurrentCultureID;
            /*
            if (WebHelper.isLogin())
            {
                return Convert.ToInt32(HttpContext.Current.Session["currentCultureID"].ToString());
            }
            else return __defaultCurrentCultureID;
             */
        }
    }

    public static string getIdiomaKey
    {
        get
        {
            if (HttpContext.Current.Session["currentCultureKey"] != null)
            {
                return HttpContext.Current.Session["currentCultureKey"].ToString();
            }
            else return __defaultCurrentCultureKey;
            /*
            if (WebHelper.isLogin())
            {
                return HttpContext.Current.Session["currentCultureKey"].ToString();
            }
            else return __defaultCurrentCultureKey;
            */
        }
    }


    public static string getHtmlControl(Control Control)
    {
        string output = "";
        if (Control == null) return output;
        using (StringWriter swControl = new StringWriter())
        {
            using (HtmlTextWriter htwControl = new HtmlTextWriter(swControl))
            {
                Control.RenderControl(htwControl);
            }
            output = swControl.ToString();
        }
        return output;
    }

   
    public static String TemplateBodyVoucherFacturacion(string urlimg, string emisor, string tipodoc, string nrodoc, string rucemisor,
                                          string ruccliente, string fechaemision, string lblEquipoGoGDN)
    {
        String fileName = "";
        int p = 1;
        fileName = String.Format("~/template/{0}/FacturaBody.htm", (p == getIdiomaId ? "es" : "en"));
        String pathFile = System.Web.HttpContext.Current.Server.MapPath(fileName);
        StreamReader sr;
        String output = "";
        FileInfo fi = new FileInfo(pathFile);
        if (File.Exists(pathFile))
        {
            sr = File.OpenText(pathFile);
            output = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
        }
        String body = output.Replace("||URLIMG||", urlimg);
        body = body.Replace("||EMISOR||", emisor);
        body = body.Replace("||TIPODOCUMENTO||", tipodoc);
        body = body.Replace("||SERIENRO||", nrodoc);
        body = body.Replace("||RUCEMISOR||", rucemisor);
        body = body.Replace("||RUCCLIENTE||", ruccliente);
        body = body.Replace("||FECHAEMISION||", fechaemision);

        return body;
    }
    public static String TemplateBodyExternoFacturacion(string urlimg, string emisor, string tipodoc, string nrodoc, string rucemisor,
                                          string pass, string fechaemision, string lblEquipoGoGDN)
    {
        String fileName = "";
        int p = 1;
        fileName = String.Format("~/template/{0}/DocumentoExternoBody.htm", (p == getIdiomaId ? "es" : "en"));
        String pathFile = System.Web.HttpContext.Current.Server.MapPath(fileName);
        StreamReader sr;
        String output = "";
        FileInfo fi = new FileInfo(pathFile);
        if (File.Exists(pathFile))
        {
            sr = File.OpenText(pathFile);
            output = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
        }
        String body = output.Replace("||URLIMG||", urlimg);
        body = body.Replace("||EMISOR||", emisor);
        body = body.Replace("||TIPODOCUMENTO||", tipodoc);
        body = body.Replace("||SERIENRO||", nrodoc);
        body = body.Replace("||RUCEMISOR||", rucemisor);
        body = body.Replace("||PASS||", pass);
        body = body.Replace("||FECHAEMISION||", fechaemision);

        return body;
    }

    public static string Left(string param, int length)
    {
        string result = param.Substring(0, length);
        return result;
    }

    public static string Right(string param, int length)
    {
        string result = param.Substring(param.Length - length, length);
        return result;
    }

    //FechaNativa yyyyMMdd
    public static string FormatoFecha(string FechaNativa, bool pHora)
    {
        string FecNac = string.Empty;

        if (FechaNativa != null && (!FechaNativa.Equals("19000101") || !FechaNativa.Equals("19000101 00:00:00")))
        {
            String AnoNac = FechaNativa.Substring(0, 4);
            String MesNac = FechaNativa.Substring(4, 2);
            String DiaNac = FechaNativa.Substring(6, 2);

            string Hora = string.Empty;
            if (pHora)
                Hora = ' ' + Right(FechaNativa, 8);

            if (getIdiomaId.Equals(1))//Español
                FecNac = DiaNac + "/" + MesNac + "/" + AnoNac;
            else
                FecNac = MesNac + "/" + DiaNac + "/" + AnoNac;

            FecNac += Hora;
        }

        return FecNac;
    }


    public static string getAppSetting(string _valor)
    {
        return ConfigurationManager.AppSettings[_valor];
    }

  

    public static void CreaXML(string _cProveedor, string _mGuidAll, string _XML, string _Nombre, string _TipoRequerimiento)
    {
        /*-Begin-Request*/
        string strGuid_PT = _mGuidAll;
        string strRuta = getAppSetting("RutaXML") + "/";
        string strExtension = ".xml";
        XmlDocument xmlDocumento = new XmlDocument();
        XmlDocument xmlDocumentofirmado = new XmlDocument();
        try
        {
            //xmlDocumentofirmado = firmardocumento(_XML);

            xmlDocumento.LoadXml(_XML);





            xmlDocumento.Save(strRuta + _cProveedor + strExtension);
        }
        catch (Exception ex)
        {
            string texto = "XML:\n";
            texto += _XML + "\n\n\n";
            texto += "Message:" + ex.Message + (ex.InnerException != null ? "\nInnerException:" + ex.InnerException.Message : "");
            texto += "\nStackTrace:" + ex.StackTrace + (ex.InnerException != null ? "\nInnerException.StackTrace:" + ex.InnerException.StackTrace : "");

            //using (StreamWriter sw = File.CreateText(strRuta + strNombre + "_" + strGuid_PT + "_" + strTipoRequerimiento + ".txt"))
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(strRuta + _Nombre + "_" + strGuid_PT + "_" + _TipoRequerimiento + ".txt"))
            {
                sw.WriteLine(texto);
                sw.Close();
            }
            throw ex;
        }
        /*-End-Request*/
    }




    /**********************************************************
       * Creado Por   : Henrry Cruzado (30 de Abril 2015)
       * Descripcion  : Obtiene un listado del RangoUsuario por el id de la session.
       **********************************************************/
    /// <summary>
    /// Obtiene un listado del RangoUsuario por el id de la session.
    /// </summary>
    /// <returns></returns>
    public static List<ReporteConcarBSPBE> ListarRepConcarCriteriosBSPDA(String _Emisor, DateTime _FechaInicial, DateTime _FechaFinal, string _tipodoc)
    {
        try
        {
            List<ReporteConcarBSPBE> lRepConcarBSP = new List<ReporteConcarBSPBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeListarReporteConcarBoletosAereos", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@EMISOR", SqlDbType.Char, 20).Value = _Emisor;
                    oCommand.Parameters.Add("@FECHAINICIAL", SqlDbType.DateTime).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHAFINAL", SqlDbType.DateTime).Value = _FechaFinal;
                    oCommand.Parameters.Add("@TIPODOC", SqlDbType.Char, 5).Value = _tipodoc;
                    oConnection.Open();
                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {
                                ReporteConcarBSPBE oRepFactura = new ReporteConcarBSPBE();
                                oRepFactura.Subdiario = drListado.GetString(drListado.GetOrdinal("SUBDIARIO"));
                                oRepFactura.NroComprobante = drListado.GetString(drListado.GetOrdinal("NROCOMPROBANTE"));
                                oRepFactura.FechaEmision = drListado.GetDateTime(drListado.GetOrdinal("FECHAEMISION"));
                                oRepFactura.Moneda = drListado.GetString(drListado.GetOrdinal("MONEDA"));
                                oRepFactura.Periodo = drListado.GetString(drListado.GetOrdinal("PERIODO"));
                                oRepFactura.Tipocambio = Convert.ToString(drListado.GetDecimal(drListado.GetOrdinal("TIPOCAMBIO")));
                                oRepFactura.TipoConversion = drListado.GetString(drListado.GetOrdinal("TIPOCONVERSION"));
                                oRepFactura.FlagConversion = drListado.GetString(drListado.GetOrdinal("FLAGCONVERSION"));
                                oRepFactura.FechaTipoCambio = drListado.GetDateTime(drListado.GetOrdinal("FECHATIPOCAMBIO"));
                                oRepFactura.TipoCuenta = drListado.GetString(drListado.GetOrdinal("TIPOCUENTA"));
                                oRepFactura.CodigoAnexo = drListado.GetString(drListado.GetOrdinal("CODIGOANEXO"));
                                oRepFactura.CodCentroCosto = drListado.GetString(drListado.GetOrdinal("CODCENTROCOSTO"));
                                oRepFactura.DebeHab = drListado.GetString(drListado.GetOrdinal("DEBEHAB"));
                                oRepFactura.ImporteOriginal = Convert.ToString(drListado.GetDecimal(drListado.GetOrdinal("IMPORTEORIGINAL")));
                                oRepFactura.ImporteDolares = drListado.GetString(drListado.GetOrdinal("IMPORTEDOLARES"));
                                oRepFactura.ImporteSoles = drListado.GetString(drListado.GetOrdinal("IMPORTESOLES"));
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOCUMENTO"));
                                oRepFactura.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOCUMENTO"));
                                oRepFactura.FechaDocumento = drListado.GetDateTime(drListado.GetOrdinal("FECHADOCUMENTO"));
                                oRepFactura.FechaVencimiento = drListado.GetString(drListado.GetOrdinal("FECHAVENCIMIENTO"));
                                oRepFactura.CodigoArea = drListado.GetString(drListado.GetOrdinal("CODIGOAREA"));
                                oRepFactura.Glosa = drListado.GetString(drListado.GetOrdinal("GLOSA"));
                                oRepFactura.CodigoAuxiliar = drListado.GetString(drListado.GetOrdinal("CODIGOAUXILIAR"));
                                oRepFactura.MedioPago = drListado.GetString(drListado.GetOrdinal("MEDIOPAGO"));
                                oRepFactura.TipoDocReferencia = drListado.GetString(drListado.GetOrdinal("TIPODOCREFERENCIA"));
                                oRepFactura.NroDocReferencia = drListado.GetString(drListado.GetOrdinal("NRODOCREFERENCIA"));
                                oRepFactura.FechaDocReferencia = drListado.GetString(drListado.GetOrdinal("FECHADOCREFERENCIA"));
                                oRepFactura.BaseDocReferencia = drListado.GetString(drListado.GetOrdinal("BASEDOCREFERENCIA"));
                                oRepFactura.IgvProvicion = drListado.GetString(drListado.GetOrdinal("IGVPROVICION"));
                                oRepFactura.TipoRefMQ = drListado.GetString(drListado.GetOrdinal("TIPOREFMQ"));
                                oRepFactura.NroSerieCajaRegistra = drListado.GetString(drListado.GetOrdinal("NROSERIECAJAREGISTRA"));
                                oRepFactura.FechaOperacion = drListado.GetString(drListado.GetOrdinal("FECHAOPERACION"));
                                oRepFactura.TipoDetAsa = drListado.GetString(drListado.GetOrdinal("TIPODETASA"));
                                oRepFactura.TasaDetraccion = drListado.GetString(drListado.GetOrdinal("TASADETRACCION"));
                                oRepFactura.ImporteDetraccionUsd = drListado.GetString(drListado.GetOrdinal("IMPORTEDETRACCIONUSD"));
                                oRepFactura.ImporteDetraccionSol = drListado.GetString(drListado.GetOrdinal("IMPORTEDETRACCIONSOL"));

                                lRepConcarBSP.Add(oRepFactura);
                            }
                        }
                    }
                }
            }
            return lRepConcarBSP;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   

    public static bool ExisteCarpeta(string _rutaCarpeta)
    {
        DirectoryInfo dr = new DirectoryInfo(_rutaCarpeta);
        if (dr.Exists)
            return true;
        else
            return false;
    }

   



   


    public static byte[] ConvertirFileToByteArray(string ruta)
    {

        FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read);
        /*Create a byte array of file stream length*/
        byte[] b = new byte[fs.Length];
        /*Read block of bytes from stream into the byte array*/
        fs.Read(b, 0, System.Convert.ToInt32(fs.Length));
        /*Close the File Stream*/
        fs.Close();

        return b;
    }


    // Funcion para leer archivo
    public static byte[] ReadFile(string strArchivo)
    {
        FileStream f = new FileStream(strArchivo, FileMode.Open, FileAccess.Read);
        int size = (int)f.Length;
        byte[] data = new byte[size];
        size = f.Read(data, 0, size);
        f.Close();
        return data;
    }


    public static string SerializarXML(Object objeto)
    {
        XmlSerializer oXmlSerializer = null;
        try
        {
            using (MemoryStream oMemoryStream = new MemoryStream())
            {
                using (StreamWriter oStreamWriter = new StreamWriter(oMemoryStream, Encoding.Unicode))
                {
                    oXmlSerializer = new XmlSerializer(objeto.GetType());
                    oXmlSerializer.Serialize(oStreamWriter, objeto);
                    int i_Puntero = (int)oMemoryStream.Length;
                    byte[] arr_Bytes = new byte[i_Puntero];
                    oMemoryStream.Seek(0, SeekOrigin.Begin);
                    oMemoryStream.Read(arr_Bytes, 0, i_Puntero);
                    UnicodeEncoding UE = new UnicodeEncoding();
                    return UE.GetString(arr_Bytes).Trim();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (oXmlSerializer != null)
            {
                oXmlSerializer = null;
            }
        }
    }
    public static void KeyWinnonative()
    {
        //  Dim oConexion As String = ConfigurationManager.AppSettings("connectionString")
        //AeDalcConexion conexion = new AeDalcConexion();
        //conexion.sqlConexion.Open();
        //icdReader = clsHelper.sExecuteReader(conexion.sqlConexion, CommandType.StoredProcedure, "RET_VAL_KEY", clsHelper.sParameter("@COD", SqlDbType.VarChar, 5, ParameterDirection.Input, "WN"));
        //while (icdReader.Read)
        //{
        //    p_key = icdReader["NVALOR"];
        //}
        //icdReader.Dispose();
        //conexion.sqlConexion.Close();
    }

  

}