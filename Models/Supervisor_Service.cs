using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class Supervisor_Request
    {
        [JsonProperty("a")]
        public string codEquipo { get; set; }
    }

    public class Supervisor_Response
    {
        [JsonProperty("a")]
        public List<E_Persona> oListaSupervisor { get; set; }
    }

    public class Supervisor_Service
    {
        public List<E_Persona> obtener_supervisor(string codEquipo)
        {
            CampaniaService.Ges_CampaniaServiceClient campaniaServices = new CampaniaService.Ges_CampaniaServiceClient("BasicHttpBinding_IGes_CampaniaService");

            Supervisor_Request oRequest = new Supervisor_Request();
            oRequest.codEquipo = codEquipo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Supervisor_Request>(oRequest);
            dataJson = campaniaServices.Listar_Supervisor_Por_CodCampania(request);

            Supervisor_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Supervisor_Response>(dataJson);

            return response.oListaSupervisor;
        }
    }
}