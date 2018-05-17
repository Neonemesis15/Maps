using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    public class Generador_Request
    {
        [JsonProperty("a")]
        public string codEquipo { get; set; }

        [JsonProperty("b")]
        public string CodSupervisor { get; set; }
    }

    public class Generador_Response
    {
        [JsonProperty("a")]
        public List<E_Persona> oListaGenerador { get; set; }
    }

    public class Obtener_Seguimiento_Generador_Request
    {
        [JsonProperty("a")]
        public string codEquipo { get; set; }

        [JsonProperty("b")]
        public string codPais { get; set; }

        [JsonProperty("c")]
        public string codDepartamento { get; set; }

        [JsonProperty("d")]
        public string codProvincia { get; set; }

        [JsonProperty("e")]
        public string codDistrito { get; set; }

        [JsonProperty("f")]
        public string codGestor { get; set; }

        [JsonProperty("g")]
        public string fecha { get; set; }
    }

    public class Obtener_Seguimiento_Generador_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_Seguimiento_Ruta seguimientoRuta { get; set; }
    }

    public class Generador_Service
    {
        public List<E_Persona> obtener_generador(string codEquipo, string CodSupervisor)
        {
            CampaniaService.Ges_CampaniaServiceClient campaniaServices = new CampaniaService.Ges_CampaniaServiceClient("BasicHttpBinding_IGes_CampaniaService");

            Generador_Request oRequest = new Generador_Request();
            oRequest.codEquipo = codEquipo;
            oRequest.CodSupervisor = CodSupervisor;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Generador_Request>(oRequest);
            dataJson = campaniaServices.Listar_Generadores_Por_CodCampania_Por_CodSupervisor(request);

            Generador_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Generador_Response>(dataJson);

            return response.oListaGenerador;
        }

        public E_Seguimiento_Ruta Obtener_seguimiento_generador(string codEquipo, string codPais, string codDepartamento, string codProvincia,
            string codDistrito, string codGestor, string fecha)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_Seguimiento_Generador_Request oRequest = new Obtener_Seguimiento_Generador_Request();
            oRequest.codEquipo = codEquipo;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codDistrito = codDistrito;
            oRequest.codGestor = codGestor;
            oRequest.fecha = fecha;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_Seguimiento_Generador_Request>(oRequest);
            dataJson = mapServices.Obtener_Seguimiento_Generador(request);

            Obtener_Seguimiento_Generador_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_Seguimiento_Generador_Response>(dataJson);

            return response.seguimientoRuta;
        }
    }
}