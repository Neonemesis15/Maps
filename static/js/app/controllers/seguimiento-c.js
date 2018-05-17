'use strict';

controllers.controller("seguimiento", ["$scope", "$http", "setpagetitle", "googlemaps", function ($scope, $http, setpagetitle, googlemaps) {
    setpagetitle("Seguimiento GPS");
    require({ paths: ["/static/dgrid"] },
            [
                "dojo/parser",
                "dojo/dom-style",
                "dojo/dom",
                "dijit/registry",
                "dojox/dgauges/CircularRangeIndicator",
                "dgrid/OnDemandGrid",
                "dojox/widget/Standby",
                "dojox/dgauges/components/default/CircularLinearGauge",
                "dijit/layout/BorderContainer",
                "dijit/layout/ContentPane",
                "dijit/layout/TabContainer",
                "dojox/layout/ExpandoPane",
                "dijit/form/DateTextBox",
                "dojo/domReady!"
            ],
            function (parser, ds, dom, registry, cri, grid, sb) {
                parser.parse();
                map.refresh();
                ds.set("preloader", "display", "none");

                /* function that returns an object of options for gauge initialization */
                var options = function (s, v, f) {
                    return {
                        start: s,
                        value: v,
                        radius: 40,
                        startThickness: 15,
                        endThickness: 15,
                        fill: f,
                        interactionMode: "none"
                    }
                }

                function addIndicator2(gauge) {
                    var redRange = new cri(options(0, 60, "green"));
                    var yellowRange = new cri(options(10, 160, "yellow"));
                    var greenRange = new cri(options(160, 200, "red"));

                    /* hack for add custom indicator to default gauge from dojo */
                    gauge._elements[1].addIndicator("redRange", redRange, true);
                    gauge._elements[1].addIndicator("yellowRange", yellowRange, true);
                    gauge._elements[1].addIndicator("greenRange", greenRange, true);
                }

                function addIndicator(gauge) {
                    var redRange = new cri(options(0, 20, "red"));
                    var yellowRange = new cri(options(20, 70, "yellow"));
                    var greenRange = new cri(options(70, 100, "green"));

                    /* hack for add custom indicator to default gauge from dojo */
                    gauge._elements[1].addIndicator("redRange", redRange, true);
                    gauge._elements[1].addIndicator("yellowRange", yellowRange, true);
                    gauge._elements[1].addIndicator("greenRange", greenRange, true);
                }

                var efectividad = registry.byId("efectividad");
                addIndicator(efectividad);

                var prom_atencion = registry.byId("prom_atencion");
                addIndicator2(prom_atencion);

                var prom_traslado = registry.byId("prom_traslado");
                addIndicator2(prom_traslado);

                prom_atencion.set("maximum", 200);
                prom_traslado.set("maximum", 200);

                $scope.Grid = grid;

                dojo.connect(registry.byId("map"), "resize", function (changeSize, resultSize) {
                    map.refresh();
                });

                $scope.standbymap = new sb({ target: "mapPanel" });
                $scope.standbypanel = new sb({ target: "rigthCol" });

                document.body.appendChild($scope.standbymap.domNode);
                document.body.appendChild($scope.standbypanel.domNode);

                $scope.standbymap.startup();
                $scope.standbypanel.startup();

                $scope.grid_visitados = new grid({
                    columns: {
                        posicion: "Orden",
                        nombrePtoVenta: "Nombre",
                        inicioVisita: "Inicio Visita",
                        finVisita: "Fin Visita",
                        atencion: "Atención",
                        traslado: "Traslado",
                        estado: "Estado",
                        zona: "Zona",
                        distrito: "Distrito",
                        direccion: "Dirección"
                    }
                }, "grid_visitados");

                $scope.grid_novisitados = new grid({
                    columns: {
                        posicion: "Orden",
                        nombrePtoVenta: "Nombre",
                        zona: "Zona",
                        distrito: "Distrito",
                        direccion: "Dirección"
                    }
                }, "grid_novisitados");
            });

    var map = googlemaps('#map', gmaps.global.lima.center.lat, gmaps.global.lima.center.lng, 9, "ROADMAP");
    var exist = $scope.$parent.exist;
    var validcoord = $scope.$parent.validcoord;

    $http.post('/Reports/obtener_Generador', { codCampania: globals.codEquipo, codSupervisor: "0" }).success(function (response) {
        $scope.gies = response;
    });

    $http.get('/static/js/data/bak/lima.json?').success(function (data) {
        var options = {};
        options.geojson = data;
        options.geotype = 0;
        $scope.objects = map.getFromGeoJSON(options);
        $scope.drawlayer();
    });

    $scope.layers = [{ text: 'Zonas', val: 1, done: true }, { text: 'Distritos', val: 2, done: false}];

    var btn_data_clicked = function (polygon) {
        if (polygon) {
            //map.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
            $scope.departamento = "0";
            $scope.provincia = "0";
            $scope.distrito = "0";

            if (polygon.TYPE == 1) {
                $scope.zona = polygon.UBIGEO;
                $scope.departamento = "15";
                $scope.provincia = "01";
                $scope.distrito = "0";
            }
            if (polygon.TYPE == 2) {
                $scope.departamento = polygon.UBIGEO.substring(0, 2);
                $scope.provincia = polygon.UBIGEO.substring(2, 4);
                $scope.distrito = polygon.UBIGEO.substring(4, 6);
                $scope.zona = polygon.CODZONA;
            }
        }
        else {
            alert("Oops, ocurrió algo inesperado, por favor vuelva a intentarlo.");
        }
    }

    var restorecolour = function () {
        angular.forEach(map.polygons, function (polygon) {
            polygon.setOptions(polygon.properties);
        });
    }

    var polyclick = function (obj) {
        if (obj) {
            google.maps.event.addListener(obj, "click", function (evt) {
                btn_data_clicked(this);
                restorecolour();
                this.setOptions(gmaps.global.style.geometry.selected);
                map.map.fitBounds(this.getBounds());
                $scope.selectedpolygon = this;
            });
        }
    }

    $scope.drawlayer = function () {
        map.removePolygons();
        angular.forEach($scope.layers, function (layer) {
            if (layer.done) {
                for (var i = 0; i < $scope.objects.length; i++) {
                    if ($scope.objects[i].properties.TYPE == layer.val) {
                        var poly = map.drawPolygon($scope.objects[i]);
                        polyclick(poly);
                    }
                }
            }
        });
    }

    $scope.resize = function () {
        map.refresh();
    }

    ////////////// CHART

    var chart = function (store) {
        var categories = [];
        var data = [];

        var dataLabels = {
            enabled: true, rotation: -90,
            color: '#424242', align: 'right', x: 10, y: 10,
            style: {
                    fontSize: '14px',
                    fontFamily: 'Verdana, sans-serif'
                    }
        }

        angular.forEach(store, function (item) {
            categories.push(item.nombrePtoVenta.substring(0, 9));
            data.push(parseFloat(item.atencionSeg.toFixed(2)));
        });

        var grafico = new Highcharts.Chart({

            chart: {
                renderTo: 'chart',
                type: 'column'
            },

            title: {
                text: 'Tiempo transcurrido en PDV.'
            },

            xAxis: {
                categories: categories,
                labels: dataLabels
            },

            yAxis: {
                allowDecimals: false,
                min: 0,
                title: {
                    text: 'Tiempo Atención'
                }
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.x + '</b><br/>' +
                        this.series.name + ': ' + this.y + ' m. <br/>'
                }
            },
            series: [{
                name: "tiempo",
                data: data
            }]
        });
    }

    /* method for convert coordinates to LatLng Objects for Google Maps API V3*/
    var fromCoordToLatLng = function (o1, o2) {
        var latlngs = [];
        angular.forEach(o1, function (o) {
            var lat = o.latitud.replace(',', '.');
            var lng = o.longitud.replace(',', '.');

            if (validcoord(lat) && validcoord(lng)) {
                latlngs.push(new google.maps.LatLng(lat, lng));
            }
        });

        angular.forEach(o2, function (o) {
            var lat = o.latitud.replace(',', '.');
            var lng = o.longitud.replace(',', '.');

            if (validcoord(lat) && validcoord(lng)) {
                latlngs.push(new google.maps.LatLng(lat, lng));
            }
        });
        return latlngs;
    }

    $scope.getRoute = function () {

        var request = {
            codEquipo: globals.codEquipo,
            codPais: globals.codPais,
            codDepartamento: $scope.departamento,
            codProvincia: $scope.provincia,
            codDistrito: $scope.distrito,
            codGestor: $scope.gie,
            fecha: dijit.byId("fecha").displayedValue
        };

        $http.post('/Reports/get_seguimiento_generador', request).success(function (response) {

            var visitados = response.listPDVVisitados;
            var nvisitados = response.listPDVNoVisitados;
            var listaruta = response.listPDVRuta;

            /* adding legend inside map */
            if ($("#convention").length == 0) {
                map.addControl({
                    position: 'RIGHT_BOTTOM',
                    text: '',
                    html: '<div id="convention">' +
                      '<div><img src="/static/img/iconmarkers/blue/icon_blue.png" style="display:inline"/><span style="border-radius: 3px; background-color: #FFFFFF; color: #000000; padding: 5px;">PDV</span></div>' +
                      '<div><img src="/static/img/iconmarkers/red/icon_red.png" style="display:inline"/><span style="border-radius: 3px; background-color: #FFFFFF; color: #000000; padding: 5px;">Registro de visita</span></div>' +
                      '</div>',
                    style: {
                        width: '150px',
                        background: 'transparent',
                        boxShadow: 'none'
                    }
                });
            }

            if (visitados) {
                if (visitados.length != 0) {
                    $scope.grid_visitados.refresh();
                    $scope.grid_visitados.renderArray(visitados);
                }
                else {
                    $scope.grid_visitados.refresh();
                }

                map.removeMarkers();
                var paten = 0;
                var ptras = 0;
                var index = 0;

                angular.forEach(visitados, function (point) {
                    index++;

                    var lat = point.latitud.replace(',', '.');
                    var lng = point.longitud.replace(',', '.');

                    if (validcoord(lat) && validcoord(lng)) {

                        var foto1 = "/static/img/no-disponible.png";
                        var foto2 = "/static/img/no-disponible.png";

                        if (point.listaFoto.length > 0) {
                            try {
                                foto1 = globals.rutaFoto + point.listaFoto[0].nombreFoto;
                                foto2 = globals.rutaFoto + point.listaFoto[1].nombreFoto;
                            }
                            catch (e) { }
                        }

                        var marker = map.addMarker({
                            lat: lat,
                            lng: lng,
                            title: point.nombrePtoVenta,
                            code: point.codigoPtoVenta,
                            icon: gmaps.global.icons.numeric("red", index),
                            infoWindow: {
                                content: '<table>' +
                                     '<tr><td colspan="2" style="text-align:center; font-weight:bold;">' + point.nombrePtoVenta + '</td></tr>' +
                                     '<tr><td>Inicio de Visita:</td><td>' + point.inicioVisita + '</td></tr>' +
                                     '<tr><td>Fin de Visita</td><td>' + point.finVisita + '</td></tr>' +
                                     '<tr><td><img src="' + foto1 + '" /></td><td><img src="' + foto2 + '" /></td></tr>' +
                                     '</table>'
                            }
                        });

                        paten = paten + point.atencionSeg;
                        ptras = ptras + point.trasladoSeg;
                    }
                });

                paten = (paten / visitados.length).toFixed(0);
                ptras = (ptras / visitados.length).toFixed(0);

                index = 0;
            }

            if (nvisitados) {
                if (nvisitados.length != 0) {
                    $scope.grid_novisitados.refresh();
                    $scope.grid_novisitados.renderArray(nvisitados);
                }
                else {
                    $scope.grid_novisitados.refresh();
                }
            }


            if (exist(listaruta)) {
                var index2 = 0;
                angular.forEach(listaruta, function (point) {
                    index2++;

                    var lat = point.latitud.replace(',', '.');
                    var lng = point.longitud.replace(',', '.');

                    if (validcoord(lat) && validcoord(lng)) {
                        var marker = map.addMarker({
                            lat: lat,
                            lng: lng,
                            title: point.nombrePtoVenta,
                            code: point.codigoPtoVenta,
                            icon: gmaps.global.icons.numeric("blue", index2),
                            infoWindow: {
                                content: '<table>' +
                                     '<tr><td colspan="2" style="text-align:center; font-weight:bold;">' + point.nombrePtoVenta + '</td></tr>' +
                                     '<tr><td>Dirección:</td><td>' + point.direccion + '</td></tr>' +
                                     '</table>'
                            }
                        });
                    }
                });
                index2 = 0;
            }
            else {
                alert("No existen registros de ruta para este Gie.");
            }


            $scope.name_gie = $(".select2-choice span").text();

            if (exist(visitados)) {
                map.fitBounds(fromCoordToLatLng(listaruta, visitados));

                $scope.visitados = visitados.length;
                $scope.novisitados = nvisitados.length;

                $scope.efectividad = (($scope.visitados / listaruta.length) * 100).toFixed(0);

                dijit.registry.byId("efectividad").set("value", $scope.efectividad);
                dijit.registry.byId("prom_atencion").set("value", paten);
                dijit.registry.byId("prom_traslado").set("value", ptras);

                chart(visitados);

            }
            else {
                alert("No existen registros de visitas para este Gie.");
            }
        });
    }

} ]);