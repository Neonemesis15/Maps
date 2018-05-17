using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    public class Utils 
    {
        public E_DinamicArray Obtener_ultimoperiodo(String opcion, String filtros) {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");
            llenarTablas_Request oRequest = new llenarTablas_Request();
            oRequest.opcion = opcion;
            oRequest.filtros = filtros;

            String request = Lucky.CFG.JavaMovil.HelperJson.Serialize<llenarTablas_Request>(oRequest);
            String data = mapServices.UtilXplMaps(request);

            llenarTablas_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<llenarTablas_Response>(data);

            return response.oE_DinamicArray;
        }

        public E_DatosFiltros Obtener_DatosFiltros(String CodPersona)
        {
            MapService.Ges_MapsServiceClient cliente = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_DatosFiltro_x_Persona_Request oResquest = new Obtener_DatosFiltro_x_Persona_Request();
            oResquest.CodPersona = CodPersona;


            string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_DatosFiltro_x_Persona_Request>(oResquest);

            string datosJson = cliente.Obtener_DatosFiltro_x_Persona(request);

            Obtener_DatosFiltro_x_Persona_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_DatosFiltro_x_Persona_Response>(datosJson);

            return response.oE_DatosFiltros;
        }
    
    }

    #region Request

        public class llenarTablas_Request
        {
            [JsonProperty("a")]
            public string opcion { get; set; }
            [JsonProperty("b")]
            public string filtros { get; set; }
        }

        public class Obtener_DatosFiltro_x_Persona_Request
        {
            [JsonProperty("a")]
            public string CodPersona { get; set; }
        }

    #endregion

    #region Response

        public class llenarTablas_Response : BaseResponse
        {
            [JsonProperty("a")]
            public E_DinamicArray oE_DinamicArray { get; set; }
        }

        public class Obtener_DatosFiltro_x_Persona_Response : BaseResponse
        {
            [JsonProperty("a")]
            public E_DatosFiltros oE_DatosFiltros { get; set; }
        }

    #endregion



}