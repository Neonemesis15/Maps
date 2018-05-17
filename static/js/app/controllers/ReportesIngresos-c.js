'use strict';

controllers.controller('reporteingresos', ['$scope', '$http', '$filter', 'setpagetitle', function ($scope, $http, $filter, setpagetitle) {
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
        domStyle.set("preloader", "display", "none");

        dojo.connect(registry.byId("map"), "resize", function (changeSize, resultSize) {
            container.refresh();
        });

        //$scope.standbymap = new Standby({ target: "mapPanel" });
        //$scope.standbypanel = new Standby({ target: "rigthCol" });


        //document.body.appendChild($scope.standbymap.domNode);
        //document.body.appendChild($scope.standbypanel.domNode);


        //$scope.standbymap.startup();
        //$scope.standbypanel.startup();

        $scope.loadyear();

        $scope.codmodulo = "XPL_MAPS_COLGATE"
    });

    $scope.loadyear = function () {
        $http.post('/Reports/get_anios').success(function (response) {
            $scope.years = response;
            $scope.loadmounts();
            //$scope.period = "";
        });
    }

    $scope.loadmounts = function () {
        $http.post('/Reports/get_meses').success(function (response) {
            $scope.months = response;
            $scope.loadSemanas();
            //$scope.period = "";
        });
    }

    $scope.loadSemanas = function () {
        var request = {
            anio: $scope.anio_diario,
            mes: $scope.month_diario
        }

        $http.post('/Reports/get_SemanasxMes', request)
            .success(function (response) {
                $scope.sems = response;
                console.log(request);
            });
    }

    $scope.load_ingresos_anuales = function () {

        if ($scope.anio_anual == "" || $scope.anio_anual == undefined) {
            $scope.IngresosAnuales = ""
            return;
        }

        var request = {
            tipoVisita: "3",
            codModulo: $scope.codmodulo,
            anio: $scope.anio_anual,
            mes: "",
            semana: ""
        }
        console.log("Envio");
        console.log(request);

        $http.post('/Reports/get_ListConsulta_IngresosModulo', request).success(function (response) {
            $scope.periods = response;
            console.log("Respuesta");
            console.log(response);
            var IngresosAnuales = [];
            angular.forEach(response, function (dat) {
                var detalles = [];
                var data = { idusuario: dat.codUsuario,
                    usuario: dat.nombreUsuario,
                    ejecutivo: dat.nombreCompleto,
                    detalles: detalles,
                    imgresultado: '/static/img/icon-' + dat.resultado + '.png',
                    valresultado: ''
                };

                angular.forEach(dat.detalles, function (det) {
                    var detalle = { nombre: det.nombreFecha,
                        subnombre: det.nombreFecha2,
                        img: "/static/img/icon-"+det.valor+".png"
                    };
                    /*if (det.valor > 0) {
                        detalle.img = "/static/img/icon-check.png";
                    } else {
                        detalle.img = "/static/img/icon-uncheck.png";
                    }*/
                    detalles.push(detalle);
                });

                data.detalles = detalles;
                /*
                data.idusuario = dat.idusuario;
                data.usuario = dat.usuario;
                data.ejecutivo = dat.ejecutivo;

                if (dat.resultado >= 50) {
                data.imgresultado = "/static/img/icon-green.png";
                }
                else if (dat.resultado >= 30 && dat.resultado < 50) {
                data.imgresultado = "/static/img/icon-yellow.png";
                }
                else if (dat.resultado > 0 && dat.resultado < 30) {
                data.imgresultado = "/static/img/icon-gray.png";
                }
                else {
                data.imgresultado = "/static/img/icon-red.png";
                }*/

                IngresosAnuales.push(data);

            });


            $scope.IngresosAnuales = IngresosAnuales;

        });

        /*

        var response = [];
        var IngresosAnuales = [];
        var cant = 10;
        var cant_d = 5;
        for (var i = 0; i < cant; i++) {
        var detalles = [];
        var data = { idusuario: '', usuario: '', ejecutivo: '', detalles: detalles, resultado: '' };
        for (var j = 1; j < cant_d; j++) {
        var detalle = { nombre: '', subnombre: '', valor: '' };
        detalle.nombre = "det " + j;
        detalle.subnombre = "sub" + j;
        detalle.valor = Math.round(Math.random() * 100);
        detalles.push(detalle);
        }

        data.idusuario = "Id " + i;
        data.usuario = "Usuario " + i;
        data.ejecutivo = "Ejecutivo " + i;
        data.resultado = Math.round(Math.random() * 100);
        data.detalles = detalles;

        //var data = { serie: serie, detalle: detalles };

        response.push(data);
        }

        angular.forEach(response, function (dat) {
        var detalles = [];
        var data = { idusuario: '', usuario: '', ejecutivo: '', detalles: detalles, resultado: '', valresultado: '' };
        angular.forEach(dat.detalles, function (det) {
        var detalle = { nombre: det.nombre, subnombre: det.subnombre, img: '' };
        if (det.valor > 0) {
        detalle.img = "/static/img/icon-check.png";
        } else {
        detalle.img = "/static/img/icon-uncheck.png";
        }
        detalles.push(detalle);
        });

        data.idusuario = dat.idusuario;
        data.usuario = dat.usuario;
        data.ejecutivo = dat.ejecutivo;
        if (dat.resultado >= 50) {
        data.imgresultado = "/static/img/icon-green.png";
        }
        else if (dat.resultado >= 30 && dat.resultado < 50) {
        data.imgresultado = "/static/img/icon-yellow.png";
        }
        else if (dat.resultado > 0 && dat.resultado < 30) {
        data.imgresultado = "/static/img/icon-gray.png";
        }
        else {
        data.imgresultado = "/static/img/icon-red.png";
        }
        data.detalles = detalles;
        data.valresultado = dat.resultado;
        IngresosAnuales.push(data);

        });

        $scope.IngresosAnuales = IngresosAnuales;
        */
    }

    $scope.load_ingresos_semanales = function () {

        if ($scope.anio_semanal == "" || $scope.anio_semanal == undefined) {
            $scope.IngresosSemanales = ""
            return;
        }

        if ($scope.month_semanal == "" || $scope.month_semanal == undefined) {
            $scope.IngresosSemanales = ""
            return;
        }

        var request = {
            tipoVisita: "2",
            codModulo: $scope.codmodulo,
            anio: $scope.anio_semanal,
            mes: $scope.month_semanal,
            semana: ""

        }
        console.log("Envio");
        console.log(request);

        $http.post('/Reports/get_ListConsulta_IngresosModulo', request).success(function (response) {
            $scope.periods = response;
            console.log("Respuesta");
            console.log(response);
            var IngresosSemanales = [];
            angular.forEach(response, function (dat) {
                var detalles = [];
                var data = { idusuario: dat.codUsuario,
                    usuario: dat.nombreUsuario,
                    ejecutivo: dat.nombreCompleto,
                    detalles: detalles,
                    imgresultado: '/static/img/icon-' + dat.resultado + '.png',
                    valresultado: ''
                };

                angular.forEach(dat.detalles, function (det) {
                    var detalle = { nombre: det.nombreFecha,
                        subnombre: det.nombreFecha2,
                        img: ''
                    };
                    if (det.valor > 0) {
                        detalle.img = "/static/img/icon-check.png";
                    } else {
                        detalle.img = "/static/img/icon-uncheck.png";
                    }
                    detalles.push(detalle);
                });

                data.detalles = detalles;
                /*
                data.idusuario = dat.idusuario;
                data.usuario = dat.usuario;
                data.ejecutivo = dat.ejecutivo;

                if (dat.resultado >= 50) {
                data.imgresultado = "/static/img/icon-green.png";
                }
                else if (dat.resultado >= 30 && dat.resultado < 50) {
                data.imgresultado = "/static/img/icon-yellow.png";
                }
                else if (dat.resultado > 0 && dat.resultado < 30) {
                data.imgresultado = "/static/img/icon-gray.png";
                }
                else {
                data.imgresultado = "/static/img/icon-red.png";
                }*/

                IngresosSemanales.push(data);

            });


            $scope.IngresosSemanales = IngresosSemanales;

        });

        /*
        var response = [];
        var IngresosSemanales = [];
        var cant = 10;
        var cant_d = 5;
        for (var i = 0; i < cant; i++) {
        var detalles = [];
        var data = { idusuario: '', usuario: '', ejecutivo: '', detalles: detalles, resultado: '' };
        for (var j = 1; j < cant_d; j++) {
        var detalle = { nombre: '', subnombre: '', valor: '' };
        detalle.nombre = "det " + j;
        detalle.subnombre = "sub" + j;
        detalle.valor = Math.round(Math.random() * 100);
        detalles.push(detalle);
        }

        data.idusuario = "Id " + i;
        data.usuario = "Usuario " + i;
        data.ejecutivo = "Ejecutivo " + i;
        data.resultado = Math.round(Math.random() * 100);
        data.detalles = detalles;

        //var data = { serie: serie, detalle: detalles };

        response.push(data);
        }

        angular.forEach(response, function (dat) {
        var detalles = [];
        var data = { idusuario: '', usuario: '', ejecutivo: '', detalles: detalles, resultado: '', valresultado: '' };
        angular.forEach(dat.detalles, function (det) {
        var detalle = { nombre: det.nombre, subnombre: det.subnombre, img: '' };
        if (det.valor > 0) {
        detalle.img = "/static/img/icon-check.png";
        } else {
        detalle.img = "/static/img/icon-uncheck.png";
        }
        detalles.push(detalle);
        });

        data.idusuario = dat.idusuario;
        data.usuario = dat.usuario;
        data.ejecutivo = dat.ejecutivo;
        if (dat.resultado >= 50) {
        data.imgresultado = "/static/img/icon-green.png";
        }
        else if (dat.resultado >= 30 && dat.resultado < 50) {
        data.imgresultado = "/static/img/icon-yellow.png";
        }
        else if (dat.resultado > 0 && dat.resultado < 30) {
        data.imgresultado = "/static/img/icon-gray.png";
        }
        else {
        data.imgresultado = "/static/img/icon-red.png";
        }
        data.detalles = detalles;
        data.valresultado = dat.resultado;
        IngresosSemanales.push(data);

        });

        
        $scope.IngresosSemanales = IngresosSemanales;
        */
    }

    $scope.load_ingresos_diarios = function () {

        if ($scope.anio_diario == "" || $scope.anio_diario == undefined) {
            $scope.IngresosDiarios = ""
            return;
        }

        if ($scope.month_diario == "" || $scope.month_diario == undefined) {
            $scope.IngresosDiarios = ""
            return;
        }

        if ($scope.sem_diario == "" || $scope.sem_diario == undefined) {
            $scope.IngresosDiarios = ""
            return;
        }

        var request = {
            tipoVisita: "1",
            codModulo: $scope.codmodulo,
            anio: $scope.anio_diario,
            mes: $scope.month_diario,
            semana: $scope.sem_diario
        }

        $http.post('/Reports/get_ListConsulta_IngresosModulo', request).success(function (response) {
            
            $scope.periods = response;

            console.log("datoss");
            console.log(response);

            var IngresoDiarios = [];

            if (!response) {
                var data = { idusuario: "Sin Datos",
                    usuario: "Sin Datos",
                    ejecutivo: "Sin Datos",
                    detalles: "Sin Datos",
                    imgresultado: "Sin Datos",
                    valresultado: "Sin Datos"
                };

                IngresoDiarios.push(data);
            }

            angular.forEach(response, function (dat) {
                var detalles = [];
                var data = { idusuario: dat.codUsuario,
                    usuario: dat.nombreUsuario,
                    ejecutivo: dat.nombreCompleto,
                    detalles: detalles,
                    imgresultado: '/static/img/icon-' + dat.resultado + '.png',
                    valresultado: ''
                };

                angular.forEach(dat.detalles, function (det) {
                    var detalle = { nombre: det.nombreFecha,
                        subnombre: det.nombreFecha2,
                        img: ''
                    };
                    if (det.valor > 0) {
                        detalle.img = "/static/img/icon-check.png";
                    } else {
                        detalle.img = "/static/img/icon-uncheck.png";
                    }
                    detalles.push(detalle);
                });

                data.detalles = detalles;
                /*
                data.idusuario = dat.idusuario;
                data.usuario = dat.usuario;
                data.ejecutivo = dat.ejecutivo;

                if (dat.resultado >= 50) {
                data.imgresultado = "/static/img/icon-green.png";
                }
                else if (dat.resultado >= 30 && dat.resultado < 50) {
                data.imgresultado = "/static/img/icon-yellow.png";
                }
                else if (dat.resultado > 0 && dat.resultado < 30) {
                data.imgresultado = "/static/img/icon-gray.png";
                }
                else {
                data.imgresultado = "/static/img/icon-red.png";
                }*/

                IngresoDiarios.push(data);

            });


            $scope.IngresosDiarios = IngresoDiarios;

        });

        /*
        var response = [];
        var IngresoDiarios = [];
        var cant = 10;
        var cant_d = 5;
        for (var i = 0; i < cant; i++) {
        var detalles = [];
        var data = { idusuario: '', usuario: '', ejecutivo: '', detalles: detalles, resultado: '' };
        for (var j = 1; j < cant_d; j++) {
        var detalle = { nombre: '', subnombre: '', valor: '' };
        detalle.nombre = "det " + j;
        detalle.subnombre = "sub" + j;
        detalle.valor = Math.round(Math.random() * 100);
        detalles.push(detalle);
        }

        data.idusuario = "Id " + i;
        data.usuario = "Usuario " + i;
        data.ejecutivo = "Ejecutivo " + i;
        data.resultado = Math.round(Math.random() * 100);
        data.detalles = detalles;

        //var data = { serie: serie, detalle: detalles };

        response.push(data);
        }
        
        angular.forEach(response, function (dat) {
        var detalles = [];
        var data = { idusuario: '', usuario: '', ejecutivo: '', detalles: detalles, resultado: '', valresultado: '' };
        angular.forEach(dat.detalles, function (det) {
        var detalle = { nombre: det.nombre, subnombre: det.subnombre, img: '' };
        if (det.valor > 0) {
        detalle.img = "/static/img/icon-check.png";
        } else {
        detalle.img = "/static/img/icon-uncheck.png";
        }
        detalles.push(detalle);
        });

        data.idusuario = dat.idusuario;
        data.usuario = dat.usuario;
        data.ejecutivo = dat.ejecutivo;
        if (dat.resultado >= 50) {
        data.imgresultado = "/static/img/icon-green.png";
        }
        else if (dat.resultado >= 30 && dat.resultado < 50) {
        data.imgresultado = "/static/img/icon-yellow.png";
        }
        else if (dat.resultado > 0 && dat.resultado < 30) {
        data.imgresultado = "/static/img/icon-gray.png";
        }
        else {
        data.imgresultado = "/static/img/icon-red.png";
        }
        data.detalles = detalles;
        data.valresultado = dat.resultado;
        IngresoDiarios.push(data);

        });


        $scope.IngresosDiarios = IngresoDiarios;
        */

    }



} ]);