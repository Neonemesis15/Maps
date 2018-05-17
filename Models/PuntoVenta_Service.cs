using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucky.Entity.Common.Servicio;
using Newtonsoft.Json;
using Lucky.CFG.JavaMovil;

namespace Xplora.GIS.Models
{

#region Request

    public class Obtener_PuntoVentaRango_NN_Request
    {
        [JsonProperty("a")]
        public string codCanal { get; set; }

        [JsonProperty("b")]
        public string codPais { get; set; }

        [JsonProperty("c")]
        public string ubigeo { get; set; }

        [JsonProperty("d")]
        public string codRango { get; set; }

        [JsonProperty("e")]
        public string codPeriodo { get; set; }

        [JsonProperty("f")] // Otros Parametros
        public string otrosParametros { get; set; }
    }

#endregion

#region Response

    public class Obtener_PuntoVentaRango_NN_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> oListPuntoVenta { get; set; }
    }

#endregion


    public class PuntoVenta_Request
    {
        [JsonProperty("a")]
        public string codEquipo { get; set; }

        [JsonProperty("b")]
        public string codGenerador { get; set; }

        [JsonProperty("c")]
        public string reportsPlanning { get; set; }

        [JsonProperty("d")]
        public string codPais { get; set; }

        [JsonProperty("e")]
        public string codDepartamento { get; set; }

        [JsonProperty("f")]
        public string codProvincia { get; set; }

        [JsonProperty("g")]
        public string codSector { get; set; }

        [JsonProperty("h")]
        public string codDistrito { get; set; }
    }

    public class PuntoVenta_Response
    {
        [JsonProperty("a")]
        public List<E_PuntoVentaMapa> listaPuntosVenta { get; set; }
    }
    
    public class PuntoVentaDato_Request
    {
        [JsonProperty("a")]
        public string codPtoVentaCliente { get; set; }

        [JsonProperty("b")]
        public string reportsPlanning { get; set; }
    }

    public class PuntoVentaDato_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_PuntoVentaDatosMapa puntoVenta { get; set; }
    }

    public class M_PuntoVentaDatosMapa
    {
        [JsonProperty("a")]
        public string codPuntoVenta { get; set; }
        [JsonProperty("c")]
        public string direccion { get; set; }
        [JsonProperty("d")]
        public string distrito { get; set; }
        [JsonProperty("g")]
        public string nombre { get; set; }
        [JsonProperty("b")]
        public string nombrePuntoVenta { get; set; }
        [JsonProperty("e")]
        public string sector { get; set; }
        [JsonProperty("f")]
        public string ultimaVisita { get; set; }
    }

    public class PresenciaPuntoVentaDato_Request
    {
        [JsonProperty("a")]
        public string codEquipo { get; set; }

        [JsonProperty("b")]
        public string reportsPlanning { get; set; }

        [JsonProperty("c")]
        public string codPtoVenta { get; set; }
    }

    public class PresenciaPuntoVentaDato_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_Presencia_PDV> listaPresenciaPDV { get; set; }
    }

    public class ElemVisibilidadPuntoVenta_Request
    {
        [JsonProperty("a")]
        public string codEquipo { get; set; }

        [JsonProperty("b")]
        public string reportsPlanning { get; set; }

        [JsonProperty("c")]
        public string codPtoVenta { get; set; }
    }

    public class ElemVisibilidadPuntoVenta_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_ElemVisibilidad_PDV> listaPresenciaPDV { get; set; }
    }

    public class VentaPuntoVenta_Request
    {
        [JsonProperty("a")]
        public string codEquipo { get; set; }

        [JsonProperty("b")]
        public string reportsPlanning { get; set; }

        [JsonProperty("c")]
        public string codPtoVenta { get; set; }
    }

    public class VentaPuntoVenta_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_Ventas_PDV> listaPresenciaPDV { get; set; }
    }

    public class FotoPuntoVenta_Request
    {
        [JsonProperty("a")]
        public string reportsPlanning { get; set; }

        [JsonProperty("b")]
        public string codPtoVenta { get; set; }
    }

    public class FotoPuntoVenta_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_Foto_PDV fotoPDV { get; set; }
    }

    public class HistorialFotoPuntoVenta_Request
    {
        [JsonProperty("a")]
        public string reportsPlanning { get; set; }

        [JsonProperty("b")]
        public string codPtoVenta { get; set; }
    }

    public class HistorialFotoPuntoVenta_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_HistorialFoto_PDV> historialFotoPDV { get; set; }
    }
    //XploraMaps - Lima
    public class PuntoVentaPresenciaSKU_Request
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
        public string codProducto { get; set; }

        [JsonProperty("h")]
        public string codPeriodo { get; set; }
    }
    public class PuntoVentaPresenciaSKU_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> oListPuntoVenta { get; set; }
    }
    public class PuntoVentaPresenciaSKUToExcel_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel oExportExcel { get; set; }
    }
    //XploraMaps - Provincias
    public class PuntoVentaPresenciaSKU_Prov_Request
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
        public string codProducto { get; set; }

        [JsonProperty("h")]
        public string codPeriodo { get; set; }

        [JsonProperty("i")]//Add 08/11/2012
        public string codOficina { get; set; }
    }
    public class PuntoVentaPresenciaSKU_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> oListPuntoVenta { get; set; }
    }
    public class PuntoVentaPresenciaSKUToExcel_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel oExportExcel { get; set; }
    }

    //XploraMaps - NIVEL NACIONAL
    public class Obtener_PuntoVentaPresenciaSKU_NN_Request
    {
        [JsonProperty("a")]
        public string codCanal { get; set; }

        [JsonProperty("b")]
        public string codPais { get; set; }

        [JsonProperty("c")]
        public string ubigeo { get; set; }

        [JsonProperty("d")]
        public string codProducto { get; set; }

        [JsonProperty("e")]
        public string codPeriodo { get; set; }

        [JsonProperty("f")] // Add 29-05-2013
        public string otrosParametros { get; set; }
    }
    public class Obtener_PuntoVentaPresenciaSKU_NN_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> oListPuntoVenta { get; set; }
    }

    
    public class Obtener_PuntoVentaElemVisibilidad_NN_Request
    {
        [JsonProperty("a")]
        public string codCanal { get; set; }

        [JsonProperty("b")]
        public string codPais { get; set; }

        [JsonProperty("c")]
        public string ubigeo { get; set; }

        [JsonProperty("d")]
        public string codElemento { get; set; }

        [JsonProperty("e")]
        public string codPeriodo { get; set; }

        [JsonProperty("f")] // Add 29-05-2013
        public string otrosParametros { get; set; }
    }



    public class Obtener_PuntoVentaElemVisibilidad_NN_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> oListPuntoVenta { get; set; }
    }




    //XploraMaps - Lima Reemplazado por Obtener_PuntoVentaElemVisibilidad_NN_Request
    public class PuntoVentaPresenciaElemVisibilidad_Request
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
        public string codElemento { get; set; }

        [JsonProperty("h")]
        public string codPeriodo { get; set; }
    }
    public class PuntoVentaPresenciaElemVisibilidad_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> oListPuntoVenta { get; set; }
    }
    
    public class PuntoVentaPresenciaElemVisibilidadToExcel_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel oExportExcel { get; set; }
    }
    //XploraMaps - Provincia
    public class PuntoVentaPresenciaElemVisibilidad_Prov_Request
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
        public string codElemento { get; set; }

        [JsonProperty("h")]
        public string codPeriodo { get; set; }

        [JsonProperty("i")]//Add 
        public string codOficina { get; set; }
    }
    public class PuntoVentaPresenciaElemVisibilidad_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> oListPuntoVenta { get; set; }
    }
    public class PuntoVentaPresenciaElemVisibilidadToExcel_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel oExportExcel { get; set; }
    }

    //XploraMaps - Bodegas
    public class Obtener_PdvByVentas_Request
    {
        [JsonProperty("a")]
        public string codCliente { get; set; }
        [JsonProperty("b")]
        public string codCanal { get; set; }
        [JsonProperty("c")]
        public string ubigeo { get; set; }
        [JsonProperty("d")]
        public string codSku { get; set; }
        [JsonProperty("e")]
        public string idReportsPlanning { get; set; }
        [JsonProperty("f")] // Add 29-05-2013
        public string otrosParametros { get; set; }
    }

    public class Obtener_PdvByVentas_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> listaPtoVenta { get; set; }
    }



    //Xplora Maps - Lima

    //Reemplazado por Obtener_PuntoVentaRango_NN_Request
    public class PuntoVentaPresenciaRango_Request
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
        public string codRango { get; set; }

        [JsonProperty("h")]
        public string codPeriodo { get; set; }
    }
    public class PuntoVentaPresenciaRango_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> oListPuntoVenta { get; set; }
    }
    public class PuntoVentaPresenciaRangoToExcel_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel oExportExcel { get; set; }
    }
    //Xplora Maps - Provincia
    public class PuntoVentaPresenciaRango_Prov_Request
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
        public string codRango { get; set; }

        [JsonProperty("h")]
        public string codPeriodo { get; set; }

        [JsonProperty("i")]//Add 08/11/2012
        public string codOficina { get; set; }
    }
    public class PuntoVentaPresenciaRango_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public List<E_PresenciaPDV> oListPuntoVenta { get; set; }
    }
    public class PuntoVentaPresenciaRangoToExcel_Prov_Response : BaseResponse
    {
        [JsonProperty("a")]
        public E_ExportExcel oExportExcel { get; set; }
    }


    public class PuntoVenta_Service
    {
        public List<E_PuntoVentaMapa> obtener_puntoVenta(string codEquipo, string codGenerador, string reportsPlanning, string codPais, string codDepartamento, string codProvincia, string codSector, string codDistrito)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVenta_Request oRequest = new PuntoVenta_Request();
            oRequest.codEquipo = codEquipo;
            oRequest.codGenerador = codGenerador;
            oRequest.reportsPlanning = reportsPlanning;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codSector = codSector;
            oRequest.codDistrito = codDistrito;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVenta_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntosVentaMapa(request);

            PuntoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVenta_Response>(dataJson);

            return response.listaPuntosVenta;
        }

        public E_PuntoVentaDatosMapa obtener_datoPuntoVenta(string codPtoVentaCliente, string reportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaDato_Request oRequest = new PuntoVentaDato_Request();
            oRequest.codPtoVentaCliente = codPtoVentaCliente;
            oRequest.reportsPlanning = reportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaDato_Request>(oRequest);
            dataJson = mapServices.Obtener_DatosPuntosVentaMapa(request);

            PuntoVentaDato_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaDato_Response>(dataJson);

            return response.puntoVenta;
        }

        public List<E_Presencia_PDV> obtener_presencia_puntoVenta(string codEquipo, string reportsPlanning, string codPtoVenta)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PresenciaPuntoVentaDato_Request oRequest = new PresenciaPuntoVentaDato_Request();
            oRequest.codEquipo = codEquipo;            
            oRequest.reportsPlanning = reportsPlanning;
            oRequest.codPtoVenta = codPtoVenta;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PresenciaPuntoVentaDato_Request>(oRequest);
            dataJson = mapServices.Obtener_Presencia_PuntoVenta(request);

            PresenciaPuntoVentaDato_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PresenciaPuntoVentaDato_Response>(dataJson);

            return response.listaPresenciaPDV;
        }

        public List<E_Ventas_PDV> obtener_venta_puntoVenta(string codEquipo, string reportsPlanning, string codPtoVenta)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            VentaPuntoVenta_Request oRequest = new VentaPuntoVenta_Request();
            oRequest.codEquipo = codEquipo;
            oRequest.reportsPlanning = reportsPlanning;
            oRequest.codPtoVenta = codPtoVenta;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<VentaPuntoVenta_Request>(oRequest);
            dataJson = mapServices.Obtener_Venta_PuntoVenta(request);

            VentaPuntoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<VentaPuntoVenta_Response>(dataJson);

            return response.listaPresenciaPDV;
        }

        public List<E_ElemVisibilidad_PDV> obtener_elemvisibilidad_puntoVenta(string codEquipo, string reportsPlanning, string codPtoVenta)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            ElemVisibilidadPuntoVenta_Request oRequest = new ElemVisibilidadPuntoVenta_Request();
            oRequest.codEquipo = codEquipo;
            oRequest.reportsPlanning = reportsPlanning;
            oRequest.codPtoVenta = codPtoVenta;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<ElemVisibilidadPuntoVenta_Request>(oRequest);
            dataJson = mapServices.Obtener_ElemVisibilida_PuntoVenta(request);

            ElemVisibilidadPuntoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<ElemVisibilidadPuntoVenta_Response>(dataJson);

            return response.listaPresenciaPDV;
        }

        public E_Foto_PDV obtener_foto_puntoVenta(string reportsPlanning, string codPtoVenta)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            FotoPuntoVenta_Request oRequest = new FotoPuntoVenta_Request();
            oRequest.reportsPlanning = reportsPlanning;
            oRequest.codPtoVenta = codPtoVenta;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<FotoPuntoVenta_Request>(oRequest);
            dataJson = mapServices.Obtener_Foto_PuntoVenta(request);

            FotoPuntoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<FotoPuntoVenta_Response>(dataJson);

            return response.fotoPDV;
        }

        public List<E_HistorialFoto_PDV> obtener_historialfoto_puntoVenta(string reportsPlanning, string codPtoVenta)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            HistorialFotoPuntoVenta_Request oRequest = new HistorialFotoPuntoVenta_Request();
            oRequest.reportsPlanning = reportsPlanning;
            oRequest.codPtoVenta = codPtoVenta;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<HistorialFotoPuntoVenta_Request>(oRequest);
            dataJson = mapServices.Obtener_HistorialFoto_PuntoVenta(request);

            HistorialFotoPuntoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<HistorialFotoPuntoVenta_Response>(dataJson);

            return response.historialFotoPDV;
        }
        //XploraMaps - Lima
        public List<E_PresenciaPDV> Obtener_PuntoVentaPresenciaSKU(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codProducto, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaSKU_Request oRequest = new PuntoVentaPresenciaSKU_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codProducto = codProducto;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaSKU_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaSKU_NN(request);

            PuntoVentaPresenciaSKU_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaSKU_Response>(dataJson);

            return response.oListPuntoVenta;
        }

        //XploraMaps - Lima
        public List<E_PresenciaPDV> Obtener_PuntoVentaPresenciaSKU_NN(string codCanal, string codPais, string ubigeo, string codProducto, string codPeriodo, string otrosParametros)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_PuntoVentaPresenciaSKU_NN_Request oRequest = new Obtener_PuntoVentaPresenciaSKU_NN_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.ubigeo = ubigeo;
            oRequest.codPeriodo= codPeriodo;
            oRequest.codProducto = codProducto;
            oRequest.otrosParametros = otrosParametros;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PuntoVentaPresenciaSKU_NN_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaSKU_NN(request);

            Obtener_PuntoVentaPresenciaSKU_NN_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_PuntoVentaPresenciaSKU_NN_Response>(dataJson);

            return response.oListPuntoVenta;
        }


        //XploraMaps - Provincia
        public List<E_PresenciaPDV> Obtener_PuntoVentaPresenciaSKU_Prov(string codCanal, string codPais,string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codProducto, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaSKU_Prov_Request oRequest = new PuntoVentaPresenciaSKU_Prov_Request();
            oRequest.codOficina = codOficina;
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codProducto = codProducto;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaSKU_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaSKU_Prov(request);

            PuntoVentaPresenciaSKU_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaSKU_Prov_Response>(dataJson);

            return response.oListPuntoVenta;
        }

        
        //XploraMaps - Nivel Nacional
        public List<E_PresenciaPDV> Obtener_PuntoVentaElemVisibilidad_NN(string codCanal, string codPais, string ubigeo,  string codElemento, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_PuntoVentaElemVisibilidad_NN_Request oRequest = new Obtener_PuntoVentaElemVisibilidad_NN_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.ubigeo = ubigeo;
            oRequest.codElemento = codElemento;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PuntoVentaElemVisibilidad_NN_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaElemVisibilidad_NN(request);

            Obtener_PuntoVentaElemVisibilidad_NN_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_PuntoVentaElemVisibilidad_NN_Response>(dataJson);

            return response.oListPuntoVenta;
        }


        //Fueron Reemplazados por 	Obtener_PuntoVentaElemVisibilidad_NN
        //Xplora Maps -Lima
        public List<E_PresenciaPDV> Obtener_PuntoVentaPresenciaElemVisibilidad(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codElemento, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaElemVisibilidad_Request oRequest = new PuntoVentaPresenciaElemVisibilidad_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codElemento = codElemento;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaElemVisibilidad_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaElemVisibilidad(request);

            PuntoVentaPresenciaElemVisibilidad_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaElemVisibilidad_Response>(dataJson);

            return response.oListPuntoVenta;
        }
        //Xplora Maps - Provincia
        public List<E_PresenciaPDV> Obtener_PuntoVentaPresenciaElemVisibilidad_Prov(string codCanal, string codPais, string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codElemento, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaElemVisibilidad_Prov_Request oRequest = new PuntoVentaPresenciaElemVisibilidad_Prov_Request();
            oRequest.codOficina = codOficina;
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codElemento = codElemento;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaElemVisibilidad_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaElemVisibilidad_Prov(request);

            PuntoVentaPresenciaElemVisibilidad_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaElemVisibilidad_Prov_Response>(dataJson);

            return response.oListPuntoVenta;
        }

        //Xplora Maps - Bodegas
        
        public List<E_PresenciaPDV> Obtener_PdvByVentas(string codCliente, string codCanal, string ubigeo, string codSku, string idReportsPlanning)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_PdvByVentas_Request oRequest = new Obtener_PdvByVentas_Request();
            oRequest.codCliente = codCliente;
            oRequest.codCanal = codCanal;
            oRequest.ubigeo = ubigeo;
            oRequest.codSku = codSku;
            oRequest.idReportsPlanning = idReportsPlanning;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PdvByVentas_Request>(oRequest);
            dataJson = mapServices.Obtener_PdvByVentas(request);

            Obtener_PdvByVentas_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_PdvByVentas_Response>(dataJson);

            return response.listaPtoVenta;
        }

        public List<E_PresenciaPDV> Obtener_PuntoVentaRango_NN(string codCanal, string codPais, string ubigeo, string codRango, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_PuntoVentaRango_NN_Request oRequest = new Obtener_PuntoVentaRango_NN_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.ubigeo = ubigeo;
            oRequest.codRango = codRango;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PuntoVentaRango_NN_Request>(oRequest);
            //dataJson = mapServices.Obtener_PuntoVentaPresenciaRango(request);
            dataJson = mapServices.Obtener_PuntoVentaRango_NN(request);

            Obtener_PuntoVentaRango_NN_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_PuntoVentaRango_NN_Response>(dataJson);

            return response.oListPuntoVenta;
        }

        #region ObtenerPuntosVenta Rev02

            public List<E_PresenciaPDV> Obtener_PuntoVentaRango_Rev02(string codCanal, string codPais, string ubigeo, string codRango, string codPeriodo, string otrosParametros)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");
                Obtener_PuntoVentaRango_NN_Request oRequest = new Obtener_PuntoVentaRango_NN_Request();
                oRequest.codCanal = codCanal;
                oRequest.codPais = codPais;
                oRequest.ubigeo = ubigeo;
                oRequest.codRango = codRango;
                oRequest.codPeriodo = codPeriodo;
                oRequest.otrosParametros = otrosParametros;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PuntoVentaRango_NN_Request>(oRequest);
                string dataJson = mapServices.Obtener_PuntoVentaRango_NN_V1_Rev02(request);

                Obtener_PuntoVentaRango_NN_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_PuntoVentaRango_NN_Response>(dataJson);

                return response.oListPuntoVenta;
            }

            public List<E_PresenciaPDV> Obtener_PuntoVentaPresenciaSKU_Rev02(string codCanal, string codPais, string ubigeo, string codProducto, string codPeriodo, string otrosParametros)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                Obtener_PuntoVentaPresenciaSKU_NN_Request oRequest = new Obtener_PuntoVentaPresenciaSKU_NN_Request();
                oRequest.codCanal = codCanal;
                oRequest.codPais = codPais;
                oRequest.ubigeo = ubigeo;
                oRequest.codPeriodo = codPeriodo;
                oRequest.codProducto = codProducto;
                oRequest.otrosParametros = otrosParametros;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PuntoVentaPresenciaSKU_NN_Request>(oRequest);
                string dataJson = mapServices.Obtener_PuntoVentaPresenciaSKU_NN_V1_Rev02(request);

                Obtener_PuntoVentaPresenciaSKU_NN_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_PuntoVentaPresenciaSKU_NN_Response>(dataJson);

                return response.oListPuntoVenta;
            }

            public List<E_PresenciaPDV> Obtener_PuntoVentaElemVisibilidad_Rev02(string codCanal, string codPais, string ubigeo, string codElemento, string codPeriodo, string otrosParametros)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                Obtener_PuntoVentaElemVisibilidad_NN_Request oRequest = new Obtener_PuntoVentaElemVisibilidad_NN_Request();
                oRequest.codCanal = codCanal;
                oRequest.codPais = codPais;
                oRequest.ubigeo = ubigeo;
                oRequest.codElemento = codElemento;
                oRequest.codPeriodo = codPeriodo;
                oRequest.otrosParametros = otrosParametros;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PuntoVentaElemVisibilidad_NN_Request>(oRequest);
                string dataJson = mapServices.Obtener_PuntoVentaElemVisibilidad_NN_V1_Rev02(request);

                Obtener_PuntoVentaElemVisibilidad_NN_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_PuntoVentaElemVisibilidad_NN_Response>(dataJson);

                return response.oListPuntoVenta;
            }

            public List<E_PresenciaPDV> Obtener_PdvByVentas_Rev02(string codCliente, string codCanal, string ubigeo, string codSku, string idReportsPlanning, string otrosParametros)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                Obtener_PdvByVentas_Request oRequest = new Obtener_PdvByVentas_Request();
                oRequest.codCliente = codCliente;
                oRequest.codCanal = codCanal;
                oRequest.ubigeo = ubigeo;
                oRequest.codSku = codSku;
                oRequest.idReportsPlanning = idReportsPlanning;
                oRequest.otrosParametros = otrosParametros;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PdvByVentas_Request>(oRequest);
                string dataJson = mapServices.Obtener_PdvByVentas_V1_Rev02(request);

                Obtener_PdvByVentas_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_PdvByVentas_Response>(dataJson);

                return response.listaPtoVenta;
            }

        #endregion


        #region ObtenerDatos x PuntoVenta Rev02

            public E_Foto_PDV Obtener_Fotos_PuntoVenta_Rev02(string reportsPlanning, string codPtoVenta)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                FotoPuntoVenta_Request oRequest = new FotoPuntoVenta_Request();
                oRequest.reportsPlanning = reportsPlanning;
                oRequest.codPtoVenta = codPtoVenta;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<FotoPuntoVenta_Request>(oRequest);
                string dataJson = mapServices.Obtener_Foto_PuntoVenta(request);

                FotoPuntoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<FotoPuntoVenta_Response>(dataJson);

                return response.fotoPDV;
            }

            public E_PuntoVentaDatosMapa Obtener_Datos_PuntoVenta_Rev02(string codPtoVentaCliente, string reportsPlanning)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                PuntoVentaDato_Request oRequest = new PuntoVentaDato_Request();
                oRequest.codPtoVentaCliente = codPtoVentaCliente;
                oRequest.reportsPlanning = reportsPlanning;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaDato_Request>(oRequest);
                string dataJson = mapServices.Obtener_DatosPuntosVentaMapa(request);

                PuntoVentaDato_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaDato_Response>(dataJson);

                return response.puntoVenta;
            }

            public List<E_Presencia_PDV> Obtener_Presencia_PuntoVenta_Rev02(string codEquipo, string reportsPlanning, string codPtoVenta)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                PresenciaPuntoVentaDato_Request oRequest = new PresenciaPuntoVentaDato_Request();
                oRequest.codEquipo = codEquipo;
                oRequest.reportsPlanning = reportsPlanning;
                oRequest.codPtoVenta = codPtoVenta;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PresenciaPuntoVentaDato_Request>(oRequest);
                string dataJson = mapServices.Obtener_Presencia_PuntoVenta(request);

                PresenciaPuntoVentaDato_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PresenciaPuntoVentaDato_Response>(dataJson);

                return response.listaPresenciaPDV;
            }

            public List<E_Ventas_PDV> Obtener_Ventas_PuntoVenta_Rev02(string codEquipo, string reportsPlanning, string codPtoVenta)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                VentaPuntoVenta_Request oRequest = new VentaPuntoVenta_Request();
                oRequest.codEquipo = codEquipo;
                oRequest.reportsPlanning = reportsPlanning;
                oRequest.codPtoVenta = codPtoVenta;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<VentaPuntoVenta_Request>(oRequest);
                string dataJson = mapServices.Obtener_Venta_PuntoVenta(request);

                VentaPuntoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<VentaPuntoVenta_Response>(dataJson);

                return response.listaPresenciaPDV;
            }

            public List<E_ElemVisibilidad_PDV> Obtener_ElementoVisibilidad_PuntoVenta_Rev02(string codEquipo, string reportsPlanning, string codPtoVenta)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                ElemVisibilidadPuntoVenta_Request oRequest = new ElemVisibilidadPuntoVenta_Request();
                oRequest.codEquipo = codEquipo;
                oRequest.reportsPlanning = reportsPlanning;
                oRequest.codPtoVenta = codPtoVenta;

                string request = Lucky.CFG.JavaMovil.HelperJson.Serialize<ElemVisibilidadPuntoVenta_Request>(oRequest);
                string dataJson = mapServices.Obtener_ElemVisibilida_PuntoVenta(request);

                ElemVisibilidadPuntoVenta_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<ElemVisibilidadPuntoVenta_Response>(dataJson);

                return response.listaPresenciaPDV;
            }

        #endregion


            //Reemplazado por Obtener_PuntoVentaRango_NN
        //Xplora Maps - Lima
        public List<E_PresenciaPDV> Obtener_PuntoVentaPresenciaRango(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codRango, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaRango_Request oRequest = new PuntoVentaPresenciaRango_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codRango = codRango;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaRango_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaRango(request);

            PuntoVentaPresenciaRango_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaRango_Response>(dataJson);

            return response.oListPuntoVenta;
        }
        //Xplora Maps - Provincia
        public List<E_PresenciaPDV> Obtener_PuntoVentaPresenciaRango_Prov(string codCanal, string codPais,string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codRango, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaRango_Prov_Request oRequest = new PuntoVentaPresenciaRango_Prov_Request();
            oRequest.codOficina = codOficina;
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codRango = codRango;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaRango_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaRango_Prov(request);

            PuntoVentaPresenciaRango_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaRango_Prov_Response>(dataJson);

            return response.oListPuntoVenta;
        }
        
        //Xplora Maps - Lima
        public E_ExportExcel Obtener_PuntoVentaPresenciaSKUToExcel(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codProducto, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaSKU_Request oRequest = new PuntoVentaPresenciaSKU_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codProducto = codProducto;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaSKU_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaSKUToExcel(request);

            PuntoVentaPresenciaSKUToExcel_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaSKUToExcel_Response>(dataJson);

            return response.oExportExcel;
        }
        
        //Xplora Maps - Provincia
        public E_ExportExcel Obtener_PuntoVentaPresenciaSKUToExcel_Prov(string codCanal, string codPais, string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codProducto, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaSKU_Prov_Request oRequest = new PuntoVentaPresenciaSKU_Prov_Request();

            oRequest.codOficina = codOficina;//Add
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codProducto = codProducto;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaSKU_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaSKUToExcel_Prov(request);

            PuntoVentaPresenciaSKUToExcel_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaSKUToExcel_Prov_Response>(dataJson);

            return response.oExportExcel;
        }

        //Xplora Maps - Nivel Nacional
        public E_ExportExcel Obtener_PuntoVentaElemVisibilidadToExcel_NN(string codCanal, string codPais, string ubigeo, string codElemento, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            Obtener_PuntoVentaElemVisibilidad_NN_Request oRequest = new Obtener_PuntoVentaElemVisibilidad_NN_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.ubigeo = ubigeo;
            oRequest.codElemento = codElemento;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PuntoVentaElemVisibilidad_NN_Request>(oRequest);
            //dataJson = mapServices.Obtener_PuntoVentaPresenciaElemVisibilidadToExcel(request);
            dataJson = mapServices.Obtener_PuntoVentaElemVisibilidadToExcel_NN(request);

            PuntoVentaPresenciaElemVisibilidadToExcel_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaElemVisibilidadToExcel_Response>(dataJson);

            return response.oExportExcel;
        }


        //Xplora Maps - Lima  --REEMPLAZADO POR Obtener_PuntoVentaElemVisibilidadToExcel_NN
        public E_ExportExcel Obtener_PuntoVentaPresenciaElemVisibilidadToExcel(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codElemento, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaElemVisibilidad_Request oRequest = new PuntoVentaPresenciaElemVisibilidad_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codElemento = codElemento;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaElemVisibilidad_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaElemVisibilidadToExcel(request);

            PuntoVentaPresenciaElemVisibilidadToExcel_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaElemVisibilidadToExcel_Response>(dataJson);

            return response.oExportExcel;
        }
        
        //Xplora Maps - Provincia
        public E_ExportExcel Obtener_PuntoVentaPresenciaElemVisibilidadToExcel_Prov(string codCanal, string codPais,string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codElemento, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaElemVisibilidad_Prov_Request oRequest = new PuntoVentaPresenciaElemVisibilidad_Prov_Request();
            oRequest.codOficina = codOficina;
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codElemento = codElemento;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaElemVisibilidad_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaElemVisibilidadToExcel_Prov(request);

            PuntoVentaPresenciaElemVisibilidadToExcel_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaElemVisibilidadToExcel_Prov_Response>(dataJson);

            return response.oExportExcel;
        }
        
        //XploraMaps - Lima
        public E_ExportExcel Obtener_PuntoVentaPresenciaRangoToExcel(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codRango, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaRango_Request oRequest = new PuntoVentaPresenciaRango_Request();
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codRango = codRango;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaRango_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaRangoToExcel(request);

            PuntoVentaPresenciaRangoToExcel_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaRangoToExcel_Response>(dataJson);

            return response.oExportExcel;
        }
        //XploraMaps - Provincias
        public E_ExportExcel Obtener_PuntoVentaPresenciaRangoToExcel_Prov(string codCanal, string codPais,string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codRango, string codPeriodo)
        {
            MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

            PuntoVentaPresenciaRango_Prov_Request oRequest = new PuntoVentaPresenciaRango_Prov_Request();
            oRequest.codOficina = codOficina;
            oRequest.codCanal = codCanal;
            oRequest.codPais = codPais;
            oRequest.codDepartamento = codDepartamento;
            oRequest.codProvincia = codProvincia;
            oRequest.codZona = codZona;
            oRequest.codDistrito = codDistrito;
            oRequest.codRango = codRango;
            oRequest.codPeriodo = codPeriodo;

            string request;
            string dataJson;

            request = Lucky.CFG.JavaMovil.HelperJson.Serialize<PuntoVentaPresenciaRango_Prov_Request>(oRequest);
            dataJson = mapServices.Obtener_PuntoVentaPresenciaRangoToExcel_Prov(request);

            PuntoVentaPresenciaRangoToExcel_Prov_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<PuntoVentaPresenciaRangoToExcel_Prov_Response>(dataJson);

            return response.oExportExcel;
        }


        #region NivelNacional

        #region Obtener_PdvByVentasToExcel

            public class Obtener_PdvByVentasToExcel_Request
            {
                [JsonProperty("a")]
                public string codCliente { get; set; }
                [JsonProperty("b")]
                public string codCanal { get; set; }
                [JsonProperty("c")]
                public string ubigeo { get; set; }
                [JsonProperty("d")]
                public string codSku { get; set; }
                [JsonProperty("e")]
                public string codPeriodo { get; set; }
            }

            public class Obtener_PdvByVentasToExcel_Response : BaseResponse
            {
                [JsonProperty("a")]
                public E_ExportExcel oExportExcel { get; set; }
            }

        /*
            public class E_ExportExcel
            {
               

                [JsonProperty("b")]
                public string[][] Contents { get; set; }
                [JsonProperty("a")]
                public string[] Header { get; set; }
            }
        */

            //Nivel Nacional
            public E_ExportExcel Obtener_PdvByVentasToExcel(string codCliente, string codCanal, string ubigeo, string codSku, string codPeriodo)
            {
                MapService.Ges_MapsServiceClient mapServices = new MapService.Ges_MapsServiceClient("BasicHttpBinding_IGes_MapsService");

                Obtener_PdvByVentasToExcel_Request oRequest = new Obtener_PdvByVentasToExcel_Request();
                oRequest.codCanal = codCanal;
                oRequest.codCliente = codCliente;
                oRequest.ubigeo = ubigeo;
                oRequest.codSku = codSku;
                oRequest.codPeriodo = codPeriodo;

                string request;
                string dataJson;

                request = Lucky.CFG.JavaMovil.HelperJson.Serialize<Obtener_PdvByVentasToExcel_Request>(oRequest);
                dataJson = mapServices.Obtener_PdvByVentasToExcel(request);

                Obtener_PdvByVentasToExcel_Response response = Lucky.CFG.JavaMovil.HelperJson.Deserialize<Obtener_PdvByVentasToExcel_Response>(dataJson);

                return response.oExportExcel;
            }

            #endregion

        #endregion

    }
}