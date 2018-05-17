using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Servicio;
using Xplora.GIS.Models;
using Xplora.GIS.Models.Util;
using System;





namespace Xplora.GIS.Controllers
{
    /// <summary>
    /// Class ReportsController
    /// </summary>
    /// 

    public class ReportsController : Controller
    {
        //
        // GET: /Reports/
        private List<EPlaning> listplanning;
        private Planning bplanning;
        private PuntosDV bpuntosdv;
        private Company bcompany;
        private List<ECompany> listcompany;

        static DataTable dtVenta;
        static DataTable dtPresencia;
        static DataTable dtReportPresencia;
        static DataTable dtReportVentasSubCategoria;

        public string modulo_id = "XPL_MAPS_COLGATE";
        public string person_id = "";
        public string company_id = "";
        public string machine_id = "";



        public ReportsController()
        {
            listplanning = new List<EPlaning>();
            bplanning = new Planning();
            bpuntosdv = new PuntosDV();
            bcompany = new Company();
            listcompany = new List<ECompany>();
        }

        #region Views GET
        
        [HttpGet]
        public ActionResult SeguimientoGPS()
        {
            try
            {
                person_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data"], "usr");
                company_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data2"], "usr2");
                machine_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data3"], "usr3"); ;
            }
            catch (System.Exception e)
            {
                person_id = "";
                company_id = "";
                machine_id = "";
            }

            //person_id = "2494";
            //company_id = "1562";
            //machine_id = "";

            if (person_id == "" || person_id == null)
            {
                try
                {
                    person_id = Session["usuario"].ToString();
                    company_id = Session["company"].ToString();
                    machine_id = Session["machine"].ToString();

                    //Grabando el Ingreso al Maps
                    //Sesiones ses = new Sesiones();
                    //int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

                    //if (respuesta != 0)
                    //{
                    //    Response.Redirect("http://sige.lucky.com.pe");
                    //}

                    Session["usuario"] = person_id;
                    Session["company"] = company_id;
                    Session["machine"] = machine_id;

                    return View();

                }
                catch (Exception e)
                {
                    Response.Redirect("http://sige.lucky.com.pe");
                    return null;
                }

            }
            else
            {
                Session["usuario"] = person_id;
                Session["company"] = company_id;
                Session["machine"] = machine_id;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Lima()
        {
            person_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data"], "usr");
            company_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data2"], "usr2");
            machine_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data3"], "usr3"); ;

            if (person_id == "" || person_id == null)
            {
                person_id = Session["usuario"].ToString();
                company_id = Session["company"].ToString();
                machine_id = Session["machine"].ToString();
            }

            if (person_id == "" || person_id == null)
            {
                Response.Redirect("http://sige.lucky.com.pe");
                //System.Web.HttpContext.Current.Response.Redirect("www.sige.lucky.com.pe");
            }

            //Grabando el Ingreso al Maps
            Sesiones ses = new Sesiones();
            int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

            if (respuesta != 0)
            {
                Response.Redirect("http://sige.lucky.com.pe");
            }

            Session["usuario"] = person_id;
            Session["company"] = company_id;
            Session["machine"] = machine_id;

            return View();
        }

        [HttpGet]
        public ActionResult Province()
        {
            person_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data"], "usr");
            company_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data2"], "usr2");
            machine_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data3"], "usr3"); ;

            if (person_id == "" || person_id == null)
            {
                person_id = Session["usuario"].ToString();
                company_id = Session["company"].ToString();
                machine_id = Session["machine"].ToString();
            }

            if (person_id == "" || person_id == null)
            {
                Response.Redirect("http://sige.lucky.com.pe");
                //System.Web.HttpContext.Current.Response.Redirect("www.sige.lucky.com.pe");
            }

            //Grabando el Ingreso al Maps
            Sesiones ses = new Sesiones();
            int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id,machine_id);

            if (respuesta != 0)
            {
                Response.Redirect("http://sige.lucky.com.pe");
            }

            Session["usuario"] = person_id;
            Session["company"] = company_id;
            Session["machine"] = machine_id;

            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                person_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data"], "usr");
                company_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data2"], "usr2");
                machine_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data3"], "usr3"); ;
            }
            catch(System.Exception e){
                person_id = "";
                company_id = "";
                machine_id = "";
            }

            if (person_id == "" || person_id == null)
            {
                Response.Redirect("http://sige.lucky.com.pe");
                //System.Web.HttpContext.Current.Response.Redirect("www.sige.lucky.com.pe");
            }

            //Grabando el Ingreso al Maps
            Sesiones ses = new Sesiones();
            int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

            if (respuesta != 0)
            {
                Response.Redirect("http://sige.lucky.com.pe");
            }

            Session["usuario"] = person_id;
            Session["company"] = company_id;
            Session["machine"] = machine_id;

            return Index();
        }

        [HttpGet]
        public ActionResult ReportesIngresos()
        {
            try
            {
                person_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data"], "usr");
                company_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data2"], "usr2");
                machine_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data3"], "usr3"); ;
            }
            catch (System.Exception e)
            {
                person_id = "";
                company_id = "";
                machine_id = "";
            }

            //person_id = "2494";
            //company_id = "1562";
            //machine_id = "";

            if (person_id == "" || person_id == null)
            {
                try
                {
                    person_id = Session["usuario"].ToString();
                    company_id = Session["company"].ToString();
                    machine_id = Session["machine"].ToString();

                    //Grabando el Ingreso al Maps
                    //Sesiones ses = new Sesiones();
                    //int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

                    //if (respuesta != 0)
                    //{
                        //Response.Redirect("http://sige.lucky.com.pe");
                    //}

                    Session["usuario"] = person_id;
                    Session["company"] = company_id;
                    Session["machine"] = machine_id;

                    return View();

                }
                catch (Exception e)
                {
                    Response.Redirect("http://sige.lucky.com.pe");
                    return null;
                }

            }
            else
            {
                Session["usuario"] = person_id;
                Session["company"] = company_id;
                Session["machine"] = machine_id;
                return View();
            }
        }

