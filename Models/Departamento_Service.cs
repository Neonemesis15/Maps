using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class Departamento_Request
    {
        [JsonProperty("a")]
        public string codPais { get; set; }
    }

    public class Departamento_Response
    {
        [JsonProperty("a")]
        public List<E_Departamento> listaDepartamento { get; set; }
    }

    public class Departamento_Service
    {
        public List<E_Departamento> obtener_Departamentos(string codPais)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Departamento_Request oRequest = new Departamento_Request();
            oRequest.codPais = codPais;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Departamento_Request>(oRequest);
            dataJson = mapServices.Obtener_Departamento_Por_CodPais(request);

            Departamento_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Departamento_Response>(dataJson);

            return response.listaDepartamento;
        }
    }
}