using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de ConfiguracionBE
/// </summary>
public class ConfiguracionBE
{
    public string NombTxt { get; set; }

    public string obtenerconfiguracionlogoDA(string _Emisor)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            string filas = "";

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("uspdatosconfiguracion", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.VarChar, 15).Value = _Emisor;
                    oCommand.Parameters.Add("@LOGO", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    oConnection.Open();
                    oCommand.ExecuteNonQuery();


                    filas = Convert.ToString(oCommand.Parameters["@LOGO"].Value);
                }
            }
            return filas;
        }
        catch (Exception ex) { throw ex; }
    }

	public ConfiguracionBE()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}