        #endregion

        #region Partial Views GET

        [OutputCache(Duration = 10)]
        public ActionResult Principal()
        {
           try
            {
                person_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data"], "usr");
                company_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data2"], "usr2");
                machine_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data3"], "usr3"); ;
            }
            catch(System.Exception e){
                person_id = "";
                company_id = "";
                machine_id = "";
            }


            if (person_id == "" || person_id == null)
            {
                person_id = Session["usuario"].ToString();
                company_id = Session["company"].ToString();
                machine_id = Session["machine"].ToString();
            }

            //Grabando el Ingreso al Maps
            Sesiones ses = new Sesiones();
            int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

            if (respuesta != 0)
            {
                Response.Redirect("http://sige.lucky.com.pe");
            }

            Session["usuario"] = person_id;
            Session["company"] = company_id;
            Session["machine"] = machine_id;

            return PartialView();
        }

        [OutputCache(Duration = 10)]
        public ActionResult Mayorista()
        {
            try
            {
                person_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data"], "usr");
                company_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data2"], "usr2");
                machine_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data3"], "usr3"); ;
            }
            catch (System.Exception e)
            {
                person_id = "";
                company_id = "";
                machine_id = "";
            }

            person_id = "2494";
            company_id = "1562";
            machine_id = "";

            if (person_id == "" || person_id == null)
            {
                try
                {
                    person_id = Session["usuario"].ToString();
                    company_id = Session["company"].ToString();
                    machine_id = Session["machine"].ToString();

                    //Grabando el Ingreso al Maps
                    Sesiones ses = new Sesiones();
                    int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

                    if (respuesta != 0)
                    {
                        Response.Redirect("http://sige.lucky.com.pe");
                    }

                    Session["usuario"] = person_id;
                    Session["company"] = company_id;
                    Session["machine"] = machine_id;

                    return View();

                }
                catch (Exception e)
                {
                    Response.Redirect("http://sige.lucky.com.pe");
                    return null;
                }

            }
            else
            {

                //Grabando el Ingreso al Maps
                /*
                Sesiones ses = new Sesiones();
                int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

                if (respuesta != 0)
                {
                    Response.Redirect("http://sige.lucky.com.pe");
                }
                */
                Session["usuario"] = person_id;
                Session["company"] = company_id;
                Session["machine"] = machine_id;
                return View();
            }
        }

        [OutputCache(Duration = 10)]
        public ActionResult Minorista()
        {
            try
            {
                person_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data"], "usr");
                company_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data2"], "usr2");
                machine_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data3"], "usr3"); ;
            }
            catch (System.Exception e)
            {
                person_id = "";
                company_id = "";
                machine_id = "";
            }

            person_id = "2494";
            company_id = "1562";
            machine_id = "";

            if (person_id == "" || person_id == null)
            {
                try
                {
                    person_id = Session["usuario"].ToString();
                    company_id = Session["company"].ToString();
                    machine_id = Session["machine"].ToString();

                    //Grabando el Ingreso al Maps
                    Sesiones ses = new Sesiones();
                    int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

                    if (respuesta != 0)
                    {
                        Response.Redirect("http://sige.lucky.com.pe");
                    }

                    Session["usuario"] = person_id;
                    Session["company"] = company_id;
                    Session["machine"] = machine_id;

                    return View();

                }
                catch (Exception e)
                {
                    Response.Redirect("http://sige.lucky.com.pe");
                    return null;
                }

            }
            else
            {

                //Grabando el Ingreso al Maps
                /*
                Sesiones ses = new Sesiones();
                int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

                if (respuesta != 0)
                {
                    Response.Redirect("http://sige.lucky.com.pe");
                }
                */
                Session["usuario"] = person_id;
                Session["company"] = company_id;
                Session["machine"] = machine_id;
                return View();
            }
        }

        [OutputCache(Duration = 10)]
        public ActionResult Bodegas()
        {
            try
            {
                person_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data"], "usr");
                company_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data2"], "usr2");
                machine_id = Lucky.CFG.Util.Encriptacion.QueryStringDecode(Request.QueryString["data3"], "usr3"); ;
            }
            catch(System.Exception e){
                person_id = "";
                company_id = "";
                machine_id = "";
            }

            person_id = "2494";
            company_id = "1562";
            machine_id = "";

            if (person_id == "" || person_id == null)
            {
                try
                {
                    person_id = Session["usuario"].ToString();
                    company_id = Session["company"].ToString();
                    machine_id = Session["machine"].ToString();

                    //Grabando el Ingreso al Maps
                    Sesiones ses = new Sesiones();
                    int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

                    if (respuesta != 0)
                    {
                        Response.Redirect("http://sige.lucky.com.pe");
                    }
                    Session["usuario"] = person_id;
                    Session["company"] = company_id;
                    Session["machine"] = machine_id;

                    return View();
                }
                catch (Exception e)
                {
                    Response.Redirect("http://sige.lucky.com.pe");
                    return null;
                }

            }
            else {

                //Grabando el Ingreso al Maps
                Sesiones ses = new Sesiones();
                int respuesta = ses.GrabarIngresoMaps(person_id, modulo_id, machine_id);

                if (respuesta != 0)
                {
                    Response.Redirect("http://sige.lucky.com.pe");
                }

                Session["usuario"] = person_id;
                Session["company"] = company_id;
                Session["machine"] = machine_id;
                return View(); 
            }
        }
        #endregion
        
