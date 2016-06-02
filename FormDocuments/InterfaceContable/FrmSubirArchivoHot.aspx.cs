using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

public partial class FormDocuments_InterfaceContable_FrmSubirArchivoHot : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        // Specify the path on the server to
        // save the uploaded file to.
        string savePath = @"C:\hotcargado\";


        if (FileUpload1.HasFile)
        {
            // Get the size in bytes of the file to upload.
            int fileSize = FileUpload1.PostedFile.ContentLength;

            // Allow only files less than 2,100,000 bytes (approximately 2 MB) to be uploaded.
            if (fileSize < 2100000)
            {

                // Append the name of the uploaded file to the path.
                savePath += Server.HtmlEncode(FileUpload1.FileName);


                FileUpload1.SaveAs(savePath);



                int counter = 0;
                string line;
                string line0;
                string dato = "";

                decimal lineCount = File.ReadAllLines(string.Format(@"C:\hotcargado\{0}", FileUpload1.FileName)).Count();
                decimal catidaddecimal = lineCount / 100;
                decimal cantinsertar = Math.Truncate(Convert.ToDecimal(lineCount / 100));

                decimal cantidadinsertarango = catidaddecimal - cantinsertar;
                decimal rangofinal = cantidadinsertarango * 100;


                System.IO.StreamReader file =
                    new System.IO.StreamReader(string.Format(@"C:\hotcargado\{0}", FileUpload1.FileName));



                string tablacabecera = "<table  class=gridContainer  width=100% > <tr  >  <th > <b>RECORDID</b></th>";
                tablacabecera = tablacabecera + " <th class=headerGlass> <b>CASSAREACODE</b></th>";
                tablacabecera = tablacabecera + " <th class=headerGlass> <b>AIRLINEPREFIX</b></th>";
                tablacabecera = tablacabecera + " <th class=headerGlass> <b>FECHAINICIOPERIODO</b></th>";
                tablacabecera = tablacabecera + " <th class=headerGlass> <b>FECHAFINPERIODO</b></th>";
                tablacabecera = tablacabecera + " <th class=headerGlass> <b>FECHAFAC</b></th>";
                tablacabecera = tablacabecera + " <th class=headerGlass> <b>FILENUMBER</b></th>";
                tablacabecera = tablacabecera + " <th class=headerGlass> <b>MONEDA</b></th>";



                StringBuilder XmlCabecera = new StringBuilder();


                line = file.ReadLine();
                if (counter == 0)
                {
                    XmlCabecera.Append("<Root>");
                    XmlCabecera.Append("<BodyCab>");
                    XmlCabecera.AppendLine(String.Format("<RECORDID>{0}</RECORDID>", line.Substring(0, 3)));
                    XmlCabecera.AppendLine(String.Format("<CASSAREACODE>{0}</CASSAREACODE>", line.Substring(3, 2)));
                    XmlCabecera.AppendLine(String.Format("<AIRLINEPREFIX>{0}</AIRLINEPREFIX>", line.Substring(5, 3)));
                    XmlCabecera.AppendLine(String.Format("<FECHAINICIOPERIODO>{0}</FECHAINICIOPERIODO>", line.Substring(8, 6)));
                    XmlCabecera.AppendLine(String.Format("<FECHAFINPERIODO>{0}</FECHAFINPERIODO>", line.Substring(14, 6)));
                    XmlCabecera.AppendLine(String.Format("<FECHAFAC>{0}</FECHAFAC>", line.Substring(20, 6)));
                    XmlCabecera.AppendLine(String.Format("<FILENUMBER>{0}</FILENUMBER>", line.Substring(26, 2)));
                    XmlCabecera.AppendLine(String.Format("<MONEDA>{0}</MONEDA>", line.Substring(28, 3)));
                    XmlCabecera.Append("</BodyCab>");
                    XmlCabecera.Append("</Root>");

                    tablacabecera = tablacabecera + "<tr class=dataItemRow>";
                    tablacabecera = tablacabecera + "<td class=dataItem align=center >" + line.Substring(0, 3) + "  </td>";// RECORDID
                    tablacabecera = tablacabecera + "<td class=dataItem align=center >" + line.Substring(3, 2) + "  </td>";// CASSAREACODE
                    tablacabecera = tablacabecera + "<td class=dataItem align=center >" + line.Substring(5, 3) + "  </td>";// AIRLINE PREFIX
                    tablacabecera = tablacabecera + "<td class=dataItem align=center >" + line.Substring(8, 6) + "  </td>";// FECHA INICIO PERIODO
                    tablacabecera = tablacabecera + "<td class=dataItem align=center >" + line.Substring(14, 6) + "  </td>";// FECHA FIN PERIODO
                    tablacabecera = tablacabecera + "<td class=dataItem align=center >" + line.Substring(20, 6) + "  </td>"; //FECHA DE FACTURACION
                    tablacabecera = tablacabecera + "<td class=dataItem align=center>" + line.Substring(26, 2) + "  </td>"; //filenumber
                    tablacabecera = tablacabecera + "<td class=dataItem align=center>" + line.Substring(28, 3) + "  </td>"; //MONEDA

                    tablacabecera = tablacabecera + " </tr>";
                    counter++;
                }

                tablacabecera = tablacabecera + " </table>";

                wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
                ClGenerarCass objgenerarcass = new ClGenerarCass();
                string respuestacabecera = objgenerarcass.registrarCabeceraHotDA(CodigoEmisor, UsuarioId, UsuarioIP, XmlCabecera.ToString());
                string respuestahot = "";



                int iterancciones = 1;
                if (cantinsertar > 0)
                {

                    iterancciones = 100;

                }

                string linea = "";
                //for (int k = 0; k < cantinsertar; k++)
                //{
                String XmlCuerpo = "";
                int contadorcien = 0;
                //   XmlCuerpo.Append("<RootCuerpo>");
                while ((line = file.ReadLine()) != null)
                {
                    // line = file.ReadLine();



                    if (counter > 0 && line.Substring(0, 3) == "AWM" && line.Substring(0, 3) != "TTT")
                    {
                        XmlCuerpo = XmlCuerpo + ("<Cuerpo>");

                        //   line.Substring(4, 24);
                        // dato = dato + line.Substring(4, 24) + "  <br />";
                        XmlCuerpo = XmlCuerpo + (String.Format("<RECORDID>{0}</RECORDID>", line.Substring(0, 3)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<BRANCHID>{0}</BRANCHID>", line.Substring(3, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<VATID>{0}</VATID>", line.Substring(4, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AIRPREF>{0}</AIRPREF>", line.Substring(5, 3)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AWBSERIALN>{0}</AWBSERIALN>", line.Substring(8, 8)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AWMNROMODCHECK>{0}</AWMNROMODCHECK>", line.Substring(16, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<ORIGIN>{0}</ORIGIN>", line.Substring(18, 3)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AGENCODE>{0}</AGENCODE>", line.Substring(21, 11)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AWBUSEIND>{0}</AWBUSEIND>", line.Substring(32, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<LATEIND>{0}</LATEIND>", line.Substring(33, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<DESTI>{0}</DESTI>", line.Substring(36, 3)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<FECHA>{0}</FECHA>", line.Substring(39, 6)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<GROSS>{0}</GROSS>", line.Substring(45, 7)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<WEIGHTIND>{0}</WEIGHTIND>", line.Substring(52, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<MONEDA>{0}</MONEDA>", line.Substring(53, 3)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<WEIGHTCHARGWPP>{0}</WEIGHTCHARGWPP>", line.Substring(56, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<VALUATIONCHARGEPP>{0}</VALUATIONCHARGEPP>", line.Substring(68, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<CHANGEDUECARRIER>{0}</CHANGEDUECARRIER>", line.Substring(80, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<CHANGEDUEAGENT>{0}</CHANGEDUEAGENT>", line.Substring(92, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<WEIGHTCHARGWCC>{0}</WEIGHTCHARGWCC>", line.Substring(104, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<VALUATIONCHARGECC>{0}</VALUATIONCHARGECC>", line.Substring(116, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<OTHERCHANGEDUECARRIERCC>{0}</OTHERCHANGEDUECARRIERCC>", line.Substring(128, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<OTHERCHANGEDUEAGENTCC>{0}</OTHERCHANGEDUEAGENTCC>", line.Substring(140, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<COMISSIONPERCENTAGE>{0}</COMISSIONPERCENTAGE>", line.Substring(152, 4)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<COMISSION>{0}</COMISSION>", line.Substring(156, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<TAXDUEAIRLINEIND>{0}</TAXDUEAIRLINEIND>", line.Substring(168, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AGENTREFERENCEDATA>{0}</AGENTREFERENCEDATA>", line.Substring(169, 14)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<DATEAWBACCEPTANCE>{0}</DATEAWBACCEPTANCE>", line.Substring(193, 6)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<RATEOFEXCHANGE>{0}</RATEOFEXCHANGE>", line.Substring(199, 11)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<DISCOUNT>{0}</DISCOUNT>", line.Substring(210, 12)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<TAXDUEAIRLINEINVOICE>{0}</TAXDUEAIRLINEINVOICE>", line.Substring(222, 8)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<TAXDUEAGENTCOMMISSION>{0}</TAXDUEAGENTCOMMISSION>", line.Substring(230, 8)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<VATONCOMMISION>{0}</VATONCOMMISION>", "0"));
                        XmlCuerpo = XmlCuerpo + (String.Format("<NUMBERDOC>{0}</NUMBERDOC>", "0"));
                        XmlCuerpo = XmlCuerpo + ("</Cuerpo>");
                    }

                    else
                        if (counter > 0 && (line.Substring(0, 3).Trim() == "CCR" || line.Substring(0, 3).Trim() == "DCR") && line.Substring(0, 3) != "TTT")
                        {

                            XmlCuerpo = XmlCuerpo + ("<Cuerpo>");
                            XmlCuerpo = XmlCuerpo + (String.Format("<RECORDID>{0}</RECORDID>", line.Substring(0, 3)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<BRANCHID>{0}</BRANCHID>", line.Substring(3, 1)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<VATID>{0}</VATID>", line.Substring(4, 1)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AIRPREF>{0}</AIRPREF>", line.Substring(5, 3)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AWBSERIALN>{0}</AWBSERIALN>", line.Substring(8, 8)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AWMNROMODCHECK>{0}</AWMNROMODCHECK>", line.Substring(16, 1)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<ORIGIN>{0}</ORIGIN>", line.Substring(18, 3)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AGENCODE>{0}</AGENCODE>", line.Substring(21, 11)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AWBUSEIND>{0}</AWBUSEIND>", line.Substring(32, 6)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<LATEIND>{0}</LATEIND>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<DESTI>{0}</DESTI>", line.Substring(180, 3)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<FECHA>{0}</FECHA>", line.Substring(52, 6)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<GROSS>{0}</GROSS>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<WEIGHTIND>{0}</WEIGHTIND>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<MONEDA>{0}</MONEDA>", line.Substring(38, 3)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<WEIGHTCHARGWPP>{0}</WEIGHTCHARGWPP>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<VALUATIONCHARGEPP>{0}</VALUATIONCHARGEPP>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<CHANGEDUECARRIER>{0}</CHANGEDUECARRIER>", line.Substring(111, 12)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<CHANGEDUEAGENT>{0}</CHANGEDUEAGENT>", line.Substring(98, 12)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<WEIGHTCHARGWCC>{0}</WEIGHTCHARGWCC>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<VALUATIONCHARGECC>{0}</VALUATIONCHARGECC>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<OTHERCHANGEDUECARRIERCC>{0}</OTHERCHANGEDUECARRIERCC>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<OTHERCHANGEDUEAGENTCC>{0}</OTHERCHANGEDUEAGENTCC>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<COMISSIONPERCENTAGE>{0}</COMISSIONPERCENTAGE>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<COMISSION>{0}</COMISSION>", line.Substring(135, 12)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<TAXDUEAIRLINEIND>{0}</TAXDUEAIRLINEIND>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AGENTREFERENCEDATA>{0}</AGENTREFERENCEDATA>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<DATEAWBACCEPTANCE>{0}</DATEAWBACCEPTANCE>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<RATEOFEXCHANGE>{0}</RATEOFEXCHANGE>", line.Substring(41, 11)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<DISCOUNT>{0}</DISCOUNT>", line.Substring(159, 12)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<TAXDUEAIRLINEINVOICE>{0}</TAXDUEAIRLINEINVOICE>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<TAXDUEAGENTCOMMISSION>{0}</TAXDUEAGENTCOMMISSION>", 1));
                            XmlCuerpo = XmlCuerpo + (String.Format("<VATONCOMMISION>{0}</VATONCOMMISION>", line.Substring(147, 12)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<NUMBERDOC>{0}</NUMBERDOC>", line.Substring(32, 6)));

                            XmlCuerpo = XmlCuerpo + ("</Cuerpo>");

                        }

                        else
                            if (counter > 0 && (line.Substring(0, 3).Trim() == "CCO" || line.Substring(0, 3).Trim() == "DCO") && line.Substring(0, 3) != "TTT")
                            {

                                XmlCuerpo = XmlCuerpo + ("<Cuerpo>");
                                XmlCuerpo = XmlCuerpo + (String.Format("<RECORDID>{0}</RECORDID>", line.Substring(0, 3)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<BRANCHID>{0}</BRANCHID>", line.Substring(3, 1)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<VATID>{0}</VATID>", line.Substring(4, 1)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AIRPREF>{0}</AIRPREF>", line.Substring(5, 3)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AWBSERIALN>{0}</AWBSERIALN>", line.Substring(8, 8)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AWMNROMODCHECK>{0}</AWMNROMODCHECK>", line.Substring(16, 1)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<ORIGIN>{0}</ORIGIN>", line.Substring(18, 3)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AGENCODE>{0}</AGENCODE>", line.Substring(21, 11)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AWBUSEIND>{0}</AWBUSEIND>", line.Substring(32, 6)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<LATEIND>{0}</LATEIND>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<DESTI>{0}</DESTI>", line.Substring(180, 3)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<FECHA>{0}</FECHA>", line.Substring(52, 6)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<GROSS>{0}</GROSS>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<WEIGHTIND>{0}</WEIGHTIND>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<MONEDA>{0}</MONEDA>", line.Substring(38, 3)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<WEIGHTCHARGWPP>{0}</WEIGHTCHARGWPP>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<VALUATIONCHARGEPP>{0}</VALUATIONCHARGEPP>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<CHANGEDUECARRIER>{0}</CHANGEDUECARRIER>", line.Substring(111, 12)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<CHANGEDUEAGENT>{0}</CHANGEDUEAGENT>", line.Substring(98, 12)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<WEIGHTCHARGWCC>{0}</WEIGHTCHARGWCC>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<VALUATIONCHARGECC>{0}</VALUATIONCHARGECC>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<OTHERCHANGEDUECARRIERCC>{0}</OTHERCHANGEDUECARRIERCC>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<OTHERCHANGEDUEAGENTCC>{0}</OTHERCHANGEDUEAGENTCC>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<COMISSIONPERCENTAGE>{0}</COMISSIONPERCENTAGE>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<COMISSION>{0}</COMISSION>", line.Substring(135, 12)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<TAXDUEAIRLINEIND>{0}</TAXDUEAIRLINEIND>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AGENTREFERENCEDATA>{0}</AGENTREFERENCEDATA>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<DATEAWBACCEPTANCE>{0}</DATEAWBACCEPTANCE>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<RATEOFEXCHANGE>{0}</RATEOFEXCHANGE>", line.Substring(41, 11)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<DISCOUNT>{0}</DISCOUNT>", line.Substring(159, 12)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<TAXDUEAIRLINEINVOICE>{0}</TAXDUEAIRLINEINVOICE>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<TAXDUEAGENTCOMMISSION>{0}</TAXDUEAGENTCOMMISSION>", 1));
                                XmlCuerpo = XmlCuerpo + (String.Format("<VATONCOMMISION>{0}</VATONCOMMISION>", line.Substring(147, 12)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<NUMBERDOC>{0}</NUMBERDOC>", line.Substring(32, 6)));
                                XmlCuerpo = XmlCuerpo + ("</Cuerpo>");

                            }
                    if (counter == 100 && contadorcien < cantinsertar)
                    {
                        contadorcien = contadorcien + 1;
                        string XmlCuerpofinal = "<RootCuerpo>" + XmlCuerpo + "</RootCuerpo>";
                        respuestahot = objgenerarcass.registrarHotDA(Convert.ToInt32(respuestacabecera), UsuarioId, UsuarioIP, XmlCuerpofinal.ToString().Replace("&", "&amp;"));

                        counter = 0;
                        XmlCuerpo = "";

                    }

                    if (counter == rangofinal - 2 && contadorcien == cantinsertar)
                    {

                        string XmlCuerpofinal = "<RootCuerpo>" + XmlCuerpo + "</RootCuerpo>";
                        respuestahot = objgenerarcass.registrarHotDA(Convert.ToInt32(respuestacabecera), UsuarioId, UsuarioIP, XmlCuerpofinal.ToString().Replace("&", "&amp;"));

                        counter = 0;
                        XmlCuerpo = "";

                    }


                    counter++;
                }


                if (Convert.ToInt32(respuestacabecera) > 0)
                {



                    ltrtablacabecera.Text = tablacabecera;
                    ltrtabla.Text = "Son" + "  " + Convert.ToString(lineCount - 2) + "  " + "Registros Ingresados Correctamente";
                }
                else
                    ltrtabla.Text = respuestacabecera;

                file.Close();


            }
            else
            {
                // Notify the user why their file was not uploaded.
                ltrtabla.Text = "El archivo  " + "no puede exceder los 2MB.";
            }
        }
        else
        {
            // Notify the user that a file was not uploaded.
            ltrtabla.Text = "You did not specify a file to upload.";
        }








    }
    protected void btnAceptarCass_Click(object sender, EventArgs e)
    {
        // Specify the path on the server to
        // save the uploaded file to.
        string savePath = @"C:\hotcargado\";


        if (FileUpload2.HasFile)
        {
            // Get the size in bytes of the file to upload.
            int fileSize = FileUpload2.PostedFile.ContentLength;

            // Allow only files less than 2,100,000 bytes (approximately 2 MB) to be uploaded.
            if (fileSize < 2100000)
            {

                // Append the name of the uploaded file to the path.
                savePath += Server.HtmlEncode(FileUpload2.FileName);


                FileUpload2.SaveAs(savePath);



                int counter = 0;
                string line;
                string line0;
                string dato = "";

                decimal lineCount = File.ReadAllLines(string.Format(@"C:\hotcargado\{0}", FileUpload2.FileName)).Count();
                decimal catidaddecimal = lineCount / 100;
                decimal cantinsertar = Math.Truncate(Convert.ToDecimal(lineCount / 100));

                decimal cantidadinsertarango = catidaddecimal - cantinsertar;
                decimal rangofinal = cantidadinsertarango * 100;


                System.IO.StreamReader file =
                    new System.IO.StreamReader(string.Format(@"C:\hotcargado\{0}", FileUpload2.FileName));



                string tablacabeceracass = "<table  class=gridContainer  width=100% > <tr  >  <th > <b>RECORDID</b></th>";
                tablacabeceracass = tablacabeceracass + " <th class=headerGlass> <b>CASSAREACODE</b></th>";
                tablacabeceracass = tablacabeceracass + " <th class=headerGlass> <b>AIRLINEPREFIX</b></th>";
                tablacabeceracass = tablacabeceracass + " <th class=headerGlass> <b>FECHAINICIOPERIODO</b></th>";
                tablacabeceracass = tablacabeceracass + " <th class=headerGlass> <b>FECHAFINPERIODO</b></th>";
                tablacabeceracass = tablacabeceracass + " <th class=headerGlass> <b>FECHAFAC</b></th>";
                tablacabeceracass = tablacabeceracass + " <th class=headerGlass> <b>FILENUMBER</b></th>";

                StringBuilder XmlCabecera = new StringBuilder();

                line = file.ReadLine();
                if (counter == 0)
                {
                    XmlCabecera.Append("<Root>");
                    XmlCabecera.Append("<BodyCab>");
                    XmlCabecera.AppendLine(String.Format("<RECORDID>{0}</RECORDID>", line.Substring(0, 3)));
                    XmlCabecera.AppendLine(String.Format("<CASSAREACODE>{0}</CASSAREACODE>", line.Substring(3, 2)));
                    XmlCabecera.AppendLine(String.Format("<AIRLINEPREFIX>{0}</AIRLINEPREFIX>", line.Substring(16, 3)));
                    XmlCabecera.AppendLine(String.Format("<FECHAINICIOPERIODO>{0}</FECHAINICIOPERIODO>", line.Substring(19, 6)));
                    XmlCabecera.AppendLine(String.Format("<FECHAFINPERIODO>{0}</FECHAFINPERIODO>", line.Substring(25, 6)));
                    XmlCabecera.AppendLine(String.Format("<FECHAFAC>{0}</FECHAFAC>", line.Substring(31, 6)));
                    XmlCabecera.AppendLine(String.Format("<FILENUMBER>{0}</FILENUMBER>", line.Substring(37, 2)));
                    XmlCabecera.Append("</BodyCab>");
                    XmlCabecera.Append("</Root>");
                    tablacabeceracass = tablacabeceracass + "<tr class=dataItemRow>";
                    tablacabeceracass = tablacabeceracass + "<td class=dataItem align=center >" + line.Substring(0, 3) + "  </td>";// RECORDID
                    tablacabeceracass = tablacabeceracass + "<td class=dataItem align=center >" + line.Substring(3, 2) + "  </td>";// CASSAREACODE
                    tablacabeceracass = tablacabeceracass + "<td class=dataItem align=center >" + line.Substring(16, 3) + "  </td>";// AIRLINE PREFIX
                    tablacabeceracass = tablacabeceracass + "<td class=dataItem align=center >" + line.Substring(19, 6) + "  </td>";// FECHA INICIO PERIODO
                    tablacabeceracass = tablacabeceracass + "<td class=dataItem align=center >" + line.Substring(25, 6) + "  </td>";// FECHA FIN PERIODO
                    tablacabeceracass = tablacabeceracass + "<td class=dataItem align=center >" + line.Substring(31, 6) + "  </td>"; //FECHA DE FACTURACION
                    tablacabeceracass = tablacabeceracass + "<td class=dataItem align=center>" + line.Substring(37, 2) + "  </td>"; //filenumber
                    tablacabeceracass = tablacabeceracass + " </tr>";
                    counter++;
                }

                tablacabeceracass = tablacabeceracass + " </table>";

                wswsDocument.EsavDocumentClient servicioDocument = new wswsDocument.EsavDocumentClient();
                ClGenerarCass objgenerarcass = new ClGenerarCass();
                string respuestacabecera = objgenerarcass.registrarCabeceraCassDA(CodigoEmisor, UsuarioId, UsuarioIP, XmlCabecera.ToString());
                string respuestahot = "";



                int iterancciones = 1;
                if (cantinsertar > 0)
                {

                    iterancciones = 100;

                }

                string linea = "";
                string valorr="";
                string fechr = "";
                //for (int k = 0; k < cantinsertar; k++)
                //{
                String XmlCuerpo = "";
                int contadorcien = 0;
                //   XmlCuerpo.Append("<RootCuerpo>");
                while ((line = file.ReadLine()) != null)
                {
                    // line = file.ReadLine();



                    if (counter > 0 && line.Substring(0, 3) == "AWM" && line.Substring(0, 3) != "TTT")
                    {
                        XmlCuerpo = XmlCuerpo + ("<Cuerpo>");

                        //   line.Substring(4, 24);
                        // dato = dato + line.Substring(4, 24) + "  <br />";
                        XmlCuerpo = XmlCuerpo + (String.Format("<RECORDID>{0}</RECORDID>", line.Substring(0, 3)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<BRANCHID>{0}</BRANCHID>", line.Substring(3, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<VATID>{0}</VATID>", line.Substring(4, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AIRPREF>{0}</AIRPREF>", line.Substring(16, 3)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AWBSERIALN>{0}</AWBSERIALN>", line.Substring(19, 8)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AWMNROMODCHECK>{0}</AWMNROMODCHECK>", line.Substring(28, 1)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<ORIGIN>{0}</ORIGIN>", line.Substring(29, 3)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<AGENCODE>{0}</AGENCODE>", line.Substring(5, 11)));
                        XmlCuerpo = XmlCuerpo + (String.Format("<NUMBERDOC>{0}</NUMBERDOC>", line.Substring(235, 14)));
                    
                        XmlCuerpo = XmlCuerpo + (String.Format("<FECHA>{0}</FECHA>", line.Substring(38,6)));
                        valorr =  line.Substring(61, 5);
                        if (valorr == "00000")
                        {
                            XmlCuerpo = XmlCuerpo + (String.Format("<Valorventa>{0}</Valorventa>", line.Substring(110, 5)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<duecarrier>{0}</duecarrier>", line.Substring(135, 5)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<dueagent>{0}</dueagent>", line.Substring(146, 5)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<TIPO>{0}</TIPO>", "GC"));
                        }
                        else
                        {
                            XmlCuerpo = XmlCuerpo + (String.Format("<Valorventa>{0}</Valorventa>", valorr));
                            XmlCuerpo = XmlCuerpo + (String.Format("<duecarrier>{0}</duecarrier>", "0.00"));
                            XmlCuerpo = XmlCuerpo + (String.Format("<dueagent>{0}</dueagent>", line.Substring(87, 5)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<TIPO>{0}</TIPO>", "GP"));
                        }

                        XmlCuerpo = XmlCuerpo + ("</Cuerpo>");
                    }

                    else
                        if (counter > 0 && (line.Substring(0, 3).Trim() == "CCR" || line.Substring(0, 3).Trim() == "DCR") && line.Substring(0, 3) != "TTT")
                        {

                            XmlCuerpo = XmlCuerpo + ("<Cuerpo>");

                            //   line.Substring(4, 24);
                            // dato = dato + line.Substring(4, 24) + "  <br />";
                            XmlCuerpo = XmlCuerpo + (String.Format("<RECORDID>{0}</RECORDID>", line.Substring(0, 3)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<BRANCHID>{0}</BRANCHID>", line.Substring(3, 1)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<VATID>{0}</VATID>", line.Substring(4, 1)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AIRPREF>{0}</AIRPREF>", line.Substring(5, 3)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AWBSERIALN>{0}</AWBSERIALN>", line.Substring(32, 6)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AWMNROMODCHECK>{0}</AWMNROMODCHECK>", line.Substring(28, 1)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<ORIGIN>{0}</ORIGIN>", line.Substring(18, 3)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<AGENCODE>{0}</AGENCODE>", line.Substring(21, 11)));
                            XmlCuerpo = XmlCuerpo + (String.Format("<NUMBERDOC>{0}</NUMBERDOC>", line.Substring(172, 11)));
                           
                            XmlCuerpo = XmlCuerpo + (String.Format("<FECHA>{0}</FECHA>", line.Substring(38, 6)));
                            valorr = line.Substring(61, 5);
                            if (valorr == "00000")
                            {
                                XmlCuerpo = XmlCuerpo + (String.Format("<Valorventa>{0}</Valorventa>", line.Substring(110, 5)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<duecarrier>{0}</duecarrier>", line.Substring(135, 5)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<dueagent>{0}</dueagent>", line.Substring(146, 5)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<TIPO>{0}</TIPO>", "GC"));
                            }
                            else
                            {
                                XmlCuerpo = XmlCuerpo + (String.Format("<Valorventa>{0}</Valorventa>", valorr));
                                XmlCuerpo = XmlCuerpo + (String.Format("<duecarrier>{0}</duecarrier>", "0.00"));
                                XmlCuerpo = XmlCuerpo + (String.Format("<dueagent>{0}</dueagent>", line.Substring(87, 5)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<TIPO>{0}</TIPO>", "GP"));
                            }
                            XmlCuerpo = XmlCuerpo + ("</Cuerpo>");
                        }

                        else
                            if (counter > 0 && (line.Substring(0, 3).Trim() == "CCO" || line.Substring(0, 3).Trim() == "DCO") && line.Substring(0, 3) != "TTT")
                            {

                                XmlCuerpo = XmlCuerpo + ("<Cuerpo>");

                                //   line.Substring(4, 24);
                                // dato = dato + line.Substring(4, 24) + "  <br />";
                                XmlCuerpo = XmlCuerpo + (String.Format("<RECORDID>{0}</RECORDID>", line.Substring(0, 3)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<BRANCHID>{0}</BRANCHID>", line.Substring(3, 1)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<VATID>{0}</VATID>", line.Substring(4, 1)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AIRPREF>{0}</AIRPREF>", line.Substring(5, 3)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AWBSERIALN>{0}</AWBSERIALN>", line.Substring(32, 6)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AWMNROMODCHECK>{0}</AWMNROMODCHECK>", line.Substring(28, 1)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<ORIGIN>{0}</ORIGIN>", line.Substring(18, 3)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<AGENCODE>{0}</AGENCODE>", line.Substring(21, 11)));
                                XmlCuerpo = XmlCuerpo + (String.Format("<NUMBERDOC>{0}</NUMBERDOC>", line.Substring(172, 11)));
                                
                                XmlCuerpo = XmlCuerpo + (String.Format("<FECHA>{0}</FECHA>", line.Substring(38, 6)));
                                valorr = line.Substring(61, 5);
                                if (valorr == "00000")
                                {
                                    XmlCuerpo = XmlCuerpo + (String.Format("<Valorventa>{0}</Valorventa>", line.Substring(110, 5)));
                                    XmlCuerpo = XmlCuerpo + (String.Format("<duecarrier>{0}</duecarrier>", line.Substring(135, 5)));
                                    XmlCuerpo = XmlCuerpo + (String.Format("<dueagent>{0}</dueagent>", line.Substring(146, 5)));
                                    XmlCuerpo = XmlCuerpo + (String.Format("<TIPO>{0}</TIPO>", "GC"));
                                }
                                else
                                {
                                    XmlCuerpo = XmlCuerpo + (String.Format("<Valorventa>{0}</Valorventa>", valorr));
                                    XmlCuerpo = XmlCuerpo + (String.Format("<duecarrier>{0}</duecarrier>", "0.00"));
                                    XmlCuerpo = XmlCuerpo + (String.Format("<dueagent>{0}</dueagent>", line.Substring(87, 5)));
                                    XmlCuerpo = XmlCuerpo + (String.Format("<TIPO>{0}</TIPO>", "GP"));
                                }
                                XmlCuerpo = XmlCuerpo + ("</Cuerpo>");

                            }
                    if (counter == 100 && contadorcien < cantinsertar)
                    {
                        contadorcien = contadorcien + 1;
                        string XmlCuerpofinal = "<RootCuerpo>" + XmlCuerpo + "</RootCuerpo>";
                        respuestahot = objgenerarcass.registrarCassDA(Convert.ToInt32(respuestacabecera), UsuarioId, UsuarioIP, XmlCuerpofinal.ToString().Replace("&", "&amp;"));

                        counter = 0;
                        XmlCuerpo = "";

                    }

                    if (counter == rangofinal - 2 && contadorcien == cantinsertar)
                    {

                        string XmlCuerpofinal = "<RootCuerpo>" + XmlCuerpo + "</RootCuerpo>";
                        respuestahot = objgenerarcass.registrarCassDA(Convert.ToInt32(respuestacabecera), UsuarioId, UsuarioIP, XmlCuerpofinal.ToString().Replace("&", "&amp;"));

                        counter = 0;
                        XmlCuerpo = "";

                    }


                    counter++;
                }


                if (Convert.ToInt32(respuestacabecera) > 0)
                {



                    ltrtablacass.Text = tablacabeceracass;
                    ltrcassdescripcion.Text = "Son" + "  " + Convert.ToString(lineCount - 2) + "  " + "Registros Ingresados Correctamente";
                }
                else
                    ltrcassdescripcion.Text = respuestacabecera;

                file.Close();


            }
            else
            {
                // Notify the user why their file was not uploaded.
                ltrtabla.Text = "El archivo  " + "no puede exceder los 2MB.";
            }
        }
        else
        {
            // Notify the user that a file was not uploaded.
            ltrtabla.Text = "You did not specify a file to upload.";
        }





    }
    }
