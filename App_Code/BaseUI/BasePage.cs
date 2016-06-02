using System;
using System.Collections.Generic;
using System.Web;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Linq;
using System.Web.Hosting;


/// <summary>
/// Descripción breve de BasePage
/// </summary>
public class BasePage : System.Web.UI.Page, ICallbackEventHandler
{
    #region "Propiedades"

    private bool m_sendViewStateOnCallBack = true;
    private List<string> _ResultCallback = new List<string>();
    private string m_ViewStateID;

    private bool _validateNavigation = false;
    public bool validateNavigation
    {
        get { return _validateNavigation; }
        set { _validateNavigation = value; }
    }

    private bool _disablePageCaching = true;
    public bool disablePageCaching
    {
        get { return _disablePageCaching; }
        set { _disablePageCaching = value; }
    }

    private bool _changeFrameSecure = false;
    public bool changeFrameSecure
    {
        get { return _changeFrameSecure; }
        set { _changeFrameSecure = value; }
    }

    //inicio - Session
    public WebSession CurrentUser;
    public string CodigoEmisor { get; set; }
    public string UsuarioId { get; set; }
    public string Nombres { get; set; }
    public string Ruc { get; set; }
    public string Sucursal { get; set; }
    public string RazonSocial { get; set; }
    public string Direccion { get; set; }
    public string Email { get; set; }
    public string mGuil { get; set; }
    //inicio - Session

