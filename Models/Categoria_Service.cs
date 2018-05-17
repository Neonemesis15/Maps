using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    public class Categoria_Por_CodCampania_y_CodReporte_Request
    {
        [JsonProperty("a")]
        public string CodEquipo { get; set; }

        [JsonProperty("b")]
        public string CodReporte { get; set; }
    }

    public class Categoria_Por_CodCampania_y_CodReporte_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<Lucky.Entity.Common.Servicio.E_Categoria> oListaCategoria { get; set; }
    }

    public class Categoria_Service
    {
        public List<Lucky.Entity.Common.Servicio.E_Categoria> obtener_Categoria(string CodEquipo, string CodReporte)
        {
            CampaniaService.Ges_CampaniaServiceClient campaniServices = new CampaniaService.Ges_CampaniaServiceClient("BasicHttpBinding_IGes_CampaniaService");
            Categoria_Por_CodCampania_y_CodReporte_Request oRequest = new Categoria_Por_CodCampania_y_CodReporte_Request();
            oRequest.CodEquipo = CodEquipo;
            oRequest.CodReporte = CodReporte;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Categoria_Por_CodCampania_y_CodReporte_Request>(oRequest);
            dataJson = campaniServices.Listar_Categoria_Por_CodCampania_y_CodReporte(request);

            Categoria_Por_CodCampania_y_CodReporte_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Categoria_Por_CodCampania_y_CodReporte_Response>(dataJson);

            return response.oListaCategoria;
        }
    }
}