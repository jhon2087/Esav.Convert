using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

public partial class FormDocuments_InterfaceContable_FrmSubirClientesRuc : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        /*
               string savePath = @"E:\padronsunat\";
               int counter = 0;
                string line;
                string line0;
                string dato = "";
                decimal lineCount = File.ReadAllLines(string.Format(@"E:\padronsunat\{0}", "padron_sunat1.txt")).Count();
                decimal catidaddecimal = lineCount / 500;
                decimal cantinsertar = Math.Truncate(Convert.ToDecimal(lineCount / 500));
                decimal cantidadinsertarango = catidaddecimal - cantinsertar;
                decimal rangofinal = cantidadinsertarango * 500;
                System.IO.StreamReader file =
                new System.IO.StreamReader(string.Format(@"E:\padronsunat\{0}", "padron_sunat1.txt"));

                ClGenerarCass objgenerarcass = new ClGenerarCass();
                string respuesta = "";
                StringBuilder XmlCabecera = new StringBuilder();
                int iterancciones = 1;
                if (cantinsertar > 0)
                {

                    iterancciones = 500;

                }

                string linea = "";
                String XmlCuerpo = "";
                int contadorcien = 0;
                //   XmlCuerpo.Append("<RootCuerpo>");
                while ((line = file.ReadLine()) != null)
                {
                  
                    if (counter > 0 )
                    {
                        XmlCuerpo = XmlCuerpo + ("<Cuerpo>");
                        XmlCuerpo = XmlCuerpo + (String.Format("<RUC>{0}</RUC>", line.Substring(0, 11)));
                        string datos = line.Substring(12, line.Length - 12).ToString().Replace("<", "&lt;");
                        datos = datos.ToString().Replace("&", "&amp;");
                        datos = datos.ToString().Replace("'", "&apos;");
                        datos = datos.ToString().Replace("Ñ", "&ntilde;");
                        datos = datos.ToString().Replace("Ã", "&Atilde;");
                        datos = datos.ToString().Replace("\"", "&quot;");
                        datos = datos.ToString().Replace("`", "&#96;");
                        XmlCuerpo = XmlCuerpo + (String.Format("<DESCRIPCION>{0}</DESCRIPCION>", datos));
                        
                        XmlCuerpo = XmlCuerpo + ("</Cuerpo>");
                    }

                 if (counter == 500 && contadorcien < cantinsertar)
                    {
                        contadorcien = contadorcien + 1;
                        string XmlCuerpofinal = "<RootCuerpo>" + XmlCuerpo + "</RootCuerpo>";
                        respuesta = objgenerarcass.registrarRucClienteSunatDA(XmlCuerpofinal.ToString());
                        counter = 0;
                        XmlCuerpo = "";
                       
                    }

                    if (counter == rangofinal - 1 && contadorcien == cantinsertar)
                    {
                        string XmlCuerpofinal = "<RootCuerpo>" + XmlCuerpo + "</RootCuerpo>";
                        respuesta = objgenerarcass.registrarRucClienteSunatDA(XmlCuerpofinal.ToString());
                        counter = 0;
                        XmlCuerpo = "";
                  }

                    counter++;
                }

                ltrtablacabecera.Text = respuesta;
                file.Close();

       */

   string envio=     insertarDatoCliente();
   ltrtablacabecera.Text = envio;

    }


    public string insertarDatoCliente()

    {
        string savePath = @"E:\padronsunat\";
        int counter = 0;
        string line;
        string line0;
        string dato = "";
        string[] dirsProveedores = Directory.GetFiles(savePath, "*.txt");
        int cantidadProveedores = dirsProveedores.Length;

        for (int a = 0; a < cantidadProveedores; a++)
        {
            decimal lineCount = File.ReadAllLines(string.Format(@"E:\padronsunat\{0}", "padron_sunat" + (a+1) + ".txt")).Count();
            decimal catidaddecimal = lineCount / 500;
            decimal cantinsertar = Math.Truncate(Convert.ToDecimal(lineCount / 500));
            decimal cantidadinsertarango = catidaddecimal - cantinsertar;
            decimal rangofinal = cantidadinsertarango * 500;
            System.IO.StreamReader file =
            new System.IO.StreamReader(string.Format(@"E:\padronsunat\{0}", "padron_sunat" + (a + 1) + ".txt"));
            ClGenerarCass objgenerarcass = new ClGenerarCass();
            string respuesta = "";
            StringBuilder XmlCabecera = new StringBuilder();
            int iterancciones = 1;
            if (cantinsertar > 0)
            {
                iterancciones = 500;
            }
            while ((line = file.ReadLine()) != null)
            {
                if (counter > 0)
                {
                   string ruc =    line.Substring(0, 11);
                   string datos = line.Substring(12, line.Length - 12);

                   respuesta = objgenerarcass.registrarRucClienteSunatUnoUno(ruc, datos);
                 }

                counter = counter + 1;
            }
            file.Close();
        }
       
    //    string savePath = @"E:\padronsunat\";
    //    int counter = 0;
    //    string line;
    //    string line0;
    //    string dato = "";
    //    decimal lineCount = File.ReadAllLines(string.Format(@"E:\padronsunat\{0}", "padron_sunat1.txt")).Count();
    //    decimal catidaddecimal = lineCount / 500;
    //    decimal cantinsertar = Math.Truncate(Convert.ToDecimal(lineCount / 500));
    //    decimal cantidadinsertarango = catidaddecimal - cantinsertar;
    //    decimal rangofinal = cantidadinsertarango * 500;
    //    System.IO.StreamReader file =
    //    new System.IO.StreamReader(string.Format(@"E:\padronsunat\{0}", "padron_sunat1.txt"));

    //    ClGenerarCass objgenerarcass = new ClGenerarCass();
    //    string respuesta = "";
    //    StringBuilder XmlCabecera = new StringBuilder();
    //    int iterancciones = 1;
    //    if (cantinsertar > 0)
    //    {
    //        iterancciones = 500;
    //    }

    //while ((line = file.ReadLine()) != null)
    //    {

    //        if (counter > 0)
    //        {
    //           string ruc =    line.Substring(0, 11);
    //           string datos = line.Substring(12, line.Length - 12);

    //           respuesta = objgenerarcass.registrarRucClienteSunatUnoUno(ruc, datos);
    //         }

    //        counter = counter + 1;

                 
    //    }
    //file.Close();

        return "OK";
      
    
    }



}