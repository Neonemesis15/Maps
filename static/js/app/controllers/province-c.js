'use strict';
globals.codCanal = '2008';

controllers.controller('mapa', ['$scope', '$http', 'setpagetitle', 'googlemaps', function ($scope, $http, setpagetitle, googlemaps) {
    setpagetitle("Xplora GIS - Provincia");
    require(
        [
            "dojo/parser",
            "dojo/dom-style",
            "dojo/dom",
            "dijit/registry",
            "dojox/fx/easing",
            "dojox/dgauges/components/default/CircularLinearGauge",
            "dojox/dgauges/CircularRangeIndicator",
            "dgrid/OnDemandGrid",
            "dojox/charting/Chart",
            "dojox/charting/axis2d/Default",
            "dojox/charting/plot2d/Markers",
            "dojox/charting/plot2d/Columns",
            "dojox/charting/plot2d/Grid",
            "dojox/charting/action2d/Highlight",
            "dojox/charting/action2d/MoveSlice",
            "dojox/charting/action2d/Tooltip",
            "dojox/charting/action2d/Magnify",
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
            "dijit/form/DateTextBox",
            "dojo/domReady!"
        ],
        function (parser, domStyle, dom, registry, easing, CircularLinearGauge, CircularRangeIndicator, Grid, Chart, Default, Markers, Columns, Grid2d, Highlight, MoveSlice, Tooltip, Magnify, Standby) {
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

    var container = googlemaps('#map', gmaps.global.peru.center.lat, gmaps.global.peru.center.lng, 5, "Básico");

    $scope.objects = {};

    $http.get('/static/js/data/bak/departamentos_peru.json').success(function (data) {
        var options = {};
        options.geojson = data;
        options.geotype = 1;
        container.loadFromGeoJSON(options);
    });

    $http.get('/static/js/data/bak/Distritos_Oficinas_Prov_Unidos.json').success(function (data) {
        var options = {};
        options.geojson = data;
        options.geotype = 0;
        $scope.objects.contorno = container.getFromGeoJSON(options);
        $http.get('/static/js/data/bak/distritos_oficinas_prov.json').success(function (data) {
            var options = {};
            options.geojson = data;
            options.geotype = 0;
            $scope.objects.distritos = container.getFromGeoJSON(options);
        });
        $scope.drawlayer();
    });

    $scope.layers = [{ text: 'Oficina', val: 1, done: true }, { text: 'Distritos', val: 2, done: false}];

    /**
    @function: drawlayer
    @author: Angel Ortiz
    @description: This method draws layers loaded from geojson files.
    Implements method for assing random colour and
    assign random colours based in on given colour from parent
    **/
    $scope.drawlayer = function () {
        container.removePolygons();
        $scope.ofcol = {};

        angular.forEach($scope.layers, function (layer) {
            if (layer.done) {
                if (layer.text == "Oficina") {
                    angular.forEach($scope.objects.contorno, function (obj) {
                        var opts = getRandomOptions();
                        if (obj.length == undefined) {
                            var poly = container.drawPolygon(obj);
                            poly.properties = opts;
                            poly.properties.TYPE = "1";
                            poly.setOptions(poly.properties);
                            $scope.polyclick(poly);
                            $scope.ofcol[obj.properties.cod_Oficina.toString()] = opts.fillColor;
                        }
                        else {
                            angular.forEach(obj, function (object) {
                                var poly = container.drawPolygon(object);
                                poly.properties = opts;
                                poly.properties.TYPE = "1";
                                poly.setOptions(poly.properties);
                                $scope.polyclick(poly);
                                $scope.ofcol[object.properties.cod_Oficina.toString()] = opts.fillColor;
                            });
                        }
                    });
                }
                else if (layer.text == "Distritos") {
                    if ($scope.selectedpolygon != undefined || $scope.selectedpolygon != null) {
                        angular.forEach($scope.objects.distritos, function (object) {
                            if (object.properties.cod_Oficina == $scope.selectedpolygon.cod_Oficina) {
                                var poly = container.drawPolygon(object);
                                poly.properties = getRandomOptions();
                                poly.properties.TYPE = "2";
                                poly.setOptions(poly.properties);
                                $scope.polyclick(poly);
                            }
                        });
                    }
                    else {
                        alert("Debe seleccionar una oficina para poder visualizar la capa de distritos.");
                        layer.done = false;
                    }
                }
            }
        });
    }

    $http.post('/Reports/get_anios')
        .success(function (response) {
            $scope.years = response;
        });

    $http.post('/Reports/get_meses')
        .success(function (response) {
            $scope.months = response;
        });

    $scope.loadperiods = function () {
        $scope.polyclick($scope.selectedpolygon);
        container.removeMarkers();
        $http.post('/Reports/get_periodos', { CodServicio: globals.codServicio, CodCanal: globals.codCanal, CodCliente: globals.codCliente, CodReporte: globals.codReporte.toString(), Anio: $scope.year, Mes: $scope.month })
        .success(function (response) {
            $scope.periods = response;
        });
    }

    $http.post('/Reports/get_tipoCluster')
        .success(function (response) {
            $scope.clusters = response;
        });

    $http.post('/Reports/get_categorias', { codEquipo: globals.codEquipo, codReporte: globals.codReporte })
        .success(function (response) {
            $scope.categories = response;
        });

    $scope.loadproducts = function () {
        $http.post('/Reports/get_productos', { CodEquipo: globals.codEquipo, CodCliente: globals.codCliente, CodCategoria: $scope.category, CodSubCategoria: globals.codSubCategoria, CodMarca: globals.codMarca })
            .success(function (response) {
                $scope.products = response;
            });
    }

    $scope.polyclick = function (obj, dijit) {
        if (obj) {
            google.maps.event.addListener(obj, "click", function (evt) {

                $scope.btn_data_clicked(this);
                $scope.get_overview_box(this);
                restoreDefaultColor($scope.selectedpolygon);
                this.setOptions(gmaps.global.style.geometry.selected);
                container.map.fitBounds(this.getBounds());
                $scope.selectedpolygon = this;
                //$scope.registry.byId("rigthCol").toggle();
            });

            var textlabel = "";
            if (obj.TYPE == "1") {
                textlabel = obj.Name_Oficina;
            }
            else if (obj.TYPE == "2") {
                textlabel = obj.NOMB_DIST;
            }
            var marker = new MarkerWithLabel({
                position: new google.maps.LatLng(0, 0),
                draggable: false,
                raiseOnDrag: false,
                map: container.map,
                labelContent: textlabel,
                labelAnchor: new google.maps.Point(30, 20),
                labelClass: "labels", // the CSS class for the label
                labelStyle: { opacity: 1.0 },
                icon: "http://placehold.it/1x1",
                visible: false
            });

            google.maps.event.addListener(obj, "mousemove", function (event) {
                marker.setPosition(event.latLng);
                marker.setVisible(true);
            });
            google.maps.event.addListener(obj, "mouseout", function (event) {
                marker.setVisible(false);
            });
        }
    }

    $scope.btn_data_clicked = function (polygon) {
        if (polygon) {
            container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });

            $scope.zona = "0";
            $scope.oficina = polygon.cod_Oficina;
            $scope.nombre_oficina = polygon.Name_Oficina;
            $scope.departamento = polygon.CODIGO_PRO.substring(0, 2);
            $scope.provincia = polygon.CODIGO_PRO.substring(2, 4);

            if (polygon.TYPE == 1) {
                $scope.udistrito = "0";
                $scope.distrito = "0";
            }
            if (polygon.TYPE == 2) {
                $scope.distrito = polygon.CODIGO_DIS.substring(4, 6);
                $scope.udistrito = polygon.CODIGO_DIS;
            }
            $scope.get_data(polygon);

        }
        else {
            alert("Oops, ocurrió algo inesperado, por favor vuelva a intentarlo.");
        }
    }

    $scope.get_overview_box = function (properties) {

        var dcluster = {
            codOficina: $scope.oficina,
            codZona: $scope.zona,
            codDistrito: $scope.udistrito,
            idPlanning: globals.codEquipo,
            reportsPlanning: $scope.period
        }

        $http.post("/Reports/get_cluster_and_representatividad_Prov", dcluster).success(
            function (response) {
                $scope.resumen = response;
                dijit.registry.byId("rigthCol").set("title", "Informative Panel " + $scope.resumen.representatividadZonaDistritoMap.representatividadSector.nombre);
            }
        );
    }

    $scope.get_data = function (properties) {

        $scope.standbymap.show();
        $scope.standbypanel.show();

        container.removeMarkers();
        var type = properties.TYPE;
        var ubigeo = null;

        var tipo = parseInt(type) + 2;
        if (tipo == 3)
            ubigeo = properties.cod_Oficina;
        else
            ubigeo = properties.CODIGO_DIS;

        var data = { codOficina: $scope.oficina, codZona: $scope.zona, codDistrito: $scope.distrito, coddepartamento: $scope.departamento, codciudad: $scope.provincia, reportsPlanning: $scope.period };

        $http.post("/Reports/get_presencia_Prov_Din", data).success(function (response) {
            $("#tb_rangos tbody").empty();
            $("#tb_mandatorios tbody").empty();
            $("#tb_ev_canti tbody").empty();
            $("#tb_ev_check tbody").empty();

            if (response != null) {
                $scope.presencia = response.listaPresencia;
                if (response.listaPresencia) {
                    for (var i = 0; i < $scope.presencia.length; i++) {
                        if ($scope.presencia[i].id_tipo == 'RANG-00') {
                            var prango = (($scope.presencia[i].coverage / $scope.presencia[i].totalcoverage) * 100).toFixed(0);
                            $("#tb_rangos tbody").append('<tr><td><li rel="' + $scope.presencia[i].id_elemento + '" class="icon-map-marker botonmarker markersRangos"></li> <li rel="' + $scope.presencia[i].id_elemento + '" rel2="3" class="icon-download-alt botonmarker downloadsRango"></li> ' + $scope.presencia[i].id_elemento + '</td><td>' + prango + '%</td></tr>');
                        }
                        if ($scope.presencia[i].id_tipo == '04') {
                            $("#tb_mandatorios tbody").append('<tr><td><li rel="' + $scope.presencia[i].id_elemento + '" class="icon-map-marker botonmarker markersSKU"></li> <li rel="' + $scope.presencia[i].id_elemento + '" rel2="1" class="icon-download-alt botonmarker downloadsSKU"></li> ' + $scope.presencia[i].nombre_elemento + '</td><td>' + $scope.presencia[i].coverage + '</td><td>' + (($scope.presencia[i].coverage / $scope.presencia[i].totalcoverage) * 100).toFixed(0) + '%</td></tr>');
                        }
                    }
                    $scope.standbymap.hide();
                    $scope.standbypanel.hide();
                }

                $(".downloadsSKU").on("click", function () {
                    $scope.fndownloads($(this));
                });

                $(".downloadsRango").on("click", function () {
                    $scope.fndownloads($(this));
                });

                $(".markersRangos").on("click", function () {
                    $scope.standbymap.show();
                    $scope.standbypanel.show();
                    container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });

                    var row = $(this);
                    var idelemento = row.attr("rel");
                    container.removeMarkers();

                    var _request = {
                        codCanal: globals.codCanal,
                        codPais: globals.codPais,
                        codOficina: $scope.oficina,
                        codDepartamento: $scope.departamento,
                        codProvincia: $scope.provincia,
                        codZona: $scope.zona,
                        codDistrito: $scope.distrito,
                        codRango: idelemento,
                        codPeriodo: $scope.period
                    }

                    $http.post('/Reports/Obtener_PuntoVentaPresenciaRango_Prov', _request)
                        .success(function (response) {
                            if (response != null) {
                                angular.forEach(response, function (point) {
                                    if (point.latitud && point.longitud) {
                                        container.addMarker({
                                            lat: point.latitud,
                                            lng: point.longitud,
                                            title: point.nombrePuntoVenta,
                                            code: point.codPuntoVenta,
                                            icon: gmaps.global.minicons[point.color.toLowerCase()],
                                            click: function (e) {
                                                $scope.get_pdvdata(e.code);
                                            }
                                        });
                                    }
                                });
                                google.maps.event.clearListeners($scope.selectedpolygon, "click");
                                try { $(".row-selected").removeClass("row-selected"); } catch (ev) { }
                                row.parent().parent().addClass("row-selected");

                                $scope.standbymap.hide();
                                $scope.standbypanel.hide();
                            } else {
                                //alert("");
                            }
                        });
                });

                $(".markersSKU").on("click", function () {
                    $scope.standbymap.show();
                    $scope.standbypanel.show();
                    container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });
                    var row = $(this);
                    var sku = row.attr("rel");
                    container.removeMarkers();

                    var _request = {
                        codCanal: globals.codCanal,
                        codPais: globals.codPais,
                        codDepartamento: $scope.departamento,
                        codProvincia: $scope.provincia,
                        codZona: $scope.zona,
                        codDistrito: $scope.distrito,
                        codProducto: sku,
                        codPeriodo: $scope.period
                    }

                    $http.post('/Reports/Obtener_PuntoVentaPresenciaSKU', _request)
                    .success(function (response) {
                        if (response != null) {
                            angular.forEach(response, function (point) {
                                if (point.latitud && point.longitud) {
                                    container.addMarker({
                                        lat: point.latitud,
                                        lng: point.longitud,
                                        title: point.nombrePuntoVenta,
                                        code: point.codPuntoVenta,
                                        icon: gmaps.global.minicons[point.color.toLowerCase()],
                                        click: function (e) {
                                            $scope.get_pdvdata(e.code);
                                        }
                                    });
                                }
                            });
                            google.maps.event.clearListeners($scope.selectedpolygon, "click");
                            try { $(".row-selected").removeClass("row-selected"); } catch (ev) { }
                            row.parent().parent().addClass("row-selected");
                            $scope.addlegend({ greentext: "Con Presencia", redtext: "Sin Presencia" });

                            $scope.standbymap.hide();
                            $scope.standbypanel.hide();

                        } else {
                            //alert("");
                        }
                    });
                });

                if (response.listaElemVisibilidad) {

                    $("#tb_ev_canti thead tr").empty();
                    $("#tb_ev_check thead tr").empty();
                    $("#tb_ev_canti tbody").empty();
                    $("#tb_ev_check tbody").empty();

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

                    $("#tb_ev_canti thead tr").append('<th>Elementos de Visibilidad</th>');
                    $("#tb_ev_check thead tr").append('<th>Elementos de Visibilidad</th>');

                    angular.forEach(headers, function (col) {
                        $("#tb_ev_canti thead tr").append('<th>' + col + '</th>');
                        $("#tb_ev_check thead tr").append('<th>' + col + '</th>');
                    });

                    angular.forEach(body, function (row) {
                        var li1 = '<li rel="' + row.cod_elemento + '" class="icon-map-marker botonmarker markersElemVisibilidad"></li>';
                        var li2 = '<li rel="' + row.cod_elemento + '" rel2="2" class="icon-download-alt botonmarker downloads"></li>';

                        var values = '';

                        angular.forEach(row.valor, function (value) {
                            values = values + '<td>' + value + '</td>';
                        });

                        var images = '';

                        angular.forEach(row.valor, function (value) {
                            var iconpro = (value > 0) ? '<img src="/static/img/icon-check.png"></img>' : '';
                            images = images + '<td>' + iconpro + '</td>';
                        });

                        $("#tb_ev_canti tbody").append('<tr><td>' + li1 + li2 + row.nombre_elemento + '</td>' + values + '</tr>');
                        $("#tb_ev_check tbody").append('<tr><td>' + li1 + li2 + row.nombre_elemento + '</td>' + images + '</tr>');
                    });

                    $(".downloads").on("click", function () {
                        $scope.fndownloads($(this));
                    });

                    $(".markersElemVisibilidad").on("click", function () {
                        $scope.standbymap.show();
                        $scope.standbypanel.show();
                        container.removeControl({ position: "RIGHT_BOTTOM", index: 0 });

                        var row = $(this);
                        var idelement = row.attr("rel");
                        container.removeMarkers();

                        var _request = {
                            codCanal: globals.codCanal,
                            codPais: globals.codPais,
                            codOficina: $scope.oficina,
                            codDepartamento: $scope.departamento,
                            codProvincia: $scope.provincia,
                            codZona: $scope.zona,
                            codDistrito: $scope.distrito,
                            codElemento: idelement,
                            codPeriodo: $scope.period
                        }

                        $http.post('/Reports/Obtener_PuntoVentaPresenciaElemVisibilidad_Prov', _request)
                            .success(function (response) {
                                if (response != null) {
                                    angular.forEach(response, function (point) {
                                        if (point.latitud && point.longitud) {
                                            container.addMarker({
                                                lat: point.latitud,
                                                lng: point.longitud,
                                                title: point.nombrePuntoVenta,
                                                code: point.codPuntoVenta,
                                                icon: gmaps.global.minicons[point.color.toLowerCase()],
                                                click: function (e) {
                                                    $scope.get_pdvdata(e.code);
                                                }
                                            });
                                        }
                                    });
                                    google.maps.event.clearListeners($scope.selectedpolygon, "click");
                                    try { $(".row-selected").removeClass("row-selected"); } catch (ev) { }
                                    row.parent().parent().addClass("row-selected");
                                    $scope.addlegend({ greentext: "Con Elemento", redtext: "Sin Elemento" });

                                    $scope.standbymap.hide();
                                    $scope.standbypanel.hide();
                                } else { }
                            });
                    });
                }
            }
            else {
                alert("Oops... No hemos recibido datos del servidor, por favor vuelve a intentarlo.");
            }
        });

        $http.post("/Reports/get_ventas", { tipo: tipo, codigo: ubigeo, reportsPlanning: $scope.period }).success(
            function (response) {
                $("#tb_ventas tbody").empty();
                $("#tb_ventas thead").empty();
                if (response != null) {
                    $("#loading").remove();
                    if (response.length > 0) {

                        var lista = {};
                        var header = {};
                        var footer = {};
                        var total = 0;
                        var n = 1;
                        var m = 1;

                        $.each(response, function (x, venta) {
                            if (venta.idCategoria != 0) {
                                if (!lista[venta.idCategoria]) {
                                    lista[venta.idCategoria] = { categoria: venta.nombreCategoria, data: [{ distribuidora: venta.nombreDistribuidora, venta: venta.venta}] };
                                }
                                else {
                                    lista[venta.idCategoria].data.push({ distribuidora: venta.nombreDistribuidora, venta: venta.venta });
                                }

                                if (!header[venta.nombreDistribuidora]) {
                                    header[venta.nombreDistribuidora] = { nombre: venta.nombreDistribuidora };
                                }
                            }
                            else if (venta.idCategoria == 0) {
                                if (!footer[venta.nombreDistribuidora]) {
                                    footer[venta.nombreDistribuidora] = { venta: venta.venta };
                                }
                            }
                        });
                        if (header) {
                            $("#tb_ventas thead").append('<tr></tr>');
                            $("#tb_ventas thead tr").append('<th><li id="ventasicon" class="icon-plus-sign"></li> Sell Out</th>');
                            $.each(header, function (x, td) {
                                $("#tb_ventas thead tr").append('<th>' + td.nombre + '</th>');
                                n += 1;
                            });
                            $("#tb_ventas thead tr").append('<th>Total</th>');
                        }

                        if (lista) {
                            var i = 0;
                            var tot = 0;

                            $.each(lista, function (x, row) {
                                var sum = 0;
                                $("#tb_ventas tbody").append('<tr class="conceadable"><td>' + row.categoria + '</td></tr>');
                                $.each(row.data, function (y, dist) {
                                    $("#tb_ventas tbody tr:eq(" + i + ")").append('<td>' + $scope.format_soles(dist.venta.toString()) + '</td>');
                                    sum += dist.venta;
                                });
                                $("#tb_ventas tbody tr:eq(" + i + ")").append('<td>' + $scope.format_soles(sum.toString()) + '</td>');
                                i += 1;
                                tot += sum;
                            });
                            total = tot;
                        }

                        if (footer) {
                            var suma = 0;
                            $("#tb_ventas tbody").append('<tr class="foot"><td>Total</td></tr>');
                            $.each(footer, function (x, ft) {
                                $(".foot").append('<td>' + $scope.format_soles(ft.venta.toString()) + '</td>');
                                m += 1;
                                suma += ft.venta;
                            });
                            $(".foot").append('<td>' + $scope.format_soles(suma.toString()) + '</td>');
                        }
                    }
                    $(".conceadable").hide();
                    $scope.standbymap.hide();
                    $scope.standbypanel.hide();
                }
                else {
                    alert("No hay registros de ventas.");
                }
            });
    }

    $scope.fndownloads = function (obj) {
        var row = obj;
        var idelemento = row.attr("rel");
        var idOpcion = row.attr("rel2");

        var _request = {
            codCanal: globals.codCanal,
            codPais: globals.codPais,
            codDepartamento: $scope.departamento,
            codOficina: $scope.oficina,
            codProvincia: $scope.provincia,
            codZona: $scope.zona,
            codDistrito: $scope.distrito,
            codPresencia: idelemento,
            codPeriodo: $scope.period,
            codOpcion: idOpcion
        }

        try { $(".row-selected").removeClass("row-selected"); } catch (ev) { }
        row.parent().parent().addClass("row-selected");

        $.ajax({
            type: 'POST',
            url: '/Reports/Obtener_PuntoVentaPresenciaToExcel_Prov',
            data: _request,
            context: document.body,
            async: false,   //NOTE THIS
            success: function () {  //THIS ALSO CHANGED
                $scope.success = true;
            }
        });

        if ($scope.success) { //AND THIS CHANGED
            window.open('/Reports/exportarExcelPresencia?nombreArchivo=Presencia');
        }
    }

    $scope.get_pdvdata = function (codpdv) {

        $http.post('/Reports/get_foto_puntoventa', { codPtoVenta: codpdv, reportsPlanning: $scope.period }).success(function (response) {
            $scope.photos = {};
            if (response.length > 0) {
                angular.forEach(response, function (photo, key) {
                    if (photo != "") {
                        $scope.photos[key] = globals.rutaFoto + photo;
                    }
                    else {
                        $scope.photos[key] = "/static/img/no-disponible.png";
                    }
                });
            }
            else {
                $scope.photos["interiorAntes"] = "/static/img/no-disponible.png";
                $scope.photos["exteriorAntes"] = "/static/img/no-disponible.png";
                $scope.photos["interiorDespues"] = "/static/img/no-disponible.png";
                $scope.photos["exteriorDespues"] = "/static/img/no-disponible.png";
            }
        });

        $http.post('/Reports/get_datoPuntoventa', { codPtoVentaCliente: codpdv, reportsPlanning: $scope.period }).success(function (response) {
            if (response != null) {
                $scope.pdvinfo = response;
                var t = setTimeout(function () { $scope.registry.byId("popup").show(); }, 2000);
            }
        });

        $http.post("/Reports/get_presencia_puntoventa", { codEquipo: globals.codEquipo, reportsPlanning: $scope.period, codPtoVenta: codpdv })
            .success(function (response) {
                if (response != null) {
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

        $http.post("/Reports/get_ventas_puntoventa", { codEquipo: globals.codEquipo, reportsPlanning: $scope.period, codPtoVenta: codpdv })
            .success(function (response) {
                $scope.sales = response;
            });

        $http.post("/Reports/get_elemvisibilidad_puntoventa", { codEquipo: globals.codEquipo, reportsPlanning: $scope.period, codPtoVenta: codpdv })
            .success(function (response) {
                $scope.visibility = response;
            });
    }

    $scope.addlegend = function (params) {
        container.addControl({
            position: 'RIGHT_BOTTOM',
            text: '',
            html: '<ul id="semaforo">' +
            '     <li>Leyenda</li>' +
            '     <li><img src="/static/img/green-legend.png"/>' + params.greentext + '</li>' +
            '     <li><img src="/static/img/red-legend.png"/>' + params.redtext + '</li>' +
            '</ul>',
            style: {
                margin: '5px 5px 5px 5px',
                padding: '1px 6px',
                border: 'solid 1px #717B87',
                background: '#ffffff'
            }
        });
    }

    $scope.format_soles = function (val) {
        var value = val.toString();
        var result = "S/. ";
        var decimal = value.indexOf(".");

        if (decimal != -1) {
            var million = value.substring(0, decimal);
            if (million.length > 3) {
                result = result + million.substring(0, million.length - 3) + "," + million.substring(million.length - 3);
            }
            else {
                result = result + million;
            }
            result = result + value.substring(decimal);
        } else {
            if (value.length > 3) {
                result = result + value.substring(0, value.length - 3) + "," + value.substring(value.length - 3);
            }
            else {
                result = result + value;
            }
        }

        return result;
    }

    $scope.percentage = function (cantidad, total) {
        var p = ((cantidad / total) * 100).toFixed(0);
        if (isNaN(p)) {
            return "0";
        }
        else {
            return p;
        }
    }



    var getparams = function () {

        var object = {
            codServicio: globals.codServicio,
            codCanal: globals.codCanal,
            codCliente: globals.codCliente,
            codPais: globals.codPais,
            codOficina: $scope.oficina,
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
        }

        return object;
    }

    $scope.drawcharts = function () {
        $http.post('/Reports/Obtener_Grafico_Tendencia_Prov', getparams())
            .success(function (response) {

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
                    }]
                });
            }
        );


        $http.post('/Reports/Obtener_Grafico_Variacion_Prov', getparams())
            .success(function (response) {

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

                    series: series
                });

            }
        );

    }

    $scope.export_excel = function (button) {

        var _request = {

            codServicio: globals.codServicio,
            codCanal: globals.codCanal,
            codCliente: globals.codCliente,
            codPais: globals.codPais,
            codOficina: $scope.oficina,
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

        $.ajax({
            type: 'POST',
            url: '/Reports/Obtener_Datos_Grafico_Excel_Prov',
            data: _request,
            context: document.body,
            async: false,   //NOTE THIS
            success: function () {  //THIS ALSO CHANGED
                $scope.success = true;
            }
        });

        if ($scope.success) { //AND THIS CHANGED
            window.open('/Reports/exportarExcelPresencia?nombreArchivo=DatosOrigen');
        }
    }
} ]);