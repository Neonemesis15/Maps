using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;
using Lucky.CFG.JavaMovil;


namespace Xplora.GIS.Models
{
    public class OficinasPorPersona_Request
    {
        [JsonProperty("a")]
        public int CodPersona { get; set; }

        [JsonProperty("b")]
        public string CodCanal { get; set; }

        [JsonProperty("c")]
        public int CodCompania { get; set; }
    }
    public class OficinasPorPersona_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_Oficina> Oficinas { get; set; }
    }

    public class Oficina_Service
    {
        public OficinasPorPersona_Response Obtener_OficinasPorCanalAndCompania(string codCanal, string codCompania)
        {
            ReportClientService.Ges_ReporteClienteClient reportClientService = new ReportClientService.Ges_ReporteClienteClient("BasicHttpBinding_IGes_ReporteCliente");

            OficinasPorPersona_Request oRequest = new OficinasPorPersona_Request();
            oRequest.CodPersona = 0;
            oRequest.CodCanal = codCanal;
            oRequest.CodCompania = int.Parse(codCompania.ToString());

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<OficinasPorPersona_Request>(oRequest);
            dataJson = reportClientService.Obtener_OficinasPorCodPersonaAndCanalAndCompania(request);

            OficinasPorPersona_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<OficinasPorPersona_Response>(dataJson);
            return response;
        }
    }
}