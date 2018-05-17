using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    #region Request
        public class PresenciaEleVisibilidad_Request
        {
            [JsonProperty("a")]
            public int servicio { get; set; }

            [JsonProperty("b")]
            public string canal { get; set; }

            [JsonProperty("c")]
            public int codCliente { get; set; }

            [JsonProperty("d")]
            public string ubigeo { get; set; }

            [JsonProperty("e")]
            public int reportsPlanning { get; set; }
        }



        public class Obtener_Presencia_ElemeVisibilidad_NN_Request
        {
            [JsonProperty("a")]
            public string servicio { get; set; }

            [JsonProperty("b")]
            public string canal { get; set; }

            [JsonProperty("c")]
            public string codCliente { get; set; }

            [JsonProperty("d")]
            public string ubigeo { get; set; }

            [JsonProperty("e")]
            public string reportsPlanning { get; set; }

            [JsonProperty("f")] //Add 28-05-2013 - Psa
            public string otrosParametros { get; set; }

        }


    #endregion

    #region Response

    public class Obtener_Presencia_ZonaDistrito_Din_Response
    {
        [JsonProperty("a")]
        public List<E_PresenciaZonaDistrito> listaPresencia { get; set; }

        [JsonProperty("b")]
        public List<E_ElemVisibilidad> listaElemVisibilidad { get; set; }
    }

    public class E_ElemVisibilidad
    {
        //public E_ElemVisibilidad();

        [JsonProperty("a")]
        public string cod_compania { get; set; }
        [JsonProperty("c")]
        public List<E_ElemVisibilidad_Detalle> detalle { get; set; }
        [JsonProperty("b")]
        public string nombre_compania { get; set; }
    }

    public class E_ElemVisibilidad_Detalle
    {
        /*public E_ElemVisibilidad_Detalle();*/

        [JsonProperty("a")]
        public string cod_elemento { get; set; }
        [JsonProperty("c")]
        public string nombre_elemento { get; set; }
        [JsonProperty("b")]
        public string valor_elemento { get; set; }
    }


    public class Obtener_Presencia_ElemeVisibilidad_NN_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaNN> listaPresencia { get; set; }

        [JsonProperty("b")]
        public List<E_ElemVisibilidad> listaElementosVisibilidad { get; set; }

        
    }

    #endregion
    
    public class PresenciaEleVisibilidad_Service
    {
        public Obtener_Presencia_ZonaDistrito_Din_Response obtener_PresenciaEleVisibilidad(int servicio, string canal, int codCliente, string ubigeo, int reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PresenciaEleVisibilidad_Request oRequest = new PresenciaEleVisibilidad_Request();
            oRequest.servicio = servicio;
            oRequest.canal = canal;
            oRequest.codCliente = codCliente;
            oRequest.ubigeo = ubigeo;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PresenciaEleVisibilidad_Request>(oRequest);
            dataJson = mapServices.Obtener_Presencia_EleVisibilidad_NN(request);

            Obtener_Presencia_ZonaDistrito_Din_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_Presencia_ZonaDistrito_Din_Response>(dataJson);

            return response;
        }

        /*
         public class Obtener_Presencia_ElemeVisibilidad_NN_Request
    {
        [JsonProperty(""a"")]
        public int servicio { get; set; }

        [JsonProperty(""b"")]
        public string canal { get; set; }

        [JsonProperty(""c"")]
        public int codCliente { get; set; }

        [JsonProperty(""d"")]
        public string ubigeo { get; set; }

        [JsonProperty(""e"")]
        public int reportsPlanning { get; set; }

        [JsonProperty(""f"")] //Add 28-05-2013 - Psa
        public string otrosParametros { get; set; }
    }
    public class Obtener_Presencia_ElemeVisibilidad_NN_Response : BaseResponse
    {
        [JsonProperty(""a"")]
        public List<E_PresenciaNN> listaPresencia { get; set; }

        [JsonProperty(""b"")]
        public List<E_ElemVisibilidad> listaElementosVisibilidad { get; set; }

        
    }
    */


        public Obtener_Presencia_ElemeVisibilidad_NN_Response Obtener_Elementos_Visibilidad(String servicio, String canal, String codCliente, String ubigeo, String idReportsPlanning, String otrosParametros)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_Presencia_ElemeVisibilidad_NN_Request oRequest = new Obtener_Presencia_ElemeVisibilidad_NN_Request();
            oRequest.servicio = servicio;
            oRequest.codCliente = codCliente;
            oRequest.canal = canal;
            oRequest.ubigeo = ubigeo;
            oRequest.reportsPlanning = idReportsPlanning;
            oRequest.otrosParametros = otrosParametros;

            String request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_Presencia_ElemeVisibilidad_NN_Request>(oRequest);
            String data = mapServices.Obtener_Presencia_EleVisibilidad_NN_V1_Rev02(request);


            Obtener_Presencia_ElemeVisibilidad_NN_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_Presencia_ElemeVisibilidad_NN_Response>(data);

            return response;
        } 
    
    
    }
}