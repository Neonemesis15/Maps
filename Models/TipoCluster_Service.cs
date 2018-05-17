using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucky.CFG.JavaMovil;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    public class TipoCluster_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_TipoCluster> ListTipoCluster { get; set; }
    }

    public class TipoCluster_Service
    {
        public List<E_TipoCluster> obtener_TipoCluster()
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            string dataJson;

            dataJson = mapServices.Obtener_TipoCluster();

            TipoCluster_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<TipoCluster_Response>(dataJson);

            return response.ListTipoCluster;
        }
    }
}