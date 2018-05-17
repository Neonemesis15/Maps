using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    public class Sesiones
    {
        public class GrabarIngresoMaps_Request
        {

            [JsonProperty("a")]
            public string person_id { get; set; }

            [JsonProperty("b")]
            public string modulo_id { get; set; }

            [JsonProperty("c")]
            public string machine_id { get; set; }
        }

        public class GrabarIngresoMaps_Response : BaseResponse  {

            //[JsonProperty("a")]
            //public Boolean respuesta { get; set; }
        }

        public int GrabarIngresoMaps(String person_id, String modulo_id, String machine_id) {


            ReportClientService.Ges_ReporteClienteClient reportClientServices = new ReportClientService.Ges_ReporteClienteClient("BasicHttpBinding_IGes_ReporteCliente");

            GrabarIngresoMaps_Request oRequest = new GrabarIngresoMaps_Request();
            oRequest.person_id = person_id;
            oRequest.modulo_id = modulo_id;
            oRequest.machine_id = machine_id;

            String request = Lucky.CFG.JavaMovil.HelperJson.Serialize<GrabarIngresoMaps_Request>(oRequest);
            //String data = reportClientServices.registrar_IngresoModulo(request);

            //GrabarIngresoMaps_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<GrabarIngresoMaps_Response>(data);

            //return response.Estado;
            return 0;
        }


        public class obtener_SemanasByAnioMes_Request
        {
            [JsonProperty("a")]
            public string anio { get; set; }
            [JsonProperty("b")]
            public string mes { get; set; }
        }

        public class obtener_SemanasByAnioMes_Response : BaseResponse
        {
            [JsonProperty("a")]
            public List<E_Semana> oListE_Semana { get; set; }
        }

        public class E_Semana
        {
            //public E_Semana();

            [JsonProperty("a")]
            public string codSemana { get; set; }
            [JsonProperty("b")]
            public string semanaDescripcion { get; set; }
        }

        public IList<E_Semana> get_SemanasxMes(string anio, string mes) {

            ReportClientService.Ges_ReporteClienteClient reportClientServices = new ReportClientService.Ges_ReporteClienteClient("BasicHttpBinding_IGes_ReporteCliente");

            obtener_SemanasByAnioMes_Request oRequest = new obtener_SemanasByAnioMes_Request();
            oRequest.anio = anio;
            oRequest.mes = mes;

            String request = Lucky.CFG.JavaMovil.HelperJson.Serialize<obtener_SemanasByAnioMes_Request>(oRequest);
            String data = reportClientServices.obtener_SemanasByAnioMes(request);

            obtener_SemanasByAnioMes_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<obtener_SemanasByAnioMes_Response>(data);

            return response.oListE_Semana;
        }

        public class E_Consulta_IngresosModulo
        {
            //public E_Consulta_IngresosModulo();

            [JsonProperty("a")]
            public string codUsuario { get; set; }
            [JsonProperty("e")]
            public List<E_Consulta_IngresoModulo_Detalle> detalles { get; set; }
            [JsonProperty("c")]
            public string nombreCompleto { get; set; }
            [JsonProperty("b")]
            public string nombreUsuario { get; set; }
            [JsonProperty("d")]
            public string resultado { get; set; }

        }

        public class E_Consulta_IngresoModulo_Detalle
        {
            //public E_Consulta_IngresoModulo_Detalle();

            [JsonProperty("a")]
            public string nombreFecha { get; set; }
            [JsonProperty("b")]
            public string nombreFecha2 { get; set; }
            [JsonProperty("c")]
            public string valor { get; set; }
        }

        public class obtener_IngresosModulo_Request
        {
            [JsonProperty("a")]
            public string codUsuario { get; set; }
            [JsonProperty("b")]
            public string codModulo { get; set; }
            [JsonProperty("c")]
            public string tipoVisita { get; set; }
            [JsonProperty("d")]
            public string anio { get; set; }
            [JsonProperty("e")]
            public string mes { get; set; }
            [JsonProperty("f")]
            public string semana { get; set; } 

        }

        public class obtener_IngresosModulo_Response : BaseResponse
        {
            [JsonProperty("a")]
            public List<E_Consulta_IngresosModulo> oListE_Consulta_IngresosModulo { get; set; }
        }


        public IList<E_Consulta_IngresosModulo> get_ListConsulta_IngresosModulo(string codUsuario, string codModulo, string tipoVisita, string anio, string mes, string semana) {

            ReportClientService.Ges_ReporteClienteClient reportClientServices = new ReportClientService.Ges_ReporteClienteClient("BasicHttpBinding_IGes_ReporteCliente");

            obtener_IngresosModulo_Request oRequest = new obtener_IngresosModulo_Request();
            oRequest.codUsuario = codUsuario;
            oRequest.tipoVisita = tipoVisita;
            oRequest.codModulo = codModulo;
            oRequest.anio = anio;
            oRequest.mes = mes;
            oRequest.semana = semana;
            

            String request = Lucky.CFG.JavaMovil.HelperJson.Serialize<obtener_IngresosModulo_Request>(oRequest);
            String data = reportClientServices.obtener_IngresosModulo(request);

            obtener_IngresosModulo_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<obtener_IngresosModulo_Response>(data);

            return response.oListE_Consulta_IngresosModulo;
        }


    }
}