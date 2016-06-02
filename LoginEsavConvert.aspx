<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginEsavConvert.aspx.cs" Inherits="LoginEsavConvert" %>
<%@ Register src="~/Controles/ucLoginEsavConvert.ascx" tagname="ucLogin" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Esav Convert</title>
    <%-- <link href="App_Themes/Default/base.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Default/layout.css" rel="stylesheet" type="text/css" />--%>
    <link href="css/style.css" rel="stylesheet" type="text/css" media="all"/>
    <script src="js/jquery.min.js"></script>
    <script src="js/easyResponsiveTabs.js" type="text/javascript"></script>
   
</head>
<body class="bodyy">
    <div class="head" >
		<div class="logo" style="height: 176px;">
			<div class="logo-top">
				<img src="images/logo.png" style="height: 40px;WIDTH: 243px;background-color: white; margin: 22px;padding: 13px;margin-top: 23px;">
			</div>
			
		</div>		
		<div class="login">
                    <div class="sap_tabs">
                            <div id="horizontalTab" style="display: block; width: 100%; margin: 0px;">
                                <ul class="resp-tabs-list">
						            <li class="resp-tab-item resp-tab-active" aria-controls="tab_item-0" role="tab"><span>Ingresar</span></li>
						            <li class="resp-tab-item" aria-controls="tab_item-1" role="tab"><label>/</label><span>Go</span></li>
						            <div class="clearfix"></div>
					            </ul>
                                <div class="resp-tabs-container">
                                        
                                        <form id="form1" runat="server">
                                        <div>
                                         <table width="100%" cellspacing="0" cellpadding="0" >
                                            <tr>
                                                <td align="center">
                                                    <uc1:ucLogin ID="ucLogin1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                        </form>
                                </div>
                            </div>
                    </div>
        </div>


	</div><!-- container -->
  
    <div class="account">
			<ul>
				
				<li><p>¿Olvidaste tu <a href="#"> Usuario y/o Contraseña?</a></p></li>
				<div class="clear"></div>
			</ul>
	</div>

</body>
</html>
