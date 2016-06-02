using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ReporteConsolidado
/// </summary>
public class ReporteConsolidado
{

    public int IdDocumento { get; set; }
    public string IdEmisor { get; set; }
    public int IdRango { get; set; }
    public string TipoDocumento { get; set; }
    public string Serie { get; set; }
    public string NroDocumento { get; set; }
    public string RazonSocial { get; set; }
    public string Sede { get; set; }
    public DateTime FechaEmision { get; set; }
    public decimal ImporteTotal { get; set; }
  
	public ReporteConsolidado()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}