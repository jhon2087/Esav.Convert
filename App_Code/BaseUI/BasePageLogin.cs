using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de BasePageLogin
/// </summary>
public class BasePageLogin : BasePage 
{
    #region "Propiedades"

    #endregion

    #region "Eventos"

    protected override void OnLoad(EventArgs e)
    {

        if (!IsCallback)
        {
            /* && (System.Web.HttpContext.Current.Handler.ToString() != "ASP.login_aspx" && System.Web.HttpContext.Current.Handler.ToString() != "ASP.system_seguridad_changepassword_aspx")*/
            if (CurrentUser == null)
            {
                System.Web.HttpContext.Current.Session.Abandon();
                System.Web.HttpContext.Current.Response.Redirect(WebHelper.getProtocoloUrl() + "/Login.aspx");
            }
        }
        base.OnLoad(e);
    }

    #endregion

        #region "Procedimientos"

        #endregion
}