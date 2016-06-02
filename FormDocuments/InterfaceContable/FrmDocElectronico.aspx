<%@ Page Title="" Language="C#" MasterPageFile="~/FormDocuments/MainConvert.master" AutoEventWireup="true" CodeFile="FrmDocElectronico.aspx.cs" Inherits="FormDocuments_InterfaceContable_FrmDocElectronico" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cpCuponeraBody" Runat="Server">
   
    <div class="body">
  
        <p align="center" style="font-size: 22px;color: #6E93BF;font-weight: bolder;padding-top: 37px;">
        <asp:Literal ID="Literal3" runat="server" Text="DOCUMENTOS EMITIDOS ELECTRONICAMENTE "></asp:Literal>
    </p>
    <br/>
                <table width="100%" cellspacing="5" border="0">
                  
                <tr>
                    <td align="right" style="  width: 92px;     height: 44px;">
                        <asp:Literal runat="server" ID="ltNombre" Text="Fecha Inicial"></asp:Literal> :
                    </td>
                    <td style="height: 44px;width: 126px;">
                        <asp:TextBox ID="txtFechaInicial" onkeypress="return onlyNumbersAndSlash(event);" runat="server" Width="70px" MaxLength="10"></asp:TextBox>

                    </td>
                    <td align="right" style="width: 89px;">
                        <asp:Literal runat="server" ID="Literal2" Text="Fecha Final"></asp:Literal> :   &nbsp;</td>
                    <td style="height: 74px; width: 110px;">
                        <asp:TextBox ID="txtFechaFinal" onkeypress="return onlyNumbersAndSlash(event);" runat="server" Width="70px" MaxLength="10"></asp:TextBox>

                    </td>
                    <td align="right" style="width: 30px;">Serie:&nbsp;</td>
                    <div class="col-md-7 col-sm-9 col-xs-12">
            <td align="right" style="width: 40px;">

                <asp:DropDownList ID="DDLserie" runat="server" class="form-control" ></asp:DropDownList>
            </td></div>
                    <td style=" width: 120px;" align="right"> 
                        
                        <asp:Literal runat="server" ID="ltrtipodoc" Text="Tipo de Documento"></asp:Literal> 
                        :</td>
                        
                            <td>      
                              <div class="col-md-7 col-sm-9 col-xs-12">                   
                        <select id="ddltipodocumento" name="ddltipodocumento" runat="server" class="form-control">
                            <option value="00">Seleccione</option>
                            <option value="FA">Factura</option>
                            <option value="BO">Boleta de Venta</option>
                            <option value="NC">Nota de Crédito</option>
                            <option value="ND">Nota de Débito</option>
                            <option value="DC">Documento de Cobranza</option>
                            <option value="TD">Todos los Documentos</option>
                        </select></div></td>
                    <td align="left" style="  height: 44px;">
                        
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="padding-top:5px; width: 92px;"></td>
                    <td style="padding-top:5px;" colspan="3" align="right">
                         <input type="button" id="btnBuscar"  value="BUSCAR"  class="btn btn-success" onclick="btnBuscar_onclick()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                       &nbsp;&nbsp;&nbsp;</td>
                    <td style="padding-top:5px;" colspan="2" align="left">
                                        
                        <input type="button" id="btnSalir" runat="server"  class="btn btn-danger"  onclick="return btnSalir_onclick()" value="SALIR" style=" padding-left: 70px;padding-right: 70px;" /></td>
                    <td style="padding-top:5px;" colspan="2" align="center">
                                        
           <asp:Button ID="btngenera" runat="server"   type="button"  class="btn btn-warning" Text="Generar" onclick="btngenera_Click"  Height="34px" Width="160px" style="display: none;"/>
                           
              
                    </td>
                </tr>
                <tr>
                    <td style="padding-top:5px;"colspan="10">
                     &nbsp;
                    </td>
                     
                </tr>
                <tr id="descarga" >
                    <td style="padding-top:5px;"colspan="2">
                    </td>
                     <td colspan="5" align="center" style="font-size: 15px;color: red;font-weight: bolder;padding-top: 3px;" height="35px" >
                        <asp:Literal runat="server" ID="Literal4"  Text="click en GENERAR para procesar .xls"></asp:Literal>  &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                        <input runat="server" type="button" id="btndescargar" class="btn btn-success fa fa-download" value="Descarga" onclick="return btnSalir_onclick()" />
                    </td>
                     <td colspan="3">
                    </td>
                </tr>
                </table>
                 <br /><br />
                   <div id ="divGridCarrito">
        <asp:Repeater runat="server" ID="rptListado" 
              onitemdatabound="rptListado_ItemDataBound" 
              onitemcommand="rptListado_ItemCommand">
        <HeaderTemplate>
            <table class="table table-striped responsive-utilities jambo_table dataTable"  width="100%" >
          
                <tr  style="background-color: #3F4458;">       
                 <th style="color:white;" class="headerGlass" width="15%"><b><asp:Literal runat="server" id="Li77" Text="<%$Resources:Label, lblFecha %>"></asp:Literal></b></th>                     
                 <th style="color:white;" class="headerGlass" width="15%"><b><asp:Literal runat="server" id="Lil78" Text="<%$Resources:Label, lblTipo %>"></asp:Literal></b></th>                     
                 <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Lil71" Text="<%$Resources:Label, lblNroDocumento %>"></asp:Literal></b></th>
                 <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Ll74" Text="<%$Resources:Label, lblMoneda %>"></asp:Literal></b></th>      
                 <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Lil79" Text="<%$Resources:Label, lblValorVenta %>"></asp:Literal></b></th>      
                 <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Lil170" Text="<%$Resources:Label, lblTotalIgv %>"></asp:Literal></b></th>      
                 <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Litl711" Text="<%$Resources:Label, lblTotalisc %>"></asp:Literal></b></th>      
                 <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Lal172" Text="<%$Resources:Label, lblTotalOtros %>"></asp:Literal></b></th>      
                  <th style="color:white;" class="headerGlass" width="5%"><b><asp:Literal runat="server" id="Lral75" Text="<%$Resources:Label, lblTotaltotalotrostributos %>"></asp:Literal></b></th>        
                  <th style="color:white;" class="headerGlass"  width="5%"> <b><asp:Literal runat="server" id="Lit773" Text="<%$Resources:Label, lblTotalimportesoles %>"></asp:Literal></b></th>
                 <th style="color:white;" class="headerGlass"  width="5%"> <b><asp:Literal runat="server" id="Literal1" Text="<%$Resources:Label, lblTotalimportedolares %>"></asp:Literal></b></th>
                  </tr>


        </HeaderTemplate>                
        <ItemTemplate>
        
                <tr  class="dataItemRow">
                                        
                        <td class="dataItem" align="center"><%# Eval("FechaEmisionREP")%></td>
                        <td class="dataItem"  align="center"><%# Eval("TipoDocumento")%></td>
                        <td  class="dataItem"  align="center"><%# Eval("NroDocumento")%></td>
                        <td  class="dataItem" align="center"><%# Eval("moneda")%></td>
                         <td  class="dataItem" align="center"><%# Eval("ValorVenta")%></td>
                          
                         <td  class="dataItem" align="center"><%# Eval("TotalIgv")%></td>
                         <td class="dataItem" align="center"><%# Eval("TotalIsc")%></td>
                           
                           <td class="dataItem" align="center"><%# Eval("TotalOtros")%></td>
                        <td class="dataItem"  align="center"><%# Eval("TotalOtrostributos")%></td>
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

            $("#btnBuscar").click(function () {
                $("#ctl00_ctl00_cpMainBody_cpCuponeraBody_btngenera").css("display", "block");
            });

          
            

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
            var serie = $("#" + '<%= DDLserie.ClientID %>').val();
            var fecha = $("#" + '<%= txtFechaInicial.ClientID %>').val();
            var fechaFinal = $("#" + '<%= txtFechaFinal.ClientID %>').val();
            var tipodoc = $("#" + '<%= ddltipodocumento.ClientID %>').val();
            $(".dataItemRow").css("display", "none");
            $(".trFooter").css("display", "none");
            $(".gridLoading").css("display", "");

            pageIndex = idx;

            DoCallBack("cargarGrillas", fecha + ':' + fechaFinal + ':' + tipodoc + ':' + idx + ':' + serie, End_goPage);

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


            var serie = $("#" + '<%= DDLserie.ClientID %>').val();
            var fecha = $("#" + '<%= txtFechaInicial.ClientID %>').val();
            var fechaFinal = $("#" + '<%= txtFechaFinal.ClientID %>').val();
            var tipodoc = $("#" + '<%= ddltipodocumento.ClientID %>').val();
            $(".dataItemRow").css("display", "none");
            $(".trFooter").css("display", "none");
            $(".gridLoading").css("display", "");
          
            DoCallBack("cargarGrillas", fecha + ':' + fechaFinal + ':' + tipodoc + ':' + 1+':' + serie, End_Buscar);

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

       function lnkDescargar_onClick(_url) {


           window.location.href = _url;

       }
function btnSalir_onclick() {

       }
       
</script>

</asp:Content>



