using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class Periodo_Request
    {
        [JsonProperty("a")]
        public string CodServicio { get; set; }

        [JsonProperty("b")]
        public string CodCanal { get; set; }

        [JsonProperty("c")]
        public string CodCliente { get; set; }

        [JsonProperty("d")]
        public string CodReporte { get; set; }

        [JsonProperty("e")]
        public string Anio { get; set; }

        [JsonProperty("f")]
        public string Mes { get; set; }
    }

    public class Periodo_Response
    {
        [JsonProperty("a")]
        public List<E_Periodo> oListaPeriodo { get; set; }
    }

    public class Periodo_Service
    {
        public List<E_Periodo> obtener_Periodo(string CodServicio, string CodCanal, string CodCliente, string CodReporte, string Anio, string Mes)
        {
            CampaniaService.Ges_CampaniaServiceClient campaniServices = new CampaniaService.Ges_CampaniaServiceClient("BasicHttpBinding_IGes_CampaniaService");
            Periodo_Request oRequest = new Periodo_Request();
            oRequest.CodServicio = CodServicio;
            oRequest.CodCanal = CodCanal;
            oRequest.CodCliente = CodCliente;
            oRequest.CodReporte = CodReporte;
            oRequest.Anio = Anio;
            oRequest.Mes = Mes;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Periodo_Request>(oRequest);
            dataJson = campaniServices.Listar_Periodo_Por_CodServicio_CodCanal_CodCliente_CodReporte_Anio_Mes(request);

            Periodo_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Periodo_Response>(dataJson);

            return response.oListaPeriodo;
        }
    }
}