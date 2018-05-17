using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.CFG.JavaMovil;
using Lucky.Entity.Common.Servicio;

namespace Xplora.GIS.Models
{
    #region XploraMaps -Lima
    public class Representatividad_And_Cluster_Request
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

    public class Representatividad_And_Cluster_Response
    {
        [JsonProperty("a")]
        public E_ClusterZonaDistrito_Group clusterZonaDistritoMap { get; set; }

        [JsonProperty("b")]
        public E_Representatividad_Group representatividadZonaDistritoMap { get; set; }
    }
    #endregion

    #region XploraMaps - Provincias
    public class Representatividad_And_Cluster_Prov_Request
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

    public class Representatividad_And_Cluster_Prov_Response
    {
        [JsonProperty("a")]
        public E_ClusterZonaDistrito_Group clusterZonaDistritoMap { get; set; }

        [JsonProperty("b")]
        public E_Representatividad_Group representatividadZonaDistritoMap { get; set; }
    }
    #endregion


    

    public class ClusterRepresentatividad_Service
    {
        //XploraMaps - Lima
        public Representatividad_And_Cluster_Response Obtener_Cluster_Representatividad(string codZona, string codDistrito, string idPlanning, string reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Representatividad_And_Cluster_Request oRequest = new Representatividad_And_Cluster_Request();
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.idPlanning = idPlanning;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Representatividad_And_Cluster_Request>(oRequest);
            dataJson = mapServices.Obtener_Representatividad_And_Cluster(request);

            Representatividad_And_Cluster_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Representatividad_And_Cluster_Response>(dataJson);

            return response;
        }
        //XploraMaps - Provincias
        public Representatividad_And_Cluster_Prov_Response Obtener_Cluster_Representatividad_Prov(string codOficina,string codZona, string codDistrito, string idPlanning, string reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Representatividad_And_Cluster_Prov_Request oRequest = new Representatividad_And_Cluster_Prov_Request();
            oRequest.codOficina = codOficina;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.idPlanning = idPlanning;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Representatividad_And_Cluster_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_Representatividad_And_Cluster_Prov(request);

            Representatividad_And_Cluster_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Representatividad_And_Cluster_Prov_Response>(dataJson);

            return response;
        }


        #region XploraNacional

        #region Obtener_Cluster_Representatividad_NN
        
            public class Obtener_Representatividad_And_Cluster_NN_Request
        {
            [JsonProperty("a")]
            public string ubigeo { get; set; }

            [JsonProperty("b")]
            public string idPlanning { get; set; }

            [JsonProperty("c")]
            public string idReportsPlanning { get; set; }

        }
            
            public class Obtener_Representatividad_And_Cluster_NN_Response : BaseResponse
        {
            [JsonProperty("a")]
            public E_Representatividad_And_Cluster_NN listaRepresentatividad_And_Cluster_NN { get; set; }
        }

            public class E_Representatividad_And_Cluster_NN
            {

                [JsonProperty("b")]
                public List<E_Cluster_NN> oListE_Cluster_NN { get; set; }
                [JsonProperty("a")]
                public List<E_Representatividad_NN> oListE_Representatividad_NN { get; set; }
            }
        
            public class E_Cluster_NN
        {
            [JsonProperty("b")]
            public string cantidad { get; set; }
            [JsonProperty("c")]
            public string codigo { get; set; }
            [JsonProperty("a")]
            public string descripcion { get; set; }
            [JsonProperty("d")]
            public int posicion { get; set; }
        }

            public class E_Representatividad_NN
            {

                [JsonProperty("b")]
                public int cantidad { get; set; }
                [JsonProperty("a")]
                public string nombre { get; set; }
                [JsonProperty("c")]
                public int posicion { get; set; }
            }

            //Xplora Maps - Nivel Nacional
            /*
            public class Obtener_Representatividad_And_Cluster_NN_Mod_Request
            {
                [JsonProperty("a")]
                public string ubigeo { get; set; }
                [JsonProperty("b")]
                public string idPlanning { get; set; }
                [JsonProperty("c")]
                public string idReportsPlanning { get; set; }
            }*/
            
