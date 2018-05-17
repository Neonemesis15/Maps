using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Lucky.Entity.Common.Servicio;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{
    #region Request

        public class DatosTendenciaSKUMandatorios_Prov_Request
        {
            [JsonProperty("a")]
            public string codServicio { get; set; }

            [JsonProperty("b")]
            public string codCanal { get; set; }

            [JsonProperty("c")]
            public string codCliente { get; set; }

            [JsonProperty("d")]
            public string codPais { get; set; }

            [JsonProperty("e")]
            public string codDpto { get; set; }

            [JsonProperty("f")]
            public string codCity { get; set; }

            [JsonProperty("g")]
            public string codDistrito { get; set; }

            [JsonProperty("h")]
            public string codSector { get; set; }

            [JsonProperty("i")]
            public string codCluster { get; set; }

            [JsonProperty("j")]
            public string codYear { get; set; }

            [JsonProperty("k")]
            public string codMes { get; set; }

            [JsonProperty("l")]
            public string codPeriodo { get; set; }

            [JsonProperty("m")]
            public string codOpcion { get; set; }

            [JsonProperty("n")]//Add 09/11/2012
            public string codOficina { get; set; }

            [JsonProperty("o")] // Add 30-05-2013
            public string otrosParametros { get; set; }
        }


    #endregion


    //XploraMaps - Lima
    public class ObtenerGraficoTendencia_Request
    {
        [JsonProperty("a")]
        public string codServicio { get; set; }

        [JsonProperty("b")]
        public string codCanal { get; set; }

        [JsonProperty("c")]
        public string codCliente { get; set; }

        [JsonProperty("d")]
        public string codPais { get; set; }

        [JsonProperty("e")]
        public string codDepartamento { get; set; }

        [JsonProperty("f")]
        public string codProvincia { get; set; }

        [JsonProperty("g")]
        public string codZona { get; set; }

        [JsonProperty("h")]
        public string codDistrito { get; set; }

        [JsonProperty("i")]
        public string codCategoria { get; set; }

        [JsonProperty("j")]
        public string codProducto { get; set; }

        [JsonProperty("k")]
        public string codCluster { get; set; }

        [JsonProperty("l")]
        public string anio { get; set; }

        [JsonProperty("m")]
        public string mes { get; set; }

        [JsonProperty("n")]
        public string codPeriodo { get; set; }

        [JsonProperty("o")]
        public string codOpcion { get; set; }
    }
    public class ObtenerGraficoTendencia_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_Grafico_Tendencia> oListGraficoTendencia { get; set; }
    }
    //XploraMaps - Provincia
    public class ObtenerGraficoTendencia_Prov_Request
    {
        [JsonProperty("a")]
        public string codServicio { get; set; }

        [JsonProperty("b")]
        public string codCanal { get; set; }

        [JsonProperty("c")]
        public string codCliente { get; set; }

        [JsonProperty("d")]
        public string codPais { get; set; }

        [JsonProperty("e")]
        public string codDepartamento { get; set; }

        [JsonProperty("f")]
        public string codProvincia { get; set; }

        [JsonProperty("g")]
        public string codZona { get; set; }

        [JsonProperty("h")]
        public string codDistrito { get; set; }

        [JsonProperty("i")]
        public string codCategoria { get; set; }

        [JsonProperty("j")]
        public string codProducto { get; set; }

        [JsonProperty("k")]
        public string codCluster { get; set; }

        [JsonProperty("l")]
        public string anio { get; set; }

        [JsonProperty("m")]
        public string mes { get; set; }

        [JsonProperty("n")]
        public string codPeriodo { get; set; }

        [JsonProperty("o")]
        public string codOpcion { get; set; }

        [JsonProperty("p")]//Add 09/11/2012
        public string codOficina { get; set; }
    }
    public class ObtenerGraficoTendencia_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_Grafico_Tendencia> oListGraficoTendencia { get; set; }
    }

    //XploraMaps - Lima
    public class ObtenerGraficoVariacion_Request
    {
        [JsonProperty("a")]
        public string codServicio { get; set; }

        [JsonProperty("b")]
        public string codCanal { get; set; }

        [JsonProperty("c")]
        public string codCliente { get; set; }

        [JsonProperty("d")]
        public string codPais { get; set; }

        [JsonProperty("e")]
        public string codDepartamento { get; set; }

        [JsonProperty("f")]
        public string codProvincia { get; set; }

        [JsonProperty("g")]
        public string codZona { get; set; }

        [JsonProperty("h")]
        public string codDistrito { get; set; }

        [JsonProperty("i")]
        public string codCategoria { get; set; }

        [JsonProperty("j")]
        public string codProducto { get; set; }

        [JsonProperty("k")]
        public string codCluster { get; set; }

        [JsonProperty("l")]
        public string anio { get; set; }

        [JsonProperty("m")]
        public string mes { get; set; }

        [JsonProperty("n")]
        public string codPeriodo { get; set; }

        [JsonProperty("o")]
        public string codOpcion { get; set; }

        [JsonProperty("p")] // Add 28-05-2013 - Psa
        public string otrosParametros { get; set; }
    }
    public class ObtenerGraficoVariacion_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_Grafico_Variacion> oListGraficoVariacion { get; set; }
    }
    //XploraMaps - Provincia
    public class ObtenerGraficoVariacion_Prov_Request
    {
        [JsonProperty("a")]
        public string codServicio { get; set; }

        [JsonProperty("b")]
        public string codCanal { get; set; }

        [JsonProperty("c")]
        public string codCliente { get; set; }

        [JsonProperty("d")]
        public string codPais { get; set; }

        [JsonProperty("e")]
        public string codDepartamento { get; set; }

        [JsonProperty("f")]
        public string codProvincia { get; set; }

        [JsonProperty("g")]
        public string codZona { get; set; }

        [JsonProperty("h")]
        public string codDistrito { get; set; }

        [JsonProperty("i")]
        public string codCategoria { get; set; }

        [JsonProperty("j")]
        public string codProducto { get; set; }

        [JsonProperty("k")]
        public string codCluster { get; set; }

        [JsonProperty("l")]
        public string anio { get; set; }

        [JsonProperty("m")]
        public string mes { get; set; }

        [JsonProperty("n")]
        public string codPeriodo { get; set; }

        [JsonProperty("o")]
        public string codOpcion { get; set; }

        [JsonProperty("p")]//Add 09/11/2012
        public string codOficina { get; set; }
    }
    public class ObtenerGraficoVariacion_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_Grafico_Variacion> oListGraficoVariacion { get; set; }
    }

    //XploraMaps - Lima
    public class DatosTendenciaSKUMandatorios_Request
    {
        [JsonProperty("a")]
        public string codServicio { get; set; }

        [JsonProperty("b")]
        public string codCanal { get; set; }

        [JsonProperty("c")]
        public string codCliente { get; set; }

        [JsonProperty("d")]
        public string codPais { get; set; }

        [JsonProperty("e")]
        public string codDpto { get; set; }

        [JsonProperty("f")]
        public string codCity { get; set; }

        [JsonProperty("g")]
        public string codDistrito { get; set; }

        [JsonProperty("h")]
        public string codSector { get; set; }

        [JsonProperty("i")]
        public string codCluster { get; set; }

        [JsonProperty("j")]
        public string codYear { get; set; }

        [JsonProperty("k")]
        public string codMes { get; set; }

        [JsonProperty("l")]
        public string codPeriodo { get; set; }

        [JsonProperty("m")]
        public string codOpcion { get; set; }
    }
    
    
    
    
    
    public class DatosTendenciaSKUMandatorios_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel datosTendencia { get; set; }
    }
    //XploraMaps - Provincia
    
    public class DatosTendenciaSKUMandatorios_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel datosTendencia { get; set; }
    }

    //XploraMaps - Lima
    public class DatosVariacionSKUMandatorios_Request
    {
        [JsonProperty("a")]
        public string codServicio { get; set; }

        [JsonProperty("b")]
        public string codCanal { get; set; }

        [JsonProperty("c")]
        public string codCliente { get; set; }

        [JsonProperty("d")]
        public string codPais { get; set; }

        [JsonProperty("e")]
        public string codDpto { get; set; }

        [JsonProperty("f")]
        public string codCity { get; set; }

        [JsonProperty("g")]
        public string codDistrito { get; set; }

        [JsonProperty("h")]
        public string codSector { get; set; }

        [JsonProperty("i")]
        public string codCluster { get; set; }

        [JsonProperty("j")]
        public string codYear { get; set; }

        [JsonProperty("k")]
        public string codMes { get; set; }

        [JsonProperty("l")]
        public string codPeriodo { get; set; }

        [JsonProperty("m")]
        public string codOpcion { get; set; }
    }
    public class DatosVariacionSKUMandatorios_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel datosVariacion { get; set; }
    }
    
    //XploraMaps - Provincia
    public class DatosVariacionSKUMandatorios_Prov_Request
    {
        [JsonProperty("a")]
        public string codServicio { get; set; }

        [JsonProperty("b")]
        public string codCanal { get; set; }

        [JsonProperty("c")]
        public string codCliente { get; set; }

        [JsonProperty("d")]
        public string codPais { get; set; }

        [JsonProperty("e")]
        public string codDpto { get; set; }

        [JsonProperty("f")]
        public string codCity { get; set; }

        [JsonProperty("g")]
        public string codDistrito { get; set; }

        [JsonProperty("h")]
        public string codSector { get; set; }

        [JsonProperty("i")]
        public string codCluster { get; set; }

        [JsonProperty("j")]
        public string codYear { get; set; }

        [JsonProperty("k")]
        public string codMes { get; set; }

        [JsonProperty("l")]
        public string codPeriodo { get; set; }

        [JsonProperty("m")]
        public string codOpcion { get; set; }

        [JsonProperty("n")]//Add 09/11/2012
        public string codOficina { get; set; }

        [JsonProperty("o")]//Add 31-05-2013
        public string otrosParametros { get; set; }

    }
    public class DatosVariacionSKUMandatorios_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel datosVariacion { get; set; }
    }
    

    public class Grafico_Service
    {   //XploraMaps Lima 
        public ObtenerGraficoTendencia_Response Obtener_Grafico_Tendencia(string codServicio, string codCanal, string codCliente, string codPais,
            string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string codCategoria, string codProducto, string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            ObtenerGraficoTendencia_Request oRequest = new ObtenerGraficoTendencia_Request();
            oRequest.codServicio = codServicio;
            oRequest.codCanal = codCanal;
            oRequest.codCliente = codCliente;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codCategoria = codCategoria;
            oRequest.codProducto = codProducto;
            oRequest.codCluster = codCluster;
            oRequest.anio = anio;
            oRequest.mes = mes;
            oRequest.codPeriodo = codPeriodo;
            oRequest.codOpcion = codOpcion;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<ObtenerGraficoTendencia_Request>(oRequest);
            dataJson = mapServices.Obtener_Grafico_Tendencia(request);

            ObtenerGraficoTendencia_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<ObtenerGraficoTendencia_Response>(dataJson);

            return response;
        }
        //XploraMaps Provincia 
        public ObtenerGraficoTendencia_Prov_Response Obtener_Grafico_Tendencia_Prov(string codServicio, string codCanal, string codCliente, string codPais,
            string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string codCategoria, string codProducto, string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            ObtenerGraficoTendencia_Prov_Request oRequest = new ObtenerGraficoTendencia_Prov_Request();
            oRequest.codOficina = codOficina;
            oRequest.codServicio = codServicio;
            oRequest.codCanal = codCanal;
            oRequest.codCliente = codCliente;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codCategoria = codCategoria;
            oRequest.codProducto = codProducto;
            oRequest.codCluster = codCluster;
            oRequest.anio = anio;
            oRequest.mes = mes;
            oRequest.codPeriodo = codPeriodo;
            oRequest.codOpcion = codOpcion;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<ObtenerGraficoTendencia_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_Grafico_Tendencia_Prov(request);

            ObtenerGraficoTendencia_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<ObtenerGraficoTendencia_Prov_Response>(dataJson);

            return response;
        }

        //XploraMaps Lima 
        public ObtenerGraficoVariacion_Response Obtener_Grafico_Variacion(string codServicio, string codCanal, string codCliente, string codPais,
            string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string codCategoria, string codProducto, string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            ObtenerGraficoVariacion_Request oRequest = new ObtenerGraficoVariacion_Request();
            oRequest.codServicio = codServicio;
            oRequest.codCanal = codCanal;
            oRequest.codCliente = codCliente;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codCategoria = codCategoria;
            oRequest.codProducto = codProducto;
            oRequest.codCluster = codCluster;
            oRequest.anio = anio;
            oRequest.mes = mes;
            oRequest.codPeriodo = codPeriodo;
            oRequest.codOpcion = codOpcion;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<ObtenerGraficoVariacion_Request>(oRequest);
            dataJson = mapServices.Obtener_Grafico_Variacion(request);

            ObtenerGraficoVariacion_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<ObtenerGraficoVariacion_Response>(dataJson);

            return response;
        }



        //XploraMaps Provincia 
        public ObtenerGraficoVariacion_Prov_Response Obtener_Grafico_Variacion_Prov(string codServicio, string codCanal, string codCliente, string codPais,
            string codOficina ,string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string codCategoria, string codProducto, string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            ObtenerGraficoVariacion_Prov_Request oRequest = new ObtenerGraficoVariacion_Prov_Request();
            oRequest.codOficina = codOficina;
            oRequest.codServicio = codServicio;
            oRequest.codCanal = codCanal;
            oRequest.codCliente = codCliente;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codCategoria = codCategoria;
            oRequest.codProducto = codProducto;
            oRequest.codCluster = codCluster;
            oRequest.anio = anio;
            oRequest.mes = mes;
            oRequest.codPeriodo = codPeriodo;
            oRequest.codOpcion = codOpcion;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<ObtenerGraficoVariacion_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_Grafico_Variacion_Prov(request);

            ObtenerGraficoVariacion_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<ObtenerGraficoVariacion_Prov_Response>(dataJson);

            return response;
        }
        
        //XploraMaps Lima 
        public E_ExportExcel Obtener_Datos_Tendencia(string codServicio, string codCanal, string codCliente, string codPais,
            string codDpto, string codCity, string codDistrito, string codSector, string codCluster, string codYear,
            string codMes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            DatosTendenciaSKUMandatorios_Request oRequest = new DatosTendenciaSKUMandatorios_Request();

            oRequest.codServicio = codServicio;
            oRequest.codCanal = codCanal;
            oRequest.codCliente = codCliente;
            oRequest.codPais = codPais;
            oRequest.codDpto = codDpto;
            oRequest.codCity = codCity;
            oRequest.codDistrito = codDistrito;
            oRequest.codSector = codSector;
            oRequest.codCluster = codCluster;
            oRequest.codYear = codYear;
            oRequest.codMes = codMes;
            oRequest.codPeriodo = codPeriodo;
            oRequest.codOpcion = codOpcion;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<DatosTendenciaSKUMandatorios_Request>(oRequest);
            dataJson = mapServices.Obtener_Datos_Tendencia(request);

            DatosTendenciaSKUMandatorios_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<DatosTendenciaSKUMandatorios_Response>(dataJson);

            return response.datosTendencia;
        }



        #region Rev02

            public E_ExportExcel Obtener_Datos_Tendencia_Rev02(string codServicio, string codCanal, string codCliente, string codPais,
            string codDpto, string codCity, string codDistrito, string codSector, string codCluster, string codYear,
            string codMes, string codPeriodo, string codOpcion, string codOficina, string otrosParametros)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                DatosTendenciaSKUMandatorios_Prov_Request oRequest = new DatosTendenciaSKUMandatorios_Prov_Request();
                oRequest.codServicio = codServicio;
                oRequest.codCanal = codCanal;
                oRequest.codCliente = codCliente;
                oRequest.codPais = codPais;
                oRequest.codDpto = codDpto;
                oRequest.codCity = codCity;
                oRequest.codDistrito = codDistrito;
                oRequest.codSector = codSector;
                oRequest.codCluster = codCluster;
                oRequest.codYear = codYear;
                oRequest.codMes = codMes;
                oRequest.codPeriodo = codPeriodo;
                oRequest.codOpcion = codOpcion;
                oRequest.codOficina = codOficina;
                oRequest.otrosParametros = otrosParametros;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<DatosTendenciaSKUMandatorios_Prov_Request>(oRequest);
                string dataJson = mapServices.Obtener_Datos_Tendencia_Prov_V1_Rev02(request);
                DatosTendenciaSKUMandatorios_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<DatosTendenciaSKUMandatorios_Prov_Response>(dataJson);

                return response.datosTendencia;
            }

            public E_ExportExcel Obtener_Datos_Variacion_Rev02(string codServicio, string codCanal, string codCliente, string codPais,
            string codDpto, string codCity, string codDistrito, string codSector, string codCluster, string codYear,
            string codMes, string codPeriodo, string codOpcion, string codOficina, string otrosParametros)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                DatosVariacionSKUMandatorios_Prov_Request oRequest = new DatosVariacionSKUMandatorios_Prov_Request();

                oRequest.codServicio = codServicio;
                oRequest.codCanal = codCanal;
                oRequest.codCliente = codCliente;
                oRequest.codPais = codPais;
                oRequest.codDpto = codDpto;
                oRequest.codCity = codCity;
                oRequest.codDistrito = codDistrito;
                oRequest.codSector = codSector;
                oRequest.codCluster = codCluster;
                oRequest.codYear = codYear;
                oRequest.codMes = codMes;
                oRequest.codPeriodo = codPeriodo;
                oRequest.codOpcion = codOpcion;
                oRequest.codOficina = codOficina;
                oRequest.otrosParametros = otrosParametros;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<DatosVariacionSKUMandatorios_Prov_Request>(oRequest);
                string dataJson  = mapServices.Obtener_Datos_Variacion_Prov_V1_Rev02(request);

                DatosVariacionSKUMandatorios_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<DatosVariacionSKUMandatorios_Prov_Response>(dataJson);

                return response.datosVariacion;
            }

        #endregion


        //XploraMaps Provincia 
        public E_ExportExcel Obtener_Datos_Tendencia_Prov(string codServicio, string codCanal, string codCliente, string codPais,
            string codOficina ,string codDpto, string codCity, string codDistrito, string codSector, string codCluster, string codYear,
            string codMes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            DatosTendenciaSKUMandatorios_Prov_Request oRequest = new DatosTendenciaSKUMandatorios_Prov_Request();

            oRequest.codOficina = codOficina;
            oRequest.codServicio = codServicio;
            oRequest.codCanal = codCanal;
            oRequest.codCliente = codCliente;
            oRequest.codPais = codPais;
            oRequest.codDpto = codDpto;
            oRequest.codCity = codCity;
            oRequest.codDistrito = codDistrito;
            oRequest.codSector = codSector;
            oRequest.codCluster = codCluster;
            oRequest.codYear = codYear;
            oRequest.codMes = codMes;
            oRequest.codPeriodo = codPeriodo;
            oRequest.codOpcion = codOpcion;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<DatosTendenciaSKUMandatorios_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_Datos_Tendencia_Prov(request);

            DatosTendenciaSKUMandatorios_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<DatosTendenciaSKUMandatorios_Prov_Response>(dataJson);

            return response.datosTendencia;
        }
        
        //XploraMaps Lima 
        public E_ExportExcel Obtener_Datos_Variacion(string codServicio, string codCanal, string codCliente, string codPais,
            string codDpto, string codCity, string codDistrito, string codSector, string codCluster, string codYear,
            string codMes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            DatosVariacionSKUMandatorios_Request oRequest = new DatosVariacionSKUMandatorios_Request();

            oRequest.codServicio = codServicio;
            oRequest.codCanal = codCanal;
            oRequest.codCliente = codCliente;
            oRequest.codPais = codPais;
            oRequest.codDpto = codDpto;
            oRequest.codCity = codCity;
            oRequest.codDistrito = codDistrito;
            oRequest.codSector = codSector;
            oRequest.codCluster = codCluster;
            oRequest.codYear = codYear;
            oRequest.codMes = codMes;
            oRequest.codPeriodo = codPeriodo;
            oRequest.codOpcion = codOpcion;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<DatosVariacionSKUMandatorios_Request>(oRequest);
            dataJson = mapServices.Obtener_Datos_Variacion(request);

            DatosVariacionSKUMandatorios_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<DatosVariacionSKUMandatorios_Response>(dataJson);

            return response.datosVariacion;
        }
        //XploraMaps - Provincia
        public E_ExportExcel Obtener_Datos_Variacion_Prov(string codServicio, string codCanal, string codCliente, string codPais,
            string codOficina,string codDpto, string codCity, string codDistrito, string codSector, string codCluster, string codYear,
            string codMes, string codPeriodo, string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            DatosVariacionSKUMandatorios_Prov_Request oRequest = new DatosVariacionSKUMandatorios_Prov_Request();

            oRequest.codOficina = codOficina;
            oRequest.codServicio = codServicio;
            oRequest.codCanal = codCanal;
            oRequest.codCliente = codCliente;
            oRequest.codPais = codPais;
            oRequest.codDpto = codDpto;
            oRequest.codCity = codCity;
            oRequest.codDistrito = codDistrito;
            oRequest.codSector = codSector;
            oRequest.codCluster = codCluster;
            oRequest.codYear = codYear;
            oRequest.codMes = codMes;
            oRequest.codPeriodo = codPeriodo;
            oRequest.codOpcion = codOpcion;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<DatosVariacionSKUMandatorios_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_Datos_Variacion_Prov(request);

            DatosVariacionSKUMandatorios_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<DatosVariacionSKUMandatorios_Prov_Response>(dataJson);

            return response.datosVariacion;
        }

        #region 1.2.-Entidad - Formato Final - Mostrar Gráfico 01
                public class E_TGraf_01{
                    [JsonProperty("a")]
                    public List<E_TG_Leyenda> Leyendas { get; set; }
                    [JsonProperty("b")]
                    public List<E_TG_Categoria> Categorias { get; set; }
                }
                #region a.-Leyendas
                    public class E_TG_Leyenda{
                        [JsonProperty("a")]
                        public string Leyenda { get; set; }
                    }
                #endregion
                #region b.-Categorias
                    public class E_TG_Categoria{
                        [JsonProperty("a")]
                        public string type { get; set; }
                        [JsonProperty("b")]
                        public string name { get; set; }
                        [JsonProperty("c")]
                        public List<E_TG_Value> values { get; set; }
                    }
                    #region b.1.-Values
                        public class E_TG_Value{
                            [JsonProperty("a")]
                            public string value { get; set; }
                        }
                    #endregion
                #endregion
        #endregion

        public class GraficoVentasVsTendencia_Request { 
                [JsonProperty("a")]
                public string codServicio { get; set; }

                [JsonProperty("b")]
                public string codCanal { get; set; }

                [JsonProperty("c")]
                public string codCliente { get; set; }

                [JsonProperty("d")]
                public string codPais { get; set; }

                [JsonProperty("e")]
                public string codDepartamento { get; set; }

                [JsonProperty("f")]
                public string codProvincia { get; set; }

                [JsonProperty("g")]
                public string codZona { get; set; }

                [JsonProperty("h")]
                public string codDistrito { get; set; }

                [JsonProperty("i")]
                public string codCategoria { get; set; }

                [JsonProperty("j")]
                public string codProducto { get; set; }

                [JsonProperty("k")]
                public string codCluster { get; set; }

                [JsonProperty("l")]
                public string anio { get; set; }

                [JsonProperty("m")]
                public string mes { get; set; }

                [JsonProperty("n")]
                public string codPeriodo { get; set; }

                [JsonProperty("o")]
                public string codOpcion { get; set; }
        }
        public class GraficoVentasVsTendencia_Response : BaseResponse {
            [JsonProperty("a")]
            public List<E_TGraf_01> oTGraf_01 { get; set; }
        }

        public IList<E_TGraf_01> get_GraficoVentasVsTendencia(string codServicio, string codCanal, string codCliente, string codPais,
            string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto, string codCluster,
            string codAnio, string codMes, string codPeriodo,string codOpcion)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            GraficoVentasVsTendencia_Request oRequest = new GraficoVentasVsTendencia_Request();
            oRequest.codServicio = codServicio;
            oRequest.codCanal = codCanal;
                oRequest.codCliente = codCliente;
                oRequest.codPais = codPais;
                oRequest.codDepartamento = codDepartamento;
                oRequest.codProvincia = codProvincia;
                oRequest.codZona = codZona;
                oRequest.codDistrito = codDistrito;
                oRequest.codCategoria = codCategoria;
                oRequest.codProducto = codProducto;
                oRequest.codCluster = codCluster;
                oRequest.anio = codAnio;
                oRequest.mes = codMes;
                oRequest.codPeriodo = codPeriodo;
                oRequest.codOpcion = codOpcion;

                String request = Lucky.CFG.JavaMovil.HelperJson.Serialize<GraficoVentasVsTendencia_Request>(oRequest);
                String dataJson = mapServices.Obtener_Graf_VentasVsPresencia(request);

                GraficoVentasVsTendencia_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<GraficoVentasVsTendencia_Response>(dataJson);

                return response.oTGraf_01;
        }

        #region Request

            public class GraficoVentasVsTendencia_Rev02_Request
            {
                [JsonProperty("a")]
                public string codServicio { get; set; }

                [JsonProperty("b")]
                public string codCanal { get; set; }

                [JsonProperty("c")]
                public string codCliente { get; set; }
                
                //-- Deshabilitado
                //[JsonProperty(""d"")]
                //public string codPais { get; set; }
                //-- Deshabilitado
                //[JsonProperty(""e"")]
                //public string codDepartamento { get; set; }
                //-- Deshabilitado
                //[JsonProperty(""f"")]
                //public string codProvincia { get; set; }
                //-- Deshabilitado
                //[JsonProperty(""g"")]
                //public string codZona { get; set; }
                //-- Deshabilitado
                //[JsonProperty(""h"")]
                //public string codDistrito { get; set; }

                [JsonProperty("i")]
                public string codCategoria { get; set; }

                [JsonProperty("j")]
                public string codProducto { get; set; }

                [JsonProperty("k")]
                public string codCluster { get; set; }

                [JsonProperty("l")]
                public string anio { get; set; }

                [JsonProperty("m")]
                public string mes { get; set; }

                [JsonProperty("n")]
                public string codPeriodo { get; set; }

                [JsonProperty("o")]
                public string codOpcion { get; set; }

                //Se reemplaza este parametro por el Tipo de Ubigeo y Ubigeo. (@Ubigeo)
                [JsonProperty("p")]
                public string codOficina { get; set; }

                //Aqui va el codigo de mercado
                [JsonProperty("q")] // 28-05-2013 - Psa
                public string otrosParametros { get; set; }
            }

            public class GraficoVariacion_Rev02_Request
            {
                [JsonProperty("a")]
                public string codServicio { get; set; }

                [JsonProperty("b")]
                public string codCanal { get; set; }

                [JsonProperty("c")]
                public string codCliente { get; set; }

                /*
                [JsonProperty("d")]
                public string codPais { get; set; }

                [JsonProperty("e")]
                public string codDepartamento { get; set; }

                [JsonProperty("f")]
                public string codProvincia { get; set; }

                [JsonProperty("g")]
                public string codZona { get; set; }

                [JsonProperty("h")]
                public string codDistrito { get; set; }
                */
                [JsonProperty("i")]
                public string codCategoria { get; set; }

                [JsonProperty("j")]
                public string codProducto { get; set; }

                [JsonProperty("k")]
                public string codCluster { get; set; }

                [JsonProperty("l")]
                public string anio { get; set; }

                [JsonProperty("m")]
                public string mes { get; set; }

                [JsonProperty("n")]
                public string codPeriodo { get; set; }

                [JsonProperty("o")]
                public string codOpcion { get; set; }

                //Se reemplaza este parametro por el Tipo de Ubigeo y Ubigeo. (@Ubigeo)
                [JsonProperty("p")]
                public string codOficina { get; set; }

                [JsonProperty("q")] // Add 28-05-2013 - Psa
                public string otrosParametros { get; set; }
            }

        #endregion

        #region Response

            public class GraficoVentasVsTendencia_Rev02_Response : BaseResponse
            {
                [JsonProperty("a")]
                public List<E_TGraf_01> oTGraf_01 { get; set; }
            }

            public class GraficoVariacion_Rev02_Response : BaseResponse
            {
                [JsonProperty("a")]
                public List<E_Grafico_Variacion> oListGraficoVariacion { get; set; }
            }

        #endregion



        #region Graficos Rev02

            public IList<E_TGraf_01> Obtener_GraficoVentasVsTendencia_Rev02(string codServicio, string codCanal, string codCliente, string codCategoria, string codProducto, string codCluster, string codAnio, string codMes, string codPeriodo, string codOficina, string codOpcion, string otrosParametros)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                GraficoVentasVsTendencia_Rev02_Request oRequest = new GraficoVentasVsTendencia_Rev02_Request();
                oRequest.codServicio = codServicio;
                oRequest.codCanal = codCanal;
                oRequest.codCliente = codCliente;
                oRequest.codCategoria = codCategoria;
                oRequest.codProducto = codProducto;
                oRequest.codCluster = codCluster;
                oRequest.anio = codAnio;
                oRequest.mes = codMes;
                oRequest.codPeriodo = codPeriodo;
                oRequest.codOficina = codOficina;
                oRequest.codOpcion = codOpcion;
                oRequest.otrosParametros = otrosParametros;

                String request = Lucky.CFG.JavaMovil.HelperJson.Serialize<GraficoVentasVsTendencia_Rev02_Request>(oRequest);
                String dataJson = mapServices.Obtener_Graf_VentasVsPresencia_Prov_V1_Rev02(request);

                GraficoVentasVsTendencia_Rev02_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<GraficoVentasVsTendencia_Rev02_Response>(dataJson);

                return response.oTGraf_01;
            }

            public GraficoVariacion_Rev02_Response Obtener_GraficoVariacion_Rev02(string codServicio, string codCanal, string codCliente, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto, string codCluster, string codAnio, string codMes, string codPeriodo, string codOficina, string codOpcion, string otrosParametros)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                GraficoVariacion_Rev02_Request oRequest = new GraficoVariacion_Rev02_Request();
                oRequest.codServicio = codServicio;
                oRequest.codCanal = codCanal;
                oRequest.codCliente = codCliente; 
                oRequest.codCategoria = codCategoria;
                oRequest.codProducto = codProducto;
                oRequest.codCluster = codCluster;
                oRequest.anio = codAnio;
                oRequest.mes = codMes;
                oRequest.codPeriodo = codPeriodo;
                oRequest.codOpcion = codOpcion;
                oRequest.codOficina = codOficina;
                oRequest.otrosParametros = otrosParametros;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<GraficoVariacion_Rev02_Request>(oRequest);

                string dataJson = mapServices.Obtener_Grafico_Variacion_V1_Rev02(request);

                GraficoVariacion_Rev02_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<GraficoVariacion_Rev02_Response>(dataJson);

                return response;
            }


        #endregion


    }

   

}