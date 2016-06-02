using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Descripción breve de WebApplication
/// </summary>
public class WebApplication
{
    private static WebApplication instancia = null;
    public static WebApplication getInstance()
    {
        if (instancia == null)
        {
            instancia = new WebApplication();
        }
        return instancia;
    }
    private WebApplication()
    {
    }

    public string getAgencyGoGDN
    {
        get
        {
            return ConfigurationManager.AppSettings["GoGDNExpressAgency"].ToString();
        }
    }
    public string getApplicationName
    {
        get
        {
            return ConfigurationManager.AppSettings["ApplicationName"].ToString();
        }
    }

    public string getApplicationMode
    {
        get
        {
            return ConfigurationManager.AppSettings["ApplicationMode"];
        }
    }

    public string getApplicationHostOnDevelopmentMode
    {
        get
        {
            return ConfigurationManager.AppSettings["ApplicationHostOnDevelopmentMode"];
        }
    }

    public string getApplicationUrl
    {
        get
        {
            return ConfigurationManager.AppSettings["Application.url"].ToString();
        }
    }


    public string getCodeMerchant
    {
        get
        {
            return ConfigurationManager.AppSettings["GoGDNExpressMerchant"];
        }
    }


    public string getHostValue
    {
        get
        {
            string subdomain = "";
            var hostname = HttpContext.Current.Request.Url.Host;
            if (getApplicationMode == "DEV")
            {
                subdomain = getApplicationHostOnDevelopmentMode;
            }
            else
            {
                if (hostname.Split('.').Length > 1)
                {
                    int index = hostname.IndexOf(".");
                    subdomain = hostname.Substring(0, index);
                }
            }
            return subdomain;
        }
    }

    public string getWebEmail
    {
        get
        {
            return ConfigurationManager.AppSettings["CorreoWeb"].ToString();
        }
    }

    public string getNameEmail
    {
        get
        {
            return ConfigurationManager.AppSettings["NombreCorreo"].ToString();
        }
    }
    public string getUsuarioEmail
    {
        get
        {
            return ConfigurationManager.AppSettings["LoginCorreo"].ToString();
        }
    }

    public string getPasswordEmailSMTP
    {
        get
        {
            return ConfigurationManager.AppSettings["ClaveCorreo"].ToString();
        }
    }

    public string getHostSMTP
    {
        get
        {
            return ConfigurationManager.AppSettings["HostMail"].ToString();
        }
    }

    public string getPortSMTP
    {
        get
        {
            return ConfigurationManager.AppSettings["PortMail"].ToString();
        }
    }

    public string getCorreoAdmin
    {
        get
        {
            return ConfigurationManager.AppSettings["CorreoAdmin"].ToString();
        }
    }

    public string getCorreoAdmin1
    {
        get
        {
            return ConfigurationManager.AppSettings["CorreoAdmin1"].ToString();
        }
    }
    public string getCorreoBanking
    {
        get
        {
            return ConfigurationManager.AppSettings["CorreoBanking"].ToString();
        }
    }


    public int getIdUsuarioVirtual
    {
        get
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["idUsuarioVirtual"].ToString());
        }
    }

    public string getUrlWebInicial
    {
        get
        {
            return ConfigurationManager.AppSettings["urlWebInicial"].ToString();
        }
    }

    public string getCorreoInforme
    {
        get
        {
            return ConfigurationManager.AppSettings["CorreoInforme"].ToString();
        }
    }

    public bool getShowMessageAvisoHotel
    {
        get
        {
            bool showMessageAvisoHotel = false;
            bool.TryParse(ConfigurationManager.AppSettings["showMessageAvisoHotel"], out showMessageAvisoHotel);
            return showMessageAvisoHotel;
        }
    }

    public string getGoogleAPIKey
    {
        get
        {
            return ConfigurationManager.AppSettings["GoogleAPIKey"].ToString();
        }
    }

    public string getCorreoDeveloper
    {
        get
        {
            return ConfigurationManager.AppSettings["CorreoDeveloper"].ToString();
        }
    }
}