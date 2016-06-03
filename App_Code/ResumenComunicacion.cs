using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de ResumenComunicacion
/// </summary>
public class ResumenComunicacion
{
	public ResumenComunicacion()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}


  

    public int NroFilasResCom(string tipodocumento, string fechain, string fechafin, string idemisor)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            int filas = 0;

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("NroFilasResCom", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;                   
                    oCommand.Parameters.Add("@tipodoc", SqlDbType.VarChar, 3).Value = tipodocumento;
                    oCommand.Parameters.Add("@fechain", SqlDbType.DateTime).Value = fechain;
                    oCommand.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = fechafin;
                    oCommand.Parameters.Add("@idemisor", SqlDbType.VarChar, 50).Value = idemisor;
                    oCommand.Parameters.Add("@fill", SqlDbType.Int).Direction = ParameterDirection.Output;

                    oConnection.Open();
                    oCommand.ExecuteNonQuery();

                    filas = Convert.ToInt32(oCommand.Parameters["@fill"].Value);
                }
            }
            return filas;
        }
        catch (Exception ex) { throw ex; }
    }




    public List<ResComunica> ResumenComunicacionBaja(int pag, int reg, string fechaini, string fechafin, string tipodocumento, string idemisor)
    {
        try
        {

            List<ResComunica> lobjComunicacion = new List<ResComunica>();
            String strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("ReporteComunicacion", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@Pagina", SqlDbType.Int, 3).Value = pag;
                    oCommand.Parameters.Add("@RegistrosporPagina", SqlDbType.Int, 3).Value = reg;
                    oCommand.Parameters.Add("@idemisor", SqlDbType.Char, 15).Value = idemisor;
                    oCommand.Parameters.Add("@tipodoc", SqlDbType.Char, 3).Value = tipodocumento;
                    oCommand.Parameters.Add("@FECHAINI", SqlDbType.DateTime).Value = fechaini;
                    oCommand.Parameters.Add("@FECHAFIN", SqlDbType.DateTime).Value = fechafin;
                  
                    oConnection.Open();
                    using (SqlDataReader drListado = oCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                    {
                        if (drListado.HasRows)
                        {
                            while ((drListado.Read()))
                            {
                                ResComunica objComunicacion = new ResComunica();
                                objComunicacion.idcomunicacion = drListado.GetInt32(drListado.GetOrdinal("IDCOMUNICACION"));
                                objComunicacion.nrodoc = drListado.GetString(drListado.GetOrdinal("NRODOCUMENTO"));
                                objComunicacion.serie = drListado.GetString(drListado.GetOrdinal("SERIE"));
                                objComunicacion.tipodoc = drListado.GetString(drListado.GetOrdinal("TIPODOC"));   
                                lobjComunicacion.Add(objComunicacion);
                            }
                        }
                    }
                }
            }
            return lobjComunicacion;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}