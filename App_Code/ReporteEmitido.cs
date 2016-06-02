using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de ReporteEmitido
/// </summary>
public class ReporteEmitido
{
    public int IdDocumento { get; set; }
    public string IdEmisor { get; set; }
    public int IdRango { get; set; }
    public string Serie { get; set; }
    public string NroDocumento { get; set; }
    public string RazonSocial { get; set; }
    public string Sede { get; set; }
    public DateTime FechaEmision { get; set; }
    public string Moneda { get; set; }
    public decimal Cantidad { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal BaseImponible { get; set; }
    public decimal IGV { get; set; }
    public decimal ISC { get; set; }
    public decimal OtrosCargos { get; set; }
    public decimal OtrosTributos { get; set; }
    public decimal Descuento { get; set; }
    public decimal ImporteTotal { get; set; }

    public int obtenerRepDocumentoCriteriosFilasDA(String _CodigoUsuario, string _RazonSocial, string _NroFactura, string _Serie, decimal _Monto, DateTime _Fechini, DateTime _Fechfin, string _tipodoc)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            int filas = 0;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeListarFacturaCriteriosFilasConvert", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@UsuarioId", SqlDbType.VarChar, 50).Value = _CodigoUsuario;
                    oCommand.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 250).Value = _RazonSocial;
                    oCommand.Parameters.Add("@NroFactura", SqlDbType.Char, 20).Value = _NroFactura;
                    oCommand.Parameters.Add("@Serie", SqlDbType.Char, 20).Value = _Serie;
                    oCommand.Parameters.Add("@Monto", SqlDbType.Decimal).Value = _Monto;

                    oCommand.Parameters.Add("@nRetVal", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    oCommand.Parameters.Add("@fechini", SqlDbType.DateTime).Value = _Fechini;
                    oCommand.Parameters.Add("@fechfin", SqlDbType.DateTime).Value = _Fechfin;
                    oCommand.Parameters.Add("@tipo", SqlDbType.VarChar, 2).Value = _tipodoc;
                    oConnection.Open();
                    oCommand.ExecuteNonQuery();

                    filas = Convert.ToInt32(oCommand.Parameters["@nRetVal"].Value);
                }
            }
            return filas;
        }
        catch (Exception ex) { throw ex; }
    }


    public List<ReporteEmitido> ListarRepDocumentoCriteriosDA(String _CodigoUsuario, string _RazonSocial, string _NroFactura, string _Serie, decimal _Monto, int _nTamanhoPagina, int _nPaginaActual, DateTime _fechini, DateTime _fechfin, string _tipodoc)
    {
        try
        {
            List<ReporteEmitido> lRepFactura = new List<ReporteEmitido>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspMaeListarFacturaCriteriosConvert", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@UsuarioId", SqlDbType.VarChar, 50).Value = _CodigoUsuario;
                    oCommand.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 250).Value = _RazonSocial;
                    oCommand.Parameters.Add("@NroFactura", SqlDbType.Char, 20).Value = _NroFactura;
                    oCommand.Parameters.Add("@Serie", SqlDbType.Char, 20).Value = _Serie;
                    oCommand.Parameters.Add("@Monto", SqlDbType.Decimal).Value = _Monto;

                    oCommand.Parameters.Add("@nPaginaTamanho", SqlDbType.Int, 5).Value = _nTamanhoPagina;
                    oCommand.Parameters.Add("@nPaginaActual", SqlDbType.Int, 5).Value = _nPaginaActual;
                    oCommand.Parameters.Add("@fechini", SqlDbType.DateTime).Value = _fechini;
                    oCommand.Parameters.Add("@fechfin", SqlDbType.DateTime).Value = _fechfin;
                    oCommand.Parameters.Add("@tipo", SqlDbType.VarChar, 2).Value = _tipodoc;
                    oConnection.Open();
                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {
                                ReporteEmitido oRepFactura = new ReporteEmitido();
                                oRepFactura.IdDocumento = drListado.GetInt32(drListado.GetOrdinal("IDDOCS"));
                                oRepFactura.IdEmisor = drListado.GetString(drListado.GetOrdinal("IDEMISOR"));
                                oRepFactura.IdRango = drListado.GetInt32(drListado.GetOrdinal("IDRANGO"));
                                oRepFactura.Serie = drListado.GetString(drListado.GetOrdinal("SERIE"));
                                oRepFactura.NroDocumento = drListado.GetString(drListado.GetOrdinal("NRODOCUMENTO"));
                                oRepFactura.RazonSocial = drListado.GetString(drListado.GetOrdinal("RAZSOCIAL"));
                                oRepFactura.FechaEmision = drListado.GetDateTime(drListado.GetOrdinal("FECHAEMISION"));
                                oRepFactura.Moneda = drListado.GetString(drListado.GetOrdinal("TIPOMENEDA"));
                                oRepFactura.ImporteTotal = drListado.GetDecimal(drListado.GetOrdinal("IMPORTETOTAL"));
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




	public ReporteEmitido()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}