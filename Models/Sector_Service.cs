using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class Sector_request
    {
        [JsonProperty("a")]
        public string codPais { get; set; }

        [JsonProperty("b")]
        public string codDepartamento { get; set; }

        [JsonProperty("c")]
        public string codProvincia { get; set; }
    }

    public class Sector_Response
    {
        [JsonProperty("a")]
        public List<E_Sector> listaSector { get; set; }
    }

    public class Sector_Service
    {
        public List<E_Sector> obtener_sector(string codPais, string codDepartamento, string codProvincia)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Sector_request oRequest = new Sector_request();
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Sector_request>(oRequest);
            dataJson = mapServices.Obtener_Sector_Por_PaisDepartamentoProvincia(request);

            Sector_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Sector_Response>(dataJson);

            return response.listaSector;
        }
    }
}