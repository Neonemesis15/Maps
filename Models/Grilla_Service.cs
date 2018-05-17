using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    public class EvolucionVentaSKUMandatorios_Request
    {
        [JsonProperty("a")]
        public E_Filtros_XplMap_Colgate oE_Filtros_XplMap_Colgate { get; set; }
    }

    public class EvolucionVentaSKUMandatorios_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_VentasZonaDistrito_Detalle_List> oE_VentasZonaDistrito_Detalle { get; set; }
    }

    public class EvolucionPresenciaSKUMandatorios_Request
    {
        [JsonProperty("a")]
        public E_Filtros_XplMap_Colgate oE_Filtros_XplMap_Colgate { get; set; }
    }

    public class EvolucionPresenciaSKUMandatorios_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaZonaDistrito_Detalle_List> oE_PresenciaZonaDistrito_Detalle { get; set; }
    }

    public class Grilla_Service
    {
        public EvolucionVentaSKUMandatorios_Response Obtener_Evolucion_Venta_SKUMandatorios(string codServicio, string codCanal, string codCliente,
            string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto,
            string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            EvolucionVentaSKUMandatorios_Request oRequest = new EvolucionVentaSKUMandatorios_Request();
            E_Filtros_XplMap_Colgate parametros = new E_Filtros_XplMap_Colgate();
            parametros.codServicio = codServicio;
            parametros.codCanal = codCanal;
            parametros.codCliente = codCliente;
            parametros.codPais = codPais;
            parametros.codDepartamento = codDepartamento;
            parametros.codProvincia = codProvincia;
            parametros.codZona = codZona;
            parametros.codDistrito = codDistrito;
            parametros.codCategoria = codCategoria;
            parametros.codProducto = codProducto;
            parametros.codCluster = codCluster;
            parametros.anio = anio;
            parametros.mes = mes;
            parametros.codPeriodo = codPeriodo;
            parametros.codOpcion = codOpcion;

            oRequest.oE_Filtros_XplMap_Colgate = parametros;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<EvolucionVentaSKUMandatorios_Request>(oRequest);
            dataJson = mapServices.Evolucion_Venta_SKUMandatorios(request);

            EvolucionVentaSKUMandatorios_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<EvolucionVentaSKUMandatorios_Response>(dataJson);

            return response;
        }

        public EvolucionPresenciaSKUMandatorios_Response Obtener_Evolucion_Presencia_SKUMandatorios(string codServicio, string codCanal, string codCliente,
            string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto,
            string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            EvolucionPresenciaSKUMandatorios_Request oRequest = new EvolucionPresenciaSKUMandatorios_Request();
            E_Filtros_XplMap_Colgate parametros = new E_Filtros_XplMap_Colgate();
            parametros.codServicio = codServicio;
            parametros.codCanal = codCanal;
            parametros.codCliente = codCliente;
            parametros.codPais = codPais;
            parametros.codDepartamento = codDepartamento;
            parametros.codProvincia = codProvincia;
            parametros.codZona = codZona;
            parametros.codDistrito = codDistrito;
            parametros.codCategoria = codCategoria;
            parametros.codProducto = codProducto;
            parametros.codCluster = codCluster;
            parametros.anio = anio;
            parametros.mes = mes;
            parametros.codPeriodo = codPeriodo;
            parametros.codOpcion = codOpcion;

            oRequest.oE_Filtros_XplMap_Colgate = parametros;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<EvolucionPresenciaSKUMandatorios_Request>(oRequest);
            dataJson = mapServices.EvolucionPresenciaSKUMandatorios(request);

            EvolucionPresenciaSKUMandatorios_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<EvolucionPresenciaSKUMandatorios_Response>(dataJson);

            return response;
        }
    }
}