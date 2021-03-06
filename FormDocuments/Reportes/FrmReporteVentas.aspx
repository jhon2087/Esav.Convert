﻿<%@ Page Title="" Language="C#" MasterPageFile="~/FormDocuments/MainConvert.master" AutoEventWireup="true" CodeFile="FrmReporteVentas.aspx.cs" Inherits="FormDocuments_Reportes_FrmReporteVentas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cpCuponeraBody" Runat="Server">
    <div class="body">
     <p align="center" style="font-size: 22px;color: #6E93BF;font-weight: bolder;padding-top: 37px;">
        <asp:Literal ID="Literal3" runat="server" Text=" CONSOLIDADO DE VENTAS"></asp:Literal>
    </p><br/><br/>
    <br/>
                <table width="100%" cellspacing="5" border="0">
                 
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                    <td align="right" style="  width: 290px;     height: 44px;">
                        <asp:Literal runat="server" ID="ltNombre" Text="Fecha Reporte"></asp:Literal> :
                    </td>
                    <td style="height: 44px;width: 126px;">
                        <asp:TextBox ID="txtFechaInicial" onkeypress="return onlyNumbersAndSlash(event);" runat="server" Width="70px" MaxLength="10"></asp:TextBox>

                    </td>
                    
                    <td align="right" style="width: 30px;">Serie:&nbsp;</td>
            <td align="right" style="width: 40px;">
                <asp:DropDownList ID="DDLserie" runat="server"></asp:DropDownList>
            </td>
                   
                    <td align="left" style="  height: 44px;">
                        
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="padding-top:5px; width: 92px;"></td>
                    <td style="padding-top:5px;" colspan="3" align="right">
                         <input  id="btnBuscar"  class="btn btn-success"  value="BUSCAR" onclick=" btnBuscar_onclick()" Height="34px" Width="160px" />
                       </td>
                    <td style="padding-top:5px;" colspan="2" align="left">
                                        
                        <input id="btnSalir" runat="server"  class="btn btn-danger" value="SALIR" onclick="return btnSalir_onclick()" Height="34px" Width="160px"" 
         
          /></td>
                    <td style="padding-top:5px;" colspan="2" align="left">
                                        
           <asp:Button ID="btngenera" runat="server"  class="btn btn-warning" Text="GENERAR" onclick="btngenera_Click" 
                            Height="34px" Width="160px" />
                    </td>
                </tr>
                </table><br />
                 
                   <div id ="divGridCarrito">
        <asp:Repeater runat="server" ID="rptListado" 
              onitemdatabound="rptListado_ItemDataBound" 
              onitemcommand="rptListado_ItemCommand">
        <HeaderTemplate>
            <table class="gridContainer"  width="100%">
                <tr  >       
                 <th class="headerGlass" width="15%"><b><asp:Literal runat="server" id="Li77" Text="<%$Resources:Label, lblFecha %>"></asp:Literal></b></th>                     
                 <th class="headerGlass" width="15%"><b><asp:Literal runat="server" id="Lil78" Text="<%$Resources:Label, lblTipo %>"></asp:Literal></b></th>                     
                 <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Lil71" Text="<%$Resources:Label, lblSerie %>"></asp:Literal></b></th>
                 <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Literal2" Text="<%$Resources:Label, lblRango %>"></asp:Literal></b></th>
                 <th class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Ll74" Text="<%$Resources:Label, lblMoneda %>"></asp:Literal></b></th>      
                 <th class="headerGlass"  width="5%"> <b><asp:Literal runat="server" id="Lit773" Text="<%$Resources:Label, lblTotalimportesoles %>"></asp:Literal></b></th>
                 <th class="headerGlass"  width="5%"> <b><asp:Literal runat="server" id="Literal1" Text="<%$Resources:Label, lblTotalimportedolares %>"></asp:Literal></b></th>
                  </tr>
        </HeaderTemplate>                
        <ItemTemplate>
        
                <tr  class="dataItemRow">
                                        
                        <td class="dataItem" align="center"><%# Eval("FechaEmisionREP")%></td>
                        <td class="dataItem" align="center"><%# Eval("TipoDocumento")%></td>
                        <td class="dataItem"  align="center"><%# Eval("Serie")%></td>
                        <td  class="dataItem"  align="center"><%# Eval("Rango")%></td>
                        <td  class="dataItem" align="center"><%# Eval("moneda")%></td>
                        <td  class="dataItem"  align="center"><%# Eval("ImporteTotal")%></td>
                        <td  class="dataItem"  align="center"><%# Eval("ImporteTotaldolares")%></td>
                        
                                       
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
    
    <p>
    </p>
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



    });
    /* end - create datepicker */


    var pageIndex = 1;
    //inicio - funciones de paginado
    function goPage(idx) {
        var fecha = $("#" + '<%= txtFechaInicial.ClientID %>').val();
        $(".dataItemRow").css("display", "none");
        $(".trFooter").css("display", "none");
        $(".gridLoading").css("display", "");

        pageIndex = idx;

        DoCallBack("cargarGrillas", fecha + ":"+idx, End_goPage);

    }


    function End_goPage(arg) {
        var mData = arg.split(":::");
        $(".dataItemRow").css("display", "");
        //$(".trDetail").css("display", "");
        $(".trFooter").css("display", "");
        $(".gridLoading").css("display", "none");

        document.getElementById("divGridCarrito").innerHTML = mData[1];
    }
    function btnBuscar_onclick() {

        var fecha = $("#" + '<%= txtFechaInicial.ClientID %>').val();
        var serie = $("#" + '<%= DDLserie.ClientID %>').val();
        $(".dataItemRow").css("display", "none");
        $(".trFooter").css("display", "none");
        $(".gridLoading").css("display", "");

        DoCallBack("cargarGrillas", fecha + ":" + 1 + ":" + serie, End_Buscar);

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
   function lnkCancelar_onClick(_url) {
           window.location.href = _url;
       }

       function btnSalir_onclick() {

       }
       
</script>

</asp:Content>



