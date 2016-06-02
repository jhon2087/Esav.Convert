using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Descripción breve de ReporteRegularizarBE
/// </summary>
public class ReporteRegularizarBE
{
    public string NombTxt { get; set; }
    public string Sede { get; set; }
    public string FEmision { get; set; }
    public string TipoDocumento { get; set; }
    public string Serie { get; set; }
    public string Correlativo { get; set; }
    public string DetalleError { get; set; }


    public List<ReporteRegularizarBE> obtenerreporteRegularizarpi2DA(string _emisor, string fecha, string sede, string serie)
    {
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["dbDocument"].ConnectionString;

            List<ReporteRegularizarBE> lPreliminarFactura = new List<ReporteRegularizarBE>();

            using (SqlConnection oConnection = new SqlConnection(strConnection))
            {
                using (SqlCommand oCommand = new SqlCommand("usp_REPORTEREGULARIZAIONAPI2", oConnection))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.Add("@FECHH", SqlDbType.Char, 10).Value = fecha;
                    oCommand.Parameters.Add("@IDEMISOR", SqlDbType.Char, 15).Value = _emisor;
                    oCommand.Parameters.Add("@sede", SqlDbType.Char, 15).Value = sede;
                    oCommand.Parameters.Add("@serie", SqlDbType.Char, 15).Value = serie;
                    oConnection.Open();
                    using (SqlDataReader odr = oCommand.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                    {
                        if (odr.HasRows)
                        {
                            while (odr.Read())
                            {
                                ReporteRegularizarBE oFacturaPreliminar = new ReporteRegularizarBE();
                                oFacturaPreliminar.NombTxt = odr.GetString(odr.GetOrdinal("NOMBREDOC"));
                                oFacturaPreliminar.Sede = odr.GetString(odr.GetOrdinal("SEDE"));
                                oFacturaPreliminar.FEmision = odr.GetString(odr.GetOrdinal("FECHEMISION"));
                                oFacturaPreliminar.TipoDocumento = odr.GetString(odr.GetOrdinal("TIPODOCUMENTO"));
                                oFacturaPreliminar.Serie = odr.GetString(odr.GetOrdinal("SERIE"));
                                oFacturaPreliminar.Correlativo = odr.GetString(odr.GetOrdinal("NRODOC"));
                                

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



	public ReporteRegularizarBE()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}