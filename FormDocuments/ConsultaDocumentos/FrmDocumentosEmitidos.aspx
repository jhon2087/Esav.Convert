<%@ Page Title="" Language="C#" MasterPageFile="~/FormDocuments/MainConvert.master" AutoEventWireup="true" CodeFile="FrmDocumentosEmitidos.aspx.cs" Inherits="FormDocuments_ConsultaDocumentos_FrmDocumentosEmitidos" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cpCuponeraBody" Runat="Server">

    <div>
        
               <p align="center" style="font-size: 22px;color: #6E93BF;font-weight: bolder;padding-top: 37px;">
        <asp:Literal ID="Literal3" runat="server" Text=" DOCUMENTOS DE EMITIDOS"></asp:Literal>
    </p>
    <br/>
                <table width="100%" cellspacing="5" border="0">
                 
                <tr>
                    <td align="right" style="padding-top: 20px;">
                        Tipo Documento :</td>
                    <td  colspan="2" style="padding-top: 20px;">
                         <div class="col-md-5 col-sm-9 col-xs-12">

                     <select name="ddlTipoDocumento" id="ddlTipoDocumento" class="form-control"
                                        runat="server" >
	                        <option value="">[Seleccionar]</option>
	                        <option value="1">Factura</option>
	                        <option value="2">Boleta</option>
	                        <option value="3">Nota de Crédito</option>
	                        <option value="4">Nota de Débito</option>
                            <option value="5">Documento de Cobranza</option>
                        </select></div></td>
                    <td align="right" style="padding-top: 20px;" >
                        Serie </td>
                    <td >
                     <div class="col-md-5 col-sm-9 col-xs-12" style="padding-top: 20px;">
                        <asp:TextBox runat="server" ID="txtNroDocumento0" MaxLength="60" class="form-control"
                            Width="68px"></asp:TextBox> </div>   
                    </td>
                    <td style="padding-top: 20px;width: 100px;"> 
                        
                        <asp:Literal runat="server" ID="Literal2" Text="<%$ Resources:Label, lblNumero%>"></asp:Literal>  
                        
                        </td>
                    <td align="left" >
                         <div class="col-md-5 col-sm-9 col-xs-12" style="padding-top: 20px;">
                        <asp:TextBox runat="server" ID="txtNroDocumento" MaxLength="60"  class="form-control"
                            Width="130px"></asp:TextBox>  </div>  
                        
                        </td>
                </tr>
                <tr>
                    <td align="right" >
                        <asp:Literal runat="server" ID="ltNombre" Text="<%$ Resources:Label, lblCLiente%>"></asp:Literal>
                    </td>
                    <td  colspan="2" >
                     <div class="col-md-6 col-sm-9 col-xs-12">
                        <asp:TextBox runat="server" ID="txtNombres" MaxLength="60" class="form-control"  style="margin-top:8px"
                            onkeypress="return onlyLettersAndSpace(event);" Width="305px" ontextchanged="txtNombres_TextChanged" 
                           ></asp:TextBox>  </div>  
                    </td>
                   
                    <td >
                        
                         &nbsp;</td>
                    <td > 
                        
                        &nbsp;</td>
                    <td align="left" >
                        
                         &nbsp;</td>
                          <td align="left" >
                        
                         </td>
                </tr>
                <tr>
                    <td align="right" style="padding-left: 16px; width: 120px;" >
                        Fecha Emisión :
                    </td>
                    <td  >
                       
                      <%--  Inicial :&nbsp;--%>
                        <asp:TextBox runat="server" ID="txtFechaInicial" MaxLength="60" 
                            onkeypress="return onlyLettersAndSpace(event);" Width="67px" style="margin-left: 15px;"></asp:TextBox>    
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
                        
                         <input type="button" id="btnBuscar"   class="btn btn-primary"   style="width: 100px;"
                             onclick=" btnBuscar_onclick()" value="Buscar" /></td>
                    <td > 
                        
                        <input type="button" id="btnSalir" runat="server"  class="btn btn-danger" style="width: 100px;"
         
          onclick="return btnSalir_onclick()" value="Salir" /></td>
                    <td align="left" >
                        
                        </td>
                </tr>
                <tr>
                    <td style="padding-top:5px; width: 92px;" ></td>
                    <td style="padding-top:5px;" colspan="6" align="right">
                        <span style="   color: #FF3300;">(*) selecionar fechas de busqueda</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                </table>
                 <br />
             </div>   
          
      <div id ="divGridCarrito" align="center">
        <asp:Repeater runat="server" ID="rptListado" 
              onitemdatabound="rptListado_ItemDataBound" 
              onitemcommand="rptListado_ItemCommand">
        <HeaderTemplate>
            <table class="table table-striped responsive-utilities jambo_table dataTable"  width="100%">
            <thead>
                <tr  >                    
                   <th style="color:white;" class="headerGlass"  width="5%"> <b><asp:Literal runat="server" id="lblCodigo" Text="<%$Resources:Label, lblCodigo %>"></asp:Literal></b></th>
                    <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal3" Text="<%$Resources:Label, lblSerie %>"></asp:Literal></b></th>
                     <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal1" Text="<%$Resources:Label, lblNroDocumento %>"></asp:Literal></b></th>
                     <th style="color:white;" class="headerGlass" width="40%"><b><asp:Literal runat="server" id="Literal6" Text="<%$Resources:Label, lblRazonSocial %>"></asp:Literal></b></th>      
                     <th style="color:white;" class="headerGlass" width="15%"><b><asp:Literal runat="server" id="Literal7" Text="<%$Resources:Label, lblFechaEmision %>"></asp:Literal></b></th>        
                      <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal4" Text="<%$Resources:Label, lblMoneda %>"></asp:Literal></b></th>      
                     <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal5" Text="<%$Resources:Label, lblImporteTotal %>"></asp:Literal></b></th>        
                   <!--  <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal8" Text="---"></asp:Literal></b></th>      
                    <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal9" Text="---"></asp:Literal></b></th>   -->     
                    
                </tr></thead>
        </HeaderTemplate>                
        <ItemTemplate>
        
                <tr  class="dataItemRow">
                                        
                        <td class="dataItem" align="center"><%# Eval("IdDocumento")%></td>
                        <td class="dataItem"  align="center"><%# Eval("Serie")%></td>
                        <td  class="dataItem"  align="center"><%# Eval("NroDocumento")%></td>
                         <td  class="dataItem" align="center"><%# Eval("RazonSocial")%></td>
                         <td class="dataItem" align="center"><%# Eval("FechaEmision")%></td>
                         <td  class="dataItem" align="center"><%# Eval("Moneda")%></td>
                         <td class="dataItem" align="center"><%# Eval("ImporteTotal")%></td>
                      <!--     <td class="dataItem" >
                                  <asp:HyperLink runat="server" class="Linked"  Text="<%$Resources:Label, lblVer%>" ID="lnkVerFactura"></asp:HyperLink>
                                  
                            </td>  
                            <td class="dataItem" >
                                <asp:HyperLink runat="server" class="btn btn-success"  Text="<%$Resources:Label, lblDescargar%>" ID="lnkDescargar"></asp:HyperLink>
                            </td>
                       -->               
                                         
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


