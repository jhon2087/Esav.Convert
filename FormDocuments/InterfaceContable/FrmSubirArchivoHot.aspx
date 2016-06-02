<%@ Page Title="" Language="C#" MasterPageFile="~/FormDocuments/MainConvert.master" AutoEventWireup="true" CodeFile="FrmSubirArchivoHot.aspx.cs" Inherits="FormDocuments_InterfaceContable_FrmSubirArchivoHot" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cpCuponeraBody" Runat="Server">
<asp:Panel ID="Panel1" runat="server" BorderColor="#595959" BackColor="White" BorderWidth="1px" class="x_panel">
       
          <br />
     <p align="center" style="font-size: large; font-weight: bold">
            <asp:Literal ID="Literal1" runat="server" Text="PROCESAR ARCHIVOS PARA REPORTE CASS"></asp:Literal>
        </p>
      
       
    <table style="width:100%; height: 102px;" >
            <tr>
                <td class="style6">
                    </td>
                <td class="style7">
                    </td>
                <td class="style8">
                    </td>
            </tr>
            <tr>
                <td style="   width: 230px;" align="right">

                 
                    Cargar documento hot:</td>

                <td style="width: 428px;">
                <div style="position:relative;">
		                <a class='btn btn-primary' href='javascript:;' style="background-color: #FFFFFF;border-color: #989898;height: 24px;padding-left: 0px;padding-top: 0px;">
			              <asp:FileUpload ID="FileUpload1" runat="server" ForeColor="White" 
                             />
		                </a>
		                &nbsp;
		                <span class='label label-info' id="upload-file-info"></span>
	            </div>
                   
                </td>



                <td>
                    <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-success" style="width: 130px;"
                        onclick="btnAceptar_Click" Text="Grabar" />
                </td>
               
            </tr>
            <tr>
                <td style="   width: 230px;" align="right">
                    Cargar Documento Cass :</td>
                <td style="width: 428px;" align="left">
                 <div style="position:relative;">
		                <a class='btn btn-primary' href='javascript:;' style="background-color: #FFFFFF;border-color: #989898;height: 24px;padding-left: 0px;padding-top: 0px;">
			                <asp:FileUpload ID="FileUpload2" runat="server" ForeColor="White" />
		                </a>
		                &nbsp;
		                <span class='label label-info' id="upload-file-info"></span>
	            </div>
                 
                </td>
                <td>
                    <asp:Button ID="btnAceptarCass" runat="server" CssClass="btn btn-success" style="width: 130px;"
                        onclick="btnAceptarCass_Click" Text="Grabar"  />
                </td>
            </tr>
            <tr>
                <td style="   width: 230px;">
                    &nbsp;</td>
                <td align="center" style="width: 428px;">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
          <asp:Literal ID="ltrtablacabecera" runat="server"></asp:Literal>
          <br />
    <br />
    <p align="center" style="font-weight: bold">
    <asp:Literal ID="ltrtabla" runat="server"></asp:Literal>
    </p>
          <br />
           <p align="center" style="font-weight: bold">
    <asp:Literal ID="ltrtablacass" runat="server"></asp:Literal>
    </p>
          <br />
            <asp:Literal ID="ltrcassdescripcion" runat="server"></asp:Literal>
          <br />
          <br />

    </asp:Panel>
</asp:Content>

