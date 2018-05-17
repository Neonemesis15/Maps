'use strict';

/* determinando el codigo de canal para esta vista */

controllers.controller('mapa', ['$scope', '$http', '$filter', 'setpagetitle', 'googlemaps','load', 'addLegend', 'addTitle', function ($scope, $http, $filter, setpagetitle, googlemaps, load, addLegend, addTitle) {
    setpagetitle("Xplora GIS - Canal Minorista");
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
        });

    

    var container = googlemaps('#map', gmaps.global.peru.center.lat, gmaps.global.peru.center.lng, 5, "ROADMAP");
    //var departa = load('departamentos_min_a');

    $scope.$parent.Canal = "1023";
    $scope.title_view = "Nacional";
    $scope.tipoubigeo = "0";
    $scope.ubigeo = "0";
    $scope.nodecomercial = "0";
    $scope.period = '';

    $http.get().success(function () {

        var request = {
            opcion : 'ULTIMOPERIODO',
            filtros : $scope.$parent.Cliente + ',' +  $scope.$parent.Canal 
        }

        $http.post('/Minorista/Obtener_ultimoperiodo', request).success(function (response) {
            console.log("Ultimo Periodo");
            console.log(response);
            $scope.year = response.Contents[0][1];
            $scope.month = parseInt(response.Contents[0][2]);
            $scope.period = response.Contents[0][0];

            /*
            $scope.selec_departamento = null;
            $scope.obj_select = null;
            var options = {};
            console.log("cargando departamento");            
            console.log(departa);
            options.geojson = departa;
            options.geotype = 0;
            options.strokeWeight = 0;
            $scope.departamentos = container.getFromGeoJSON(options);
            $scope.tipoubigeo = "1";
            $scope.drawlayer($scope.departamentos);
            */
            $scope.get_anios();
            $scope.get_meses();
            //$scope.loadsubfiltros();
            $scope.tipoubigeo = "0";
            $scope.ubigeo = "0";
            $scope.get_data();
        });
    });

    $scope.loadsubfiltros = function () {
        //$scope.loadperiods("28", "ddl_periodoDias1", $scope.perioddias1);
    }

    
    $http.get('/static/js/data/departamentos_min_a.geojson').success(function (data) {
        $scope.selec_departamento = null;
        $scope.obj_select = null;
        var options = {};
        options.geojson = data;
        options.geotype = 0;
        options.strokeWeight = 0;
        $scope.departamentos = container.getFromGeoJSON(options);
        $scope.tipoubigeo = "1";
        $scope.drawlayer($scope.departamentos);
    });
    

    $scope.show_nacional = function () {
        $("#btn_ver_nacional").hide();
        $("#btn_ver_distritos").hide();
        $("#btn_ver_zonas").show();
        $("#btn_ver_regiones").show();
        $("#btn_ver_departamentos").show();

        $scope.selec_departamento = null;
        $scope.obj_select = null;
        $scope.title_view = "Nacional";
        $scope.tipoubigeo = "1";
        $scope.drawlayer($scope.departamentos);
        $scope.tipoubigeo = "0";
        $scope.get_data();
    }

    $scope.show_regiones = function () {
        $("#btn_ver_nacional").show();
        $("#btn_ver_distritos").hide();
        $("#btn_ver_zonas").show();
        $("#btn_ver_regiones").hide();
        $("#btn_ver_departamentos").show();
        $scope.title_view = "Regiones";
        $scope.selec_departamento = null;
        $scope.obj_select = null;

        if (!$scope.regiones_json) {
            console.log("Cargando Regiones..!!");
            $http.get('/static/js/data/regiones_esp_colgate_min.geojson').success(function (data) {
                var options = {};
                options.geojson = data;
                options.geotype = 0;
                options.strokeWeight = 1;
                $scope.regiones_json = container.getFromGeoJSON(options);
                $scope.tipoubigeo = "6";
                $scope.drawlayer($scope.regiones_json);
            });
        } else {
            $scope.tipoubigeo = "6";
            $scope.drawlayer($scope.regiones_json);
        }        
    }

    $scope.show_district = function () {        
        if (!$scope.selec_departamento) {
            alert("Seleccione un departamento..!");
            return;
        }
        $("#btn_ver_nacional").show();
        $("#btn_ver_distritos").hide();
        $("#btn_ver_zonas").show();
        $("#btn_ver_regiones").show();
        $("#btn_ver_departamentos").show();        
        $scope.title_view = "Distritos";
        if (!$scope.distritos_json) {
            console.log("Cargando distritos...!!");
            $http.get('/static/js/data/distritos_min.geojson').success(function (data) {
                var options = {};
                options.geojson = data;
                options.geotype = 0;
                options.strokeWeight = 1;
                $scope.distritos_json = container.getFromGeoJSON(options);
                var distrito = []
                angular.forEach($scope.distritos_json, function (da) {
                    if (da.length == undefined) {
                        if (da.properties.COD_DEPART == $scope.selec_departamento) {
                            distrito.push(da);
                        }
                    } else {}
                });
                $scope.selec_departamento = null;
                $scope.obj_select = null;
                $scope.tipoubigeo = "3";
                $scope.drawlayer(distrito);
            });
        } else {
            var distrito = []
            angular.forEach($scope.distritos_json, function (da) {
                if (da.length == undefined) {
                    if (da.properties.COD_DEPART == $scope.selec_departamento) {
                        distrito.push(da);
                    }
                } else {}
            });
            $scope.tipoubigeo = "3";
            $scope.drawlayer(distrito);
        }

    }

    $scope.show_zonas = function () {
        $("#btn_ver_nacional").show();
        $("#btn_ver_distritos").hide();
        $("#btn_ver_zonas").hide();
        $("#btn_ver_regiones").show();
        $("#btn_ver_departamentos").show();
        $scope.title_view = "Zonas";
        $scope.selec_departamento = null;
        $scope.obj_select = null;
        if (!$scope.zonas_json) {
            console.log("Cargando zonas...!!");
            $http.get('/static/js/data/sectores_min2.geojson').success(function (data) {
                var options = {};
                options.geojson = data;
                options.geotype = 0;
                options.strokeWeight = 1;
                $scope.zonas_json = container.getFromGeoJSON(options);
                $scope.tipoubigeo = "5";
                $scope.drawlayer($scope.zonas_json);
            });
        } else {
            $scope.tipoubigeo = "5";
            $scope.drawlayer($scope.zonas_json);
        }
    }

    $scope.drawlayer = function (data) {
        container.removePolygons();
        $scope.ofcol = {};
        container.removeControl({ position: "TOP_CENTER", index: 0 });
        addTitle(container, $scope.title_view);
        for (var i = 0; i < data.length; i++) {
            var opts = getRandomOptions();            
            if (data[i].length == undefined) {
                var poly = container.drawPolygon(data[i]);               
                if ($scope.tipoubigeo == "1") {
                    opts.fillColor = data[i].properties.COLOR;
                }
                poly.properties = opts;
                poly.properties.TYPE = "1";
                poly.setOptions(poly.properties);
                $scope.addListeners(poly);
            } else {
                for (var j = 0; j < data[i].length; j++) {
                    var poly = container.drawPolygon(data[i][j]);
                    if ($scope.tipoubigeo == "1") {
                        opts.fillColor = data[i][j].properties.COLOR;
                    }
                    poly.properties = opts;
                    poly.properties.TYPE = "1";
                    poly.setOptions(poly.properties);
                    $scope.addListeners(poly);
                }
            }
        }
    }

    $scope.restorecolour = function () {
        angular.forEach(container.polygons, function (polygon) {
            polygon.setOptions(polygon.properties);
            $scope.polyclick(polygon);
        });
    }

    $scope.resize = function () {
        container.refresh();
    }

    $scope.allowrequest = function () {
        if ($scope.year && $scope.month && $scope.period){// && $scope.tipoubigeo != '0') {
            console.log("Periodo:" + $scope.period);
            return true;
        }
        return false;
    }

    $scope.show_departamentos = function () {
        $scope.selec_departamento = null;
        $scope.obj_select = null;
        $scope.tipoubigeo = "1";
        $scope.title_view = "Departamentos";
        $scope.drawlayer($scope.departamentos);
        $("#btn_ver_nacional").show();
        $("#btn_ver_distritos").show();
        $("#btn_ver_zonas").show();
        $("#btn_ver_regiones").show();
        $("#btn_ver_departamentos").hide();
    }
    
    $scope.addListeners = function (obj) {
        if (obj) {
            google.maps.event.clearInstanceListeners(obj);

            google.maps.event.addListener(obj, "click", function (evt) {
                if ($scope.allowrequest()) {
                    $scope.obj_select = obj;
                    $scope.selectedpolygon = this;
                    btn_data_clicked(this);
                    $scope.loadmercados(obj);                    
                    restoreDefaultColor($scope.selectedpolygon);
                    this.setOptions(gmaps.global.style.geometry.selected);
                    container.map.fitBounds(this.getBounds());                    
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
                                labelContent: obj.NOMBRE,
                                labelAnchor: new google.maps.Point(30, 40),
                                labelClass: "labels", // the CSS class for the label
                                labelStyle: { opacity: 1.0 },
                                icon: "/static/img/1x1.png",
                                visible: true
                            });
                            instance.constructor = null;
                        }
                        return instance;
                    }
                }
            })();

            var Menu = (function () {

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

                            container.setContextMenu({
                                control: 'map',
                                options: [{
                                    title: 'Add marker',
                                    name: 'add_marker',
                                    action: function (e) {
                                        this.addMarker({
                                            lat: event.latLng.jb,
                                            lng: event.latLng.kb,
                                            title: 'New marker'
                                        });
                                    }
                                }, {
                                    title: 'Center here',
                                    name: 'center_here',
                                    action: function (e) {
                                        this.setCenter(e.latLng.lat(), e.latLng.lng());
                                    }
                                }]
                            });

                            instance.constructor = null;
                        }
                        return instance;
                    }
                }
            })();

            google.maps.event.addListener(obj, "rightclick", function (event) {
                //alert(event);
                /*
                if (confirm("¿Desea ver Distritos o Zonas?") ) {

                    if (!confirm(" Esta seguro ?  \n Esta es su última oportunidad .\n \n Se abrirá otra ventana \n para evitarle molestias.")) {
                        console.log("aceptar");
                        return false;
                    }
                    else {
                        console.log("else");
                        return false;
                    }
                } else {
                    console.log("cancelar");
                }*/

            });
            

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
    
    /*
    $http.post('/Reports/get_anios').success(function (response) {
            $scope.years = response;
    });

    $http.post('/Reports/get_meses').success(function (response) {
            $scope.months = response;
    });*/

    //CARGANDO EL AÑO DE ACUERDO AL CANAL SELECCIONADO

    $scope.get_anios = function () {
        $http.post('/Reports/get_anios')//, { CodCliente: $scope.CodCliente, CodCanal: $scope.canal })
            .success(function (response) {
                $scope.years = response;
                $('#ddl_anio').select2('val', $scope.year);
            });
    }

    //CARGANDO LOS MESES DE ACUERDO AL AÑO SELECCIONADO
    $scope.get_meses = function () {
        $http.post('/Reports/get_meses')//, { CodCliente: $scope.CodCliente, CodCanal: $scope.canal, CodAnio: $scope.year })
        .success(function (response) {
            $scope.months = response;
            $('#ddl_mes').select2('val', $scope.month);
        });
    }

    $scope.loadperiods = function () {
        //addListeners($scope.selectedpolygon);
        //container.removeMarkers();
        $http.post('/Reports/get_periodos', { CodServicio: $scope.$parent.Servicio, CodCanal: $scope.$parent.Canal, CodCliente: $scope.$parent.Cliente, CodReporte: $scope.$parent.Reporte.toString(), Anio: $scope.year, Mes: $scope.month })
            .success(function (response) {
                $scope.periods = response;
        });
    }

    $scope.loadmercados = function () {
        var request = { 
            codCanal: $scope.$parent.Canal, 
            codCompania: $scope.$parent.Cliente, 
            tipoubigeo: $scope.tipoubigeo, 
            ubigeo : $scope.ubigeo  
        }
        console.log("request mercados");
        console.log(request);
        $http.post('/Minorista/Obtener_Mercados_Ubigeo', request).success(function (response) {
            console.log(response);
            $scope.mercados = response.Contents;
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

    var btn_data_clicked = function (polygon) {
        if (polygon) {
            container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
            $scope.tipoubigeo = polygon.TIPO_UBIGEO;
            $scope.ubigeo = polygon.UBIGEO;
            $scope.selec_departamento = $scope.ubigeo;
            $scope.name = polygon.NOMBRE;
            console.log("Tipo Ubigeo Seleccionado : " + $scope.tipoubigeo);
            if ($scope.tipoubigeo == '1') {
                $scope.title_view = "Departamentos";
                $("#btn_ver_nacional").show();
                $("#btn_ver_distritos").show();
                $("#btn_ver_zonas").show();
                $("#btn_ver_regiones").show();
                $("#btn_ver_departamentos").hide();
            } else if ($scope.tipoubigeo == '3') {
                $scope.title_view = "Distritos";
            } else if ($scope.tipoubigeo == '5') {
                $scope.title_view = "Zonas";  
            } else if ($scope.tipoubigeo == '6') {
                $scope.title_view = "Provincias Colgate";
            } else {
                $scope.title_view = "Nacional";
            }
            container.removeControl({ position: "TOP_CENTER", index: 0 });
            addTitle(container, $scope.title_view);
            
            console.log("allowre");
            console.log($scope.allowrequest());

            if ($scope.allowrequest() == false && $scope.tipoubigeo != '0') {
                return;
            }

            $scope.get_data();
        }
        else {
            alert("Oops, ocurrió algo inesperado, por favor vuelva a intentarlo.");
        }
    }

    $scope.get_data = function () {
        $scope.standbymap.show();
        $scope.standbypanel.show();
        console.log("OBTENIENDO DATOS...!!");        

        if ($scope.obj_select) {
            dijit.registry.byId("rigthCol").set("title", "Informative Panel " + $scope.obj_select.NOMBRE);
        } else {
            dijit.registry.byId("rigthCol").set("title", "Informative Panel - Nivel Nacional ");
        }
        
        container.removeMarkers();

        var request_universo_mr = {
            ubigeo: '' + $scope.tipoubigeo + ',' + $scope.ubigeo,
            idPlanning: ''+$scope.$parent.Equipo,
            idReportsPlanning: ''+$scope.period,
            otrosParametros: ''+$scope.nodecomercial
        }

        console.log("request universo");
        console.log(request_universo_mr);

        $http.post("/Minorista/Obtener_UniversoMR", request_universo_mr).success(function (response) {
                console.log("Universo MR - Datos");
                console.log(response);
                if (response && response.length > 0) {
                    $scope.universo_mr_title = 'Datos para Universo MR';
                    $scope.universo_mr_periodo_title = response[1].title_name;
                    $scope.universo_mr_periodo_title2 = response[1].title_value;
                    $scope.universo_mr = response[0];
                    $scope.universo_mr_periodo = response[1];
                } else {
                    $scope.universo_mr_title = 'No Existen Datos para Universo MR';
                    $scope.universo_mr_periodo_title = 'No Existen Datos para periodo de Universo MR';
                    $scope.universo_mr_periodo_title2 = '';
                } 
            }
        )/*.error(function () {
            console.log("Error en Universo MR - Datos");
            $scope.universo_mr = null;
            $scope.universo_mr_title = 'Datos para Universo MR';
        })*/;

        /*
        var request_rangos_sku_mandatorio = {
            id_ReportsPlanning: $scope.period,
            tipo_ubigeo: $scope.tipoubigeo,
            ubigeo: $scope.ubigeo,
            nodecomercial: $scope.nodecomercial
        }

        $http.post("/Minorista/Obtener_UniversoMR", request_rangos_sku_mandatorio).success(function (response) {
                console.log("Rangos SKU Mandatorio");
                console.log(response);
                $scope.rangosku_mandatorio = response;
            }
        );

        var request_sku_mandatorio = {
            id_ReportsPlanning: $scope.period,
            tipo_ubigeo: $scope.tipoubigeo,
            ubigeo: $scope.ubigeo,
            nodecomercial: $scope.nodecomercial
        }

        $http.post("/Minorista/Obtener_SKU_Mandatorios", request_sku_mandatorio).success(function (response) {
                console.log("SKU Mandatorios");
                console.log(response);
                $scope.sku_mandatorio = response;
            }
        );
        */

        var request_ventas_subcategoria = {
            ubigeo: '' + $scope.tipoubigeo + ',' + $scope.ubigeo,
            //idPlanning: '' + $scope.$parent.Equipo,
            idReportsPlanning: '' + $scope.period,
            otrosParametros: '' + $scope.nodecomercial
        }

        $http.post("/Minorista/Obtener_Ventas_SubCategoria", request_ventas_subcategoria).success(function (response) {
                console.log("Ventas por SubCategoría");
                console.log(response);
                if (response && response.length>0) {
                    $scope.ventas_mandatorio = response;
                    $scope.ventas_mandatorio_title = "Sell Out";
                } else {
                    $scope.ventas_mandatorio_title = "No Hay datos de Ventas";
                }
            }
        ).error(function () {
            console.log("Error en Ventas por SubCategoría");
            $scope.ventas_mandatorio = null;
            $scope.ventas_mandatorio_title = "No Hay datos de Ventas";
        });
        
        var request_elementos_visibilidad = {
            servicio : $scope.$parent.Servicio,
            canal: '' + $scope.$parent.Canal,
            codCliente: '' + $scope.$parent.Cliente,
            ubigeo: '' + $scope.tipoubigeo + ',' + $scope.ubigeo,
            idReportsPlanning : $scope.period,
            otrosParametros: '' + $scope.nodecomercial
        }
        
        
        $http.post("/Minorista/Obtener_Elementos_Visibilidad", request_elementos_visibilidad).success(function (response) {
                console.log("Elementos de Visibilidad");
                console.log(response);
                if (response) {
                    if (response.listaElementosVisibilidad && response.listaElementosVisibilidad.length > 0) {
                        $scope.elementos_visibilidad = response.listaElementosVisibilidad;
                        $scope.elementos_visibilidad_title = ""; 
                    } else {
                        $scope.elementos_visibilidad_title = "No hay Elementos de Visibilidad";
                    }
                    if (response.listaPresencia && response.listaPresencia.length > 0) {
                        var sku_man = [];
                        var rangos = [];
                        angular.forEach(response.listaPresencia, function (datos) {
                            if(datos.id_tipo =="4"){
                                sku_man.push(datos);
                            }else{
                                rangos.push(datos);
                            }
                        });

                        $scope.sku_mandatorio = sku_man;
                        $scope.rangosku_mandatorio = rangos;
                        $scope.sku_mandatorio_title = "";
                    } else {
                        $scope.sku_mandatorio_title = "No hay datos sku mandatorio";
                    }  
                } else {
                    $scope.elementos_visibilidad_title = "No hay Elementos de Visibilidad";
                    $scope.sku_mandatorio_title = "No hay datos sku mandatorio";
                }
                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            } 
        ).error(function () {
            console.log("Error en Elementos de Visibilidad");
            $scope.standbymap.hide();
            $scope.standbypanel.hide();
        });

        /*
        $http.get("/static/js/data/data_prueba.json").success(function (response) {
                console.log("data de prueba");
                console.log(response);
                $scope.universo_mr = response;
            }
        );

        
        $http.get("/static/js/data/data_prueba_rangos.json").success(function (response) {
            console.log("data de prueba");
            console.log(response);
            $scope.rangosku_mandatorio = response;
        }
        );

        $http.get("/static/js/data/data_prueba_skumandatorios.json").success(function (response) {
            console.log("data de prueba");
            console.log(response);
            $scope.sku_mandatorio = response;
        }
        );

        $http.get("/static/js/data/data_prueba_ventas_subcategoria.json").success(function (response) {
                console.log("data de prueba");
                console.log(response);
                $scope.ventas_mandatorio = response;
            }
        );

        $http.get("/static/js/data/data_prueba_elementos_visibilidad.json").success(function (response) {
                console.log("data de prueba");
                console.log(response);
                $scope.elementos_visibilidad = response;
            }
        );

        */

        
    }

    $scope.getmarkersRangos = function (rango, rango_name, el) {
        $scope.standbymap.show();
        $scope.standbypanel.show();

        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeControl({ position: "TOP_CENTER", index: 0 });
        addTitle(container, $scope.title_view);
        container.removeMarkers();

        var request = {
            codCanal: $scope.$parent.Canal,
            codPais: $scope.$parent.Pais,
            codRango: rango,
            codPeriodo: $scope.period,
            ubigeo: '' + $scope.tipoubigeo + ',' + $scope.ubigeo,
            otrosParametros: $scope.nodecomercial
        }

        console.log("Request Obtener_PuntoVentaRango");
        console.log(request);

        var requesturl = '/Minorista/Obtener_PuntoVentaRango';

        $http.post(requesturl, request).success(function (response) {
            console.log("Obtener_PuntoVentaRango");
            console.log(response);
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

                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            else {
                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            addLegend(container, { greentext: "Con Elemento " + rango_name, redtext: "Sin Elemento " + rango_name });
        });
    }

    $scope.getmarkersSKU = function (sku,nom_element, el) {
        $scope.standbymap.show();
        $scope.standbypanel.show();

        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeControl({ position: "TOP_CENTER", index: 0 });
        addTitle(container, $scope.title_view);
        container.removeMarkers();

        var request = {
            codCanal: $scope.$parent.Canal,
            codPais: $scope.$parent.Pais,
            ubigeo: '' + $scope.tipoubigeo + ',' + $scope.ubigeo,
            codProducto: sku,
            codPeriodo: $scope.period,
            otrosParametros: $scope.nodecomercial
        }

        console.log("Request  Obtener_PuntoVentaPresenciaSKU");
        console.log(request);

        var requesturl = '/Minorista/Obtener_PuntoVentaPresenciaSKU';

        $http.post(requesturl, request).success(function (response) {
            console.log("Obtener_PuntoVentaPresenciaSK");
            console.log(response);
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

                //addLegend(container, { greentext: "Con Presencia", redtext: "Sin Presencia" });
                //addLegend(container, { greentext: "Con Elemento " + nom_element, redtext: "Sin Elemento " + nom_element });

                $scope.standbymap.hide();
                $scope.standbypanel.hide();

            }
            else {
                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            addLegend(container, { greentext: "Con Elemento " + nom_element, redtext: "Sin Elemento " + nom_element });
        });

    }

    $scope.getmarkersElem = function (element,nom_element, el) {

        $scope.standbymap.show();
        $scope.standbypanel.show();
        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeControl({ position: "TOP_CENTER", index: 0 });
        addTitle(container, $scope.title_view);
        container.removeMarkers();

        var request = {
            codCanal: $scope.$parent.Canal,
            codPais: $scope.$parent.Pais,
            ubigeo: '' + $scope.tipoubigeo + ',' + $scope.ubigeo,
            codElemento: element,
            codPeriodo: $scope.period,
            otrosParametros : $scope.nodecomercial
        }

        console.log("Request Obtener_PuntoVentaElemVisibilidad");
        console.log(request);

        var requesturl = '/Minorista/Obtener_PuntoVentaElemVisibilidad';

        $http.post(requesturl, request).success(function (response) {
            console.log("Obtener_PuntoVentaElemVisibilidad");
            console.log(response);
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

                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            else {
                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }

            addLegend(container, { greentext: "Con Elemento " + nom_element, redtext: "Sin Elemento " + nom_element });
        });

    }

    $scope.getmarkersElem_venta = function (element,nom_element, el) {
        $scope.standbymap.show();
        $scope.standbypanel.show();
        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
        container.removeControl({ position: "TOP_CENTER", index: 0 });
        addTitle(container, $scope.title_view);
        container.removeMarkers();

        var request = {
            codCliente: $scope.$parent.Cliente,
            codCanal: $scope.$parent.Canal,
            ubigeo: '' + $scope.tipoubigeo + ',' + $scope.ubigeo,
            codSku: element,
            idReportsPlanning: $scope.period,
            otrosParametros : $scope.nodecomercial
        }

        console.log("Request Obtener_PdvByVentas");
        console.log(request);

        var requesturl = '/Minorista/Obtener_PdvByVentas';
        

        $http.post(requesturl, request).success(function (response) {
            console.log("Obtener_PdvByVentas");
            console.log(response);
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

                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            else {
                $scope.standbymap.hide();
                $scope.standbypanel.hide();
            }
            addLegend(container, { greentext: "Con Elemento " + nom_element, redtext: "Sin Elemento " + nom_element });
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
            ubigeo: '' + $scope.tipoubigeo + ',' + $scope.ubigeo
        };

        var requesturl = '/Minorista/Obtener_PuntoVentaToExcel';

        if ($scope.node) {
            $scope.node.removeClass("row-selected");
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

        if (success) {
            window.open('/Minorista/exportarExcel?nombreArchivo=Presencia');
        }
    }

    $scope.fndownloads_venta = function (idElement, idOpcion, el) {

        var request = {
            codCliente: $scope.$parent.Cliente,
            codCanal: $scope.$parent.Canal,
            ubigeo: '' + $scope.tipoubigeo + ',' + $scope.ubigeo,
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
        $http.post('/Minorista/Obtener_Fotos_PuntoVenta', request).success(function (response) {
            $scope.photos = {};
            if (response) {
                angular.forEach(response, function (photo, key) {
                    $scope.photos[key] = $scope.$parent.PhotoPath + photo;
                });
            }
            else {
                $scope.photos["interiorAntes"] = $scope.$parent.NonePhoto;
                $scope.photos["exteriorAntes"] = $scope.$parent.NonePhoto;
                $scope.photos["interiorDespues"] = $scope.$parent.NonePhoto;
                $scope.photos["exteriorDespues"] = $scope.$parent.NonePhoto;
            }
        });               

        $http.post('/Reports/Obtener_Datos_PuntoVenta', request).success(function (response) {
            if ($scope.$parent.exist(response)) {
                $scope.pdvinfo = response;
                $scope.registry.byId("popup").show();
            }
        });                

        request.codEquipo = $scope.$parent.Equipo;

        $http.post("/Reports/Obtener_Presencia_PuntoVenta", request).success(function (response) {
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
        });

        $http.post("/Reports/Obtener_Ventas_PuntoVenta", request).success(function (response) {
            $scope.sales = response;
        });

        $http.post("/Reports/Obtener_ElementoVisibilidad_PuntoVenta", request).success(function (response) {
            $scope.visibility = response;
        });
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

        if ($scope.category && $scope.product && $scope.cluster && $scope.rad_chart) {
            var getparams_a = {
                codServicio: $scope.$parent.Servicio,
                codCanal: $scope.$parent.Canal,
                codCliente: $scope.$parent.Cliente,
                codCategoria: $scope.category,
                codProducto: $scope.product,
                codCluster: $scope.cluster,
                codAnio: $scope.year,
                codMes: $scope.month,
                codPeriodo: $scope.period,
                codOficina: $scope.tipoubigeo + ','+$scope.ubigeo,
                codOpcion: $scope.rad_chart,
                otrosParametros: $scope.nodecomercial
            };

            var getparams_b = {
                codServicio: $scope.$parent.Servicio,
                codCanal: $scope.$parent.Canal,
                codCliente: $scope.$parent.Cliente,
                codCategoria: $scope.category,
                codProducto: $scope.product,
                codCluster: $scope.cluster,
                anio: $scope.year,
                mes: $scope.month,
                codPeriodo: $scope.period,
                codOficina: $scope.tipoubigeo + ',' + $scope.ubigeo,
                codOpcion: $scope.rad_chart,
                otrosParametros: $scope.nodecomercial
            };

            var url_a = '/Minorista/Obtener_GraficoVentasVsTendencia';
            var url_b = '/Minorista/Obtener_GraficoVariacion';

            $http.post(url_a, getparams_a).success(function (response) {
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
                            h = h + 1;
                        });
                    });

                    var chartventas = new Highcharts.Chart({
                        chart: {
                            renderTo: 'tendencepanel',
                            zoomType: 'xy'
                        },
                        title: {
                            text: 'Ventas vs Presencia'
                        },
                        xAxis: [{
                            categories: Categories,
                            labels: dataLabels
                        }],
                        yAxis: [{//primary yAxis
                            labels: {
                                formatter: function () {
                                    return 'S/. ' + this.value + '';
                                },
                                style: {
                                    color: color1
                                }
                            },
                            title: {
                                text: 'Ventas',
                                style: {
                                    color: color1
                                }
                            },
                            min: 0
                        },{ //Secondary yAxis
                            title: {
                                text: 'Presencia',
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
                        }],
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
                                        $scope.export_excel(1);
                                    }
                                }
                            }
                        }
                    });
                }



                $scope.standbyventa_tendenciagrafi.hide();
            }

            );

            $http.post(url_b, getparams_b).success(function (response) {
                console.log("Request Second gráfico");
                console.log(getparams_b);
                console.log("Data Second gráfico");
                console.log(response);

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
                                    $scope.export_excel(2);
                                }
                            }
                        }
                    }
                });  
                $scope.standbyventa_comparativegrafi.hide();
            });

        }
        else {
            alert("Estimado usuario, por favor seleccione una Categoría, Producto, Clúster y Período (semana/mes) de las opciones.");
            $scope.standbyventa_tendenciagrafi.hide();
            $scope.standbyventa_comparativegrafi.hide();
        }


    }                  

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
            codOficina: $scope.tipoubigeo + ',' + $scope.ubigeo,
            otrosParametros: $scope.nodecomercial,
            tipo : button
        }

        var url_a = '/Minorista/Obtener_Datos_Grafico_Excel';

        console.log("Request Exporta Grafico");
        console.log(request);

        var success = false;

        $.ajax({
            type: 'POST',
            url: url_a,
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

}]);