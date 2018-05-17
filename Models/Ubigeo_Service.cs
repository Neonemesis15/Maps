using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucky.CFG.JavaMovil;
using Newtonsoft.Json;

namespace Xplora.GIS.Models
{
    public class E_DinamicArray
    {
        [JsonProperty("b")]
        public string[][] Contents { get; set; }
        [JsonProperty("a")]
        public string[] Header { get; set; }
    }


    public class llenarCombos_Request
    {
        [JsonProperty("a")]
        public string opcion { get; set; }
        [JsonProperty("b")]    
        public string filtros { get; set; }
    }

    public class llenarCombos_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_DinamicArray oE_DinamicArray { get; set; }
    }


    public class Ubigeo_Service
    {
        public E_DinamicArray Obtener_Mercados_Ubigeo(String codCanal, String codCompania, String tipoubigeo, String ubigeo)
        {
            CampaniaService.Ges_CampaniaServiceClient campaniServices = new CampaniaService.Ges_CampaniaServiceClient("BasicHttpBinding_IGes_CampaniaService");

            llenarCombos_Request oRequest = new llenarCombos_Request();
            oRequest.opcion = "NodeCommercial";
            oRequest.filtros = "" + codCanal + "," + codCompania + "," + tipoubigeo +","+ ubigeo;

            String request = Lucky.CFG.JavaMovil.HelperJson.Serialize<llenarCombos_Request>(oRequest);
            String dataJson = campaniServices.llenarCombos_Campania(request);

            llenarCombos_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<llenarCombos_Response>(dataJson);

            return response.oE_DinamicArray;
        }
    }
}
