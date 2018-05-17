using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucky.Entity.Common.Servicio;
using Newtonsoft.Json;

namespace Xplora.GIS.Models
{
    public class RepresentatividadPtoVenta_Request
    {
        [JsonProperty("a")]
        public int tipo { get; set; }

        [JsonProperty("b")]
        public string codigo { get; set; }
    }

    public class RepresentatividadPtoVenta_Response
    {
        [JsonProperty("a")]
        public E_Representatividad representatividad { get; set; }
    }

    public class RepresentatividadPtoVenta_Service
    {
        public E_Representatividad obtener_Representatividad(int tipo, string codigo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            RepresentatividadPtoVenta_Request oRequest = new RepresentatividadPtoVenta_Request();
            oRequest.tipo = tipo;
            oRequest.codigo = codigo;
            
            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<RepresentatividadPtoVenta_Request>(oRequest);
            dataJson = mapServices.Obtener_Representatividad(request);

            RepresentatividadPtoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<RepresentatividadPtoVenta_Response>(dataJson);

            return response.representatividad;
        }
    }

}