using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de BasePageMaster
/// </summary>
public class BasePageMaster : System.Web.UI.MasterPage
{
    #region "Constructor"
    public BasePageMaster()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    #endregion

    #region "Propiedades"
    public WebSession CurrentUser;
    #endregion

    #region "Eventos"
    #endregion

    #region "Procedimientos"

    public String ApplicationUrl(String control)
    {
        String url = WebApplication.getInstance().getApplicationUrl;
        url = String.Format(url, HttpContext.Current.Request.Url.Scheme, WebApplication.getInstance().getHostValue);
        return url + control;
    }




    #endregion
}