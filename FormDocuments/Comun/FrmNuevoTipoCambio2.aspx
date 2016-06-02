<%@ Page Title="" Language="C#" MasterPageFile="~/FormDocuments/MainConvert.master" AutoEventWireup="true" CodeFile="FrmNuevoTipoCambio.aspx.cs" Inherits="FormDocuments_Comun_FrmNuevoTipoCambio" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cpCuponeraBody" Runat="Server">


<asp:Panel ID="Panel1" runat="server" BorderColor="#595959" BackColor="White" BorderWidth="1px" class="body">

  <br />
     <p align="center" style="font-size: large; font-weight: bold">
            <asp:Literal ID="Literal1" runat="server" Text="REGISTRO DE TIPO DE CAMBIO"></asp:Literal>
        </p>


  <fieldset>
       <legend><span id="lblTitle">
          
           Datos Generales</span></legend>
       <div >
          
            <table style="width:100%;">
                <tr>
                    <td align="right" style="   width: 154px;" >Seleccione Fecha :</td>
                    <td style="    width: 221px;">
                       <input name="txtFecha" maxlength="25" id="txtFecha" 
                       type="text" runat="server" style="width:150px;"/><span class="req">*</span>
                    </td>
                    <td style=" width: 21px;">
                       </td>
                    <td style="   width: 421px;">
                        &nbsp;</td>
                    <td align="left" style="width:150px;">
                        &nbsp;</td>
                  </tr>
                <tr>
                    <td align="right" style="   width: 154px;">
                        <label>
                        Monto :</label></td>
                    <td style="    width: 221px;" >
                        <input name="txtMonto" maxlength="25" id="txtMonto" 
                       type="text" runat="server" style="width:150px;"/>
        </td>
                    <td style="width: 21px;">
                        &nbsp;</td>
                    <td>
                        <div id="msgEr1" style="display:none;">
                            <img id="imgError1" runat="server" alt="" 
                                src="../../gogdn/library/img/edtError.png" />
                        </div>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right"style="   width: 154px;" >
                        &nbsp;</td>
                    <td style="    width: 221px;" >
                      
                        &nbsp;</td>
                              <td style="width: 21px;">
                                    &nbsp;</td>
                                <td>
                                    <div ID="msgEr4" style="display:none;">
                                        <img id="imgError4" runat="server" alt="" 
                                src="../../gogdn/library/img/edtError.png" />
                                    </div>
                    </td>

                    <td>
                        &nbsp;</td>

                </tr>
            </table>
           
       </div>
      </fieldset>
      <br />
   <p align="center">
     <input 
                 id="BtnGrabar" type="button"  runat="server"  class="aspRubenButton" 
                 onclick="BtnGrabar_onclick()"   />

           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

           <asp:Button ID="btnSalir" runat="server" CssClass="aspRubenSalir" 
           TabIndex="4"  PostBackUrl="~/FormDocuments/Comun/FrmInicio.aspx"
           Width="149px"  Height="31px" />
          </p>
 </asp:Panel>

  <script language="javascript" type="text/javascript">

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



      });
      /* end - create datepicker */


      function BtnGrabar_onclick() {

          if (Validate()) {

              var fecha = $("#" + '<%= txtFecha.ClientID %>').val();
              var monto = $("#" + '<%= txtMonto.ClientID %>').val();

              DoCallBack("InsertarTipoCambio", fecha + ':' + monto, End_Insertar);
          }
      }

      function End_Insertar(_args) {
          var mData = _args.split(":::");
          if (mData[0] == "-1") {
              alert(mData[1]);
              return;
          }
          else {
              alert("Registro Grabado Correctamente");
              Limpiar();

          }

      }


      function Validate() {
          var isValid = true;

          if ($("#<%=txtFecha.ClientID%>").val() == "") {
              alert("Seleccione Fecha");
              $("#txtFecha").focus();
              return false;
          }
          if ($("#<%=txtMonto.ClientID%>").val() == "") {
              alert("Ingrese Monto");
              $("#txtMonto").focus();
              return false;
          }

          if (isValid) {
              showLoader(); $("#msgEr").hide();
              return true;
          }
      }


      function Limpiar() {

          $("#<%=txtFecha.ClientID%>").val("");
          $("#<%=txtMonto.ClientID%>").val("");


      }


         </script>
</asp:Content>

