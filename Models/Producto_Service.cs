using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    public class Producto_Por_CodCampania_CodCategoria_CodSubCategoria_CodMarca_Request
    {
        [JsonProperty("a")]
        public string CodEquipo { get; set; }

        [JsonProperty("b")]
        public string CodCliente { get; set; }

        [JsonProperty("c")]
        public string CodCategoria { get; set; }

        [JsonProperty("d")]
        public string CodSubCategoria { get; set; }

        [JsonProperty("e")]
        public string CodMarca { get; set; }
    }

    public class Producto_Por_CodCampania_CodCategoria_CodSubCategoria_CodMarca_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<Lucky.Entity.Common.Servicio.E_Producto> oListaProducto { get; set; }
    }

    public class Producto_Service
    {
        public List<Lucky.Entity.Common.Servicio.E_Producto> obtener_producto(string CodEquipo, string CodCliente, string CodCategoria, string CodSubCategoria, string CodMarca)
        {
            CampaniaService.Ges_CampaniaServiceClient campaniServices = new CampaniaService.Ges_CampaniaServiceClient("BasicHttpBinding_IGes_CampaniaService");
            Producto_Por_CodCampania_CodCategoria_CodSubCategoria_CodMarca_Request oRequest = new Producto_Por_CodCampania_CodCategoria_CodSubCategoria_CodMarca_Request();
            oRequest.CodEquipo = CodEquipo;
            oRequest.CodCliente = CodCliente;            
            oRequest.CodCategoria = CodCategoria;
            oRequest.CodSubCategoria = CodSubCategoria;
            oRequest.CodMarca = CodMarca;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Producto_Por_CodCampania_CodCategoria_CodSubCategoria_CodMarca_Request>(oRequest);
            dataJson = campaniServices.Listar_Producto_Por_CodCampania_CodCliente_CodCategoria_CodSubCategoria_CodMarca(request);

            Producto_Por_CodCampania_CodCategoria_CodSubCategoria_CodMarca_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Producto_Por_CodCampania_CodCategoria_CodSubCategoria_CodMarca_Response>(dataJson);

            return response.oListaProducto;
        }
    }
}