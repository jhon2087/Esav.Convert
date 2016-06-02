using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de RepConcBE
/// </summary>
public class RepConcBE
{
    public DateTime FechaEmision { get; set; }
    public string TipoDocumento { get; set; }
    public string NroDocumento { get; set; }
    public string Moneda { get; set; }
    public string CodigoCliente { get; set; }
    public string RucAgencia { get; set; }
    public string NombreAgencia { get; set; }
    public decimal ValorVenta { get; set; }
    public decimal TotalIgv { get; set; }
    public decimal TotalIsc { get; set; }
    public decimal TotalOtros { get; set; }
    public decimal TotalOtrostributos { get; set; }
    public decimal ImporteTotal { get; set; }
    public decimal ImporteTotaldolares { get; set; }
    public decimal TipoCambio { get; set; }
    public string NroDocRef { get; set; }
    public string TipoDocRef { get; set; }
    public string TipoBillete1 { get; set; }
    public string TipoBillete2 { get; set; }
    public string TipoBillete3 { get; set; }
    public string FechaEmisionREP { get; set; }
    public string Serie { get; set; }
    public string Rango { get; set; }
    public int Fila { get; set; }
    public string Sucuresal { get; set; }
    public decimal Gravado { get; set; }
    public decimal Exonerado { get; set; }
    public decimal Inafecto { get; set; }

