using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public class RechazossunatBE
{
    public string TipoDocumento { get; set; }
    public string NroDocumento { get; set; }
    public string Sucursal { get; set; }
    public string CodigoRechazo { get; set; }
    public string Descripcion { get; set; }
    public string FechaEmision { get; set; }
    public string UsuarioCreo { get; set; }
    public Int32 cantidad { get; set; }

    public List<RechazossunatBE> ListarReporteRechazosEXCELDA(string _Emisor, string _Fechaini, string _Fechafin,string _Tipodoc)
    {
        try
        {
            List<RechazossunatBE> lRepFactura = new List<RechazossunatBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTERECHAZOSSUNATEXCEL", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@IdEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@FINI", SqlDbType.VarChar, 10).Value = _Fechaini;
                    oCommand.Parameters.Add("@FFIN", SqlDbType.VarChar, 10).Value = _Fechafin;
                    oCommand.Parameters.Add("@TIPODOC", SqlDbType.VarChar, 2).Value = _Tipodoc;
                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RechazossunatBE oRepFactura = new RechazossunatBE();
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.NroDocumento = drListado.GetString(drListado.GetOrdinal("DOCUMENTO"));
                                oRepFactura.Sucursal = drListado.GetString(drListado.GetOrdinal("SUCURSAL"));
                                oRepFactura.CodigoRechazo = drListado.GetString(drListado.GetOrdinal("CODRECHAZO"));
                                oRepFactura.Descripcion = drListado.GetString(drListado.GetOrdinal("DESCRIPCION"));
                                oRepFactura.FechaEmision = drListado.GetString(drListado.GetOrdinal("FEMISION"));
                                oRepFactura.UsuarioCreo = drListado.GetString(drListado.GetOrdinal("USUARIOCREO"));
                                
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


    public List<RechazossunatBE> ListarReporteRechazosDA(string _Emisor, string _Fechaini, string _Fechafin, string _Tipodoc, int pageSize, int curPage)
    {
        try
        {
            List<RechazossunatBE> lRepFactura = new List<RechazossunatBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTERECHAZOSSUNAT", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@IdEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@FINI", SqlDbType.VarChar, 10).Value = _Fechaini;
                    oCommand.Parameters.Add("@FFIN", SqlDbType.VarChar, 10).Value = _Fechafin;
                    oCommand.Parameters.Add("@TIPODOC", SqlDbType.VarChar, 2).Value = _Tipodoc;
                    oCommand.Parameters.Add("@CURPAGE", SqlDbType.Int, 7).Value = pageSize;
                    oCommand.Parameters.Add("@PAGINA", SqlDbType.Int, 7).Value = curPage;
                    oConnection.Open();
                   
                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RechazossunatBE oRepFactura = new RechazossunatBE();
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("TIPODOC"));
                                oRepFactura.NroDocumento = drListado.GetString(drListado.GetOrdinal("DOCUMENTO"));
                                oRepFactura.Sucursal = drListado.GetString(drListado.GetOrdinal("SUCURSAL"));
                                oRepFactura.CodigoRechazo = drListado.GetString(drListado.GetOrdinal("CODRECHAZO"));
                                oRepFactura.Descripcion = drListado.GetString(drListado.GetOrdinal("DESCRIPCION"));
                                oRepFactura.FechaEmision = drListado.GetString(drListado.GetOrdinal("FEMISION"));
                                oRepFactura.UsuarioCreo = drListado.GetString(drListado.GetOrdinal("USUARIOCREO"));
                                
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

    public int ReporteRechazoscantidadDA(string _Emisor, string _Fechaini, string _Fechafin, string _Tipodoc)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            int filas = 0;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTERECHAZOSSUNATcantidad", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@IdEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@FINI", SqlDbType.VarChar, 10).Value = _Fechaini;
                    oCommand.Parameters.Add("@FFIN", SqlDbType.VarChar, 10).Value = _Fechafin;
                    oCommand.Parameters.Add("@TIPODOC", SqlDbType.VarChar, 2).Value = _Tipodoc;
                    oCommand.Parameters.Add("@cant", SqlDbType.Int, 7).Direction = ParameterDirection.Output;
                    oConnection.Open();
                    oCommand.ExecuteNonQuery();


                    filas = Convert.ToInt32(oCommand.Parameters["@cant"].Value);
                }
            }
            return filas;
        }
        catch (Exception ex) { throw ex; }
    }

    public List<RechazossunatBE> ListarReporteRechazosrESUMIDODA(string _Emisor, string _Fechaini, string _Fechafin, string _Tipodoc)
    {
        try
        {
            List<RechazossunatBE> lRepFactura = new List<RechazossunatBE>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTERECHAZOSSUNATEXCELRESUMIDO", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@IdEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@FINI", SqlDbType.VarChar, 10).Value = _Fechaini;
                    oCommand.Parameters.Add("@FFIN", SqlDbType.VarChar, 10).Value = _Fechafin;
                    oCommand.Parameters.Add("@TIPODOC", SqlDbType.VarChar, 2).Value = _Tipodoc;
                    oConnection.Open();

                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {

                                RechazossunatBE oRepFactura = new RechazossunatBE();
                                oRepFactura.TipoDocumento = drListado.GetString(drListado.GetOrdinal("SUCURSAL"));
                                oRepFactura.Sucursal = drListado.GetString(drListado.GetOrdinal("TIPDOC"));
                                oRepFactura.cantidad = drListado.GetInt32(drListado.GetOrdinal("CANT"));

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

	public RechazossunatBE()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}