        #region Metodos Post - Sin Clasificar
        

        [HttpPost]
        public JsonResult getlistclientes()
        {
            listcompany = bcompany.listarcompany();
            return Json(listcompany);
        }

        [HttpPost]
        public JsonResult getlistplanning(int company_id)
        {
            listplanning = bplanning.lista_campanias_cliente(company_id);
            return Json(listplanning);
        }

        [HttpPost]
        public JsonResult get_representatividad(int type, string ubigeo)
        {
            RepresentatividadPtoVenta_Service service = new RepresentatividadPtoVenta_Service();
            var representatividad = service.obtener_Representatividad(type, ubigeo);
            var response = new
            {
                cantidad = representatividad.cantidad,
                total = representatividad.total,
                zona = representatividad.zona
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult get_anios()
        {
            Anio_Service service = new Anio_Service();
            return Json(service.obtener_Anios());
        }

        [HttpPost]
        public JsonResult get_meses()
        {
            Mes_Service service = new Mes_Service();
            return Json(service.obtener_Meses());
        }

        [HttpPost]
        public JsonResult get_periodos(string CodServicio, string CodCanal, string CodCliente, string CodReporte, string Anio, string Mes)
        {
            Periodo_Service service = new Periodo_Service();
            return Json(service.obtener_Periodo(CodServicio, CodCanal, CodCliente, CodReporte, Anio,Mes));
        }

        [HttpPost]
        public JsonResult get_provincia(string codPais, string codDepartamento)
        {
            Provincia_Service service = new Provincia_Service();
            return Json(service.obtener_Provincias(codPais, codDepartamento));
        }

        [HttpPost]
        public JsonResult get_sector(string codPais, string codDepartamento, string codProvincia)
        {
            Sector_Service service = new Sector_Service();
            return Json(service.obtener_sector(codPais, codDepartamento, codProvincia));
        }

        [HttpPost]
        public JsonResult get_supervisor(string codEquipo)
        {
            Supervisor_Service service = new Supervisor_Service();
            return Json(service.obtener_supervisor(codEquipo));
        }

        [HttpPost]
        public JsonResult get_generador(string codEquipo, string codSupervisor)
        {
            Generador_Service service = new Generador_Service();
            return Json(service.obtener_generador(codEquipo, codSupervisor));
        }

        [HttpPost]
        public JsonResult get_puntoventa(string codEquipo, string codGenerador, string reportsPlanning, string codPais, string codDepartamento, string codProvincia, string codSector, string codDistrito)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.obtener_puntoVenta(codEquipo, codGenerador, reportsPlanning, codPais, codDepartamento, codProvincia, codSector, codDistrito));
        }

        //public ActionResult PuntoVenta(string codPtoVentaCliente)
        //{
        //    PuntoVenta_Service service = new PuntoVenta_Service();
        //    var modelo = service.obtener_datoPuntoVenta(codPtoVentaCliente);

        //    Xplora.GIS.Models.PuntoVenta_Service.M_PuntoVentaDatosMapa modeloPto = new Xplora.GIS.Models.PuntoVenta_Service.M_PuntoVentaDatosMapa();
        //    modeloPto.codPuntoVenta = modelo.codPuntoVenta;
        //    modeloPto.direccion = modelo.direccion;
        //    modeloPto.distrito = modelo.distrito;
        //    modeloPto.nombre = modelo.nombre;
        //    modeloPto.nombrePuntoVenta = modelo.nombrePuntoVenta;
        //    modeloPto.sector = modelo.sector;
        //    modeloPto.ultimaVisita = modelo.ultimaVisita;

        //    return PartialView("PuntoVenta", modeloPto);
        //}

