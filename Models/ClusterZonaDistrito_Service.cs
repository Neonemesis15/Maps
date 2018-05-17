using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucky.CFG.JavaMovil;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    #region XploraMaps - Lima
    public class ClusterZonaDistrito_Request
    {
        [JsonProperty("a")]
        public string codZona { get; set; }

        [JsonProperty("b")]
        public string codDistrito { get; set; }

        [JsonProperty("c")]
        public string idPlanning { get; set; }

        [JsonProperty("d")]
        public string reportsPlanning { get; set; }
    }
    public class ClusterZonaDistrito_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ClusterZonaDistrito_Group clusterZonaDistritoMap { get; set; }
    }
    #endregion

    #region XploraMaps - Provincia
    public class ClusterZonaDistrito_Prov_Request
    {
        [JsonProperty("a")]
        public string codZona { get; set; }

        [JsonProperty("b")]
        public string codDistrito { get; set; }

        [JsonProperty("c")]
        public string idPlanning { get; set; }

        [JsonProperty("d")]
        public string reportsPlanning { get; set; }

        [JsonProperty("e")]//Add
        public string codOficina { get; set; }
    }
    public class ClusterZonaDistrito_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ClusterZonaDistrito_Group clusterZonaDistritoMap { get; set; }
    }
    #endregion


    public class ClusterZonaDistrito_Service
    {
        //Xplora - Lima
        public E_ClusterZonaDistrito_Group Obtener_Cluster_ZonaDistrito(string codZona, string codDistrito, string idPlanning, string reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            ClusterZonaDistrito_Request oRequest = new ClusterZonaDistrito_Request();
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.idPlanning = idPlanning;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<ClusterZonaDistrito_Request>(oRequest);
            dataJson = mapServices.Obtener_ClusterZonaDistritoMap(request);

            ClusterZonaDistrito_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<ClusterZonaDistrito_Response>(dataJson);

            return response.clusterZonaDistritoMap;
        }
        //Xplora - Provincia
        public E_ClusterZonaDistrito_Group Obtener_Cluster_ZonaDistrito_Prov(string codOficina, string codZona, string codDistrito, string idPlanning, string reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            ClusterZonaDistrito_Prov_Request oRequest = new ClusterZonaDistrito_Prov_Request();
            oRequest.codOficina = codOficina;   //new
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.idPlanning = idPlanning;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<ClusterZonaDistrito_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_ClusterZonaDistritoMap_Prov(request);

            ClusterZonaDistrito_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<ClusterZonaDistrito_Prov_Response>(dataJson);

            return response.clusterZonaDistritoMap;
        }
    }
    


}