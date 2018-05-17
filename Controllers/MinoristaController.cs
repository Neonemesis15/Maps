using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Servicio;
using Xplora.GIS.Models;
using Xplora.GIS.Models.Util;

namespace Xplora.GIS.Controllers
{


    public class MinoristaController : Controller
    {
        static DataTable dtVenta;
        static DataTable dtPresencia;
        static DataTable dtReportPresencia;
        static DataTable dtReportVentasSubCategoria;

        public MinoristaController() {
        }

        #region ObtenerDatosPaneles

            [HttpPost]
            public JsonResult Obtener_ultimoperiodo(String opcion, String filtros) {
                Utils oUtils = new Utils();
                return Json(oUtils.Obtener_ultimoperiodo(opcion, filtros));
            }

            [HttpPost]//METODOS PARA OBTENER LOS PARAMETROS INICIALES
            public JsonResult Obtener_DatosFiltro(String CodPersona)
            {
                Utils oUtils = new Utils();
                return Json(oUtils.Obtener_DatosFiltros(CodPersona));
            }


            [HttpPost]//hecho
            public JsonResult Obtener_Mercados_Ubigeo( String codCanal, String codCompania,String tipoubigeo, String ubigeo) { 
              Ubigeo_Service oUbigeo = new Ubigeo_Service();
              return Json(oUbigeo.Obtener_Mercados_Ubigeo(codCanal, codCompania, tipoubigeo, ubigeo));
            }

            [HttpPost]//hecho
            public JsonResult Obtener_UniversoMR(String ubigeo, String idPlanning, String idReportsPlanning, String otrosParametros) {
                ClusterRepresentatividad_Service service = new ClusterRepresentatividad_Service();
                return Json(service.Obtener_UniversoMR_Minorista(ubigeo,idPlanning,idReportsPlanning,otrosParametros));
            }

            /*
            [HttpPost]
            public JsonResult Obtener_RangosSKU_Mandatorios(String ubigeo, String idPlanning, String idReportsPlanning, String otrosParametros)
            {
                ClusterRepresentatividad_Service service = new ClusterRepresentatividad_Service();
                return Json(service.Obtener_UniversoMR_Minorista(ubigeo, idPlanning, idReportsPlanning, otrosParametros));
            }

            [HttpPost]
            public JsonResult Obtener_SKU_Mandatorios(String servicio, String canal, String codCliente, String ubigeo, String idReportsPlanning, String otrosParametros)
            {
                ClusterRepresentatividad_Service service = new ClusterRepresentatividad_Service();
                return Json(service.Obtener_UniversoMR_Minorista(ubigeo, idPlanning, idReportsPlanning, otrosParametros));
            }*/

            [HttpPost]//hecho
            public JsonResult Obtener_Ventas_SubCategoria(String ubigeo, String idPlanning, String idReportsPlanning, String otrosParametros)
            {
                NN_Ventas_Service service = new NN_Ventas_Service();
                return Json(service.Obtener_Ventas_Rev02(ubigeo, idReportsPlanning, otrosParametros));
            }

            [HttpPost] //hecho
            public JsonResult Obtener_Elementos_Visibilidad(String servicio, String canal, String codCliente, String ubigeo, String idReportsPlanning, String otrosParametros)
            {
                PresenciaEleVisibilidad_Service service = new PresenciaEleVisibilidad_Service();
                return Json(service.Obtener_Elementos_Visibilidad(servicio, canal, codCliente, ubigeo, idReportsPlanning, otrosParametros));
            }

        #endregion

        #region ObtenerPuntosVenta

            [HttpPost]
            public JsonResult Obtener_PuntoVentaRango(string codCanal, string codPais, string ubigeo, string codRango, string codPeriodo, string otrosParametros)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                return Json(service.Obtener_PuntoVentaRango_Rev02(codCanal, codPais, ubigeo, codRango, codPeriodo, otrosParametros));
            }

