<%@ Page Title="" Language="C#" MasterPageFile="~/FormDocuments/MainConvert.master" AutoEventWireup="true" CodeFile="FrmReporteApiValidador.aspx.cs" Inherits="FormDocuments_Reportes_FrmReporteApiValidador" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cpCuponeraBody" Runat="Server">
<div class="body">
<asp:HiddenField ID="hdemisor" runat="server" />

    <asp:HiddenField ID="hdApi" runat="server" />
 
<p align="center" style="font-size: 22px;color: #6E93BF;font-weight: bolder;padding-top: 37px;">
        <asp:Literal ID="Literal2" runat="server" Text="Reporte de Archivos Enviados por el Api"></asp:Literal>
    </p> <br /><br /><br /><br />

    <table align="center">

        <tr>
            <td align="center" style="font-weight: bold;font-size: 15px;color: #6E93BF;">
                SEDE&nbsp;&nbsp;
&nbsp;</td>
            <td>
            <div class="col-md-10 col-sm-9 col-xs-12">
                <asp:DropDownList ID="DDLsede" runat="server" class="form-control" >
                </asp:DropDownList> &nbsp;&nbsp;</div>
            </td>
            <td align="center" style="font-weight: bold;font-size: 15px;color: #6E93BF;">
                SERIE&nbsp;&nbsp;
            </td>
            <td>   <div class="col-md-13 col-sm-9 col-xs-12" style="width: 140px;">
                <asp:DropDownList ID="DDLserie" runat="server" class="form-control">
                </asp:DropDownList>&nbsp;&nbsp;</div>
            </td>
            <td align="center" style="font-weight: bold;font-size: 15px;color: #6E93BF;padding-left: 36px;">
                FECHA&nbsp;&nbsp;
            </td>
            <div class="col-md-7 col-sm-9 col-xs-12">
            <td>
                <input id="txtFechabusqueda" name="txtFechabusqueda" runat="server" type="text" style="width: 115px;     margin-top: -13px;" class="form-control" />


            </td></div>
        </tr>
        <tr>
        <td colspan="6">
        &nbsp;&nbsp;
        </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Button id="btnActualizar" type="button" runat="server" class="btn btn-primary" Text="Actualizar"
                    onclick="btnActualizar_Click" />
            </td>
            <td colspan="3" align="center">
                <asp:Button id="btnrepgeneral" type="button"  runat="server"  class="btn btn-success" Text="Export Reporte General" onclick="btnrepgeneral_Click"/> <br />
                   
            </td>
        </tr>
    </table>
    <br /><br /><br /><br /><br /><br /><br /><br /><br />
                <div id="tablaperiodo" class="tablaperiodo">
                                                           
                    <h5 style="color:Black;width:100%;text-align:center;" >REPORTE GENERAL DE ARCHIVOS RECIBIDOS</h5>
                   
                    <asp:Literal ID="ltrtablatotales" runat="server"></asp:Literal> <br /> <br />
                     <h5 style="color:Black;width:100%;text-align:center;">REPORTE DE SALTOS GENERADOS POR EL AUDITADOR</h5>

                   <asp:Literal ID="ltrtablatotales2" runat="server"></asp:Literal><br /> <br />

                   <h5 style="color:Black;width:100%;text-align:center;">REPORTE DE DOCUMENTOS POR REGULARIZAR</h5>

                    <asp:Literal ID="ltrtablatotales5" runat="server"></asp:Literal><br /> <br />

                    <h5 style="color:Black;width:100%;text-align:center;">LISTA DE LOS ERRORES GENERADOS POR EL AUDITADOR</h5>

                    <asp:Literal ID="ltrtablatotales4" runat="server"></asp:Literal>
                </div>
       
                   
            
 <div>
   
                	
        <br />
        <br />
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td align="center">&nbsp;</td>
                    <td align="center">
                  &nbsp; </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    <br />
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

    });
    /* end - create datepicker */


    jQuery(document).ready(function () {
        jQuery('#<%=txtFechabusqueda.ClientID%>').datepicker({

            minDate: '-3M',
            maxDate: '-0D',
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

    var pageIndex = 1;
    //inicio - funciones de paginado
    function goPage(idx) {
        var _emisor = $("#" + '<%= hdemisor.ClientID %>').val();
        //var _API = $("#" + '<%= hdApi.ClientID %>').val();
        var _fecha = $("#" + '<%= txtFechabusqueda.ClientID %>').val();

        $(".dataItemRow").css("display", "none");
        //$(".trFooter").css("display", "none");
        $(".gridLoading").css("display", "");
        pageIndex = idx;
        DoCallBack("cargarGrilla", _emisor + ':' + _fecha, End_goPage);

    }

    function End_goPage(_arg) {
        var mData = _arg.split(":::");
        if (mData[0] == "-1") {
            alert(mData[1]);
            return;
        }

        $(".dataItemRow").css("display", "");
        //        $(".trFooter").css("display", "");
        $(".gridLoading").css("display", "none");

        document.getElementById("tablaperiodo").innerHTML = mData[1];

    }

   
    </script>
</asp:Content>

