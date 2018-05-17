'use strict';
controllers.controller('mapa', ['$scope', '$http', '$filter', 'setpagetitle', 'googlemaps', 'loadLayers', 'addLegend', function ($scope, $http, $filter, setpagetitle, googlemaps, loadLayers, addLegend) {
    setpagetitle("Xplora GIS - Canal Bodegas");
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
        $scope.standbyventa_tendenciagrafi = new Standby({ target: "tendencepanel" });
        $scope.standbyventa_comparativegrafi = new Standby({ target: "comparativepanel" });

        document.body.appendChild($scope.standbymap.domNode);
        document.body.appendChild($scope.standbypanel.domNode);
        document.body.appendChild($scope.standbyventa_tendenciagrafi.domNode);
        document.body.appendChild($scope.standbyventa_comparativegrafi.domNode);

        $scope.standbymap.startup();
        $scope.standbypanel.startup();
        $scope.standbyventa_tendenciagrafi.startup();
        $scope.standbyventa_comparativegrafi.startup();

        $scope.layers = [
        //{ name: 'Departamentos', source: 'departamentos', value: 0, done: true },
                {name: 'Oficinas', source: 'oficinas', value: 1, done: false },
                { name: 'Zonas', source: 'conos', value: 1, done: false },
                { name: 'Distritos', source: 'distritos', value: 2, done: false }
        //{ name: 'Distritos', source: 'Distritos_Oficinas_Prov_Unidos_sin_cañete', value: 2, done: false }

            ];

        $scope.objects = loadLayers($scope.layers, container);

        $scope.loadyear();
    });

    $scope.$parent.Canal = "2008";

    var container = googlemaps('#map', gmaps.global.peru.center.lat, gmaps.global.peru.center.lng, 5, "Básico");

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

    $scope.loadyear = function () {
        $http.post('/Reports/get_anios').success(function (response) {
            $scope.years = response;
            $scope.loadmounts();
            $scope.period = "";
        });
    }

    $scope.loadmounts = function () {
        $http.post('/Reports/get_meses').success(function (response) {
            $scope.months = response;
            $scope.loadperiods();
            $scope.period = "";
        });
    }

    $scope.loadperiods = function () {
        addListeners($scope.selectedpolygon);
        container.removeMarkers();
        $http.post('/Reports/get_periodos', { CodServicio: $scope.$parent.Servicio, CodCanal: $scope.$parent.Canal, CodCliente: $scope.$parent.Cliente, CodReporte: $scope.$parent.Reporte.toString(), Anio: $scope.year, Mes: $scope.month })
            .success(function (response) {
                $scope.periods = response;
            });
    }

    $http.post('/Reports/get_tipoCluster').success(function (response) {
        $scope.clusters = response;
    });

    $http.post('/Reports/get_categorias', { codEquipo: $scope.$parent.Equipo, codReporte: $scope.$parent.Reporte }).success(function (response) {
        $scope.categories = response;
    });

    $scope.loadproducts = function () {
        $http.post('/Reports/get_productos', { CodEquipo: $scope.$parent.Equipo, CodCliente: $scope.$parent.Cliente, CodCategoria: $scope.category, CodSubCategoria: $scope.$parent.SubCategoria, CodMarca: $scope.$parent.Marca })
        .success(function (response) {
            $scope.products = response;
        });
    }

    var allowrequest = function () {
        if ($scope.year && $scope.month && $scope.period)
            return true;
        return false;
    }

    var addListeners = function (obj) {

        if (obj) {

            google.maps.event.clearInstanceListeners(obj);


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


            google.maps.event.addListener(obj, "mousemove", function (event) {
                var label = Marker.getInstance();
                label.setPosition(event.latLng);
                label.setVisible(true);
            });


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
                    $scope.type = 0;
                    $scope.zona = (polygon.CZONE) ? polygon.CZONE : '0';
                }
                else {
                    $scope.type = 1;
                    $scope.oficina = (polygon.COFICINA) ? polygon.COFICINA : '0';
                }
                $scope.departamento = polygon.CODE.substring(0, 2);
                $scope.provincia = polygon.CODE.substring(2, 4);
                $scope.distrito = polygon.CODE.substring(4, 6);
            }
            else {
                if ($scope.$parent.Sectores.contains(polygon.CODE)) {
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

    var get_data = function () {

        if (!$scope.year) {
            alert("Debe Selecionar un Año.");
            return;
        }

        if (!$scope.month) {
            alert("Debe Selecionar un Mes.");
            return;
        }

        if (!$scope.period) {
            alert("Debe Selecionar un Periodo.");
            return;
        }


        //if ($("#Oficinas").is(':checked') || $("#Zonas").is(':checked') || $("#Distritos").is(':checked')) {
        if ($scope.ubigeo != null || $scope.ubigeo != "" || $scope.ubigeo != undefined) {

        }
        else {
            alert("Debe Selecionar uno de los layers.");
            return;
        }

        $scope.standbymap.show();
        $scope.standbypanel.show();

        var distrito = "0";
        var zona = "0";



        var request = {
            idPlanning: $scope.$parent.Equipo,
            reportsPlanning: $scope.period,
            ubigeo: $scope.ubigeo,
            idReportsPlanning: $scope.period
        }

        //var requesturl = '/Reports/Obtener_Cluster_Representatividad_NN';
        var requesturl = '/Reports/Obtener_Representatividad_And_Cluster_NN_Mod';

        /*
        if ($scope.ubigeo.length == 6) {
        if ($scope.$parent.Lima.contains($scope.ubigeo.substring(0, 2))) {
        $scope.type = 0;
        request.codZona = (polygon.CZONE) ? polygon.CZONE : '0';
        }
        else {
        $scope.type = 1;
        $scope.oficina = (polygon.COFICINA) ? polygon.COFICINA : '0';
        }
        $scope.departamento = polygon.CODE.substring(0, 2);
        $scope.provincia = polygon.CODE.substring(2, 4);
        $scope.distrito = polygon.CODE.substring(4, 6);
        }
        else {
        if ($scope.$parent.Sectores.contains(polygon.CODE)) {
        $scope.type = 0;
        request.codZona = polygon.CODE;
        $scope.departamento = '15';
        $scope.provincia = '01';
        }
        else {
        // OFICINA
        $scope.type = 1;
        request.codOficina = polygon.CODE;
        $scope.departamento = polygon.CDIST.substring(0, 2);
        $scope.provincia = polygon.CDIST.substring(2, 4);
        $scope.distrito = '0' //polygon.CDIST.substring(4, 6);
        }
        }


        */

        /*
        if ($("#Oficinas").is(':checked')) {
        //if ($scope.type == 0) {
        //if ($scope.ubigeo.length == 6) {
        //    distrito = $scope.ubigeo;
        //}
        //else {
        //    zona = $scope.ubigeo;
        //}
        request.codOficina = $scope.oficina;
        request.codZona = "0";
        request.codDistrito = "0";
        requesturl = '/Reports/get_cluster_and_representatividad_Prov';
        }

        if ($("#Zonas").is(':checked')) {
        request.codOficina = "0";
        request.codZona = $scope.ubigeo;
        request.codDistrito = "0";
        requesturl = '/Reports/get_cluster_and_representatividad';
        }

        if ($("#Distritos").is(':checked')) {
        //if ($scope.type == 1) {
        //request.codOficina = $scope.oficina;
        request.codOficina = "0";
        request.codZona = "0";
        request.codDistrito = $scope.ubigeo;
        requesturl = '/Reports/get_cluster_and_representatividad_Prov';
        }
        */




        $http.post(requesturl, request).success(function (response) {
            if (response) {
                if (response[0]) {
                    $scope.resumenpdv = response[0];
                    dijit.registry.byId("rigthCol").set("title", "Informative Panel " + $scope.resumenpdv.title_name);
                }
                else {
                    $scope.resumenpdv = "";
                    dijit.registry.byId("rigthCol").set("title", "Informative Panel  Sin Nombre");
                }

                if (response[1]) {
                    $scope.resumenperiodo = response[1];
                }
                else {
                    $scope.resumenperiodo = "";
                }
            }
            else {
                $scope.resumenpdv = "";
                $scope.resumenperiodo = "";
            }



            /*
            if ($scope.$parent.exist(response)) {
            if (response.clusterZonaDistritoMap != null) {
            var total = 0
            angular.forEach(response.clusterZonaDistritoMap.listClusterVisitado, function (da) {
            total = total + parseInt(da.cantidad);
            });
            $scope.totalresumenperiodo = total;
            }
            else {
            $scope.totalresumenperiodo = 0;
            }

            $scope.resumen = response;

            dijit.registry.byId("rigthCol").set("title", "Informative Panel " + $scope.name);
            }
            else {
            $scope.totalresumenperiodo = "0";
            $scope.resumen = "";
            dijit.registry.byId("rigthCol").set("title", "Informative Panel " + $scope.name);
            }

            */
        });



        container.removeMarkers();

        var data = {
            servicio: $scope.$parent.Servicio,
            canal: $scope.$parent.Canal,
            codCliente: $scope.$parent.Cliente,
            ubigeo: $scope.ubigeo,
            reportsPlanning: $scope.period
        };


        $http.post("/Reports/obtener_PresenciaEleVisibilidad", data).success(function (response) {

            console.log("dataaaaa");
            console.log(response);

            if ($scope.$parent.exist(response)) {
                $scope.listaElemVisibilidad = response.listaElemVisibilidad;
                $scope.listaPresencia = response.listaPresencia;

                if (response.listaPresencia) {
                    $scope.presencia = response.listaPresencia;
                }
                else {
                    $scope.presencia = "";
                }

                

                console.log("Elementooo");
                console.log($scope.elemvisbilidad);

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
                else {
                    $scope.vheaders = "";
                    $scope.vbody = "";
                }
            }
            else {
                $scope.listaElemVisibilidad = "";
                $scope.presencia = "";
                alert("Oops... No hemos recibido datos del servidor, por favor vuelve a intentarlo.");
                $scope.standbymap.hide();
                $scope.standbypanel.hide();

            }
        });


        $http.post("/Reports/obtener_Ventas_NN_Mod", data).success(function (response) {

            $scope.ventas_cate = response;

            /***************  ACA PETER *******************/
            if (response) {
                //Recorremos el objeto para guadar en un arreglo los totales de cada Categoria(Columna)
                var totCategoria = [];
                for (var i = 0; i < response.length; i++) {
                    totCategoria[i] = new Array();
                    var subtotales = [];
                    for (var j = 0; j < response[i].oList_Sum.length; j++) {
                        var valor = response[i].oList_Sum[j].sum_cat_dist;
                        subtotales.push(valor);
                    }
                    subtotales.push(response[i].total);
                    totCategoria[i].push(subtotales);
                }

                //limpiamos corchetes de array 
                var newarray = [];
                for (var i = 0; i < totCategoria.length; i++) {
                    for (var j = 0; j < totCategoria[i].length; j++) {
                        newarray.push(totCategoria[i][j]);
                    }
                }


                //recorremos el arreglo de totales de cada categoria para hacer la suma total
                var sumaColumna = [];
                var filas = newarray.length;
                var columnas = newarray[0].length;
                var suma = 0;

                for (var i = 0; i < columnas; i++) {
                    suma = 0;
                    for (var j = 0; j < filas; j++) {
                        suma = suma + newarray[j][i];
                    }
                    sumaColumna[i] = suma;
                }

                $scope.totales = sumaColumna;
                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            else {
                $scope.ventas_cate = "";
                $scope.totales = ""
                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            /**********************************************/


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
            ubigeo: $scope.ubigeo
        }

        var requesturl;

        if ($scope.type == 0) {
            //requesturl = '/Reports/Obtener_PuntoVentaPresenciaRango';
            requesturl = '/Reports/Obtener_PuntoVentaRango_NN';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            //requesturl = '/Reports/Obtener_PuntoVentaPresenciaRango_Prov';
            requesturl = '/Reports/Obtener_PuntoVentaRango_NN';
        }

        $http.post(requesturl, request).success(function (response) {
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

                //var node = angular.element(el.srcElement.parentElement.parentElement);
                //$scope.node = node;
                //node.addClass("row-selected");

                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            else {
                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
        });
    }

    $scope.getmarkersSKU = function (sku, el) {
        $scope.standbymap.show();
        $scope.standbypanel.show();

        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeMarkers();

        var request = {
            codCanal: $scope.$parent.Canal,
            codPais: $scope.$parent.Pais,
            ubigeo: $scope.ubigeo,
            codProducto: sku,
            codPeriodo: $scope.period,
            otrosParametros : ""
        }

        var requesturl;

        if ($scope.type == 0) {
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaSKU_NN';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            //requesturl = '/Reports/Obtener_PuntoVentaPresenciaSKU_Prov';
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaSKU_NN';
        }

        $http.post(requesturl, request).success(function (response) {
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

                // PCP 08/03/2013
                //var node = angular.element(el.srcElement.parentElement.parentElement);
                //$scope.node = node;
                //node.addClass("row-selected");


                addLegend(container, { greentext: "Con Presencia", redtext: "Sin Presencia" });

                $scope.standbymap.hide();
                $scope.standbypanel.hide();

            }
            else {
                $scope.standbymap.show();
                $scope.standbypanel.show();
            }
        });
    }

    $scope.getmarkersElem = function (element, el) {

        $scope.standbymap.show();
        $scope.standbypanel.show();
        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeMarkers();

        /*
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
        */

        var request = {
            codCanal: $scope.$parent.Canal,
            codPais: $scope.$parent.Pais,
            ubigeo: $scope.ubigeo,
            codElemento: element,
            codPeriodo: $scope.period
        }

        var requesturl;

        if ($scope.type == 0) {
            //requesturl = '/Reports/Obtener_PuntoVentaPresenciaElemVisibilidad';
            requesturl = '/Reports/Obtener_PuntoVentaElemVisibilidad_NN';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            //requesturl = '/Reports/Obtener_PuntoVentaPresenciaElemVisibilidad_Prov';
            requesturl = '/Reports/Obtener_PuntoVentaElemVisibilidad_NN';
        }

        $http.post(requesturl, request).success(function (response) {
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

                //var node = angular.element(el.srcElement.parentElement.parentElement);
                //$scope.node = node;
                //node.addClass("row-selected");

                addLegend(container, { greentext: "Con Elemento", redtext: "Sin Elemento" });

                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            else {
                $scope.standbymap.show();
                $scope.standbypanel.show();
            }
        });
    }

    $scope.getmarkersElem_venta = function (element, el) {

        $scope.standbymap.show();
        $scope.standbypanel.show();
        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeMarkers();

        var request = {
            codCliente: $scope.$parent.Cliente,
            codCanal: $scope.$parent.Canal,
            ubigeo: $scope.ubigeo,
            codSku: element,
            idReportsPlanning: $scope.period
        }


        var requesturl;

        if ($scope.type == 0) {
            requesturl = '/Reports/Obtener_PdvByVentas';
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            requesturl = '/Reports/Obtener_PdvByVentas';
        }

        $http.post(requesturl, request).success(function (response) {
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

                //var node = angular.element(el.srcElement.parentElement.parentElement);
                //$scope.node = node;
                //node.addClass("row-selected");

                addLegend(container, { greentext: "Con Elemento", redtext: "Sin Elemento" });

                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            else {
                $scope.standbymap.show();
                $scope.standbypanel.show();
            }
        });
    }

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
            codOpcion: idOpcion,
            ubigeo: $scope.ubigeo
        };

        var requesturl;

        if ($scope.type == 0) {
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaToExcel_Prov';
            //requesturl = '/Reports/Obtener_PuntoVentaElemVisibilidadToExcel_NN';
            
        }
        else if ($scope.type == 1) {
            request.codOficina = $scope.oficina;
            requesturl = '/Reports/Obtener_PuntoVentaPresenciaToExcel_Prov';
            //requesturl = '/Reports/Obtener_PuntoVentaElemVisibilidadToExcel_NN';
        }
        if ($scope.node) {
           
            $scope.node.removeClass("row-selected");
        }

        //PCP 08/03/2013
        //var node = angular.element(el.srcElement.parentElement.parentElement);
        //$scope.node = node;
        //node.addClass("row-selected");

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
    }

    $scope.fndownloads_venta = function (idElement, idOpcion, el) {

        var request = {
            codCliente: $scope.$parent.Cliente,
            codCanal: $scope.$parent.Canal,
            ubigeo: $scope.ubigeo,
            codSku: idElement,
            idReportsPlanning: $scope.period,
            codPeriodo: $scope.period
        }


        var requesturl;

        if ($scope.type == 0) {
            requesturl = '/Reports/Obtener_PdvByVentasToExcel';
        }
        else if ($scope.type == 1) {
            //request.codOficina = $scope.oficina;
            requesturl = '/Reports/Obtener_PdvByVentasToExcel';
        }

        if ($scope.node)
            $scope.node.removeClass("row-selected");

        //var node = angular.element(el.srcElement.parentElement.parentElement);
        //$scope.node = node;
        //node.addClass("row-selected");

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
            window.open('/Reports/exportarExcelVentasSubCategoria?nombreArchivo=Presencia');
        }

    }


    var get_pdvdata = function (codpdv) {

        var request = { codPtoVenta: codpdv, reportsPlanning: $scope.period };
        console.log("Data pdv");
        console.log(codpdv);
        $http.post('/Reports/get_foto_puntoventa', request).success(function (response) {
            $scope.photos = {};
            /*
            if ($scope.$parent.exist(response)) {
            */
            if (response) {
                angular.forEach(response, function (photo, key) {
                    $scope.photos[key] = $scope.$parent.PhotoPath + photo;
                    //$scope.photos["interiorAntes"] = $scope.$parent.PhotoPath + photo.interiorAntes;
                    //$scope.photos["exteriorAntes"] = $scope.$parent.PhotoPath + photo.exteriorAntes;
                    //$scope.photos["interiorDespues"] = $scope.$parent.PhotoPath + photo.interiorDespues;
                    //$scope.photos["exteriorDespues"] = $scope.$parent.PhotoPath + photo.exteriorDespues;

                    /*if (photo != "") {
                    $scope.photos[key] = $scope.$parent.PhotoPath + photo;
                    }*/
                    //else
                    //    $scope.photos[key] = $scope.$parent.NonePhoto;
                });
            }
            else {
                $scope.photos["interiorAntes"] = $scope.$parent.NonePhoto;
                $scope.photos["exteriorAntes"] = $scope.$parent.NonePhoto;
                $scope.photos["interiorDespues"] = $scope.$parent.NonePhoto;
                $scope.photos["exteriorDespues"] = $scope.$parent.NonePhoto;
            }
            //}

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



        if ($scope.category == "0" && $scope.product == "0") {
            if ($scope.cluster == undefined || $scope.cluster == "") {
                $scope.cluster = "0";
            }
        }

        if (($scope.category == "0" && $scope.product != "0") || ($scope.category != "0" && $scope.product == "0")) {
            alert("No esta permitido Cruzar una categoria con 'Todos' ");
            return;
        }

        if ($scope.rad_chart == undefined) {
            $scope.rad_chart = "1";
        }


        $scope.standbyventa_tendenciagrafi.show();
        //$scope.standbyventa_comparativegrafi.show();

        if ($scope.category && $scope.product && $scope.cluster && $scope.rad_chart) {

            var getparams_a = {
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
                codAnio: $scope.year,
                codMes: $scope.month,
                codPeriodo: $scope.period,
                codOpcion: $scope.rad_chart
            };


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
                //requesturl_a = '/Reports/Obtener_Grafico_Tendencia';
                requesturl_a = '/Reports/get_GraficoVentasVsTendencia';
                requesturl_b = '/Reports/Obtener_Grafico_Variacion';
            }
            else if ($scope.type == 1) {
                getparams.codOficina = $scope.oficina;
                //requesturl_a = '/Reports/Obtener_Grafico_Tendencia_Prov';
                requesturl_a = '/Reports/get_GraficoVentasVsTendencia';
                requesturl_b = '/Reports/Obtener_Grafico_Variacion_Prov';
            }

            $http.post(requesturl_a, getparams_a).success(function (response) {
                var Categories = [];
                var oSerie = [];
                var verLabel = "0";
                
                if (response.length > 0) {
                    angular.forEach(response[0].Leyendas, function (p_Cate) {
                        Categories.push(p_Cate.Leyenda);
                    })
                    var dataLabels = {
                        enabled: true, rotation: -90, color: '#424242', align: 'right', x: 10, y: 10,
                        style: { fontSize: '14px', fontFamily: 'Verdana, sans-serif' }
                    }
                    var h = 1;
                    var color1 = "#610B0B";
                    var color2 = "#4572A7";
                    angular.forEach(response, function (objc) {
                        angular.forEach(objc.Categorias, function (data) {
                            var dat = [];
                            angular.forEach(data.values, function (val) {
                                if (val.value == "-1") {
                                    dat.push(parseInt("0"));
                                } else {
                                    dat.push(parseInt(val.value));
                                }
                            });

                            if (data.type == "column") {
                                var bser = {
                                    name: data.name,
                                    type: data.type,
                                    color: color2,
                                    yAxis: 1,
                                    data: dat
                                };
                            } else {
                              var bser = {
                                    name: data.name,
                                    type: 'spline',//data.type,
                                    color: color1,
                                    //yAxis: 1,
                                    data: dat
                              };
                              oSerie.push(bser);
                            }
                            //oSerie.push(bser);
                            h = h + 1;
                        });
                    });                   

                    var chartventas = new Highcharts.Chart({
                        chart: {
                            renderTo: 'tendencepanel',
                            zoomType: 'xy'
                        },
                        title: {
                            text: 'Presencia' //'Ventas vs Presencia'
                        },
                        xAxis: [{
                            categories: Categories,
                            labels: dataLabels
                        }],
                        yAxis: [{//primary yAxis
                            labels: {
                                formatter: function () {
                                    return '' + this.value + '%';
                                },
                                style: {
                                    color: color1
                                }
                            },
                            title: {
                                text: 'Precencia',
                                style: {
                                    color: color1
                                }
                            },
                            min: 0
                        },/* { //Secondary yAxis
                            title: {
                                text: 'Presence',
                                style: {
                                    color: color2
                                }
                            },
                            labels: {
                                formatter: function () {
                                    return this.value + '%';
                                },
                                style: {
                                    color: color2
                                }
                            },
                            min: 0,
                            opposite: true
                        }*/],
                        tooltip: {
                            formatter: function () {
                                return '' +
                                    this.x + ': ' + this.y +
                                    (this.series.name == 'Presencia' ? ' ' : '');
                            }
                        },
                        series: oSerie,
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

                

                $scope.standbyventa_tendenciagrafi.hide();
            }

            );



            
            /*
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
                            text: 'Ventas'
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
                $scope.standbyventa_comparativegrafi.hide();
            });*/

        }
        else {
            alert("Estimado usuario, por favor seleccione una Categoría, Producto, Clúster y Período (semana/mes) de las opciones.");
            $scope.standbyventa_tendenciagrafi.hide();
            $scope.standbyventa_comparativegrafi.hide();
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
            success: function (data) {
                success = true;
            }
        });

        if (success) {
            window.open('/Reports/exportarExcelPresencia?nombreArchivo=DatosOrigen');

            //window.open('data:application/vnd.ms-excel,' + request);

            //window.open('data:application/vnd.ms-excel,' + $('#p_presencia').html());
        }
    }

    $scope.percentage = function (cantidad, total) {

        if (parseFloat(total) <= 0) {
            return 0;
        } else {
            var original = parseFloat((cantidad / total) * 100);
            var p = Math.round(original * 100) / 100;
            if (isNaN(p)) {
                return 0;
            }
            else {
                return p;
            }
        }
    }

    $scope.isNan = function (vari) {
        if (isNaN(parseFloat(vari)))
        { return 0; }
        else { return vari; }
    }


} ]);