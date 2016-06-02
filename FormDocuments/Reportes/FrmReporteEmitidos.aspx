<%@ Page Title="" Language="C#" MasterPageFile="~/FormDocuments/MainConvert.master" AutoEventWireup="true" CodeFile="FrmReporteEmitidos.aspx.cs" Inherits="FormDocuments_Reportes_FrmReporteEmitidos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cpCuponeraBody" Runat="Server">

    <div>
        <div id="dvBusqueda"  BorderColor="#595959" BackColor="White" BorderWidth="1px" class="body">
                <table width="100%" cellspacing="5" border="0">
                <tr>
                    <td align="right">
                        Tipo Documento :</td>
                    <td  colspan="2" >
                        
                     <select name="ddlTipoDocumento" id="ddlTipoDocumento" 
                runat="server" >
	<option value="">[Seleccionar]</option>
	<option value="1">Factura</option>
	<option value="2">Boleta</option>
	<option value="3">Nota de Crédito</option>
	<option value="4">Nota de Débito</option>
    <option value="5">Documento de Cobranza</option>
</select></td>
                    <td align="right" >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td > 
                        
                        </td>
                    <td align="left" >
                        
                        </td>
                </tr>
                <tr>
                    <td align="right" >
                        <asp:Literal runat="server" ID="ltNombre" Text="<%$ Resources:Label, lblCLiente%>"></asp:Literal> :
                    </td>
                    <td  colspan="2" >
                        <asp:TextBox runat="server" ID="txtNombres" MaxLength="60" 
                            onkeypress="return onlyLettersAndSpace(event);" Width="305px" ontextchanged="txtNombres_TextChanged" 
                           ></asp:TextBox>    
                    </td>
                    <td align="right">
                        Serie :</td>
                    <td >
                        <asp:TextBox runat="server" ID="txtNroDocumento0" MaxLength="60" 
                            onkeypress="return onlyLettersAndSpace(event);" Width="68px"></asp:TextBox>    
                    </td>
                    <td > 
                        
                        <asp:Literal runat="server" ID="Literal2" Text="<%$ Resources:Label, lblNumero%>"></asp:Literal> </td>
                    <td align="left" >
                        
                        <asp:TextBox runat="server" ID="txtNroDocumento" MaxLength="60" 
                            onkeypress="return onlyLettersAndSpace(event);" Width="130px"></asp:TextBox>    
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        Fecha Emisión :
                    </td>
                    <td  >
                         <asp:Literal runat="server" ID="Literal10" Text="Inicial"></asp:Literal> :
                      <%--  Inicial :&nbsp;--%>
                        <asp:TextBox runat="server" ID="txtFechaInicial" MaxLength="60" 
                            onkeypress="return onlyLettersAndSpace(event);" Width="67px"></asp:TextBox>    
                    </td>
                    <td  >
                     <asp:Literal runat="server" ID="Literal11" Text="Final"></asp:Literal> :
                      <%--  Final :--%>
                        <asp:TextBox runat="server" ID="txtFechaFinal" MaxLength="60" 
                            onkeypress="return onlyLettersAndSpace(event);" Width="68px"></asp:TextBox>    
                    </td>
                    <td align="right" >
                        </td>
                    <td >
                        &nbsp;</td>
                    <td > 
                        
                        </td>
                    <td align="left" >
                        
                        </td>
                </tr>
                <tr>
                    <td style="padding-top:5px; width: 92px;" ></td>
                    <td style="padding-top:5px;" colspan="6" align="right">
                        <span style="   color: #FF3300;">(*) selecionar fechas de busqueda</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="btnBuscar"   class="aspBuscar"   onclick=" btnBuscar_onclick()" />&nbsp;<asp:Button ID="btngenera" runat="server"      class=" aspButton" Text="Generar" onclick="btngenera_Click" 
                            Height="26px" />
                        &nbsp;<input type="button" id="btnSalir" runat="server"  class="aspRubenSalir"  style=" padding-bottom: 10px;padding-top: 5px;"
         
          onclick="return btnSalir_onclick()" />&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="padding-top:5px;" colspan="2" align="center">
                                        
                        &nbsp;</td>
                </tr>
                </table>
                 <br />
             </div>   
          
      <div id ="divGridCarrito" align="center">
        <asp:Repeater runat="server" ID="rptListado" 
              onitemdatabound="rptListado_ItemDataBound" 
              onitemcommand="rptListado_ItemCommand">
        <HeaderTemplate>
            <table class="gridContainer"  width="100%">
                <tr  >                    
                   <%--<th class="headerGlass"  width="5%"> <b><asp:Literal runat="server" id="lblCodigo" Text="<%$Resources:Label, lblCodigo %>"></asp:Literal></b></th>--%>
                    <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal3" Text="<%$Resources:Label, lblSerie %>"></asp:Literal></b></th>
                     <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal1" Text="<%$Resources:Label, lblNroDocumento %>"></asp:Literal></b></th>
                     <th class="headerGlass" width="40%"><b><asp:Literal runat="server" id="Literal6" Text="<%$Resources:Label, lblRazonSocial %>"></asp:Literal></b></th>      
                     <th class="headerGlass" width="15%"><b><asp:Literal runat="server" id="Literal7" Text="<%$Resources:Label, lblFechaEmision %>"></asp:Literal></b></th>        
                      <th class="headerGlass" width="15%"><b><asp:Literal runat="server" id="Literal9" Text="<%$Resources:Label, lblSucursal %>"></asp:Literal></b></th> 
                      <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal4" Text="<%$Resources:Label, lblMoneda %>"></asp:Literal></b></th>      
                     <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal8" Text="Cantidad"></asp:Literal></b></th> 
                     <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal98" Text="ValorUnitario"></asp:Literal></b></th>
                     <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal120" Text="Descuento"></asp:Literal></b></th>
                     <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal5" Text="IGV"></asp:Literal></b></th>  
                     <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal50" Text="<%$Resources:Label, lblImporteTotal %>"></asp:Literal></b></th>        
                     <%--<th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal8" Text="---"></asp:Literal></b></th>      
                    <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal9" Text="---"></asp:Literal></b></th>--%>        
                    
                </tr>
        </HeaderTemplate>                
        <ItemTemplate>
        
                <tr  class="dataItemRow">
                                        
                        <%--<td class="dataItem" align="center"><%# Eval("IdDocumento")%></td>--%>
                        <td class="dataItem"  align="center"><%# Eval("Serie")%></td>
                        <td  class="dataItem"  align="center"><%# Eval("NroDocumento")%></td>
                         <td  class="dataItem" align="center"><%# Eval("RazonSocial")%></td>
                         <td class="dataItem" align="center"><%# Eval("FechaEmision")%></td>
                         <td class="dataItem" align="center"><%# Eval("Sede")%></td>
                         <td  class="dataItem" align="center"><%# Eval("Moneda")%></td>
                         <td  class="dataItem" align="center"><%# Eval("Cantidad")%></td>
                         <td  class="dataItem" align="center"><%# Eval("ValorUnitario")%></td>
                         <td  class="dataItem" align="center"><%# Eval("Descuento")%></td>
                         <td  class="dataItem" align="center"><%# Eval("IGV")%></td>
                         <td class="dataItem" align="center"><%# Eval("ImporteTotal")%></td>
                           <%--<td class="dataItem" >
                                  <asp:HyperLink runat="server" class="Linked"  Text="<%$Resources:Label, lblVer%>" ID="lnkVerFactura"></asp:HyperLink>
                                  
                            </td>
                            <td class="dataItem" >
                                <asp:HyperLink runat="server" class="Linked"  Text="<%$Resources:Label, lblDescargar%>" ID="lnkDescargar"></asp:HyperLink>
                            </td>--%>
                                      
                                         
                </tr>
                        
        </ItemTemplate>
        <FooterTemplate>
        
                  <tr class="gridLoading" style="display: none">
                    <td colspan="11" align="center" valign="middle" style="background-color: #fff">
                      <table cellpadding="0" cellspacing="0" border="0" style="padding-top: 2px">
                        <tr>
                          <td>
                            <div style="width: 40px; height: 40px; background: url(../../gogdn/library/img/ajax-loader.gif) center no-repeat;">
                            </div>
                          </td>
                          <td>
                            <asp:Literal runat="server" ID="msgProcesando" Text="<%$ Resources:Label, lblModificar %>"></asp:Literal>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                                  
                  <tr id="trFooter" class="trFooter" align="center">
                        <td colspan="11">
                          <table style="height: 30px;">
                            <tr>
                              <td valign="middle">
                                &nbsp;<asp:Label runat="server" ID="lblPages"></asp:Label>
                              </td>
                              <td valign="middle">
                                <asp:HyperLink runat="server" ID="lnkFirst" Text="First"></asp:HyperLink>
                              </td>
                              <td valign="middle">
                                <asp:HyperLink runat="server" ID="lnkPrev" Text="Prev"></asp:HyperLink>
                              </td>
                              <td valign="middle">
                                <asp:Label runat="server" ID="lblCurrpage"></asp:Label>
                              </td>
                              <td valign="middle">
                                <asp:HyperLink runat="server" ID="lnkNext" Text="Next"></asp:HyperLink>
                              </td>
                              <td valign="middle">
                                <asp:HyperLink runat="server" ID="lnkLast" Text="Last"></asp:HyperLink>
                              </td>
                            </tr>
                          </table>
                        </td>
                 </tr>
             </table>
        </FooterTemplate>        
    </asp:Repeater>
    </div> 
        </div>       
   <script type="text/javascript">
       var _language = "1";
       /* begin - create datepicker */
       jQuery(document).ready(function () {
           jQuery(function ($) {
               $.datepicker.regional['1'] = {
                   clearText: 'Limpiar', clearStatus: '',
                   closeText: 'Cerrar', closeStatus: '',
                   prevText: '&#x3c;Ant', prevStatus: '',
                   prevBigText: '&#x3c;&#x3c;', prevBigStatus: '',
                   nextText: 'Sig&#x3e;', nextStatus: '',
                   nextBigText: '&#x3e;&#x3e;', nextBigStatus: '',
                   currentText: 'Hoy', currentStatus: '',
                   monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
									'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                   monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
									'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                   monthStatus: '', yearStatus: '',
                   weekHeader: 'Sm', weekStatus: '',
                   dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                   dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                   dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                   dayStatus: 'DD', dateStatus: 'D, M d',
                   dateFormat: 'dd/mm/yy', firstDay: 1,
                   initStatus: '', isRTL: false,
                   showMonthAfterYear: false, yearSuffix: ''
               };

               $.datepicker.setDefaults($.datepicker.regional[_language]);
           });

           jQuery('#<%=txtFechaInicial.ClientID%>').datepicker({
               minDate: '-1Y',
               maxDate: '+0M +0D',
               constrainInput: true,
               //Lo Nuevo
               showOn: 'both',
               buttonImage: '../../gogdn/library/img/calendar.png',
               buttonImageOnly: true,
               changeYear: true,
               changeMonth: true
               //numberOfMonths: 2
           });

           jQuery('#<%=txtFechaFinal.ClientID%>').datepicker({
               minDate: '-1Y',
               maxDate: '+0M +0D',
               constrainInput: true,
               //Lo Nuevo
               showOn: 'both',
               buttonImage: '../../gogdn/library/img/calendar.png',
               buttonImageOnly: true,
               changeYear: true,
               changeMonth: true
               //numberOfMonths: 2
           });

       });
       /* end - create datepicker */

       var pageIndex = 1;
       //inicio - funciones de paginado
       function goPage(idx) {

           var fechini = $("#" + '<%= txtFechaInicial.ClientID %>').val();
           var fechfin = $("#" + '<%= txtFechaFinal.ClientID %>').val();
           var tipodoc = $("#" + '<%= ddlTipoDocumento.ClientID %>').val();
           var RazonSocial = $("#" + '<%= txtNombres.ClientID %>').val();
           var NroDocumento = $("#" + '<%= txtNroDocumento.ClientID %>').val();
           var Serie = $("#" + '<%= txtNroDocumento0.ClientID %>').val();

           $(".dataItemRow").css("display", "none");
           $(".trFooter").css("display", "none");
           $(".gridLoading").css("display", "");
           DoCallBack("cargarGrillas", RazonSocial + ':' + NroDocumento + ":" + Serie + ":" + idx + ":" + fechini + ":" + fechfin + ":" + tipodoc, End_goPage);

       }

       function End_goPage(_arg) {
           var mData = _arg.split(":::");
           if (mData[0] == "-1") {
               alert(mData[1]);
               return;
           }

           $(".dataItemRow").css("display", "");
           $(".trFooter").css("display", "");
           $(".gridLoading").css("display", "none");

           document.getElementById("divGridCarrito").innerHTML = mData[1];

       }
       //fin - funciones de paginado


       //inicio - recuperar datos de agencia
       function lnkRetornar_onClick(_id, _cod, _name, _direccion) {

           window.parent.cargarClienteBoleta(_id, _cod, _name, _direccion);
           window.parent.CloseDialog();
       }

       //fin - recuperar datos de agencia

       function btnBuscar_onclick() {

           var fechini = $("#" + '<%= txtFechaInicial.ClientID %>').val();
           var fechfin = $("#" + '<%= txtFechaFinal.ClientID %>').val();
           var tipodoc = $("#" + '<%= ddlTipoDocumento.ClientID %>').val();
           var RazonSocial = $("#" + '<%= txtNombres.ClientID %>').val();
           var NroDocumento = $("#" + '<%= txtNroDocumento.ClientID %>').val();
           var Serie = $("#" + '<%= txtNroDocumento0.ClientID %>').val();



           $(".dataItemRow").css("display", "none");
           $(".trFooter").css("display", "none");
           $(".gridLoading").css("display", "");

           DoCallBack("cargarGrillas", RazonSocial + ':' + NroDocumento + ":" + Serie + ":" + "1" + ":" + fechini + ":" + fechfin + ":" + tipodoc, End_Buscar);

       }
       function End_Buscar(_arg) {
           var mData = _arg.split(":::");
           if (mData[0] == "-1") {
               alert(mData[1]);
               return;
           }

           $(".dataItemRow").css("display", "");
           $(".trFooter").css("display", "");
           $(".gridLoading").css("display", "none");

           document.getElementById("divGridCarrito").innerHTML = mData[1];

       }

       function lnkNuevo_onClick(_url) {


           window.location.href = _url;

       }


       function lnkDescargar_onClick(_url) {


           window.location.href = _url;

       }
       function lnkCancelar_onClick(_url) {
           window.location.href = _url;
       }

       function btnSalir_onclick() {

       }

   </script>


</asp:Content>
