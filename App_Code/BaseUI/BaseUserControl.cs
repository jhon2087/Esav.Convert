using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Reflection;

/// <summary>
/// Descripción breve de BaseUserControl
/// </summary>
public class BaseUserControl : System.Web.UI.UserControl, ICallbackEventHandler
{

    #region "Propiedades"
    private bool m_sendViewStateOnCallBack = true;
    private List<string> _ResultCallback = new List<string>();
    private string m_ViewStateID;
    #endregion

    #region "Eventos"
    public BaseUserControl()
    {

    }

    public string ApplicationUrl(String control)
    {
        string url = WebApplication.getInstance().getApplicationUrl;
        url = String.Format(url, HttpContext.Current.Request.Url.Scheme, WebApplication.getInstance().getHostValue);
        return url + control;
    }
    #endregion

    #region "Procedimientos"

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
    public void AddCallbackValue(String value)
    {
        if (Page.IsCallback)
        {
            _ResultCallback.Add(value);
        }
    }

    public void AddCallbackControl(Control control)
    {
        if (Page.IsCallback)
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

    #endregion
}