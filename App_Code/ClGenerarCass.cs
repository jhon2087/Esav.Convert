using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de ClGenerarCass
/// </summary>
public class ClGenerarCass
{

    public string registrarCabeceraHotDA(string IdEmisor, string UsuarioCreo, string IpCreacion, string XmlCabecera)
    {
        try
        {
            String mensaje = string.Empty;
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeCabeceraHotGrabar", oConnection))
                {

                    oCommand.CommandType = CommandType.StoredProcedure;

                    oCommand.Parameters.Add("@Idemisor", SqlDbType.Char, 15).Value = IdEmisor;
                    oCommand.Parameters.Add("@UsuarioCreo", SqlDbType.VarChar, 50).Value = UsuarioCreo;
                    oCommand.Parameters.Add("@IpCreacion", SqlDbType.VarChar, 15).Value = IpCreacion;
                    oCommand.Parameters.Add("@XmlCabecera", SqlDbType.Xml).Value = XmlCabecera;
                    oConnection.Open();
                    oCommand.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oCommand.ExecuteNonQuery();
                    mensaje = oCommand.Parameters["@mensaje"].Value.ToString();

                }
            }
            return mensaje;
        }
        catch (Exception ex) { throw ex; }
    }





    public string registrarHotDA(int IdCabecera, string UsuarioCreo, string IpCreacion, string XmlHot)
    {
        try
        {
            String mensaje = string.Empty;
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeHotGrabar", oConnection))
                {

                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@IdCabecera", SqlDbType.Int).Value = IdCabecera;
                    oCommand.Parameters.Add("@UsuarioCreo", SqlDbType.VarChar, 50).Value = UsuarioCreo;
                    oCommand.Parameters.Add("@IpCreacion", SqlDbType.VarChar, 15).Value = IpCreacion;
                    oCommand.Parameters.Add("@XmlHot", SqlDbType.Xml).Value = XmlHot;
                    oConnection.Open();
                    oCommand.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oCommand.ExecuteNonQuery();
                    mensaje = oCommand.Parameters["@mensaje"].Value.ToString();

                }
            }
            return mensaje;
        }
        catch (Exception ex) { throw ex; }
    }

    public string registrarCabeceraCassDA(string IdEmisor, string UsuarioCreo, string IpCreacion, string XmlCabecera)
    {
        try
        {
            String mensaje = string.Empty;
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeCabeceraCassGrabar", oConnection))
                {

                    oCommand.CommandType = CommandType.StoredProcedure;

                    oCommand.Parameters.Add("@Idemisor", SqlDbType.Char, 15).Value = IdEmisor;
                    oCommand.Parameters.Add("@UsuarioCreo", SqlDbType.VarChar, 50).Value = UsuarioCreo;
                    oCommand.Parameters.Add("@IpCreacion", SqlDbType.VarChar, 15).Value = IpCreacion;
                    oCommand.Parameters.Add("@XmlCabecera", SqlDbType.Xml).Value = XmlCabecera;
                    oConnection.Open();
                    oCommand.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oCommand.ExecuteNonQuery();
                    mensaje = oCommand.Parameters["@mensaje"].Value.ToString();

                }
            }
            return mensaje;
        }
        catch (Exception ex) { throw ex; }
    }





    public string registrarCassDA(int IdCabecera, string UsuarioCreo, string IpCreacion, string XmlHot)
    {
        try
        {
            String mensaje = string.Empty;
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeCassGrabar", oConnection))
                {

                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@IdCabecera", SqlDbType.Int).Value = IdCabecera;
                    oCommand.Parameters.Add("@UsuarioCreo", SqlDbType.VarChar, 50).Value = UsuarioCreo;
                    oCommand.Parameters.Add("@IpCreacion", SqlDbType.VarChar, 15).Value = IpCreacion;
                    oCommand.Parameters.Add("@XmlHot", SqlDbType.Xml).Value = XmlHot;
                    oConnection.Open();
                    oCommand.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oCommand.ExecuteNonQuery();
                    mensaje = oCommand.Parameters["@mensaje"].Value.ToString();

                }
            }
            return mensaje;
        }
        catch (Exception ex) { throw ex; }
    }




    public string registrarRucClienteSunatDA(string XmlRucCliente)
    {
        try
        {
            String mensaje = string.Empty;
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeRucClienteSunatGrabar", oConnection))
                {

                    oCommand.CommandType = CommandType.StoredProcedure;

                    oCommand.Parameters.Add("@XmlRucCliente", SqlDbType.Xml).Value = XmlRucCliente;
                    oConnection.Open();
                    oCommand.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oCommand.ExecuteNonQuery();
                    mensaje = oCommand.Parameters["@mensaje"].Value.ToString();

                }
            }
            return mensaje;
        }
        catch (Exception ex)
        {
            string linead = "10";
            string dato = XmlRucCliente;
            throw ex;
        }
    }




    public string registrarRucClienteSunatUnoUno(string RucCliente, string datos)
    {
        try
        {
            String mensaje = string.Empty;
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeRucClienteSunatGrabarUnoUno", oConnection))
                {

                    oCommand.CommandType = CommandType.StoredProcedure;

                    oCommand.Parameters.Add("@RucCliente", SqlDbType.Char, 12).Value = RucCliente;
                    oCommand.Parameters.Add("@Datos", SqlDbType.VarChar, 350).Value = datos;
                    oConnection.Open();
                    oCommand.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oCommand.ExecuteNonQuery();
                    mensaje = oCommand.Parameters["@mensaje"].Value.ToString();

                }
            }
            return mensaje;
        }
        catch (Exception ex)
        {
            string linead = "10";
            throw ex;
        }
    }


    public List<ReporteEmitido> ListarReporteEmitido(string _UsuarioId, string _RazonSocial, string _NroFactura, string _Serie, decimal _Monto, int _PageIndex, int _PageSize, DateTime _FechaInicial, DateTime _FechaFinal, string _TipoDocumento)
    {

        try
        {
            List<ReporteEmitido> listaReporteEmitido = new List<ReporteEmitido>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeListarFacturaCriteriosRepEmitidos", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@UsuarioId", _UsuarioId);
                    oCommand.Parameters.AddWithValue("@RazonSocial", _RazonSocial);
                    oCommand.Parameters.AddWithValue("@NroFactura", _NroFactura);
                    oCommand.Parameters.AddWithValue("@Serie", _Serie);
                    oCommand.Parameters.AddWithValue("@Monto", _Monto);
                    oCommand.Parameters.AddWithValue("@nPaginaTamanho", _PageSize);
                    oCommand.Parameters.AddWithValue("@nPaginaActual", _PageIndex);
                    oCommand.Parameters.AddWithValue("@fechini", _FechaInicial);
                    oCommand.Parameters.AddWithValue("@fechfin", _FechaFinal);
                    oCommand.Parameters.AddWithValue("@tipo", _TipoDocumento);

                    oConnection.Open();
                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {
                                ReporteEmitido objReporteEmitido = new ReporteEmitido();
                                objReporteEmitido.IdDocumento = drListado.GetInt32(drListado.GetOrdinal("IDDOCS"));
                                objReporteEmitido.IdEmisor = drListado.GetString(drListado.GetOrdinal("IDEMISOR"));
                                objReporteEmitido.IdRango = drListado.GetInt32(drListado.GetOrdinal("IDRANGO"));
                                objReporteEmitido.Serie = drListado.GetString(drListado.GetOrdinal("SERIE"));
                                objReporteEmitido.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOCUMENTO"));
                                objReporteEmitido.RazonSocial = drListado.GetString(drListado.GetOrdinal("RAZSOCIAL"));
                                objReporteEmitido.FechaEmision = drListado.GetDateTime(drListado.GetOrdinal("FECHAEMISION"));
                                objReporteEmitido.Sede = drListado.GetString(drListado.GetOrdinal("DENOMINACION"));
                                objReporteEmitido.Moneda = drListado.GetString(drListado.GetOrdinal("TIPOMENEDA"));
                                objReporteEmitido.Cantidad = drListado.GetInt32(drListado.GetOrdinal("CANTIDAD"));
                                objReporteEmitido.ValorUnitario = drListado.GetDecimal(drListado.GetOrdinal("VALORUNITARIO"));
                                objReporteEmitido.BaseImponible = drListado.GetDecimal(drListado.GetOrdinal("BASEIMPONIBLE"));
                                objReporteEmitido.IGV = drListado.GetDecimal(drListado.GetOrdinal("IGV"));
                                objReporteEmitido.ISC = drListado.GetDecimal(drListado.GetOrdinal("ISC"));
                                objReporteEmitido.IGV = drListado.GetDecimal(drListado.GetOrdinal("OTROSCARGOS"));
                                objReporteEmitido.ISC = drListado.GetDecimal(drListado.GetOrdinal("OTROSTRIBUTOS"));
                                objReporteEmitido.Descuento = drListado.GetDecimal(drListado.GetOrdinal("DESCUENTO"));
                                objReporteEmitido.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTETOTAL"));
                                listaReporteEmitido.Add(objReporteEmitido);
                            }
                        }
                    }
                }
            }
            return listaReporteEmitido;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int obtenerTotalesReporteEmitido(string _UsuarioId, string _RazonSocial, string _NroFactura, string _Serie, decimal _Monto, DateTime _FechaInicial, DateTime _FechaFinal, string _TipoDocumento)
    {

        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            int filas = 0;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeListarFacturaCriteriosRepEmitidosFilas", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@UsuarioId", _UsuarioId);
                    oCommand.Parameters.AddWithValue("@RazonSocial", _RazonSocial);
                    oCommand.Parameters.AddWithValue("@NroFactura", _NroFactura);
                    oCommand.Parameters.AddWithValue("@Serie", _Serie);
                    oCommand.Parameters.AddWithValue("@Monto", _Monto);
                    oCommand.Parameters.AddWithValue("@fechini", _FechaInicial);
                    oCommand.Parameters.AddWithValue("@fechfin", _FechaFinal);
                    oCommand.Parameters.AddWithValue("@tipo", _TipoDocumento);
                    oCommand.Parameters.Add("@nRetVal", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    oConnection.Open();
                    oCommand.ExecuteNonQuery();

                    filas = Convert.ToInt32(oCommand.Parameters["@nRetVal"].Value);
                }
            }
            return filas;
        }
        catch (Exception ex) { throw ex; }
    }

    public List<ReporteEmitido> ListarReporteEmitidoSinPaginar(string _UsuarioId, string _RazonSocial, string _NroFactura, string _Serie, decimal _Monto, int _PageIndex, int _PageSize, DateTime _FechaInicial, DateTime _FechaFinal, string _TipoDocumento)
    {

        try
        {
            List<ReporteEmitido> listaReporteEmitido = new List<ReporteEmitido>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeListarFacturaCriteriosRepEmitidosSinPaginar", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@UsuarioId", _UsuarioId);
                    oCommand.Parameters.AddWithValue("@RazonSocial", _RazonSocial);
                    oCommand.Parameters.AddWithValue("@NroFactura", _NroFactura);
                    oCommand.Parameters.AddWithValue("@Serie", _Serie);
                    oCommand.Parameters.AddWithValue("@Monto", _Monto);
                    oCommand.Parameters.AddWithValue("@nPaginaTamanho", _PageSize);
                    oCommand.Parameters.AddWithValue("@nPaginaActual", _PageIndex);
                    oCommand.Parameters.AddWithValue("@fechini", _FechaInicial);
                    oCommand.Parameters.AddWithValue("@fechfin", _FechaFinal);
                    oCommand.Parameters.AddWithValue("@tipo", _TipoDocumento);

                    oConnection.Open();
                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {
                                ReporteEmitido objReporteEmitido = new ReporteEmitido();
                                objReporteEmitido.IdDocumento = drListado.GetInt32(drListado.GetOrdinal("IDDOCS"));
                                objReporteEmitido.IdEmisor = drListado.GetString(drListado.GetOrdinal("IDEMISOR"));
                                objReporteEmitido.IdRango = drListado.GetInt32(drListado.GetOrdinal("IDRANGO"));
                                objReporteEmitido.Serie = drListado.GetString(drListado.GetOrdinal("SERIE"));
                                objReporteEmitido.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOCUMENTO"));
                                objReporteEmitido.RazonSocial = drListado.GetString(drListado.GetOrdinal("RAZSOCIAL"));
                                objReporteEmitido.FechaEmision = drListado.GetDateTime(drListado.GetOrdinal("FECHAEMISION"));
                                objReporteEmitido.Sede = drListado.GetString(drListado.GetOrdinal("DENOMINACION"));
                                objReporteEmitido.Moneda = drListado.GetString(drListado.GetOrdinal("TIPOMENEDA"));
                                objReporteEmitido.Cantidad = drListado.GetInt32(drListado.GetOrdinal("CANTIDAD"));
                                objReporteEmitido.ValorUnitario = drListado.GetDecimal(drListado.GetOrdinal("VALORUNITARIO"));
                                objReporteEmitido.BaseImponible = drListado.GetDecimal(drListado.GetOrdinal("BASEIMPONIBLE"));
                                objReporteEmitido.IGV = drListado.GetDecimal(drListado.GetOrdinal("IGV"));
                                objReporteEmitido.ISC = drListado.GetDecimal(drListado.GetOrdinal("ISC"));
                                objReporteEmitido.IGV = drListado.GetDecimal(drListado.GetOrdinal("OTROSCARGOS"));
                                objReporteEmitido.ISC = drListado.GetDecimal(drListado.GetOrdinal("OTROSTRIBUTOS"));
                                objReporteEmitido.Descuento = drListado.GetDecimal(drListado.GetOrdinal("DESCUENTO"));
                                objReporteEmitido.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTETOTAL"));
                                listaReporteEmitido.Add(objReporteEmitido);
                            }
                        }
                    }
                }
            }
            return listaReporteEmitido;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public List<ReporteConsolidado> ListarReporteConsolidado(string _UsuarioId, string _RazonSocial, string _NroFactura, string _Serie, decimal _Monto, int _PageIndex, int _PageSize, DateTime _FechaInicial, DateTime _FechaFinal, string _TipoDocumento)
    {

        try
        {
            List<ReporteConsolidado> listaReporteConsolidado = new List<ReporteConsolidado>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeListarFacturaCriteriosRepConsolidado2", oConnection))
                //using (SqlCommand oCommand = new SqlCommand("ReporteConsolidado", oConnection))
                //using (SqlCommand oCommand = new SqlCommand("ListarReporteConsolidado", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@UsuarioId", _UsuarioId);
                    oCommand.Parameters.AddWithValue("@RazonSocial", _RazonSocial);
                    oCommand.Parameters.AddWithValue("@NroFactura", _NroFactura);
                    oCommand.Parameters.AddWithValue("@Serie", _Serie);
                    oCommand.Parameters.AddWithValue("@Monto", _Monto);
                    oCommand.Parameters.AddWithValue("@nPaginaTamanho", _PageSize);
                    oCommand.Parameters.AddWithValue("@nPaginaActual", _PageIndex);
                    oCommand.Parameters.AddWithValue("@fechini", _FechaInicial);
                    oCommand.Parameters.AddWithValue("@fechfin", _FechaFinal);
                    oCommand.Parameters.AddWithValue("@tipo", _TipoDocumento);

                    oConnection.Open();
                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {
                                ReporteConsolidado objReporteConsolidado = new ReporteConsolidado();
                                objReporteConsolidado.IdDocumento = drListado.GetInt32(drListado.GetOrdinal("IDDOCS"));
                                objReporteConsolidado.IdEmisor = drListado.GetString(drListado.GetOrdinal("IDEMISOR"));
                                objReporteConsolidado.IdRango = drListado.GetInt32(drListado.GetOrdinal("IDRANGO"));
                                objReporteConsolidado.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOCUMENTOS"));
                                objReporteConsolidado.Serie = drListado.GetString(drListado.GetOrdinal("SERIE"));
                                objReporteConsolidado.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOCUMENTO"));
                                objReporteConsolidado.RazonSocial = drListado.GetString(drListado.GetOrdinal("RAZSOCIAL"));
                                objReporteConsolidado.Sede = drListado.GetString(drListado.GetOrdinal("DENOMINACION"));
                                objReporteConsolidado.FechaEmision = drListado.GetDateTime(drListado.GetOrdinal("FECHAEMISION"));
                                objReporteConsolidado.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTETOTAL"));
                                listaReporteConsolidado.Add(objReporteConsolidado);
                            }
                        }
                    }
                }
            }
            return listaReporteConsolidado;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int obtenerTotalesReporteConsolidado(string _UsuarioId, string _RazonSocial, string _NroFactura, string _Serie, decimal _Monto, DateTime _FechaInicial, DateTime _FechaFinal, string _TipoDocumento)
    {

        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            int filas = 0;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeListarFacturaCriteriosRepConsolidadoFilas2", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@UsuarioId", _UsuarioId);
                    oCommand.Parameters.AddWithValue("@RazonSocial", _RazonSocial);
                    oCommand.Parameters.AddWithValue("@NroFactura", _NroFactura);
                    oCommand.Parameters.AddWithValue("@Serie", _Serie);
                    oCommand.Parameters.AddWithValue("@Monto", _Monto);
                    oCommand.Parameters.AddWithValue("@fechini", _FechaInicial);
                    oCommand.Parameters.AddWithValue("@fechfin", _FechaFinal);
                    oCommand.Parameters.AddWithValue("@tipo", _TipoDocumento);
                    oCommand.Parameters.Add("@nRetVal", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    oConnection.Open();
                    oCommand.ExecuteNonQuery();

                    filas = Convert.ToInt32(oCommand.Parameters["@nRetVal"].Value);
                }
            }
            return filas;
        }
        catch (Exception ex) { throw ex; }
    }

    public List<ReporteConsolidado> ListarReporteConsolidadoSinPaginar(string _UsuarioId, string _RazonSocial, string _NroFactura, string _Serie, decimal _Monto, int _PageIndex, int _PageSize, DateTime _FechaInicial, DateTime _FechaFinal, string _TipoDocumento)
    {

        try
        {
            List<ReporteConsolidado> listaReporteConsolidado = new List<ReporteConsolidado>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeListarFacturaCriteriosRepConsolidadoSinPaginar", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@UsuarioId", _UsuarioId);
                    oCommand.Parameters.AddWithValue("@RazonSocial", _RazonSocial);
                    oCommand.Parameters.AddWithValue("@NroFactura", _NroFactura);
                    oCommand.Parameters.AddWithValue("@Serie", _Serie);
                    oCommand.Parameters.AddWithValue("@Monto", _Monto);
                    oCommand.Parameters.AddWithValue("@nPaginaTamanho", _PageSize);
                    oCommand.Parameters.AddWithValue("@nPaginaActual", _PageIndex);
                    oCommand.Parameters.AddWithValue("@fechini", _FechaInicial);
                    oCommand.Parameters.AddWithValue("@fechfin", _FechaFinal);
                    oCommand.Parameters.AddWithValue("@tipo", _TipoDocumento);

                    oConnection.Open();
                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {
                                ReporteConsolidado objReporteConsolidado = new ReporteConsolidado();
                                objReporteConsolidado.IdDocumento = drListado.GetInt32(drListado.GetOrdinal("IDDOCS"));
                                objReporteConsolidado.IdEmisor = drListado.GetString(drListado.GetOrdinal("IDEMISOR"));
                                objReporteConsolidado.IdRango = drListado.GetInt32(drListado.GetOrdinal("IDRANGO"));
                                objReporteConsolidado.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOCUMENTOS"));
                                objReporteConsolidado.Serie = drListado.GetString(drListado.GetOrdinal("SERIE"));
                                objReporteConsolidado.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOCUMENTO"));
                                objReporteConsolidado.RazonSocial = drListado.GetString(drListado.GetOrdinal("RAZSOCIAL"));
                                objReporteConsolidado.Sede = drListado.GetString(drListado.GetOrdinal("DENOMINACION"));
                                objReporteConsolidado.FechaEmision = drListado.GetDateTime(drListado.GetOrdinal("FECHAEMISION"));
                                objReporteConsolidado.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTETOTAL"));
                                listaReporteConsolidado.Add(objReporteConsolidado);
                            }
                        }
                    }
                }
            }
            return listaReporteConsolidado;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public ClGenerarCass()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


}