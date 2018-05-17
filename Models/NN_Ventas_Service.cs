using System.Collections.Generic;
using Lucky.Entity.Common.Servicio;
using Newtonsoft.Json;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    #region Request

        public class Obtener_Ventas_NN_Mod_Request
        {
            [JsonProperty("a")]
            public string ubigeo { get; set; }

            [JsonProperty("b")]
            public string idReportsPlanning { get; set; }

            [JsonProperty("c")]
            public string otrosParametros { get; set; }
        }

        public class E_Ventas_NN_Mod
        {

            [JsonProperty("b")]
            public string categoria { get; set; }
            [JsonProperty("a")]
            public string id_categoria { get; set; }
            [JsonProperty("c")]
            public List<E_Ventas_NN_Sku> oList_Sku { get; set; }
            [JsonProperty("d")]
            public List<E_Sum_Dist_Cat> oList_Sum { get; set; }
            [JsonProperty("e")]
            public double total { get; set; }
        }
        
        public class E_Ventas_NN_Sku
        {

            [JsonProperty("a")]
            public string cod_sku { get; set; }
            [JsonProperty("c")]
            public List<E_Ventas_NN_Dist> oList_Dist { get; set; }
            [JsonProperty("b")]
            public string sku_nombre { get; set; }
            [JsonProperty("d")]
            public double sum_cat_sku { get; set; }
        }
        
        public class E_Ventas_NN_Dist
        {

            [JsonProperty("b")]
            public string distribuidora { get; set; }
            [JsonProperty("a")]
            public string id_distribuidora { get; set; }
            [JsonProperty("c")]
            public double ventas { get; set; }
        }

        public class E_Sum_Dist_Cat
        {

            [JsonProperty("a")]
            public string id_distribuidora { get; set; }
            [JsonProperty("b")]
            public double sum_cat_dist { get; set; }
        }

        public class Obtener_PdvByVentasToExcel_Request
        {
            [JsonProperty("a")]
            public string codCliente { get; set; }
            [JsonProperty("b")]
            public string codCanal { get; set; }
            [JsonProperty("c")]
            public string ubigeo { get; set; }
            [JsonProperty("d")]
            public string codSku { get; set; }
            [JsonProperty("e")]
            public string codPeriodo { get; set; }
        }


    #endregion


    #region Response

        public class Obtener_Ventas_NN_Response : BaseResponse
        {
            [JsonProperty("a")]
            public List<E_Ventas_NN> listaVentas_NN { get; set; }
        }

        public class Obtener_PdvByVentasToExcel_Response : BaseResponse
        {
            [JsonProperty("a")]
            public E_ExportExcel oExportExcel { get; set; }
        }

        public class Obtener_Ventas_NN_Mod_Response : BaseResponse
        {
            [JsonProperty("a")]
            public List<E_Ventas_NN_Mod> listaVentas_NN { get; set; }
        }
        

    #endregion


    

    

    public class NN_Ventas_Service
    {
        public Obtener_Ventas_NN_Mod_Response obtener_Ventas_NN(string ubigeo, string idReportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_Ventas_NN_Mod_Request oRequest = new Obtener_Ventas_NN_Mod_Request();
            oRequest.ubigeo = ubigeo;
            oRequest.idReportsPlanning = idReportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_Ventas_NN_Mod_Request>(oRequest);
            dataJson = mapServices.Obtener_Ventas_NN(request);

            Obtener_Ventas_NN_Mod_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_Ventas_NN_Mod_Response>(dataJson);

            return response;
        }

        public IList<E_Ventas_NN_Mod> obtener_Ventas_NN_Mod(string ubigeo, string reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_Ventas_NN_Mod_Request oRequest = new Obtener_Ventas_NN_Mod_Request();
            oRequest.ubigeo = ubigeo;
            oRequest.idReportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_Ventas_NN_Mod_Request>(oRequest);
            dataJson = mapServices.Obtener_Ventas_NN_Mod(request);

            Obtener_Ventas_NN_Mod_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_Ventas_NN_Mod_Response>(dataJson);

            return response.listaVentas_NN;
        }

        public class E_ExportExcel
        {
            

            [JsonProperty("b")]
            public string[][] Contents { get; set; }
            [JsonProperty("a")]
            public string[] Header { get; set; }
        }

        public List<E_Ventas_NN_Mod> Obtener_Ventas_SubCategoria(string ubigeo, string idReportPlanning, string otrosparametros){

            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_Ventas_NN_Mod_Request oRequest = new Obtener_Ventas_NN_Mod_Request();
            oRequest.ubigeo = ubigeo;
            oRequest.idReportsPlanning = idReportPlanning;
            oRequest.otrosParametros = otrosparametros;

            string request = Lucky.CFG.JavaMovil.HelperJson.Serialize <Obtener_Ventas_NN_Mod_Request>(oRequest);
            string data = mapServices.Obtener_Presencia_EleVisibilidad_NN_V1_Rev02(request);

            Obtener_Ventas_NN_Mod_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_Ventas_NN_Mod_Response>(data);

            return response.listaVentas_NN;
        }


        #region Rev02

            public IList<E_Ventas_NN_Mod> Obtener_Ventas_Rev02(string ubigeo, string idReportsPlanning, string otrosParametros)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");
                Obtener_Ventas_NN_Mod_Request oRequest = new Obtener_Ventas_NN_Mod_Request();
                oRequest.ubigeo = ubigeo;
                oRequest.idReportsPlanning = idReportsPlanning;
                oRequest.otrosParametros = otrosParametros;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_Ventas_NN_Mod_Request>(oRequest);
                string dataJson = mapServices.Obtener_Ventas_NN_Mod_V1_Rev02(request);

                Obtener_Ventas_NN_Mod_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_Ventas_NN_Mod_Response>(dataJson);
                return response.listaVentas_NN;
            }

        #endregion

    }
    
}