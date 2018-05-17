using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class Mes_Response
    {
        [JsonProperty("a")]
        public List<E_Mes> oListaMes { get; set; }
    }

    public class Mes_Service
    {
        public List<E_Mes> obtener_Meses()
        {
            CampaniaService.Ges_CampaniaServiceClient campaniServices = new CampaniaService.Ges_CampaniaServiceClient("BasicHttpBinding_IGes_CampaniaService");

            string dataJson;

            dataJson = campaniServices.Listar_Meses();

            Mes_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Mes_Response>(dataJson);

            return response.oListaMes;
        }
    }
}