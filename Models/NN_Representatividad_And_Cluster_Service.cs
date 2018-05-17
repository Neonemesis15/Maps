using Lucky.Entity.Common.Servicio;
using Newtonsoft.Json;

namespace Xplora.GIS.Models
{
    public class NN_Representatividad_And_Cluster_Request
    { 
        [JsonProperty("a")] 
        public string ubigeo { get; set; } 

        [JsonProperty("b")] 
        public string idPlanning { get; set; } 

        [JsonProperty("c")] 
        public string idReportsPlanning { get; set; }
    }

    public class NN_Representatividad_And_Cluster_Response
    {
        [JsonProperty("a")]
        public E_Representatividad_And_Cluster_NN listaRepresentatividad_And_Cluster_NN { get; set; }
    }

    public class NN_Representatividad_And_Cluster_Service
    {
        public NN_Representatividad_And_Cluster_Response obtener_Representatividad_And_Cluster(string ubigeo, string idPlanning, string idReportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            NN_Representatividad_And_Cluster_Request oRequest = new NN_Representatividad_And_Cluster_Request();
            oRequest.ubigeo = ubigeo;
            oRequest.idPlanning = idPlanning;
            oRequest.idReportsPlanning = idReportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<NN_Representatividad_And_Cluster_Request>(oRequest);
            dataJson = mapServices.Obtener_Representatividad_And_Cluster_NN(request);

            NN_Representatividad_And_Cluster_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<NN_Representatividad_And_Cluster_Response>(dataJson);

            return response;
        }
    }
}
