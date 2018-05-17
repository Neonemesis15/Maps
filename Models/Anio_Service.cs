using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class Anio_Response
    {
        [JsonProperty("a")]
        public List<E_Anio> oListaAnios { get; set; }
    }

    public class Anio_Service
    {
        public List<E_Anio> obtener_Anios()
        {
            CampaniaService.Ges_CampaniaServiceClient campaniServices = new CampaniaService.Ges_CampaniaServiceClient("BasicHttpBinding_IGes_CampaniaService");

            string dataJson;

            dataJson = campaniServices.Listar_Anios();

            Anio_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Anio_Response>(dataJson);

            return response.oListaAnios;
        }
    }
}