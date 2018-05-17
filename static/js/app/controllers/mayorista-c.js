'use strict';

/* determinando el codigo de canal para esta vista */

controllers.controller('mapa', ['$scope', '$http', '$filter', 'setpagetitle', 'googlemaps', 'loadLayers', 'addLegend', function ($scope, $http, $filter, setpagetitle, googlemaps, loadLayers, addLegend) {
    setpagetitle("Xplora GIS - Canal Mayorista");
    require(
        [
            "dojo/parser",
            "dojo/dom-style",
            "dojo/dom",
            "dijit/registry",
            "dojox/widget/Standby",
            "dijit/Dialog",
            "dojo/store/JsonRest",
            "dijit/layout/BorderContainer",
            "dijit/layout/ContentPane",
            "dijit/layout/TabContainer",
            "dijit/layout/StackContainer",
			"dijit/layout/StackController",
            "dijit/layout/AccordionContainer",
            "dojox/layout/ExpandoPane",
            "dojo/domReady!"
        ],
        function (parser, domStyle, dom, registry, Standby) {
            $scope.registry = registry;
            parser.parse();
            container.refresh();
            domStyle.set("preloader", "display", "none");

            dojo.connect(registry.byId("map"), "resize", function (changeSize, resultSize) {
                container.refresh();
            });

            dojo.connect(registry.byId("popup"), "onClose", function (e) {
                $("#p_pres_pdv").hide();
                $("#p_visi_pdv").hide();
                $("#p_venta_pdv").hide();
                $("#pinicial").show();
                $("#pcompuesto").hide();
            });

            $scope.standbymap = new Standby({ target: "mapPanel" });
            $scope.standbypanel = new Standby({ target: "rigthCol" });

            document.body.appendChild($scope.standbymap.domNode);
            document.body.appendChild($scope.standbypanel.domNode);

            $scope.standbymap.startup();
            $scope.standbypanel.startup();

        });

    $scope.$parent.Canal = "1000";
    /* Init MAP */
    var container = googlemaps('#map', gmaps.global.peru.center.lat, gmaps.global.peru.center.lng, 5, "ROADMAP");

    /* Defining Layers array with all layers used in this page for load asyncronously when the page loads */
    $scope.layers = [
        { name: 'Departamentos', source: 'departamentos', value: 0, done: true },
        { name: 'Oficinas', source: 'oficinas', value: 1, done: false },
        { name: 'Sectores', source: 'conos', value: 1, done: false },
        { name: 'Distritos', source: 'distritos', value: 2, done: false }
    ];

    /* Load layers(json) into array of google maps objects) */
    $scope.objects = loadLayers($scope.layers, container);

    /**
    @function: drawlayer
    @author: Angel Ortiz
    @description: This method draws layers loaded from geojson files.
    Implements method for assing random colour and
    assign random colours based in on given colour from parent(not implement yet).
    **/

    $scope.drawlayer = function () {
        angular.forEach($scope.layers, function (layer) {
            if (layer.done)
                angular.forEach($scope.objects[layer.source], function (object) {
                    addListeners(object);
                    object.setOptions({ visible: true });
                });
            else
                angular.forEach($scope.objects[layer.source], function (object) {
                    object.setOptions({ visible: false });
                });
        });
    }

    /* **************************
    * Requests for fill combos 
    *****************************/
    $http.post('/Reports/get_anios')
        .success(function (response) {
            $scope.years = response;
        });

    $http.post('/Reports/get_meses')
        .success(function (response) {
            $scope.months = response;
        });

    $scope.loadperiods = function () {
        addListeners($scope.selectedpolygon);
        container.removeMarkers();
        $http.post('/Reports/get_periodos', { CodServicio: $scope.$parent.Servicio, CodCanal: $scope.$parent.Canal, CodCliente: $scope.$parent.Cliente, CodReporte: $scope.$parent.Reporte.toString(), Anio: $scope.year, Mes: $scope.month })
            .success(function (response) {
                $scope.periods = response;
            });
    }

    $http.post('/Reports/get_tipoCluster')
        .success(function (response) {
            $scope.clusters = response;
        });

    $http.post('/Reports/get_categorias', { codEquipo: $scope.$parent.Equipo, codReporte: $scope.$parent.Reporte })
        .success(function (response) {
            $scope.categories = response;
        });

    $scope.loadproducts = function () {
        $http.post('/Reports/get_productos', { CodEquipo: $scope.$parent.Equipo, CodCliente: $scope.$parent.Cliente, CodCategoria: $scope.category, CodSubCategoria: $scope.$parent.SubCategoria, CodMarca: $scope.$parent.Marca })
        .success(function (response) {
            $scope.products = response;
        });
    }

    /* validate if the user has selected the required filters */
    var allowrequest = function () {
        if ($scope.year && $scope.month && $scope.period)
            return true;
        return false;
    }

    /* adding listeners to every polygon on map */
    var addListeners = function (obj) {
        if (obj) {
            // clear all listener for the given object */
            google.maps.event.clearInstanceListeners(obj);

            /* adding click listener */
            google.maps.event.addListener(obj, "click", function (evt) {
                if (allowrequest()) {
                    btn_data_clicked(this);
                    restoreDefaultColor($scope.selectedpolygon);
                    this.setOptions(gmaps.global.style.geometry.selected);
                    container.map.fitBounds(this.getBounds());
                    $scope.selectedpolygon = this;
                }
                else
                    alert("Estimado usuario, previamente debe seleccionar el año, mes y período");
            });

            /* object Marker, defined as a singleton for doesnt load image many times, just once */
            var Marker = (function () {

                var instance = null;

                return new function () {
                    this.getInstance = function () {
                        if (instance == null) {
                            instance = new MarkerWithLabel({
                                position: new google.maps.LatLng(0, 0),
                                draggable: false,
                                raiseOnDrag: false,
                                map: container.map,
                                labelContent: obj.NAME,
                                labelAnchor: new google.maps.Point(30, 20),
                                labelClass: "labels", // the CSS class for the label
                                labelStyle: { opacity: 1.0 },
                                icon: "/static/img/1x1.png",
                                visible: false
                            });
                            instance.constructor = null;
                        }
                        return instance;
                    }
                }
            })();

            /* adding mousemove listener */
            google.maps.event.addListener(obj, "mousemove", function (event) {
                var label = Marker.getInstance();
                label.setPosition(event.latLng);
                label.setVisible(true);
            });

            /* adding mouseout listener */
            google.maps.event.addListener(obj, "mouseout", function (event) {
                var label = Marker.getInstance();
                label.setVisible(false);
            });
        }
    }

    var btn_data_clicked = function (polygon) {
        if (polygon) {

            container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });

            $scope.ubigeo = polygon.CODE;
            $scope.name = polygon.NAME;
            $scope.departamento = '0';
            $scope.provincia = '0';
            $scope.distrito = '0';
            $scope.oficina = '0';
            $scope.zona = '0';

            if (polygon.CODE.length == 6) {
                if ($scope.$parent.Lima.contains(polygon.CODE.substring(0, 2))) {
                    // LIMA
                    $scope.type = 0;
                    $scope.zona = (polygon.CZONE) ? polygon.CZONE : '0';
                }
                else {
                    // PROVINCIA
                    $scope.type = 1;
                    $scope.oficina = (polygon.COFICINA) ? polygon.COFICINA : '0';
                }
                $scope.departamento = polygon.CODE.substring(0, 2);
                $scope.provincia = polygon.CODE.substring(2, 4);
                $scope.distrito = polygon.CODE.substring(4, 6);
            }
            else {
                if ($scope.$parent.Sectores.contains(polygon.CODE)) {
                    // SECTOR
                    $scope.type = 0;
                    $scope.zona = polygon.CODE;
                    $scope.departamento = '15';
                    $scope.provincia = '01';
                }
                else {
                    // OFICINA
                    $scope.type = 1;
                    $scope.oficina = polygon.CODE;
                    $scope.departamento = polygon.CDIST.substring(0, 2);
                    $scope.provincia = polygon.CDIST.substring(2, 4);
                    $scope.distrito = '0' //polygon.CDIST.substring(4, 6);
                }
            }
            get_data();
        }
        else {
            alert("Oops, ocurrió algo inesperado, por favor vuelva a intentarlo.");
        }
    }

    /* OK */
    var get_data = function () {

        $scope.standbymap.show();
        $scope.standbypanel.show();

        var distrito = "0";
        var zona = "0";


        var request = {
            idPlanning: $scope.$parent.Equipo,
            reportsPlanning: $scope.period
        }

        var requesturl;

        if ($scope.type == 0) {
            if ($scope.ubigeo.length == 6) {
                distrito = $scope.ubigeo;
            }
            else {
                zona = $scope.ubigeo;
            }
            request.codZona = zona,
            request.codDistrito = distrito,
            requesturl = '/Reports/get_cluster_and_representatividad';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            requesturl = '/Reports/get_cluster_and_representatividad_Prov';
        }

        $http.post(requesturl, request).success(
            function (response) {                
                if ($scope.$parent.exist(response)) {
                    $scope.resumen = response;
                    dijit.registry.byId("rigthCol").set("title", "Informative Panel " + $scope.resumen.representatividadZonaDistritoMap.representatividadSector.nombre);
                }
            }
        );

        container.removeMarkers();

        var data = {
            servicio: $scope.$parent.Servicio,
            canal: $scope.$parent.Canal,
            codCliente: $scope.$parent.Cliente,
            ubigeo: $scope.ubigeo,
            reportsPlanning: $scope.period
        };

        /* OK */
        $http.post("/Reports/obtener_PresenciaEleVisibilidad", data).success(function (response) {

            if ($scope.$parent.exist(response)) {
                if (response.listaPresencia) {
                    $scope.presencia = response.listaPresencia;
                }

                if (response.listaElemVisibilidad) {
                    var headers = [];
                    var body = {};

                    angular.forEach(response.listaElemVisibilidad, function (empresa, index) {
                        headers.push(empresa.nombre_compania);

                        angular.forEach(empresa.detalle, function (detalle) {
                            if (!body[detalle.cod_elemento]) {
                                var reg = Object();
                                reg["cod_elemento"] = detalle.cod_elemento;
                                reg["nombre_elemento"] = detalle.nombre_elemento;
                                reg["valor"] = {};
                                reg["valor"][index] = detalle.valor_elemento;
                                body[detalle.cod_elemento] = reg;
                            }
                            else {
                                body[detalle.cod_elemento]["valor"][index] = detalle.valor_elemento;
                            }
                        });
                    });

                    $scope.vheaders = headers;
                    $scope.vbody = body;
                }
                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            else {
                alert("Oops... No hemos recibido datos del servidor, por favor vuelve a intentarlo.");
            }
        });

        /* OK */
        $http.post("/Reports/obtener_Ventas_NN", { ubigeo: $scope.ubigeo, idReportsPlanning: $scope.period }).success(
            function (response) {

                $("#tb_ventas tbody").empty();
                $("#tb_ventas thead").empty();

                if ($scope.$parent.exist(response)) {

                    response = response.listaVentas_NN;

                    if ($scope.$parent.exist(response)) {

                        if (response.length > 0) {

                            var lista = {};
                            var header = {};
                            var footer = {};
                            var total = 0;
                            var n = 1;
                            var m = 1;

                            angular.forEach(response, function (venta) {
                                if (venta.id_categoria != "0") {
                                    if (!lista[venta.id_categoria])
                                        lista[venta.id_categoria] = { categoria: venta.categoria, data: [{ distribuidora: venta.distribuidora, venta: venta.ventas}] };
                                    else
                                        lista[venta.id_categoria].data.push({ distribuidora: venta.distribuidora, venta: venta.ventas });
                                    if (!header[venta.distribuidora])
                                        header[venta.distribuidora] = { nombre: venta.distribuidora };
                                }
                                else if (venta.id_categoria == "0") {
                                    if (!footer[venta.distribuidora])
                                        footer[venta.distribuidora] = { venta: venta.ventas };
                                }
                            });

                            if (header) {
                                $("#tb_ventas thead").append('<tr></tr>');
                                $("#tb_ventas thead tr").append('<th><li id="ventasicon" class="icon-plus-sign"></li> Sell Out</th>');
                                angular.forEach(header, function (td) {
                                    $("#tb_ventas thead tr").append('<th>' + td.nombre + '</th>');
                                    n += 1;
                                });
                                $("#tb_ventas thead tr").append('<th>Total</th>');
                            }

                            if (lista) {
                                var i = 0;
                                var tot = 0;

                                angular.forEach(lista, function (row) {
                                    var sum = 0;
                                    $("#tb_ventas tbody").append('<tr class="conceadable"><td>' + row.categoria + '</td></tr>');
                                    angular.forEach(row.data, function (dist) {
                                        $("#tb_ventas tbody tr:eq(" + i + ")").append('<td>' + $filter('formatNuevoSol')(dist.venta.toString()) + '</td>');
                                        sum += dist.venta;
                                    });
                                    $("#tb_ventas tbody tr:eq(" + i + ")").append('<td>' + $filter('formatNuevoSol')(sum.toString()) + '</td>');
                                    i += 1;
                                    tot += sum;
                                });
                                total = tot;
                            }

                            if (footer) {
                                var suma = 0;
                                $("#tb_ventas tbody").append('<tr class="foot"><td>Total</td></tr>');
                                angular.forEach(footer, function (ft) {
                                    $(".foot").append('<td>' + $filter('formatNuevoSol')(ft.venta.toString()) + '</td>');
                                    m += 1;
                                    suma += ft.venta;
                                });
                                $(".foot").append('<td>' + $filter('formatNuevoSol')(suma.toString()) + '</td>');
                            }
                        }
                        $(".conceadable").hide();
                        $scope.standbymap.hide();
                        $scope.standbypanel.hide();
                    }
                }
                else {
                    //alert("No hay registros de ventas.");
                    $("#tb_ventas tbody").append('<tr>No existen registros de ventas para el periodo seleccionado.</tr>');
                }
            });
    }


    $scope.getmarkersRangos = function (rango, el) {
        $scope.standbymap.show();
        $scope.standbypanel.show();

        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeMarkers();

        var request = {
            codCanal: $scope.$parent.Canal,
            codPais: $scope.$parent.Pais,
            codRango: rango,
            codPeriodo: $scope.period,
            codDepartamento: $scope.departamento,
            codProvincia: $scope.provincia,
            codDistrito: $scope.distrito,
            codZona: $scope.zona
        }

        var requesturl;

        if ($scope.type == 0) {
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaRango';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaRango_Prov';
        }

        $http.post(requesturl, request)
            .success(function (response) {
                if ($scope.$parent.exist(response)) {
                    angular.forEach(response, function (point) {
                        if ($scope.$parent.validcoord(point.latitud) && $scope.$parent.validcoord(point.longitud)) {
                            container.addMarker({
                                lat: point.latitud,
                                lng: point.longitud,
                                title: point.nombrePuntoVenta,
                                code: point.codPuntoVenta,
                                icon: gmaps.global.minicons[point.color.toLowerCase()],
                                click: function (e) {
                                    get_pdvdata(e.code);
                                }
                            });
                        }
                    });
                    google.maps.event.clearListeners($scope.selectedpolygon, "click");

                    if ($scope.node)
                        $scope.node.removeClass("row-selected");

                    var node = angular.element(el.srcElement.parentElement.parentElement);
                    $scope.node = node;
                    node.addClass("row-selected");

                    $scope.standbymap.hide();
                    $scope.standbypanel.hide();
                } else { }
            });
    }                  /* OK */
    $scope.getmarkersSKU = function (sku, el) {
        $scope.standbymap.show();
        $scope.standbypanel.show();
        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeMarkers();

        var request = {
            codCanal: $scope.$parent.Canal,
            codPais: $scope.$parent.Pais,
            codDepartamento: $scope.departamento,
            codProvincia: $scope.provincia,
            codZona: $scope.zona,
            codDistrito: $scope.distrito,
            codProducto: sku,
            codPeriodo: $scope.period
        }

        var requesturl;

        if ($scope.type == 0) {
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaSKU';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaSKU_Prov';
        }

        $http.post(requesturl, request)
            .success(function (response) {
                if ($scope.$parent.exist(response)) {
                    angular.forEach(response, function (point) {
                        if ($scope.$parent.validcoord(point.latitud) && $scope.$parent.validcoord(point.longitud)) {
                            container.addMarker({
                                lat: point.latitud,
                                lng: point.longitud,
                                title: point.nombrePuntoVenta,
                                code: point.codPuntoVenta,
                                icon: gmaps.global.minicons[point.color.toLowerCase()],
                                click: function (e) {
                                    get_pdvdata(e.code);
                                }
                            });
                        }
                    });
                    google.maps.event.clearListeners($scope.selectedpolygon, "click");

                    if ($scope.node)
                        $scope.node.removeClass("row-selected");

                    var node = angular.element(el.srcElement.parentElement.parentElement);
                    $scope.node = node;
                    node.addClass("row-selected");
                    addLegend(container, { greentext: "Con Presencia", redtext: "Sin Presencia" });

                    $scope.standbymap.hide();
                    $scope.standbypanel.hide();

                } else { }
            });
    }                       /* OK */
    $scope.getmarkersElem = function (element, el) {
        $scope.standbymap.show();
        $scope.standbypanel.show();
        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeMarkers();

        var request = {
            codCanal: $scope.$parent.Canal,
            codPais: $scope.$parent.Pais,
            codDepartamento: $scope.departamento,
            codProvincia: $scope.provincia,
            codZona: $scope.zona,
            codDistrito: $scope.distrito,
            codElemento: element,
            codPeriodo: $scope.period
        }

        var requesturl;

        if ($scope.type == 0) {
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaElemVisibilidad';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaElemVisibilidad_Prov';
        }

        $http.post(requesturl, request)
            .success(function (response) {
                if ($scope.$parent.exist(response)) {
                    angular.forEach(response, function (point) {
                        if ($scope.$parent.validcoord(point.latitud) && $scope.$parent.validcoord(point.longitud)) {
                            container.addMarker({
                                lat: point.latitud,
                                lng: point.longitud,
                                title: point.nombrePuntoVenta,
                                code: point.codPuntoVenta,
                                icon: gmaps.global.minicons[point.color.toLowerCase()],
                                click: function (e) {
                                    get_pdvdata(e.code);
                                }
                            });
                        }
                    });
                    google.maps.event.clearListeners($scope.selectedpolygon, "click");

                    if ($scope.node)
                        $scope.node.removeClass("row-selected");

                    var node = angular.element(el.srcElement.parentElement.parentElement);
                    $scope.node = node;
                    node.addClass("row-selected");

                    addLegend(container, { greentext: "Con Elemento", redtext: "Sin Elemento" });

                    $scope.standbymap.hide();
                    $scope.standbypanel.hide();
                }
            });
    }                  /* OK */
    $scope.fndownloads = function (idElement, idOpcion, el) {
        var request = {
            codCanal: $scope.$parent.Canal,
            codPais: $scope.$parent.Pais,
            codDepartamento: $scope.departamento,
            codProvincia: $scope.provincia,
            codZona: $scope.zona,
            codDistrito: $scope.distrito,
            codPresencia: idElement,
            codPeriodo: $scope.period,
            codOpcion: idOpcion
        };

        var requesturl;

        if ($scope.type == 0) {
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaToExcel';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaToExcel_Prov';
        }

        if ($scope.node)
            $scope.node.removeClass("row-selected");

        var node = angular.element(el.srcElement.parentElement.parentElement);
        $scope.node = node;
        node.addClass("row-selected");

        var success = false;

        $.ajax({
            type: 'POST',
            url: requesturl,
            data: request,
            context: document.body,
            async: false,
            success: function () {
                success = true;
            }
        });

        if (success) {
            window.open('/Reports/exportarExcelPresencia?nombreArchivo=Presencia');
        }
    }         /* OK */


    /* Request methods to retrieve info about point of sale */
    var get_pdvdata = function (codpdv) {

        var request = { codPtoVenta: codpdv, reportsPlanning: $scope.period };
        $http.post('/Reports/get_foto_puntoventa', request).success(function (response) {
            $scope.photos = {};
            if ($scope.$parent.exist(response)) {
                if (response.length > 0) {
                    angular.forEach(response, function (photo, key) {
                        if (photo != "")
                            $scope.photos[key] = $scope.$parent.PhotoPath + photo;
                        else
                            $scope.photos[key] = $scope.$parent.NonePhoto;
                    });
                }
                else {
                    $scope.photos["interiorAntes"] = $scope.$parent.NonePhoto;
                    $scope.photos["exteriorAntes"] = $scope.$parent.NonePhoto;
                    $scope.photos["interiorDespues"] = $scope.$parent.NonePhoto;
                    $scope.photos["exteriorDespues"] = $scope.$parent.NonePhoto;
                }
            }
        });               /* OK */
        $http.post('/Reports/get_datoPuntoventa', request).success(function (response) {
            if ($scope.$parent.exist(response)) {
                $scope.pdvinfo = response;
                $scope.registry.byId("popup").show();
            }
        });                /* OK */

        request.codEquipo = $scope.$parent.Equipo;

        $http.post("/Reports/get_presencia_puntoventa", request).success(function (response) {
            if ($scope.$parent.exist(response)) {
                $scope.presenciapdv = {
                    colgate: [],
                    competencia: []
                };

                angular.forEach(response, function (pres) {
                    if (pres.idTipo == '04')
                        $scope.presenciapdv.colgate.push(pres);
                    else if (pres.idTipo == '05')
                        $scope.presenciapdv.competencia.push(pres);
                });
            }
        });          /* OK */
        $http.post("/Reports/get_ventas_puntoventa", request).success(function (response) {
            $scope.sales = response;
        });             /* OK */
        $http.post("/Reports/get_elemvisibilidad_puntoventa", request).success(function (response) {
            $scope.visibility = response;
        });    /* OK */
    }

    $scope.drawcharts = function () {

        if ($scope.category && $scope.product && $scope.cluster && $scope.rad_chart) {
            var getparams = {
                codServicio: $scope.$parent.Servicio,
                codCanal: $scope.$parent.Canal,
                codCliente: $scope.$parent.Cliente,
                codPais: $scope.$parent.Pais,
                codDepartamento: $scope.departamento,
                codProvincia: $scope.provincia,
                codZona: $scope.zona,
                codDistrito: $scope.distrito,
                codCategoria: $scope.category,
                codProducto: $scope.product,
                codCluster: $scope.cluster,
                anio: $scope.year,
                mes: $scope.month,
                codPeriodo: $scope.period,
                codOpcion: $scope.rad_chart
            };

            var requesturl_a;
            var requesturl_b;

            if ($scope.type == 0) {
                requesturl_a = '/Reports/Obtener_Grafico_Tendencia';
                requesturl_b = '/Reports/Obtener_Grafico_Variacion';
            }
            else if ($scope.type == 1) {
                getparams.codOficina = $scope.oficina;
                requesturl_a = '/Reports/Obtener_Grafico_Tendencia_Prov';
                requesturl_b = '/Reports/Obtener_Grafico_Variacion_Prov';
            }

            $http.post(requesturl_a, getparams).success(function (response) {

                var categories = [];
                var datav = [];
                var datac = [];

                var dataLabels = {
                    enabled: true, rotation: -90, color: '#424242', align: 'right', x: 10, y: 10,
                    style: { fontSize: '14px', fontFamily: 'Verdana, sans-serif' }
                }

                angular.forEach(response.oListGraficoTendencia, function (item) {
                    categories.push(item.cebecera);
                    datav.push(parseInt(item.ventas));
                    datac.push(parseInt(item.porcentaje));
                });

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'tendencepanel',
                        zoomType: 'xy'
                    },
                    title: {
                        text: 'Ventas vs Presencia'
                    },
                    xAxis: [{
                        categories: categories,
                        labels: dataLabels
                    }],
                    yAxis: [{
                        labels: {
                            formatter: function () {
                                return 'S/. ' + this.value;
                            },
                            style: {
                                color: '#89A54E'
                            }
                        },
                        title: {
                            text: 'Sales',
                            style: {
                                color: '#89A54E'
                            }
                        },
                        min: 0
                    }, {
                        title: {
                            text: 'Presence',
                            style: { color: '#4572A7' }
                        },
                        labels: {
                            formatter: function () { return this.value + '%'; },
                            style: { color: '#4572A7' }
                        },
                        min: 0,
                        opposite: true
                    }],
                    series: [{
                        name: 'Presence',
                        color: '#4572A7',
                        type: 'spline',
                        yAxis: 1,
                        data: datac

                    }, {
                        name: 'Sales',
                        color: '#89A54E',
                        type: 'column',
                        data: datav
                    }],
                    exporting: {
                        buttons: {
                            excelbutton: {
                                symbol: 'diamond',
                                x: -62,
                                symbolFill: '#B5C9DF',
                                hoverSymbolFill: '#779ABF',
                                _titleKey: 'exportExcel',
                                onclick: function () {
                                    $scope.export_excel(2);
                                }
                            }
                        }
                    }
                });
            }
            );

            $http.post(requesturl_b, getparams).success(function (response) {

                var categories = [];
                var data = {};

                var dataLabels = {
                    enabled: true, rotation: -90, color: '#424242', align: 'right', x: 10, y: 10,
                    style: { fontSize: '14px', fontFamily: 'Verdana, sans-serif' }
                }

                angular.forEach(response.oListGraficoVariacion, function (item) {
                    categories.push(item.descripcion.split(" ")[1]);
                    if (data[item.anio]) {
                        data[item.anio].push(parseInt(item.ventas));
                    }
                    else {
                        data[item.anio] = [];
                        data[item.anio].push(parseInt(item.ventas));
                    }
                });

                var series = [];

                angular.forEach(data, function (item, key) {
                    series.push({
                        name: key,
                        data: item,
                        stack: key
                    });
                });

                var chart1 = new Highcharts.Chart({
                    chart: {
                        renderTo: 'comparativepanel',
                        type: 'column'
                    },
                    title: {
                        text: 'Variación de ventas.'
                    },
                    xAxis: {
                        categories: categories,
                        labels: dataLabels
                    },
                    yAxis: {
                        allowDecimals: false,
                        min: 0,
                        title: {
                            text: 'Sales'
                        }
                    },
                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.x + '</b><br/>' +
                            this.series.name + ': ' + this.y + '<br/>' +
                            'Total: ' + this.point.stackTotal;
                        }
                    },
                    plotOptions: {
                        column: {
                            stacking: 'normal'
                        }
                    },
                    series: series,
                    exporting: {
                        buttons: {
                            excelbutton: {
                                symbol: 'diamond',
                                x: -62,
                                symbolFill: '#B5C9DF',
                                hoverSymbolFill: '#779ABF',
                                _titleKey: 'exportExcel',
                                onclick: function () {
                                    $scope.export_excel(1);
                                }
                            }
                        }
                    }
                });
            }
            );
        }
        else {
            alert("Estimado usuario, por favor seleccione una Categoría, Producto, Clúster y Período (semana/mes) de las opciones.");
        }
    }                   /* OK */
    $scope.export_excel = function (button) {

        var request = {
            codServicio: $scope.$parent.Servicio,
            codCanal: $scope.$parent.Canal,
            codCliente: $scope.$parent.Cliente,
            codPais: $scope.$parent.Pais,
            codDpto: $scope.departamento,
            codCity: $scope.provincia,
            codDistrito: $scope.distrito,
            codSector: $scope.zona,
            codCluster: $scope.cluster,
            codYear: $scope.year,
            codMes: $scope.month,
            codPeriodo: $scope.period,
            codOpcion: $scope.rad_chart,
            tipo: button
        }


        var requesturl;

        if ($scope.type == 0) {
            requesturl = '/Reports/Obtener_Datos_Grafico_Excel';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            requesturl = '/Reports/Obtener_Datos_Grafico_Excel_Prov';
        }

        var success = false;

        $.ajax({
            type: 'POST',
            url: requesturl,
            data: request,
            context: document.body,
            async: false,
            success: function () {
                success = true;
            }
        });

        /* success if response has finished */
        if (success) {
            window.open('/Reports/exportarExcelPresencia?nombreArchivo=DatosOrigen');
        }
    }           /* OK */
} ]);