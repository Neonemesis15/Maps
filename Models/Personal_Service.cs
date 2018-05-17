using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucky.CFG.JavaMovil;
using Newtonsoft.Json;

namespace Xplora.GIS.Models
{
    public class Listar_Generadores_Por_CodSupervisor_Request
    {
        [JsonProperty("a")]
        public string CodCompania { get; set; }

        [JsonProperty("b")]
        public string CodSupervisor { get; set; }
    }

    public class Listar_Generadores_Por_CodSupervisor_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<Lucky.Entity.Common.Servicio.E_Persona> oListaGenerador { get; set; }
    }

    public class Personal_Service
    {
        public List<Lucky.Entity.Common.Servicio.E_Persona> Obtener_Generadores_Por_CodCampania_Por_CodSupervisor(string codCampania, string codSupervisor)
        {

            CampaniaService.Ges_CampaniaServiceClient campaniaServices = new CampaniaService.Ges_CampaniaServiceClient("BasicHttpBinding_IGes_CampaniaService");

            Listar_Generadores_Por_CodSupervisor_Request oRequest = new Listar_Generadores_Por_CodSupervisor_Request();
            oRequest.CodCompania = codCampania;
            oRequest.CodSupervisor = codSupervisor;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Listar_Generadores_Por_CodSupervisor_Request>(oRequest);
            dataJson = campaniaServices.Listar_Generadores_Por_CodCampania_Por_CodSupervisor(request);

            Listar_Generadores_Por_CodSupervisor_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Listar_Generadores_Por_CodSupervisor_Response>(dataJson);

            return response.oListaGenerador;
        }  
    }
}