using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Descripción breve de CargarDatosBE
/// </summary>
public class CargarDatosBE
{
    public string Codigo { get; set; }
    public string Denominacion { get; set; }
    public string CodigoRango { get; set; }
    public string Serie { get; set; }

    public List<CargarDatosBE> ListarSeriesDA(string _emisor, string _sede)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            List<CargarDatosBE> lPreliminarFactura = new List<CargarDatosBE>();

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_LISTARSERIES", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@CODEMISOR", SqlDbType.Char, 15).Value = _emisor;
                    oCommand.Parameters.Add("@SUCURSAL", SqlDbType.Char, 6).Value = _sede;
                    oConnection.Open();
                    using (SqlDataReader odr = oCommand.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                    {
                        if (odr.HasRows)
                        {
                            while (odr.Read())
                            {
                                CargarDatosBE oFacturaPreliminar = new CargarDatosBE();
                                oFacturaPreliminar.CodigoRango = Convert.ToString(odr.GetInt32(odr.GetOrdinal("codigorango")));
                                oFacturaPreliminar.Serie = odr.GetString(odr.GetOrdinal("serie"));


                                lPreliminarFactura.Add(oFacturaPreliminar);
                            }
                        }
                    }
                }
            }
            return lPreliminarFactura;
        }
        catch (Exception ex) { throw ex; }
    }

    public List<CargarDatosBE> ListarSedesDA(string _emisor)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            List<CargarDatosBE> lPreliminarFactura = new List<CargarDatosBE>();

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_LISTARSEDES", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@CODEMISOR", SqlDbType.Char, 15).Value = _emisor;
                    oConnection.Open();
                    using (SqlDataReader odr = oCommand.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                    {
                        if (odr.HasRows)
                        {
                            while (odr.Read())
                            {
                                CargarDatosBE oFacturaPreliminar = new CargarDatosBE();
                                oFacturaPreliminar.Codigo = odr.GetString(odr.GetOrdinal("codigo"));
                                oFacturaPreliminar.Denominacion = odr.GetString(odr.GetOrdinal("denominacion"));


                                lPreliminarFactura.Add(oFacturaPreliminar);
                            }
                        }
                    }
                }
            }
            return lPreliminarFactura;
        }
        catch (Exception ex) { throw ex; }
    }

      public int obtenerCorrelativoReportesDA(string _Emisor, string _tipodoc,string _fecha)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            int CORRE = 0;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_CORRELATIVOREPORTESCONVERTexcel", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@tipoREP", SqlDbType.VarChar, 5).Value = _tipodoc;
                    oCommand.Parameters.Add("@fecha", SqlDbType.VarChar, 10).Value = _fecha;
                    oCommand.Parameters.Add("@CORRE", SqlDbType.Int, 7).Direction = ParameterDirection.Output;
                    oConnection.Open();
                    oCommand.ExecuteNonQuery();


                    CORRE = Convert.ToInt32(oCommand.Parameters["@CORRE"].Value);
                }
            }
            return CORRE;
        }
        catch (Exception ex) { throw ex; }
    }


      public void actualizarestadodescargaexcelDA(string _Emisor, string _tipodoc, string _archivo,string _fecha)
      {
          try
          {
              string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

             using (SqlConnection oConnection = new SqlConnection(strConnection))
              {
                  using (SqlCommand oCommand = new SqlCommand("usp_ACTUALIZARREPORTESCONVERTexcel", oConnection))
                  {
                      oCommand.CommandType = CommandType.StoredProcedure;
                      oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                      oCommand.Parameters.Add("@tipoREP", SqlDbType.VarChar, 5).Value = _tipodoc;
                      oCommand.Parameters.Add("@archivo", SqlDbType.VarChar, 225).Value = _archivo;
                      oCommand.Parameters.Add("@fecha", SqlDbType.VarChar, 10).Value = _fecha;
                      oConnection.Open();
                      oCommand.ExecuteNonQuery();

                  }
              }
             
          }
          catch (Exception ex) { throw ex; }
      }

	public CargarDatosBE()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}