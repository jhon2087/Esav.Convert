<%@ Page Title="" Language="C#" MasterPageFile="~/FormDocuments/MainConvert.master" AutoEventWireup="true" CodeFile="FrmListaTipoCambio.aspx.cs" Inherits="FormDocuments_Comun_FrmListaTipoCambio" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cpCuponeraBody" Runat="Server">
    <div id="dvBusqueda"  BorderColor="#595959" BackColor="White" BorderWidth="1px" class="body">
                <table width="100%" cellspacing="5" border="0">
                <tr style="height: 30px;">
                </tr>
                
                <tr>
                 
                    <td align="right" class="style6" >
                   
                        <asp:Literal runat="server" ID="ltNombre" Text="Seleccione Fecha"></asp:Literal> :
                   </td>                    
                    <td class="style8" style="padding-top: 13px;" >
                     <div class="col-md-9 col-sm-9 col-xs-12">
                        <asp:TextBox runat="server" ID="txtFecha" MaxLength="60" onkeypress="return onlyNumbersAndSlash(event);" Width="116" class="form-control"></asp:TextBox>    
                   </div></td>

                    <td style=" height: 44px;">
                        &nbsp;</td>
                    <td class="style9"> 
                        
                         <input type="button" id="btnBuscar"  onclick="btnBuscar_onClick();"   
                             class="btn btn-primary" value="Buscar" style="width: 130px;"  /><input type="button" 
                             id="btnNuevo"  runat="server"  class="btn btn-success" value="Nuevo"  style="width: 130px;"      /><input 
                             type="button" id="btnSalir" runat="server"  class="btn btn-danger" dir="ltr" 
                             value="Salir" style="width: 130px;" /></td>
                    
               
                </tr>
                 <tr style="height: 30px;">
                </tr>
               
                </table>
                 <br />
             </div>   
          <div id="divGridCarrito">
<asp:Repeater runat="server" ID="rptListado" onitemdatabound="rptListado_ItemDataBound">
        <HeaderTemplate>
            <table class="table table-striped responsive-utilities jambo_table dataTable"  width="100%">
             <thead>
                <tr  >                    
                   <th style="color:white;" class="headerGlass"> <b><asp:Literal runat="server" id="ltMonedaOrigen" Text="<%$Resources:Label, lblFecha %>"></asp:Literal></b></th>
                    <th style="color:white;" class="headerGlass"><b><asp:Literal runat="server" id="Literal1" Text="<%$Resources:Label, lblMonto %>"></asp:Literal></b></th>
                   
                    <th style="color:white;" class="headerGlass"><b><asp:Literal runat="server" id="Literal3" Text="<%$Resources:Label, lblUsuario %>"></asp:Literal></b></th>
                    
                    <th ><center>Modificar</center></th>
                </tr></thead>
        </HeaderTemplate>                
        <ItemTemplate>
        
                <tr  class="dataItemRow">
                                        
                        <td class="dataItem" align="center"><%# Eval("FechaTipoCambio")%></td>
                        <td class="dataItem"  align="center"><%# Eval("Monto")%></td>
                        <td  class="dataItem"  align="center"><%# Eval("UsuarioCreo")%></td>
                        <td class="dataItem" align="center">                        
                            <asp:HyperLink runat="server" style="width: 36px;height: 24px;" class="fa fa-edit btn btn-success" ID="lnkModificar"></asp:HyperLink>
                        </td>
                          
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
                                  
                  <tr id="trFooter" align="center" class="trFooter">
                        <td colspan="11" style="background-color: white;">
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
     <script language="javascript" type="text/javascript">
// <![CDATA[



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

             jQuery('#<%=txtFecha.ClientID%>').datepicker({
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

             jQuery('.ui-datepicker-trigger').addClass('cal');

         });
         /* end - create datepicker */

         var pageIndex = 1;
         //inicio - funciones de paginado
         function goPage(idx) {

             var descripcion = "0";
             var tipobusqueda = "0";

             descripcion = "1900-01-01";

             $(".dataItemRow").css("display", "none");
             $(".trFooter").css("display", "none");
             $(".gridLoading").css("display", "");
             pageIndex = idx;
             DoCallBack("cargarGrillas", idx + ':' + descripcion, End_goPage);

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
         function btnNuevo_onclick() {

         }

         function lnkNuevo_onClick(_url) {
             window.location.href = _url;
         }


         function lnkNuevo_onClick(_url) {
             window.location.href = _url;
         }

         function lnkSalir_onClick(_url) {
             window.location.href = _url;
         }

// ]]>
    </script>
</asp:Content>

