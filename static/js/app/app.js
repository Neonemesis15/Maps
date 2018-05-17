/* App module for XploraGIS */
var app = angular.module("xploragis", ["ngResource", "xploragis.filters", "xploragis.services", "xploragis.directives", "xploragis.controllers", "ui"]);
    //.config(['$routeProvider', '$locationProvider', function ($route, $location) {
        //        $route.
        //            when('/Reports/Mayorista', { templateUrl: '/Reports/Mayorista', controller: "mapa" }).
        //            when('/Reports/Province', { templateUrl: '/Reports/Province', controller: "mapa" }).
        //            when('/Reports/Colgate', { templateUrl: '/Reports/Colgate', controller: "mapa" }).
        //            when('/Reports/SeguimientoGPS', { templateUrl: '/Reports/SeguimientoGPS', controller: "mapa" }).
        //            when('/Reports/Principal', { templateUrl: '/Reports/Principal', controller: "mapa" })//.
        //            when('/', { templateUrl: '/Home/Index', controller: "mapa" })
        //            .otherwise({ redirectTo: '/' });

        // configure html5 to get links working on jsfiddle
        //$location.html5Mode(true);
        //$location.hashPrefix('!');
    //}]);

/* Defining modules */
var filters = angular.module("xploragis.filters", []);
var services = angular.module("xploragis.services", []);
var directives = angular.module("xploragis.directives", []);
var controllers = angular.module("xploragis.controllers", []);

app.run(['$rootScope', function (root) {
    root.exist = function (object) {
        if (object != undefined)
            if (object != null)
                if (object.length != 0)
                    return true;
                else return false;
            else return false;
        else return false;
    }
    root.validcoord = function (coord) {
        if (coord != undefined)
            if (coord != null)
                if (coord != "")
                    if (coord != "0.0")
                        return true;
                    else return false;
                else return false;
            else return false;
        else return false;
    }
    root.Canal = "2008"; //2008(Bodegas) - 1000(Mayor)
    root.Servicio = "254";
    root.Cliente = "1561";
    root.Equipo = "012011092692011";
    root.Pais = "589";
    root.Reporte = 58;
    root.SubCategoria = "0";
    root.Marca = "0";
    root.Periodo = "973";
    root.PhotoPath = "http://sige.lucky.com.pe/Pages/Modulos/Operativo/Reports/FotoAndroid/";
    root.NonePhoto = "/static/img/no-disponible.png";
    root.Sectores = ['39', '36', '37', '35'];
    root.Lima = ['15', '07'];
} ]);

if (!String.prototype.trim) {
    String.prototype.trim = function () {
        return this.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
    };
}

if (!Array.prototype.contains) {
    Array.prototype.contains = function (obj) {
        var i = this.length;
        while (i--) {
            if (this[i] === obj) {
                return true;
            }
        }
        return false;
    };
}

var globals = {
    codEquipo: "012011092692011",
    codPais: "589",
    codReporte: 58,
    codServicio: '254',
    codCanal: '2008',
    codCliente: '1561',
    codSubCategoria: "0",
    codMarca: "0",
    reportsPlanning: "973",
    rutaFoto: "http://sige.lucky.com.pe/Pages/Modulos/Operativo/Reports/FotoAndroid/"
}

var datos_global = {
    data : ""
}

var gmaps = {
    global: {
        peru: {
            center: {
                lat: -10.012130,
                lng: -75.548584
            }
        },
        lima: {
            center: {
                lat: -12.047379,
                lng: -77.00592
            }
        },
        style: {
            map: {
                minimal: [
                    { stylers: [{ saturation: -20}] },
                    {
                        featureType: "road",
                        stylers: [{ visibility: "off"}]
                    },
                    {
                        featureType: "administrative",
                        stylers: [{ visibility: "off"}]
                    },
                    {
                        featureType: "transit.station",
                        stylers: [{ visibility: "off"}]
                    },
                    {
                        featureType: "landscape",
                        stylers: [{ visibility: "off"}]
                    }
                ]
            },
            geometry: {
                initial: {
                    fillColor: "transparent",
                    fillOpacity: 0.9,
                    strokeColor: "#fcd209",
                    strokeOpacity: 0.9,
                    strokeWeight: 2
                },
                selected: {
                    fillColor: "#e02121",
                    fillOpacity: 0.6,
                    strokeColor: "#e02121",
                    strokeOpacity: 0.9,
                    strokeWeight: 2
                }
            },
            polyline: {
                initial: {
                    strokeColor: "#404949",
                    strokeOpacity: 1.0,
                    strokeWeight: 1
                }
            },
            district: {
                initial: {
                    fillColor: "transparent",
                    fillOpacity: 0.9,
                    strokeColor: "#fcd209",
                    strokeOpacity: 0.9,
                    strokeWeight: 2
                },
                selected: {
                    fillColor: "#0066FF",
                    fillOpacity: 0.6,
                    strokeColor: "#0066FF",
                    strokeOpacity: 0.9,
                    strokeWeight: 2
                }
            }
        },
        icons: {
            green: getIcon("map_green_24_24.png"),
            red: getIcon("map_pin_24_24.png"),
            yellow: getIcon("map_yellow_24_24.png"),
            gie: getIcon("male-green-7.png"),
            pos: getIcon("pos/green/pos_market.png"),
            numeric: function (c, n) {
                return new google.maps.MarkerImage("/static/img/iconmarkers/" + c + "/icon_" + c + "_" + n + ".png");
            }
        },
        minicons: {
            green: getIcon("green.png"),
            red: getIcon("red.png"),
            yellow: getIcon("yellow.png")
        }
    }
}

function getIcon(image) {
    return new google.maps.MarkerImage("/static/img/iconmarkers/" + image);
}

Highcharts.setOptions({
    lang: {
        exportExcel: "Exportar Excel"
    }
});