    public string UsuarioIP
    {
        get
        {
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }

    #endregion

    #region "Eventos"

    protected override void OnPreInit(EventArgs e)
    {
        ApplyTheme("Default");
        base.OnPreInit(e);
    }

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    protected override void InitializeCulture()
    {
        int currentCultureID = 1;
        string currentCultureKey = "";
        string controlName1 = "";
        //if (this.Master != null) { controlName1 = "demo"; }
        if (!string.IsNullOrEmpty(HttpContext.Current.Request["ctl00$ctl00$ddlIdioma"]))
        {
            controlName1 = "ctl00$ctl00$ddlIdioma";
        }

        if (!string.IsNullOrEmpty(HttpContext.Current.Request["ctl00$ddlIdioma"]))
        {
            controlName1 = "ctl00$ddlIdioma";
        }

        if (controlName1.Equals(""))
        {
            controlName1 = "ddlIdioma";
        }

        if (!string.IsNullOrEmpty(HttpContext.Current.Request[controlName1]))
        {
            int nIdioma = Convert.ToInt32(HttpContext.Current.Request[controlName1]);

            enumIdioma idioma = (enumIdioma)Enum.Parse(typeof(enumIdioma), nIdioma.ToString());

            if (idioma == enumIdioma.ingles)
            {
                currentCultureID = nIdioma;
                currentCultureKey = "en-US";
            }
            else
            {
                currentCultureID = nIdioma;
                currentCultureKey = "es-PE";
            }

            HttpContext.Current.Session["currentCultureID"] = currentCultureID;
            HttpContext.Current.Session["currentCultureKey"] = currentCultureKey;
        }
        else
        {
            if (HttpContext.Current.Session["currentCultureID"] != "" && HttpContext.Current.Session["currentCultureID"] != null)
            {
                currentCultureID = Convert.ToInt32(HttpContext.Current.Session["currentCultureID"]);
                currentCultureKey = HttpContext.Current.Session["currentCultureKey"].ToString();
            }
            else
            {
                string userCultureKey = "es-PE";

                currentCultureID = 1;

                if (Request.UserLanguages != null)
                {
                    userCultureKey = Request.UserLanguages[0];
                }
                if (userCultureKey.Length != 6 && userCultureKey.Length != 5)
                {
                    currentCultureID = 2;
                    currentCultureKey = "en-US";
                }
            }
        }

        CultureInfo _CultureInfo;
        try
        {
            _CultureInfo = new System.Globalization.CultureInfo(currentCultureKey);
        }
        catch (Exception)
        {
            _CultureInfo = new System.Globalization.CultureInfo("es-PE");
        }


        try
        {
            Thread.CurrentThread.CurrentCulture = _CultureInfo;
            Thread.CurrentThread.CurrentUICulture = _CultureInfo;
        }
        catch (Exception)
        {
            currentCultureKey = "en-US";
            _CultureInfo = new System.Globalization.CultureInfo(currentCultureKey);
            Thread.CurrentThread.CurrentCulture = _CultureInfo;
            Thread.CurrentThread.CurrentUICulture = _CultureInfo;
        }

        System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator;
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator;

        System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyGroupSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyGroupSeparator;
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyGroupSeparator;

        System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;

        System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberGroupSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator;
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator;

        System.Globalization.DateTimeFormatInfo.CurrentInfo.AMDesignator = "AM";
        System.Globalization.DateTimeFormatInfo.CurrentInfo.PMDesignator = "PM";

        base.InitializeCulture();
    }

    protected override void OnInit(EventArgs e)
    {

        CurrentUser = (WebSession)HttpContext.Current.Session["SessionUser"];

        /*datos del usuario logeado*/
        if (CurrentUser != null)
        {
            CodigoEmisor = CurrentUser.CodigoEmisor;
            Ruc = CurrentUser.Ruc;
            UsuarioId = CurrentUser.UsuarioId;
            Nombres = CurrentUser.Nombres;
            RazonSocial = CurrentUser.RazonSocial;
            Sucursal = CurrentUser.Sucursal;
            Direccion = CurrentUser.Direccion;
            Email = CurrentUser.Email;
            mGuil = CurrentUser.mGuil;
        }


        base.OnInit(e);
        String mScript = ClientScript.GetCallbackEventReference("", "", "", "", "", false);


        if (this.EnableViewState && this.SendViewStateOnCallBack)
        {
            ClientScript.RegisterHiddenField("__VIEWSTATEID", this.ViewStateID);
        }
        else
        {
            ClientScript.RegisterHiddenField("__VIEWSTATEID", "");
        }

        if (this.Page.IsCallback)
        {
            if (System.Web.HttpContext.Current.Request.Form["__FORMCALLBACK"] == "")
            {
                try
                {
                    this.Controls.Clear();
                }
                catch
                {
                    throw;
                }
            }
        }
    }

    #endregion

    #region "Procedimientos"

    public string ApplicationUrl(String control)
    {
        string url = WebApplication.getInstance().getApplicationUrl;
        url = String.Format(url, HttpContext.Current.Request.Url.Scheme, WebApplication.getInstance().getHostValue);
        return url + control;
    }

    public Boolean SendViewStateOnCallBack { get { return m_sendViewStateOnCallBack; } set { m_sendViewStateOnCallBack = value; } }

    public String ViewStateID
    {
        get
        {
            if (this.EnableViewState)
            {
                m_ViewStateID = "__VSTATE";
            }
            else
            {
                m_ViewStateID = "__VIEWSTATE";
            }
            return m_ViewStateID;
        }
    }

    private void ApplyTheme(string theme)
    {
        if (EnableTheming)
        {
            Page.Theme = theme;
        }
    }

    public void AddCallbackValue(String value)
    {
        if (IsCallback)
        {
            _ResultCallback.Add(value);
        }
    }

    public void AddCallbackControl(Control control)
    {
        if (IsCallback)
        {
            _ResultCallback.Add(WebHelper.getHtmlControl(control));
        }
    }

    public string GetCallbackResult()
    {
        String mReturnValue = "";
        mReturnValue = String.Join(":::", _ResultCallback.ToArray());
        return mReturnValue;
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        String[] mArguments = eventArgument.Split(new char[] { '|' });
        MethodInfo mMethodInfo;
        String[] mParameters;

        if (mArguments.Length > 1)
        {
            if (mArguments[1] == "")
            {
                mParameters = null;
            }
            else
            {
                mParameters = mArguments[1].Split(new char[] { ':' });

                for (int i = 0; i < mParameters.Length - 1; i++)
                {
                    mParameters[i] = HttpUtility.UrlDecode(mParameters[i]);
                }
            }

        }
        else
        {
            mParameters = null;
        }

        mMethodInfo = this.GetType().GetMethod(mArguments[0]);
        try
        {
            mMethodInfo.Invoke(this, mParameters);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static String GetFormatDateInfo()
    {
        String formatDate = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        String[] arayFormat = formatDate.Split('/');
        if (arayFormat[0].ToLower() == "m")
        {
            return String.Format("{0}{0}/{1}{1}/{2}", arayFormat[0], arayFormat[1], arayFormat[2]);
        }
        else
        {
            return formatDate;
        }
    }

    public String GetFormatDate()
    {
        return GetFormatDateInfo();
    }



    #endregion
}