            public class Obtener_Representatividad_And_Cluster_NN_Mod_Response : BaseResponse
            {
                [JsonProperty("a")]
                public List<E_TblDinamica> oListE_TblDinamica { get; set; }
            }


            public class E_TblDinamica
            {
                [JsonProperty("d")]
                public List<E_TblHead> heads { get; set; }
                [JsonProperty("a")]
                public string title_name { get; set; }
                [JsonProperty("c")]
                public int title_order { get; set; }
                [JsonProperty("b")]
                public string title_value { get; set; }
            }

            public class E_TblHead
            {
                [JsonProperty("f")]
                public List<E_TblDetail> details { get; set; }
                [JsonProperty("a")]
                public string head_name { get; set; }
                [JsonProperty("e")]
                public int head_order { get; set; }
                [JsonProperty("b")]
                public string head_value { get; set; }
                [JsonProperty("c")]
                public string pie_name { get; set; }
                [JsonProperty("d")]
                public string pie_value { get; set; }
            }

            public class E_TblDetail
            {
                [JsonProperty("a")]
                public string detail_name { get; set; }
                [JsonProperty("d")]
                public int detail_orden { get; set; }
                [JsonProperty("c")]
                public string detail_percentage { get; set; }
                [JsonProperty("b")]
                public string detail_value { get; set; }
            }

            public List<E_TblDinamica> Obtener_Representatividad_And_Cluster_NN_Mod(string ubigeo, string idPlanning, string idReportsPlanning)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                Obtener_Representatividad_And_Cluster_NN_Mod_Request oRequest = new Obtener_Representatividad_And_Cluster_NN_Mod_Request();
                oRequest.idPlanning = idPlanning;
                oRequest.idReportsPlanning = idReportsPlanning;
                oRequest.ubigeo = ubigeo;

                string request;
                string dataJson;

                request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_Representatividad_And_Cluster_NN_Mod_Request>(oRequest);
                dataJson = mapServices.Obtener_Representatividad_And_Cluster_NN_Mod(request);

                Obtener_Representatividad_And_Cluster_NN_Mod_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_Representatividad_And_Cluster_NN_Mod_Response>(dataJson);

                return response.oListE_TblDinamica;
            }


        #endregion

        #endregion


        #region XploraMaps - Minoristas

        public class Obtener_Representatividad_And_Cluster_NN_Mod_Request { 
            [JsonProperty("a")]
            public string ubigeo{get;set;}
            [JsonProperty("b")]
            public string idPlanning{get;set;}
            [JsonProperty("c")]
            public string idReportsPlanning{get; set;}
            [JsonProperty("d")] //Add 28-05-2013
            public string otrosParametros { get; set; }
        }
        /*
        public class Obtener_Representatividad_And_Cluster_NN_Mod_Response : BaseResponse {
            [JsonProperty("a")]
            public List<E_TblDinamica> oListE_TblDinamica { get; set; }
        }*/

        public List<E_TblDinamica> Obtener_UniversoMR_Minorista(String ubigeo, String idPlanning, String idReportsPlanning, String otrosParametros) {

            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");
            Obtener_Representatividad_And_Cluster_NN_Mod_Request oRequest = new Obtener_Representatividad_And_Cluster_NN_Mod_Request();
            oRequest.ubigeo = ubigeo;
            oRequest.idPlanning = idPlanning;
            oRequest.idReportsPlanning = idReportsPlanning;
            oRequest.otrosParametros = otrosParametros;

            String request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_Representatividad_And_Cluster_NN_Mod_Request>(oRequest);
            String dataJson = mapServices.Obtener_Representatividad_And_Cluster_NN_Mod_V1_Rev02(request);

            Obtener_Representatividad_And_Cluster_NN_Mod_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_Representatividad_And_Cluster_NN_Mod_Response>(dataJson);
            return response.oListE_TblDinamica;
        }

        #endregion

    }

}