        [HttpPost]
        public JsonResult get_historialfoto_puntoventa(string reportsPlanning, string codPtoVenta)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.obtener_historialfoto_puntoVenta(reportsPlanning, codPtoVenta));
        }

        [HttpPost]
        public JsonResult get_departamento(string codPais)
        {
            Departamento_Service service = new Departamento_Service();
            return Json(service.obtener_Departamentos(codPais));
        }

        [HttpPost]
        public JsonResult get_distrito(string codPais, string codDepartamento, string codProvincia, string codSector)
        {
            Distrito_Service service = new Distrito_Service();
            return Json(service.obtener_Distritos(codPais, codDepartamento, codProvincia, codSector));
        }

        [HttpPost]
        public JsonResult get_semaforozonadistrito(string codCanal, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto, string codPeriodo, string opcion)
        {
            SemaforoZonaDistrito_Service service = new SemaforoZonaDistrito_Service();
            return Json(service.Obtener_Semaforo_ZonaDistrito(codCanal, codDepartamento, codProvincia, codZona, codDistrito, codCategoria, codProducto, codPeriodo, opcion));
        }

        [HttpPost]
        public JsonResult get_categorias(string codEquipo, string codReporte)
        {
            Categoria_Service service = new Categoria_Service();
            return Json(service.obtener_Categoria(codEquipo, codReporte));
        }

        [HttpPost]
        public JsonResult get_productos(string CodEquipo, string CodCliente, string CodCategoria, string CodSubCategoria, string CodMarca)
        {
            Producto_Service service = new Producto_Service();
            return Json(service.obtener_producto(CodEquipo, CodCliente, CodCategoria, CodSubCategoria, CodMarca));
        }

        [HttpPost]
        public JsonResult get_tipoCluster()
        {
            TipoCluster_Service service = new TipoCluster_Service();
            return Json(service.obtener_TipoCluster());
        }

        [HttpPost]
        public JsonResult obtener_PresenciaClusterPDV(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string cluster, string codPeriodo)
        {
            PresenciaPtoVenta_Service service = new PresenciaPtoVenta_Service();
            return Json(service.obtener_PresenciaClusterPDV(codCanal, codPais, codDepartamento, codProvincia, codZona, codDistrito, cluster, codPeriodo));
        }

        [HttpPost]
        public JsonResult obtener_VentaClusterPDV(string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto, string codCluster, string codPlanning, string codPeriodo)
        {
            VentasPtoVenta_Service service = new VentasPtoVenta_Service();
            return Json(service.Obtener_PuntoVentaMapaVentas(codPais, codDepartamento, codProvincia, codZona, codDistrito, codCategoria, codProducto, codCluster, codPlanning, codPeriodo));
        }

        [HttpPost]
        public JsonResult Obtener_Evolucion_Venta_SKUMandatorios(string codServicio, string codCanal, string codCliente,
            string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto,
            string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            Grilla_Service service = new Grilla_Service();
            EvolucionVentaSKUMandatorios_Response data = service.Obtener_Evolucion_Venta_SKUMandatorios(codServicio, codCanal, codCliente, codPais, codDepartamento,
               codProvincia, codZona, codDistrito, codCategoria, codProducto, codCluster, anio, mes, codPeriodo, codOpcion);

            E_VentasZonaDistrito_Detalle_List cabecera = data.oE_VentasZonaDistrito_Detalle[0];

            if (data.Estado == 0)
            {
                if (data.oE_VentasZonaDistrito_Detalle.Count > 0)
                {
                    data.oE_VentasZonaDistrito_Detalle.RemoveAt(0);
                    dtVenta = Util.ToDataTable<E_VentasZonaDistrito_Detalle_List>(data.oE_VentasZonaDistrito_Detalle);

                    dtVenta.Columns[0].ColumnName = cabecera.nombreSKU;
                    dtVenta.Columns[1].ColumnName = cabecera.valor1;
                    dtVenta.Columns[2].ColumnName = cabecera.valor2;
                    dtVenta.Columns[3].ColumnName = cabecera.valor3;
                    dtVenta.Columns[4].ColumnName = cabecera.valor4;
                    dtVenta.Columns[5].ColumnName = cabecera.valor5;
                    dtVenta.Columns[6].ColumnName = cabecera.valor6;
                    dtVenta.Columns[7].ColumnName = cabecera.valor7;
                    dtVenta.Columns[8].ColumnName = cabecera.valor8;
                }
            }
            return Json(data);
        }

        [HttpPost]
        public JsonResult Obtener_Evolucion_Presencia_SKUMandatorios(string codServicio, string codCanal, string codCliente,
            string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto,
            string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            Grilla_Service service = new Grilla_Service();
            EvolucionPresenciaSKUMandatorios_Response data = service.Obtener_Evolucion_Presencia_SKUMandatorios(codServicio, codCanal, codCliente, codPais, codDepartamento,
                codProvincia, codZona, codDistrito, codCategoria, codProducto, codCluster, anio, mes, codPeriodo, codOpcion);

            E_PresenciaZonaDistrito_Detalle_List cabecera = data.oE_PresenciaZonaDistrito_Detalle[0];

            if (data.Estado == 0)
            {
                if (data.oE_PresenciaZonaDistrito_Detalle.Count > 0)
                {
                    data.oE_PresenciaZonaDistrito_Detalle.RemoveAt(0);
                    dtPresencia = Util.ToDataTable<E_PresenciaZonaDistrito_Detalle_List>(data.oE_PresenciaZonaDistrito_Detalle);

                    dtPresencia.Columns[0].ColumnName = cabecera.nombreSKU;
                    dtPresencia.Columns[1].ColumnName = cabecera.valor1;
                    dtPresencia.Columns[2].ColumnName = cabecera.valor2;
                    dtPresencia.Columns[3].ColumnName = cabecera.valor3;
                    dtPresencia.Columns[4].ColumnName = cabecera.valor4;
                    dtPresencia.Columns[5].ColumnName = cabecera.valor5;
                    dtPresencia.Columns[6].ColumnName = cabecera.valor6;
                    dtPresencia.Columns[7].ColumnName = cabecera.valor7;
                    dtPresencia.Columns[8].ColumnName = cabecera.valor8;
                }
            }
            return Json(data);
        }
        
        [HttpPost]
        public JsonResult Obtener_Datos_Grafico_Excel(string codServicio, string codCanal, string codCliente, string codPais,
            string codDpto, string codCity, string codDistrito, string codSector, string codCluster, string codYear,
            string codMes, string codPeriodo, string codOpcion, int tipo)
        {
            Grafico_Service service = new Grafico_Service();
            E_ExportExcel data;

            switch (tipo)
            {
                case 1: 
                    data = service.Obtener_Datos_Tendencia(codServicio, codCanal, codCliente, codPais, codDpto, codCity, codDistrito, 
                        codSector, codCluster, codYear, codMes, codPeriodo, codOpcion);
                    break;

                case 2: 
                    data = service.Obtener_Datos_Variacion(codServicio, codCanal, codCliente, codPais, codDpto, codCity, codDistrito,
                        codSector, codCluster, codYear, codMes, codPeriodo, codOpcion);
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


        [HttpPost]
        public JsonResult Obtener_Datos_Grafico_Excel_Prov(string codServicio, string codCanal, string codCliente, string codPais,
            string codOficina,string codDpto, string codCity, string codDistrito, string codSector, string codCluster, string codYear,
            string codMes, string codPeriodo, string codOpcion, int tipo)
        {
            Grafico_Service service = new Grafico_Service();
            E_ExportExcel data;

            switch (tipo)
            {
                case 1:  
                    data = service.Obtener_Datos_Tendencia_Prov(codServicio, codCanal, codCliente, codPais,codOficina, codDpto, codCity, codDistrito,
                        codSector, codCluster, codYear, codMes, codPeriodo, codOpcion);
                    break;

                case 2: 
                    data = service.Obtener_Datos_Variacion_Prov(codServicio, codCanal, codCliente, codPais,codOficina, codDpto, codCity, codDistrito,
                        codSector, codCluster, codYear, codMes, codPeriodo, codOpcion);
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

        [HttpPost]
        public JsonResult obtener_Generador(string codCampania, string codSupervisor)
        {
            Personal_Service service = new Personal_Service();
            return Json(service.Obtener_Generadores_Por_CodCampania_Por_CodSupervisor(codCampania, codSupervisor));
        }

        [HttpPost]
        public JsonResult get_seguimiento_generador(string codEquipo, string codPais, string codDepartamento, string codProvincia,
            string codDistrito, string codGestor, string fecha)
        {
            Generador_Service service = new Generador_Service();
            return Json(service.Obtener_seguimiento_generador(codEquipo, codPais, codDepartamento, codProvincia, codDistrito, 
                codGestor, fecha));
        }

        #endregion

        #region Util - Excel

        public ExcelFileResult exportarExcel(string nombreArchivo, string tipo)
        {
            nombreArchivo += ".xls";
            ExcelFileResult actionResult = null;

            if (tipo.Equals("1")) 
            {
                actionResult = new ExcelFileResult(dtVenta) { FileDownloadName = nombreArchivo.ToString() };
                dtVenta = null;
            }
            else if (tipo.Equals("2")) 
            {
                actionResult = new ExcelFileResult(dtPresencia) { FileDownloadName = nombreArchivo.ToString() };
                dtPresencia = null;
            }
            else
            {
                actionResult = new ExcelFileResult(new DataTable()) { FileDownloadName = nombreArchivo.ToString() };
            }

            return actionResult;
        }
        
        public ExcelFileResult exportarExcelPresencia(string nombreArchivo)
        {
            nombreArchivo += ".xls";
            ExcelFileResult actionResult = null;

            actionResult = new ExcelFileResult(dtReportPresencia) { FileDownloadName = nombreArchivo.ToString() };
            dtVenta = null;

            return actionResult;
        }

        public ExcelFileResult exportarExcelVentasSubCategoria(string nombreArchivo)
        {
            nombreArchivo += ".xls";
            ExcelFileResult actionResult = null;

            actionResult = new ExcelFileResult(dtReportVentasSubCategoria) { FileDownloadName = nombreArchivo.ToString() };
            dtVenta = null;

            return actionResult;
        }

        #endregion

        #region XploraMaps - Lima
        #region Sección Universo 
        
        [HttpPost]
        public JsonResult get_clusterzonadistrito(string codZona, string codDistrito, string idPlanning, string reportsPlanning)
        {
            ClusterZonaDistrito_Service service = new ClusterZonaDistrito_Service();
            return Json(service.Obtener_Cluster_ZonaDistrito(codZona, codDistrito, idPlanning, reportsPlanning));
        }
        
        [HttpPost]
        public JsonResult Obtener_Representatividad_And_Cluster_NN_Mod(string ubigeo, string idPlanning, string idReportsPlanning)
        {
            ClusterRepresentatividad_Service service = new ClusterRepresentatividad_Service();
            return Json(service.Obtener_Representatividad_And_Cluster_NN_Mod(ubigeo, idPlanning, idReportsPlanning));
        }


        [HttpPost]
        public JsonResult get_cluster_and_representatividad(string codZona, string codDistrito, string idPlanning, string reportsPlanning)
        {
            ClusterRepresentatividad_Service service = new ClusterRepresentatividad_Service();
            return Json(service.Obtener_Cluster_Representatividad(codZona, codDistrito, idPlanning, reportsPlanning));
        }

        
        #endregion
        #region Sección - Presencia SKU Mandatorio
        
        [HttpPost]
        public JsonResult get_presencia(string coddepartamento, string codciudad, string codZona, string codDistrito, int reportsPlanning)
        {
            PresenciaPtoVenta_Service service = new PresenciaPtoVenta_Service();
            int servicio = 254;
            string canal = "2008";
            int codCliente = 1561;
            return Json(service.obtener_Representatividad(servicio, canal, codCliente, coddepartamento, codciudad, codZona, codDistrito, reportsPlanning));
        }

        #region get_presencia_Din - Dinamico - Lima - Add 25-01-2013
        [HttpPost]
        public JsonResult get_presencia_Din(string coddepartamento, string codciudad, string codZona, string codDistrito, int reportsPlanning)
        {
            PresenciaPtoVenta_Service service = new PresenciaPtoVenta_Service();
            int servicio = 254;
            string canal = "2008";
            int codCliente = 1561;
            return Json(service.obtener_Representatividad_Din(servicio, canal, codCliente, coddepartamento, codciudad, codZona, codDistrito, reportsPlanning));
        }
        #endregion

        [HttpPost]
        public JsonResult Obtener_PuntoVentaRango_NN(string codCanal, string codPais, string ubigeo,string codRango, string codPeriodo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PuntoVentaRango_NN(codCanal, codPais, ubigeo, codRango, codPeriodo));
        }

        //Reemplazado por Obtener_PuntoVentaRango_NN
        [HttpPost]
        public JsonResult Obtener_PuntoVentaPresenciaRango(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codRango, string codPeriodo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PuntoVentaPresenciaRango(codCanal, codPais, codDepartamento, codProvincia, codZona, codDistrito, codRango, codPeriodo));
        }
        
        [HttpPost]
        public JsonResult Obtener_PuntoVentaPresenciaToExcel(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codPresencia, string codPeriodo, int codOpcion, string ubigeo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            E_ExportExcel data;

            switch (codOpcion)
            {
                case 1: 
                    data = service.Obtener_PuntoVentaPresenciaSKUToExcel(codCanal, codPais, codDepartamento, codProvincia, codZona, codDistrito, codPresencia, codPeriodo);
                    break;

                case 2: 
                    //data = service.Obtener_PuntoVentaPresenciaElemVisibilidadToExcel(codCanal, codPais, codDepartamento, codProvincia, codZona, codDistrito, codPresencia, codPeriodo);
                    data = service.Obtener_PuntoVentaElemVisibilidadToExcel_NN(codCanal, codPais, ubigeo, codPresencia, codPeriodo);
                    break;

                case 3: 
                    data = service.Obtener_PuntoVentaPresenciaRangoToExcel(codCanal, codPais, codDepartamento, codProvincia, codZona, codDistrito, codPresencia, codPeriodo);
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
        
        [HttpPost]
        public JsonResult Obtener_PuntoVentaPresenciaSKU(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codProducto, string codPeriodo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PuntoVentaPresenciaSKU(codCanal, codPais, codDepartamento, codProvincia, codZona, codDistrito, codProducto, codPeriodo));
        }

        [HttpPost]
        public JsonResult Obtener_PdvByVentasToExcel(string codCliente, string codCanal, string ubigeo, string codSku, string codPeriodo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            E_ExportExcel data;

            data = service.Obtener_PdvByVentasToExcel(codCliente, codCanal, ubigeo, codSku, codPeriodo);


            if (data != null)
            {
                if (data.Header != null)
                    dtReportVentasSubCategoria = Util.ConvertToDataTable(data.Header, data.Contents);
                else
                    dtReportVentasSubCategoria = new DataTable();
            }
            else
                dtReportVentasSubCategoria = new DataTable();

            return Json(true);
        }

        #endregion
        #region Sección - Ventas x SubCategoria
        
        [HttpPost]
        public JsonResult get_ventas(int tipo, string codigo, int reportsPlanning)
        {
            VentasPtoVenta_Service service = new VentasPtoVenta_Service();
            return Json(service.Obtener_Ventas_ZonaDistrito(tipo, codigo, reportsPlanning));
        }

        #endregion
        #region Sección - Elementos de Visibilidad
        [HttpPost]
        public JsonResult get_elemvisibilidad_puntoventa(string codEquipo, string reportsPlanning, string codPtoVenta)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.obtener_elemvisibilidad_puntoVenta(codEquipo, reportsPlanning, codPtoVenta));
        }

        [HttpPost] //Nivel Nacional
        public JsonResult Obtener_PuntoVentaElemVisibilidad_NN(string codCanal, string codPais, string ubigeo, string codElemento, string codPeriodo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PuntoVentaElemVisibilidad_NN(codCanal, codPais, ubigeo, codElemento, codPeriodo));
        }
        
        
        
        [HttpPost] //
        public JsonResult Obtener_PuntoVentaPresenciaSKU_NN(string codCanal, string codPais, string ubigeo, string codProducto, string codPeriodo, string otrosParametros)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PuntoVentaPresenciaSKU_NN(codCanal, codPais, ubigeo, codProducto, codPeriodo, otrosParametros));
        }


        [HttpPost] //Cambiado por •	Obtener_PuntoVentaElemVisibilidad_NN
        public JsonResult Obtener_PuntoVentaPresenciaElemVisibilidad(string codCanal, string codPais, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codElemento, string codPeriodo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PuntoVentaPresenciaElemVisibilidad(codCanal, codPais, codDepartamento, codProvincia, codZona, codDistrito, codElemento, codPeriodo));
        }

        
        [HttpPost]
        public JsonResult Obtener_PdvByVentas(string codCliente, string codCanal, string ubigeo, string codSku, string idReportsPlanning)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PdvByVentas(codCliente,codCanal,ubigeo,codSku,idReportsPlanning));
        }
        
        #endregion


        #region Sección - Graficos
        
        [HttpPost]
        public JsonResult Obtener_Grafico_Tendencia(string codServicio, string codCanal, string codCliente, string codPais,
            string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string codCategoria, string codProducto, string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            Grafico_Service service = new Grafico_Service();
            return Json(service.Obtener_Grafico_Tendencia(codServicio, codCanal, codCliente,
                    codPais, codDepartamento, codProvincia, codZona, codDistrito, codCategoria, codProducto, codCluster,
                    anio, mes, codPeriodo, codOpcion));
        }

        
        [HttpPost]
        public JsonResult Obtener_Grafico_Variacion(string codServicio, string codCanal, string codCliente, string codPais,
            string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string codCategoria, string codProducto, string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            Grafico_Service service = new Grafico_Service();
            return Json(service.Obtener_Grafico_Variacion(codServicio, codCanal, codCliente, codPais, codDepartamento, codProvincia, codZona, codDistrito,
            codCategoria, codProducto, codCluster, anio, mes, codPeriodo, codOpcion));
        }

        #endregion
        #region Sección - Visita por PDV >>Reutiliza Por Completo<<
        
        [HttpPost]
        public JsonResult get_foto_puntoventa(string reportsPlanning, string codPtoVenta)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.obtener_foto_puntoVenta(reportsPlanning, codPtoVenta));
        }

        
        [HttpPost]
        public JsonResult get_datoPuntoventa(string codPtoVenta, string reportsplanning)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.obtener_datoPuntoVenta(codPtoVenta, reportsplanning));
        }

        
        [HttpPost]
        public JsonResult get_presencia_puntoventa(string codEquipo, string reportsPlanning, string codPtoVenta)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.obtener_presencia_puntoVenta(codEquipo, reportsPlanning, codPtoVenta));
        }

        
        [HttpPost]
        public JsonResult get_ventas_puntoventa(string codEquipo, string reportsPlanning, string codPtoVenta)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.obtener_venta_puntoVenta(codEquipo, reportsPlanning, codPtoVenta));
        }
        
        #endregion
        #endregion

        #region XploraMaps - Provincias
        #region Sección Universo 
        
        [HttpPost]
        public JsonResult get_clusterzonadistrito_Prov(string codOficina,string codZona, string codDistrito, string idPlanning, string reportsPlanning)
        {
            ClusterZonaDistrito_Service service = new ClusterZonaDistrito_Service();
            return Json(service.Obtener_Cluster_ZonaDistrito_Prov(codOficina,codZona, codDistrito, idPlanning, reportsPlanning));
        }

        
        [HttpPost]
        public JsonResult get_cluster_and_representatividad_Prov(string codOficina,string codZona, string codDistrito, string idPlanning, string reportsPlanning)
        {
            ClusterRepresentatividad_Service service = new ClusterRepresentatividad_Service();
            return Json(service.Obtener_Cluster_Representatividad_Prov(codOficina, codZona, codDistrito, idPlanning, reportsPlanning));
        }

        
        #endregion
        #region Sección - Presencia SKU Mandatorio
        
        [HttpPost]
        public JsonResult get_presencia_Prov(string codOficina,string coddepartamento, string codciudad, string codZona, string codDistrito, int reportsPlanning)
        {
            PresenciaPtoVenta_Service service = new PresenciaPtoVenta_Service();
            int servicio = 254;
            string canal = "2008";
            int codCliente = 1561;
            return Json(service.obtener_Representatividad_Prov(servicio, canal, codCliente,codOficina, coddepartamento, codciudad, codZona, codDistrito, reportsPlanning));
        }
        #region get_presencia_Prov_Din - Add 25-01-2013
        [HttpPost]
        public JsonResult get_presencia_Prov_Din(string codOficina, string coddepartamento, string codciudad, string codZona, string codDistrito, int reportsPlanning)
        {
            PresenciaPtoVenta_Service service = new PresenciaPtoVenta_Service();
            int servicio = 254;
            string canal = "2008";
            int codCliente = 1561;
            return Json(service.obtener_Representatividad_Prov_Din(servicio, canal, codCliente, codOficina, coddepartamento, codciudad, codZona, codDistrito, reportsPlanning));
        }
        #endregion

        [HttpPost]
        public JsonResult Obtener_PuntoVentaPresenciaRango_Prov(string codCanal, string codPais,string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codRango, string codPeriodo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PuntoVentaPresenciaRango_Prov(codCanal, codPais, codOficina, codDepartamento, codProvincia, codZona, codDistrito, codRango, codPeriodo));
        }
        
        [HttpPost]
        public JsonResult Obtener_PuntoVentaPresenciaToExcel_Prov(string codCanal, string codPais,string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codPresencia, string codPeriodo, int codOpcion,string ubigeo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            E_ExportExcel data;

            switch (codOpcion)
            {
                case 1: 
                    data = service.Obtener_PuntoVentaPresenciaSKUToExcel_Prov(codCanal, codPais,codOficina, codDepartamento, codProvincia, codZona, codDistrito, codPresencia, codPeriodo);
                    break;
                case 2: 
                    //data = service.Obtener_PuntoVentaPresenciaElemVisibilidadToExcel_Prov(codCanal, codPais,codOficina, codDepartamento, codProvincia, codZona, codDistrito, codPresencia, codPeriodo);
                    data = service.Obtener_PuntoVentaElemVisibilidadToExcel_NN(codCanal, codPais, ubigeo, codPresencia, codPeriodo);
                    break;

                case 3:
                    data = service.Obtener_PuntoVentaPresenciaRangoToExcel_Prov(codCanal, codPais,codOficina, codDepartamento, codProvincia, codZona, codDistrito, codPresencia, codPeriodo);
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
        
        [HttpPost]
        public JsonResult Obtener_PuntoVentaPresenciaSKU_Prov(string codCanal, string codPais,string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codProducto, string codPeriodo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PuntoVentaPresenciaSKU_Prov(codCanal, codPais, codOficina, codDepartamento, codProvincia, codZona, codDistrito, codProducto, codPeriodo));
        }
        

        #endregion
        #region Sección - Ventas x SubCategoria
        

        #endregion
        #region Sección - Elementos de Visibilidad
        
        [HttpPost]
        public JsonResult Obtener_PuntoVentaPresenciaElemVisibilidad_Prov(string codCanal, string codPais,string codOficina, string codDepartamento, string codProvincia, string codZona, string codDistrito, string codElemento, string codPeriodo)
        {
            PuntoVenta_Service service = new PuntoVenta_Service();
            return Json(service.Obtener_PuntoVentaPresenciaElemVisibilidad_Prov(codCanal, codPais, codOficina, codDepartamento, codProvincia, codZona, codDistrito, codElemento, codPeriodo));
        }
        #endregion
        #region Sección - Graficos
        
        [HttpPost]
        public JsonResult Obtener_Grafico_Tendencia_Prov(string codServicio, string codCanal, string codCliente, string codPais,
            string codOficina,string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string codCategoria, string codProducto, string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            Grafico_Service service = new Grafico_Service();
            return Json(service.Obtener_Grafico_Tendencia_Prov(codServicio, codCanal, codCliente,
                    codPais,codOficina, codDepartamento, codProvincia, codZona, codDistrito, codCategoria, codProducto, codCluster,
                    anio, mes, codPeriodo, codOpcion));
        }

        
        [HttpPost]
        public JsonResult Obtener_Grafico_Variacion_Prov(string codServicio, string codCanal, string codCliente, string codPais,
            string codOficina,string codDepartamento, string codProvincia, string codZona, string codDistrito,
            string codCategoria, string codProducto, string codCluster, string anio, string mes, string codPeriodo, string codOpcion)
        {
            Grafico_Service service = new Grafico_Service();
            return Json(service.Obtener_Grafico_Variacion_Prov(codServicio, codCanal, codCliente, codPais, codOficina, codDepartamento, codProvincia, codZona, codDistrito,
            codCategoria, codProducto, codCluster, anio, mes, codPeriodo, codOpcion));
        }

        #endregion
        #region Sección - Visita por PDV >>Reutiliza Por Completo<<
        
        #endregion

        #endregion

        #region Xplora Maps Integrado


        #region Universo / Representatividad / Cluster
        [HttpPost]
        public JsonResult obtener_Representatividad_And_Cluster(string ubigeo, string idPlanning, string idReportsPlanning)
        {
            NN_Representatividad_And_Cluster_Service service = new NN_Representatividad_And_Cluster_Service();
            var o = service.obtener_Representatividad_And_Cluster(ubigeo, idPlanning, idReportsPlanning);
            return Json(o.listaRepresentatividad_And_Cluster_NN);
        }
        #endregion
        
        #region Presencia SKU Mandatorios / Elementos de Visibilidad
        [HttpPost]
        public JsonResult obtener_PresenciaEleVisibilidad(int servicio,
                    string canal, int codCliente, string ubigeo, int reportsPlanning)
        {
            PresenciaEleVisibilidad_Service service = new PresenciaEleVisibilidad_Service();

            return Json(service.obtener_PresenciaEleVisibilidad(servicio, canal, codCliente, ubigeo, reportsPlanning));
        }
        #endregion

        #region Ventas

        [HttpPost]
        public JsonResult obtener_Ventas_NN(string ubigeo, string idReportsPlanning)
        {
            NN_Ventas_Service service = new NN_Ventas_Service();

            return Json(service.obtener_Ventas_NN(ubigeo, idReportsPlanning));
        }

        [HttpPost]
        public JsonResult obtener_Ventas_NN_Mod(string ubigeo, string reportsPlanning)
        {
            NN_Ventas_Service service = new NN_Ventas_Service();

            return Json(service.obtener_Ventas_NN_Mod(ubigeo, reportsPlanning));
        }


        [HttpPost]
        public JsonResult get_GraficoVentasVsTendencia(string codServicio, string codCanal, string codCliente, string codPais,
            string codDepartamento, string codProvincia, string codZona, string codDistrito, string codCategoria, string codProducto, string codCluster,
            string codAnio, string codMes, string codPeriodo, string codOpcion) {

                Grafico_Service oGrafico = new Grafico_Service();

                return Json(oGrafico.get_GraficoVentasVsTendencia(codServicio, codCanal, codCliente, codPais, codDepartamento, codProvincia, codZona, codDistrito, codCategoria, codProducto, codCluster,
            codAnio, codMes, codPeriodo,codOpcion));
        }

        #endregion

        #endregion

        #region XploraMaps - Minorista 
            #region Filtros Superiores
                #region Obtener Oficinas - Ciudad
                [HttpPost]
                public JsonResult obtener_OficinasPorCanalAndCompania(string codCanal, string codCompania) {
                    Oficina_Service service = new Oficina_Service();
                    return Json(service.Obtener_OficinasPorCanalAndCompania(codCanal, codCompania));
                }
                #endregion
            #endregion
        #endregion

        #region XploraMaps - Ingresos

                [HttpPost]
                public JsonResult get_SemanasxMes(string anio, string mes)
                {
                    Sesiones oSesiones = new Sesiones();
                    return Json(oSesiones.get_SemanasxMes(anio, mes));
                }

                [HttpPost]
                public JsonResult get_ListConsulta_IngresosModulo(string codUsuario, string codModulo, string tipoVisita, string anio, string mes, string semana)
                {
                    Sesiones oSesiones = new Sesiones();
                    codUsuario = Session["usuario"].ToString(); 
                    return Json(oSesiones.get_ListConsulta_IngresosModulo(codUsuario, codModulo, tipoVisita,anio,mes, semana));
                }

        #endregion
    }
}