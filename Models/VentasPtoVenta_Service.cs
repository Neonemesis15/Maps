using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    public class VentasPtoVenta_Request
    {
        [JsonProperty("a")]
        public int tipo { get; set; }

        [JsonProperty("b")]
        public string codigo { get; set; }

        [JsonProperty("c")]
        public int reportsPlanning { get; set; }
    }

    public class VentasPtoVenta_Response
    {
        [JsonProperty("a")]
        public List<E_VentasZonaDistrito> listaVentas { get; set; }
    }

    public class PuntoVentaMapaVentas_Request
    {

        [JsonProperty("a")]
        public string codPais { get; set; }

        [JsonProperty("b")]
        public string codDepartamento { get; set; }

        [JsonProperty("c")]
        public string codProvincia { get; set; }

        [JsonProperty("d")]
        public string codZona { get; set; }

        [JsonProperty("e")]
        public string codDistrito { get; set; }

        [JsonProperty("f")]
        public string codCategoria { get; set; }

        [JsonProperty("g")]
        public string codProducto { get; set; }

        [JsonProperty("h")]
        public string codCluster { get; set; }

        [JsonProperty("i")]
        public string codPlanning { get; set; }

        [JsonProperty("j")]
        public string codPeriodo { get; set; }
    }

    public class PuntoVentaMapaVentas_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PuntoVentaMapaVentas> oListPuntoVentaMapaVentas { get; set; }
    }

    public class VentasPtoVenta_Service
    {
        public List<E_VentasZonaDistrito> Obtener_Ventas_ZonaDistrito(int tipo, string codigo, int reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            VentasPtoVenta_Request oRequest = new VentasPtoVenta_Request();
            oRequest.tipo = tipo;
            oRequest.codigo = codigo;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<VentasPtoVenta_Request>(oRequest);
            dataJson = mapServices.Obtener_Ventas_ZonaDistrito(request);

            VentasPtoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<VentasPtoVenta_Response>(dataJson);

            return response.listaVentas;
        }

        public List<E_PuntoVentaMapaVentas> Obtener_PuntoVentaMapaVentas(string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto, string codCluster, string codPlanning, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaMapaVentas_Request oRequest = new PuntoVentaMapaVentas_Request();
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codCategoria = codCategoria;
            oRequest.codProducto = codProducto;
            oRequest.codCluster = codCluster;
            oRequest.codPlanning = codPlanning;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaMapaVentas_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaMapaVentas(request);

            PuntoVentaMapaVentas_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaMapaVentas_Response>(dataJson);

            return response.oListPuntoVentaMapaVentas;
        }
    }
}