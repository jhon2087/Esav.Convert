<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLoginEsavConvert.ascx.cs" Inherits="Controles_ucLoginEsavConvert" %>



<style type="text/css">
    .style1
    {
        height: 39px;
    }
    .imputUser
    {}
    .style3
    {
        width: 19px;
    }
    
    input.placeholder {
    text-align: center;
}
    .style4
    {
        width: 19px;
        height: 1px;
    }
    .style5
    {
        height: 1px;
    }
</style>







<table border="0" cellpadding="0" cellspacing="2" align="center" style="height: 261px; width: 394px" 
   >
 <tr >
    <td colspan="3" align="center" class="style1"  >
     
      
      
     
     </td>
 </tr>
 <tr>
    <td align="right" class="style3"  >
        <br />
        </td>
    <td  >
        <asp:TextBox ID="txtUserName" 
            onKeyPress="return onlyNumbersOrLetters(event);Verifica(event);" runat="server" 
            oncontextmenu="return false" onpaste="return false" Width="348px" 
            TabIndex="1" MaxLength="15"    placeholder="Ingrese Usuario" 
            style="text-align: center"  Height="46px" 
            CssClass="imputUser"   BorderStyle="None"/>
              
         </td>
    <td ><div id="msgEr1" style="display:none;"><img src="../gogdn/library/img/edtError.png" runat="server" id="imgError1" alt="" /></div></td>
 
 </tr>
  
 <tr>
    <td align="right" class="style4">
        </td>
    <td class="style5" >
        </td>
    <td class="style5" ></td>
 </tr>
  
 <tr>
    <td align="right" class="style3">
      <br />
        &nbsp;</td>
    <td >
        <asp:TextBox ID="txtPassword" onKeyPress="Verifica(event);" runat="server" 
            TextMode="Password" Width="348px" TabIndex="2" MaxLength="15"  
            placeholder="Ingrese Contraseña" 
            style="margin-left: 2px; text-align: center;" Height="46px" 
            CssClass="imputPass" BorderStyle="None"/>
    </td>
    <td ><div id="msgEr2" style="display:none;"><img src="../gogdn/library/img/edtError.png" runat="server" id="imgError2" alt="" /></div></td>
 </tr>
    
 <tr>
    <td colspan="3" style="color: Red; font-size: small">
        &nbsp;<asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
    </td>
 </tr>
 <tr>
    <td align="center" class="style3">
        &nbsp;</td>
    <td colspan="2" align="left">
        <asp:Button Text="" ID="btnLogin"  
            class="aspPButton" Width="145px"
            runat="server" TabIndex="4" OnClientClick="return Validate();" 
            onclick="btnLogin_Click" Height="40px" />           
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="lnkOlvidoContrasenha" CssClass="Linked" runat="server" Text="<%$ Resources:label,  lnkOlvideClave%>"></asp:HyperLink>
         
    </td>
 </tr>
 <tr>
    <td colspan="3" align="right">
       
       <div id="dvMensajeError" runat="server" visible="false">
    <div id="msg-error" class="warning-error">
        <p>
            <b>
            <asp:Literal ID="ltErrorMensaje" runat="server"></asp:Literal>
            </b>
        </p>
    </div>
</div>
       
        &nbsp;</td>
 </tr>
</table>




<br />
<script language="javascript" type="text/javascript">


    function salir() { $("#login").show(); $("#ver").hide(); }
    function Verifica(evt) {
        evt = (evt) ? evt : event;
        var charCode = (evt.which) ? evt.which : evt.keyCode
        if (charCode == 13) {
            if (!Validate()) { return false; }
        }
    }
    function Validate() {
        var isValid = true;

        if ($("#<%=txtUserName.ClientID%>").val() == "") {
            $("#msgEr1").show();
            $("#msgEr2").hide();
            $("#msgEr3").hide();
            $("#txtUserName").focus();
            return false;
        }
        if ($("#<%=txtPassword.ClientID%>").val() == "") {
            $("#msgEr1").hide();
            $("#msgEr2").show();
            $("#msgEr3").hide();
            $("#txtPassword").focus();
            return false;
        }


        if (isValid) {
            showLoader(); $("#msgEr").hide();
            return true;
        }
    }

    function lnkOlvidoContrasenha_onClick(_url) {
        top.location.href = _url;
    }
    function lnkNuevoUsusario_onClick(_url) {
        top.location.href = _url;
    }
    function lnkNuevoAgencia_onClick(_url) {
        top.location.href = _url;
    }
   
</script>
