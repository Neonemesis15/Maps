using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class Provincia_Request
    {
        [JsonProperty("a")]
        public string codPais { get; set; }

        [JsonProperty("b")]
        public string codDepartamento { get; set; }
    }

    public class Provincia_Response
    {
        [JsonProperty("a")]
        public List<E_Provincia> listaProvincias { get; set; }
    }

    public class Provincia_Service
    {
        public List<E_Provincia> obtener_Provincias(string codPais, string codDepartamento)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Provincia_Request oRequest = new Provincia_Request();
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Provincia_Request>(oRequest);
            dataJson = mapServices.Obtener_Provincia_Por_CodDepartamento(request);

            Provincia_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Provincia_Response>(dataJson);

            return response.listaProvincias;
        }
    }
}