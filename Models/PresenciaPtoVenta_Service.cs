using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    #region Xplora - Lima
    public class PresenciaPtoVenta_Request
    {
        [JsonProperty("a")]
        public int servicio { get; set; }

        [JsonProperty("b")]
        public string canal { get; set; }

        [JsonProperty("c")]
        public int codCliente { get; set; }

        [JsonProperty("d")]
        public string coddepartamento { get; set; }

        [JsonProperty("e")]
        public string codciudad { get; set; }

        [JsonProperty("f")]
        public string codZona { get; set; }

        [JsonProperty("g")]
        public string codDistrito { get; set; }

        [JsonProperty("h")]
        public int reportsPlanning { get; set; }
    }
    public class PresenciaPtoVenta_Response
    {
        [JsonProperty("a")]
        public List<E_PresenciaZonaDistrito> listaPresencia { get; set; }

        [JsonProperty("b")]
        public List<E_ElemVisibilidadZonaDistrito> listaElementosVisibilidad { get; set; }
    }
    #region PresenciaPtoVenta_Din_Response - Lima - Add 25-01-2013
    public class PresenciaPtoVenta_Din_Response
    {
        [JsonProperty("a")]
        public List<E_PresenciaZonaDistrito> listaPresencia { get; set; }

        [JsonProperty("b")]
        public List<E_ElemVisibilidad> listaElemVisibilidad { get; set; }
    }
    #endregion


    public class PuntoVentaCluster_Request
    {
        [JsonProperty("a")]
        public string codCanal { get; set; }

        [JsonProperty("b")]
        public string codPais { get; set; }

        [JsonProperty("c")]
        public string codDepartamento { get; set; }

        [JsonProperty("d")]
        public string codProvincia { get; set; }

        [JsonProperty("e")]
        public string codZona { get; set; }

        [JsonProperty("f")]
        public string codDistrito { get; set; }

        [JsonProperty("g")]
        public string cluster { get; set; }

        [JsonProperty("h")]
        public string codPeriodo { get; set; }

    }
    public class PuntoVentaCluster_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PuntoVentaCluster> oListPuntoVentaCluster { get; set; }

    }
    #endregion

    #region Xplora - Provincias
    public class PresenciaPtoVenta_Prov_Request
    {
        [JsonProperty("a")]
        public int servicio { get; set; }

        [JsonProperty("b")]
        public string canal { get; set; }

        [JsonProperty("c")]
        public int codCliente { get; set; }

        [JsonProperty("d")]
        public string coddepartamento { get; set; }

        [JsonProperty("e")]
        public string codciudad { get; set; }

        [JsonProperty("f")]
        public string codZona { get; set; }

        [JsonProperty("g")]
        public string codDistrito { get; set; }

        [JsonProperty("h")]
        public int reportsPlanning { get; set; }

        [JsonProperty("i")]//Add
        public string codOficina { get; set; }
    }
    public class PresenciaPtoVenta_Prov_Response
    {
        [JsonProperty("a")]
        public List<E_PresenciaZonaDistrito> listaPresencia { get; set; }

        [JsonProperty("b")]
        public List<E_ElemVisibilidadZonaDistrito> listaElementosVisibilidad { get; set; }
    }
    #region PresenciaPtoVenta_Prov_Din_Response - Add 25-01-2013
    public class PresenciaPtoVenta_Prov_Din_Response
    {
        [JsonProperty("a")]
        public List<E_PresenciaZonaDistrito> listaPresencia { get; set; }

        [JsonProperty("b")]
        public List<E_ElemVisibilidad> listaElemVisibilidad { get; set; }
    }
    #endregion
    public class PuntoVentaCluster_Prov_Request
    {
        [JsonProperty("a")]
        public string codCanal { get; set; }

        [JsonProperty("b")]
        public string codPais { get; set; }

        [JsonProperty("c")]
        public string codDepartamento { get; set; }

        [JsonProperty("d")]
        public string codProvincia { get; set; }

        [JsonProperty("e")]
        public string codZona { get; set; }

        [JsonProperty("f")]
        public string codDistrito { get; set; }

        [JsonProperty("g")]
        public string cluster { get; set; }

        [JsonProperty("h")]
        public string codPeriodo { get; set; }

        [JsonProperty("i")]//Add
        public string codOficina { get; set; }

    }
    public class PuntoVentaCluster_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PuntoVentaCluster> oListPuntoVentaCluster { get; set; }

    }
    #endregion

    public class PresenciaPtoVenta_Service
    {
        //Xplora Lima
        public PresenciaPtoVenta_Response obtener_Representatividad(int servicio, string canal, int codCliente, string coddepartamento, string codciudad, string codZona, string codDistrito, int reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PresenciaPtoVenta_Request oRequest = new PresenciaPtoVenta_Request();
            oRequest.servicio = servicio;
            oRequest.canal = canal;
            oRequest.codCliente = codCliente;
            oRequest.coddepartamento = coddepartamento;
            oRequest.codciudad = codciudad;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PresenciaPtoVenta_Request>(oRequest);
            dataJson = mapServices.Obtener_Presencia_ZonaDistrito(request);

            PresenciaPtoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PresenciaPtoVenta_Response>(dataJson);

            return response;
        }
        //Xplora Provincias
        public PresenciaPtoVenta_Prov_Response obtener_Representatividad_Prov(int servicio, string canal, int codCliente, string codOficina, string coddepartamento, string codciudad, string codZona, string codDistrito, int reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PresenciaPtoVenta_Prov_Request oRequest = new PresenciaPtoVenta_Prov_Request();
            oRequest.codOficina = codOficina;//Add 07/11/2012
            oRequest.servicio = servicio;
            oRequest.canal = canal;
            oRequest.codCliente = codCliente;
            oRequest.coddepartamento = coddepartamento;
            oRequest.codciudad = codciudad;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PresenciaPtoVenta_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_Presencia_ZonaDistrito_Prov(request);

            PresenciaPtoVenta_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PresenciaPtoVenta_Prov_Response>(dataJson);

            return response;
        }
        
        #region Obtener Representatividad Dinamico - Lima - Add 25-01-2013
        public PresenciaPtoVenta_Din_Response obtener_Representatividad_Din(int servicio, string canal, int codCliente, string coddepartamento, string codciudad, string codZona, string codDistrito, int reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PresenciaPtoVenta_Request oRequest = new PresenciaPtoVenta_Request();
            oRequest.servicio = servicio;
            oRequest.canal = canal;
            oRequest.codCliente = codCliente;
            oRequest.coddepartamento = coddepartamento;
            oRequest.codciudad = codciudad;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PresenciaPtoVenta_Request>(oRequest);
            dataJson = mapServices.Obtener_Presencia_ZonaDistrito_Din(request);

            PresenciaPtoVenta_Din_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PresenciaPtoVenta_Din_Response>(dataJson);

            return response;
        }
        #endregion

        #region
        public PresenciaPtoVenta_Prov_Din_Response obtener_Representatividad_Prov_Din(int servicio, string canal, int codCliente, string codOficina, string coddepartamento, string codciudad, string codZona, string codDistrito, int reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PresenciaPtoVenta_Prov_Request oRequest = new PresenciaPtoVenta_Prov_Request();
            oRequest.codOficina = codOficina;//Add 07/11/2012
            oRequest.servicio = servicio;
            oRequest.canal = canal;
            oRequest.codCliente = codCliente;
            oRequest.coddepartamento = coddepartamento;
            oRequest.codciudad = codciudad;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PresenciaPtoVenta_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_Presencia_ZonaDistrito_Prov_Din(request);

            PresenciaPtoVenta_Prov_Din_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PresenciaPtoVenta_Prov_Din_Response>(dataJson);

            return response;
        }

        #endregion

        //Xplora Lima
        public List<E_PuntoVentaCluster> obtener_PresenciaClusterPDV(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string cluster, string codPeriodo)
        {

            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaCluster_Request oRequest = new PuntoVentaCluster_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.cluster = cluster;
            oRequest.codPeriodo = codPeriodo;
            
            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaCluster_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaCluster(request);

            PuntoVentaCluster_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaCluster_Response>(dataJson);

            return response.oListPuntoVentaCluster;
        }        
        //Xplora Provincias
        public List<E_PuntoVentaCluster> obtener_PresenciaClusterPDV_Prov(string codCanal, string codPais,string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string cluster, string codPeriodo)
        {

            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaCluster_Prov_Request oRequest = new PuntoVentaCluster_Prov_Request();
            oRequest.codOficina = codOficina;//New
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.cluster = cluster;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaCluster_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaCluster_Prov(request);

            PuntoVentaCluster_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaCluster_Prov_Response>(dataJson);

            return response.oListPuntoVentaCluster;
        }        
    }


}