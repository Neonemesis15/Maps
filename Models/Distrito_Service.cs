using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class Distrito_Request
    {
        [JsonProperty("a")]
        public string codPais { get; set; }

        [JsonProperty("b")]
        public string codDepartamento { get; set; }

        [JsonProperty("c")]
        public string codProvincia { get; set; }

        [JsonProperty("d")]
        public string codSector { get; set; }
    }

    public class Distrito_Response
    {
        [JsonProperty("a")]
        public List<E_Distrito> listaDistrito { get; set; }
    }

    public class Distrito_Service
    {
        public List<E_Distrito> obtener_Distritos(string codPais, string codDepartamento, string codProvincia, string codSector)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Distrito_Request oRequest = new Distrito_Request();
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codSector = codSector;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Distrito_Request>(oRequest);
            dataJson = mapServices.Obtener_Distrito_Por_CodSector(request);

            Distrito_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Distrito_Response>(dataJson);

            return response.listaDistrito;
        }
    }
}