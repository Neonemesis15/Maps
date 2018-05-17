using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.CFG.JavaMovil;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class SemaforoZonaDistrito_Request
    {
        [JsonProperty("a")]
        public string codCanal { get; set; }

        [JsonProperty("b")]
        public string codDepartamento { get; set; }

        [JsonProperty("c")]
        public string codProvincia { get; set; }

        [JsonProperty("d")]
        public string codZona { get; set; }

        [JsonProperty("e")]
        public string codDistrito { get; set; }

        [JsonProperty("f")]
        public string codCategoria { get; set; }

        [JsonProperty("g")]
        public string codProducto { get; set; }

        [JsonProperty("h")]
        public string codPeriodo { get; set; }

        [JsonProperty("i")]
        public string opcion { get; set; }
    }

    public class SemaforoZonaDistrito_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaZonaDistritoMap> listSemaforoZonaDistritoMap { get; set; }
    }

    public class SemaforoZonaDistrito_Service
    {
        public List<E_PresenciaZonaDistritoMap> Obtener_Semaforo_ZonaDistrito(string codCanal, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto, string codPeriodo, string opcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            SemaforoZonaDistrito_Request oRequest = new SemaforoZonaDistrito_Request();
            oRequest.codCanal = codCanal;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codCategoria = codCategoria;
            oRequest.codProducto = codProducto;
            oRequest.codPeriodo = codPeriodo;
            oRequest.opcion = opcion;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<SemaforoZonaDistrito_Request>(oRequest);
            dataJson = mapServices.Obtener_PresenciaZonaDistritoMap(request);

            SemaforoZonaDistrito_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<SemaforoZonaDistrito_Response>(dataJson);

            return response.listSemaforoZonaDistritoMap;
        }
    }
}