            [HttpPost]
            public JsonResult Obtener_PuntoVentaPresenciaSKU(string codCanal, string codPais, string ubigeo, string codProducto, string codPeriodo, string otrosParametros)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                return Json(service.Obtener_PuntoVentaPresenciaSKU_Rev02(codCanal, codPais, ubigeo, codProducto, codPeriodo, otrosParametros));
            }

            [HttpPost] //Nivel Nacional
            public JsonResult Obtener_PuntoVentaElemVisibilidad(string codCanal, string codPais, string ubigeo, string codElemento, string codPeriodo, string otrosParametros)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                return Json(service.Obtener_PuntoVentaElemVisibilidad_Rev02(codCanal, codPais, ubigeo, codElemento, codPeriodo, otrosParametros));
            }

            [HttpPost]
            public JsonResult Obtener_PdvByVentas(string codCliente, string codCanal, string ubigeo, string codSku, string idReportsPlanning, string otrosParametros)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                return Json(service.Obtener_PdvByVentas_Rev02(codCliente, codCanal, ubigeo, codSku, idReportsPlanning, otrosParametros));
            }

        #endregion

        #region ObtenerDatosGrafico

            [HttpPost]
            public JsonResult Obtener_GraficoVentasVsTendencia(string codServicio, string codCanal, string codCliente, string codCategoria, string codProducto, string codCluster, string codAnio, string codMes, string codPeriodo, string codOficina, string codOpcion, string otrosParametros)
            {
                Grafico_Service oGrafico = new Grafico_Service();
                return Json(oGrafico.Obtener_GraficoVentasVsTendencia_Rev02(codServicio, codCanal, codCliente, codCategoria, codProducto, codCluster, codAnio, codMes, codPeriodo, codOficina, codOpcion, otrosParametros));
            }

            [HttpPost]
            public JsonResult Obtener_GraficoVariacion(string codServicio, string codCanal, string codCliente, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto, string codCluster, string codAnio, string codMes, string codPeriodo, string codOficina, string codOpcion, string otrosParametros)
            {
                Grafico_Service service = new Grafico_Service();
                return Json(service.Obtener_GraficoVariacion_Rev02(codServicio, codCanal, codCliente, codPais, codDepartamento, codProvincia, codZona, codDistrito, codCategoria, codProducto, codCluster, codAnio, codMes, codPeriodo, codOficina, codOpcion, otrosParametros));
            }
        
        #endregion

        #region DescargasExcel

            [HttpPost]
            public JsonResult Obtener_PuntoVentaToExcel(string codCanal, string codPais, string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codPresencia, string codPeriodo, int codOpcion, string ubigeo)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                E_ExportExcel data;

                switch (codOpcion)
                {
                    case 1:
                        data = service.Obtener_PuntoVentaPresenciaSKUToExcel_Prov(codCanal, codPais, codOficina, codDepartamento, codProvincia, codZona, codDistrito, codPresencia, codPeriodo);
                        break;
                    case 2:
                        data = service.Obtener_PuntoVentaElemVisibilidadToExcel_NN(codCanal, codPais, ubigeo, codPresencia, codPeriodo);
                        break;

                    case 3:
                        data = service.Obtener_PuntoVentaPresenciaRangoToExcel_Prov(codCanal, codPais, codOficina, codDepartamento, codProvincia, codZona, codDistrito, codPresencia, codPeriodo);
                        break;

                    default:
                        data = new E_ExportExcel();
                        break;
                }

                if (data != null)
                {
                    if (data.Header != null)
                        dtReportPresencia = Util.ConvertToDataTable(data.Header, data.Contents);
                    else
                        dtReportPresencia = new DataTable();
                }
                else
                    dtReportPresencia = new DataTable();

                return Json(true);
            }

            public ExcelFileResult exportarExcel(string nombreArchivo)
            {
                nombreArchivo += ".xls";
                ExcelFileResult actionResult = null;

                actionResult = new ExcelFileResult(dtReportPresencia) { FileDownloadName = nombreArchivo.ToString() };
                dtVenta = null;

                return actionResult;
            }

            [HttpPost]
            public JsonResult Obtener_Datos_Grafico_Excel(string codServicio, string codCanal, string codCliente, string codPais,
                string codDpto, string codCity, string codDistrito, string codSector, string codCluster, string codYear,
                string codMes, string codPeriodo, string codOpcion, string codOficina, string otrosParametros, int tipo)
            {
                Grafico_Service service = new Grafico_Service();
                E_ExportExcel data;

                switch (tipo)
                {
                    case 1:
                        data = service.Obtener_Datos_Tendencia_Rev02(codServicio, codCanal, codCliente, codPais, codDpto, codCity, codDistrito,
                            codSector, codCluster, codYear, codMes, codPeriodo, codOpcion, codOficina, otrosParametros);
                        break;

                    case 2:
                        data = service.Obtener_Datos_Variacion_Rev02(codServicio, codCanal, codCliente, codPais, codDpto, codCity, codDistrito,
                            codSector, codCluster, codYear, codMes, codPeriodo, codOpcion, codOficina, otrosParametros);
                        break;
                    default:
                        data = new E_ExportExcel();
                        break;
                }

                if (data != null)
                {
                    if (data.Header != null)
                        dtReportPresencia = Util.ConvertToDataTable(data.Header, data.Contents);
                    else
                        dtReportPresencia = new DataTable();
                }
                else
                    dtReportPresencia = new DataTable();

                return Json(true);
            }


        #endregion

        #region ObtenerDatos_x_PuntoVenta

            [HttpPost]
            public JsonResult Obtener_Fotos_PuntoVenta(string reportsPlanning, string codPtoVenta)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                return Json(service.Obtener_Fotos_PuntoVenta_Rev02(reportsPlanning, codPtoVenta));
            }

            [HttpPost]
            public JsonResult Obtener_Datos_PuntoVenta(string codPtoVenta, string reportsplanning)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                return Json(service.Obtener_Datos_PuntoVenta_Rev02(codPtoVenta, reportsplanning));
            }

            [HttpPost]
            public JsonResult Obtener_Presencia_PuntoVenta(string codEquipo, string reportsPlanning, string codPtoVenta)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                return Json(service.Obtener_Presencia_PuntoVenta_Rev02(codEquipo, reportsPlanning, codPtoVenta));
            }

            [HttpPost]
            public JsonResult Obtener_Ventas_PuntoVenta(string codEquipo, string reportsPlanning, string codPtoVenta)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                return Json(service.Obtener_Ventas_PuntoVenta_Rev02(codEquipo, reportsPlanning, codPtoVenta));
            }

            [HttpPost]
            public JsonResult Obtener_ElementoVisibilidad_PuntoVenta(string codEquipo, string reportsPlanning, string codPtoVenta)
            {
                PuntoVenta_Service service = new PuntoVenta_Service();
                return Json(service.Obtener_ElementoVisibilidad_PuntoVenta_Rev02(codEquipo, reportsPlanning, codPtoVenta));
            }

        #endregion
    }
}