    public List<RepConcBE> exportarrepconsolidadoventasDA(string _Emisor, string _Fecha, string _serie)
    {
        try
        {
            List<RepConcBE> lRepFactura = new List<RepConcBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTEVENTASCONSOLIDADOexcel", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@fech", SqlDbType.VarChar, 10).Value = _Fecha;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@serieA", SqlDbType.VarChar, 6).Value = _serie;
                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RepConcBE oRepFactura = new RepConcBE();
                                oRepFactura.FechaEmisionREP = drListado.GetString(drListado.GetOrdinal("fechaemision"));
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.Serie = drListado.GetString(drListado.GetOrdinal("serie"));
                                oRepFactura.Sucuresal = drListado.GetString(drListado.GetOrdinal("SUCURSAL"));
                                oRepFactura.Rango = drListado.GetString(drListado.GetOrdinal("rango"));
                                oRepFactura.Fila = drListado.GetInt32(drListado.GetOrdinal("CANTIDAD"));
                                oRepFactura.Moneda = drListado.GetString(drListado.GetOrdinal("moneda"));
                                oRepFactura.ValorVenta = drListado.GetDecimal(drListado.GetOrdinal("VALORVENTA"));
                                oRepFactura.TotalIgv = drListado.GetDecimal(drListado.GetOrdinal("IGV"));
                                oRepFactura.TotalIsc = drListado.GetDecimal(drListado.GetOrdinal("ISC"));
                                oRepFactura.TotalOtros = drListado.GetDecimal(drListado.GetOrdinal("OTROSCARGOS"));
                                oRepFactura.TotalOtrostributos = drListado.GetDecimal(drListado.GetOrdinal("OTROSTRIBUTOS"));
                                /*oRepFactura.Gravado = drListado.GetDecimal(drListado.GetOrdinal("GRAVADO"));
                                oRepFactura.Exonerado = drListado.GetDecimal(drListado.GetOrdinal("EXONERADO"));
                                oRepFactura.Inafecto = drListado.GetDecimal(drListado.GetOrdinal("INAFECTO"));*/
                                oRepFactura.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTASOLES"));
                                oRepFactura.ImporteTotaldolares = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTADOLARES"));
                                oRepFactura.TipoCambio = drListado.GetDecimal(drListado.GetOrdinal("tc"));
                                lRepFactura.Add(oRepFactura);
                            }
                        }
                    }
                }
            }
            return lRepFactura;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

             public List<RepConcBE> ObtenerreportedocemitidosresumenexcelDA(string _Emisor, string _FechaInicial, string _FechaFinal, string _tipodoc, string _serie)
    {
        try
        {
            List<RepConcBE> lRepFactura = new List<RepConcBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTEVENTASRESUMIDOexcel", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHI", SqlDbType.VarChar, 10).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHF", SqlDbType.VarChar, 10).Value = _FechaFinal;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@tipodoc", SqlDbType.VarChar, 2).Value = _tipodoc;
                    oCommand.Parameters.Add("@serie", SqlDbType.VarChar, 6).Value = _serie;

                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RepConcBE oRepFactura = new RepConcBE();
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.Sucuresal = drListado.GetString(drListado.GetOrdinal("SUCURSAL"));
                                oRepFactura.Fila = drListado.GetInt32(drListado.GetOrdinal("CANTDOC"));
                                
                                lRepFactura.Add(oRepFactura);
                            }
                        }
                    }
                }
            }
            return lRepFactura;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<RepConcBE> ObtenerreportedocemitidosexcelDA(string _Emisor, string _FechaInicial, string _FechaFinal, string _tipodoc, string _serie)
    {
        try
        {
            List<RepConcBE> lRepFactura = new List<RepConcBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTEVENTASexcel", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHI", SqlDbType.VarChar, 10).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHF", SqlDbType.VarChar, 10).Value = _FechaFinal;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@tipodoc", SqlDbType.VarChar, 2).Value = _tipodoc;
                    oCommand.Parameters.Add("@serie", SqlDbType.VarChar, 6).Value = _serie;

                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RepConcBE oRepFactura = new RepConcBE();
                                oRepFactura.FechaEmisionREP = drListado.GetString(drListado.GetOrdinal("fechaemision"));
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOC"));
                                oRepFactura.Sucuresal = drListado.GetString(drListado.GetOrdinal("SUCURSAL"));
                                oRepFactura.Gravado = drListado.GetDecimal(drListado.GetOrdinal("GRABADO"));
                                oRepFactura.Exonerado = drListado.GetDecimal(drListado.GetOrdinal("EXONERADO"));
                                oRepFactura.Inafecto = drListado.GetDecimal(drListado.GetOrdinal("INAFECTO"));
                                oRepFactura.RucAgencia = drListado.GetString(drListado.GetOrdinal("RUC"));
                                oRepFactura.NombreAgencia = drListado.GetString(drListado.GetOrdinal("RAZONSOCIAL"));
                                oRepFactura.Moneda = drListado.GetString(drListado.GetOrdinal("moneda"));
                                oRepFactura.ValorVenta = drListado.GetDecimal(drListado.GetOrdinal("VALORVENTA"));
                                oRepFactura.TotalIgv = drListado.GetDecimal(drListado.GetOrdinal("IGV"));
                                oRepFactura.TotalIsc = drListado.GetDecimal(drListado.GetOrdinal("ISC"));
                                oRepFactura.TotalOtros = drListado.GetDecimal(drListado.GetOrdinal("OTROSCARGOS"));
                                oRepFactura.TotalOtrostributos = drListado.GetDecimal(drListado.GetOrdinal("OTROSTRIBUTOS"));
                                oRepFactura.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTASOLES"));
                                oRepFactura.ImporteTotaldolares = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTADOLARES"));
                                oRepFactura.TipoCambio = drListado.GetDecimal(drListado.GetOrdinal("tc"));
                                lRepFactura.Add(oRepFactura);
                            }
                        }
                    }
                }
            }
            return lRepFactura;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void generareportedocemitidosexcelDA(string _Emisor, string _FechaInicial, string _FechaFinal, string _tipodoc, string _serie, string nombrearchivo)
    {
        try
        {
            List<RepConcBE> lRepFactura = new List<RepConcBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_GENERARREPORTEVENTASexcel", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHI", SqlDbType.VarChar, 10).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHF", SqlDbType.VarChar, 10).Value = _FechaFinal;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@tipodoc", SqlDbType.VarChar, 2).Value = _tipodoc;
                    oCommand.Parameters.Add("@serie", SqlDbType.VarChar, 6).Value = _serie;
                    oCommand.Parameters.Add("@archivo", SqlDbType.VarChar, 225).Value = nombrearchivo;
                    oConnection.Open();
                    oCommand.ExecuteNonQuery();
            
               }
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<RepConcBE> ListarRepVentasConsolidadoDA(string _Emisor, string _Fecha, string _serie)
    {
        try
        {
            List<RepConcBE> lRepFactura = new List<RepConcBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTEVENTASCONSOLIDADO", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@fech", SqlDbType.VarChar, 10).Value = _Fecha;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@serieA", SqlDbType.VarChar, 6).Value = _serie;
                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RepConcBE oRepFactura = new RepConcBE();
                                oRepFactura.FechaEmisionREP = drListado.GetString(drListado.GetOrdinal("fechaemision"));
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.Serie = drListado.GetString(drListado.GetOrdinal("serie"));
                                oRepFactura.Rango = drListado.GetString(drListado.GetOrdinal("rango"));
                                oRepFactura.Moneda = drListado.GetString(drListado.GetOrdinal("moneda"));
                                oRepFactura.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTASOLES"));
                                oRepFactura.ImporteTotaldolares = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTADOLARES"));
                                oRepFactura.TipoCambio = drListado.GetDecimal(drListado.GetOrdinal("tc"));
                                lRepFactura.Add(oRepFactura);
                            }
                        }
                    }
                }
            }
            return lRepFactura;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public int obtenerRepDocumentosElectronicosFilasDA(string _Emisor, string _FechaInicial, string _FechaFinal, string _tipodoc,string serie)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            int filas = 0;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTEVENTASCANTIDAD", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHI", SqlDbType.VarChar, 10).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHF", SqlDbType.VarChar, 10).Value = _FechaFinal;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@tipodoc", SqlDbType.VarChar, 2).Value = _tipodoc;
                    oCommand.Parameters.Add("@serie", SqlDbType.VarChar, 2).Value = serie;
                    oCommand.Parameters.Add("@Cantidad", SqlDbType.Int, 7).Direction = ParameterDirection.Output;
                    oConnection.Open();
                    oCommand.ExecuteNonQuery();


                    filas = Convert.ToInt32(oCommand.Parameters["@Cantidad"].Value);
                }
            }
            return filas;
        }
        catch (Exception ex) { throw ex; }
    }


    public List<RepConcBE> ListarRepDocumentosElectronicosDA(string _Emisor, string _FechaInicial, string _FechaFinal, string _tipodoc, string _serie, int _nTamanhoPagina, int _nPaginaActual)
    {
        try
        {
            List<RepConcBE> lRepFactura = new List<RepConcBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTEVENTAS", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHI", SqlDbType.VarChar, 10).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHF", SqlDbType.VarChar, 10).Value = _FechaFinal;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@tipodoc", SqlDbType.VarChar, 2).Value = _tipodoc;
                    oCommand.Parameters.Add("@serie", SqlDbType.VarChar, 6).Value = _serie;
                    oCommand.Parameters.Add("@TamañoPagina", SqlDbType.Int, 5).Value = _nTamanhoPagina;
                    oCommand.Parameters.Add("@PaginaActual", SqlDbType.Int, 5).Value = _nPaginaActual;
                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RepConcBE oRepFactura = new RepConcBE();
                                oRepFactura.FechaEmisionREP = drListado.GetString(drListado.GetOrdinal("fechaemision"));
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOC"));
                                oRepFactura.Moneda = drListado.GetString(drListado.GetOrdinal("moneda"));
                                oRepFactura.ValorVenta = drListado.GetDecimal(drListado.GetOrdinal("VALORVENTA"));
                                oRepFactura.TotalIgv = drListado.GetDecimal(drListado.GetOrdinal("IGV"));
                                oRepFactura.TotalIsc = drListado.GetDecimal(drListado.GetOrdinal("ISC"));
                                oRepFactura.TotalOtros = drListado.GetDecimal(drListado.GetOrdinal("OTROSCARGOS"));
                                oRepFactura.TotalOtrostributos = drListado.GetDecimal(drListado.GetOrdinal("OTROSTRIBUTOS"));
                                oRepFactura.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTASOLES"));
                                oRepFactura.ImporteTotaldolares = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTADOLARES"));
                                oRepFactura.TipoCambio = drListado.GetDecimal(drListado.GetOrdinal("tc"));

                                lRepFactura.Add(oRepFactura);
                            }
                        }
                    }
                }
            }
            return lRepFactura;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int obtenerRepDocCobranzaFilasDA(string _Emisor, string _FechaInicial, string _FechaFinal)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            int filas = 0;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTEDOCUMENTOCOBRANZACANTIDAD", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHI", SqlDbType.VarChar, 10).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHF", SqlDbType.VarChar, 10).Value = _FechaFinal;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    
                    oCommand.Parameters.Add("@Cant", SqlDbType.Int, 7).Direction = ParameterDirection.Output;
                    oConnection.Open();
                    oCommand.ExecuteNonQuery();

                    filas = Convert.ToInt32(oCommand.Parameters["@Cant"].Value);
                }
            }
            return filas;
        }
        catch (Exception ex) { throw ex; }
    }


    public List<RepConcBE> ListarRepDocCobranzaDA(string _Emisor, string _FechaInicial, string _FechaFinal, int _nTamanhoPagina, int _nPaginaActual)
    {
        try
        {
            List<RepConcBE> lRepFactura = new List<RepConcBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTEDOCUMENTOCOBRANZA", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHI", SqlDbType.VarChar, 10).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHF", SqlDbType.VarChar, 10).Value = _FechaFinal;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                   
                    oCommand.Parameters.Add("@TamañoPagina", SqlDbType.Int, 5).Value = _nTamanhoPagina;
                    oCommand.Parameters.Add("@PaginaActual", SqlDbType.Int, 5).Value = _nPaginaActual;
                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RepConcBE oRepFactura = new RepConcBE();
                                oRepFactura.FechaEmisionREP = drListado.GetString(drListado.GetOrdinal("fechaemision"));
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOC"));
                                oRepFactura.Moneda = drListado.GetString(drListado.GetOrdinal("moneda"));
                                oRepFactura.ValorVenta = drListado.GetDecimal(drListado.GetOrdinal("VALORVENTA"));
                                oRepFactura.TotalIgv = drListado.GetDecimal(drListado.GetOrdinal("IGV"));
                                oRepFactura.TotalIsc = drListado.GetDecimal(drListado.GetOrdinal("ISC"));
                                oRepFactura.TotalOtros = drListado.GetDecimal(drListado.GetOrdinal("OTROSCARGOS"));
                                oRepFactura.TotalOtrostributos = drListado.GetDecimal(drListado.GetOrdinal("OTROSTRIBUTOS"));
                                oRepFactura.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTASOLES"));
                                oRepFactura.ImporteTotaldolares = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTADOLARES"));
                                oRepFactura.TipoCambio = drListado.GetDecimal(drListado.GetOrdinal("tc"));

                                lRepFactura.Add(oRepFactura);
                            }
                        }
                    }
                }
            }
            return lRepFactura;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<RepConcBE> ObtenerreportedocCobranzaresumenexcelDA(string _Emisor, string _FechaInicial, string _FechaFinal)
    {
        try
        {
            List<RepConcBE> lRepFactura = new List<RepConcBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTECOBRANZARESUMIDOexcel", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHI", SqlDbType.VarChar, 10).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHF", SqlDbType.VarChar, 10).Value = _FechaFinal;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                   
                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RepConcBE oRepFactura = new RepConcBE();
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.Sucuresal = drListado.GetString(drListado.GetOrdinal("SUCURSAL"));
                                oRepFactura.Fila = drListado.GetInt32(drListado.GetOrdinal("CANTDOC"));

                                lRepFactura.Add(oRepFactura);
                            }
                        }
                    }
                }
            }
            return lRepFactura;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<RepConcBE> ObtenerreportedocCobranzaexcelDA(string _Emisor, string _FechaInicial, string _FechaFinal)
    {
        try
        {
            List<RepConcBE> lRepFactura = new List<RepConcBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTECOBRANZAexcel", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHI", SqlDbType.VarChar, 10).Value = _FechaInicial;
                    oCommand.Parameters.Add("@FECHF", SqlDbType.VarChar, 10).Value = _FechaFinal;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                 

                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RepConcBE oRepFactura = new RepConcBE();
                                oRepFactura.FechaEmisionREP = drListado.GetString(drListado.GetOrdinal("fechaemision"));
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOC"));
                                oRepFactura.Sucuresal = drListado.GetString(drListado.GetOrdinal("SUCURSAL"));
                                oRepFactura.Gravado = drListado.GetDecimal(drListado.GetOrdinal("GRABADO"));
                                oRepFactura.Exonerado = drListado.GetDecimal(drListado.GetOrdinal("EXONERADO"));
                                oRepFactura.Inafecto = drListado.GetDecimal(drListado.GetOrdinal("INAFECTO"));
                                oRepFactura.RucAgencia = drListado.GetString(drListado.GetOrdinal("RUC"));
                                oRepFactura.NombreAgencia = drListado.GetString(drListado.GetOrdinal("RAZONSOCIAL"));
                                oRepFactura.Moneda = drListado.GetString(drListado.GetOrdinal("moneda"));
                                oRepFactura.ValorVenta = drListado.GetDecimal(drListado.GetOrdinal("VALORVENTA"));
                                oRepFactura.TotalIgv = drListado.GetDecimal(drListado.GetOrdinal("IGV"));
                                oRepFactura.TotalIsc = drListado.GetDecimal(drListado.GetOrdinal("ISC"));
                                oRepFactura.TotalOtros = drListado.GetDecimal(drListado.GetOrdinal("OTROSCARGOS"));
                                oRepFactura.TotalOtrostributos = drListado.GetDecimal(drListado.GetOrdinal("OTROSTRIBUTOS"));
                                oRepFactura.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTASOLES"));
                                oRepFactura.ImporteTotaldolares = drListado.GetDecimal(drListado.GetOrdinal("IMPORTEVENTADOLARES"));
                                oRepFactura.TipoCambio = drListado.GetDecimal(drListado.GetOrdinal("tc"));
                                lRepFactura.Add(oRepFactura);
                            }
                        }
                    }
                }
            }
            return lRepFactura;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public RepConcBE()
    {

    }
}