/* Services */
'use strict';

//definiendo el servicio para cambiar el título de la página
services.factory('setpagetitle', ['$window', function ($window) {
    return function (title) {
        $window.document.title = title;
    };
} ]);

//definiendo el servicio para inicializar un chart
services.factory('setchart', function () {
    return function (chart, name, categories, data, color) {
        chart.xAxis[0].setCategories(categories, false);
        chart.series[0].remove(false);
        chart.addSeries({
            name: name,
            data: data,
            color: color || 'white'
        }, false);
        chart.redraw();
    }
});

services.factory('addLegend', function () {
    return function (gmap, params) {
        gmap.addControl({
            position: 'RIGHT_BOTTOM',
            text: '',
            html: '<ul id="semaforo">' +
                  '<li>Leyenda</li>' +
                  '<li><img src="/static/img/green-legend.png"/>' + params.greentext + '</li>' +
                  '<li><img src="/static/img/red-legend.png"/>' + params.redtext + '</li>' +
                  '</ul>',
            style: {
                margin: '5px 5px 5px 5px',
                padding: '1px 6px',
                border: 'solid 1px #717B87',
                background: '#ffffff'
            }
        });
    }
});


services.factory('addTitle', function () {
    return function (gmap, params) {
        gmap.addControl({
            position: 'TOP_CENTER',
            text: '',
            html: '<spand>Vista '+params +'</spand>',
            style: {
                color : '#145896',
                margin: '5px 5px 5px 5px',
                padding: '1px 6px',
                border: 'solid 1px #717B87',
                background: '#ffffff'
            }
        });
    }
});


services.factory('googlemaps', function () {

    return function (container, lat, lng, zoom, maptype) {

        var basicmap = new google.maps.StyledMapType(gmaps.global.style.map.minimal, { name: "Básico" });
        //var terrainmap = new google.maps.StyledMapType(gmaps.global.style.map.minimal, { name: "Básico" });
        var maptypes = ['Básico', google.maps.MapTypeId.ROADMAP, google.maps.MapTypeId.TERRAIN, google.maps.MapTypeId.SATELLITE, google.maps.MapTypeId.HYBRID];

        container = new GMaps({
            div: container,
            lat: lat,
            lng: lng,
            zoom: zoom,
            enableNewStyle: true,
            scaleControl: false,
            disableDoubleClickZoom: true,
            mapTypeControlOptions: {
                mapTypeIds: maptypes
            }
        });

        container.map.mapTypes.set('Básico', basicmap);

        var gmtype = google.maps.MapTypeId[maptype];
        if (gmtype) container.map.setMapTypeId(gmtype);
        else container.map.setMapTypeId(maptype);

        //container.map.setMapTypeId(google.maps.MapTypeId.TERRAIN);
        container.map.setMapTypeId(google.maps.MapTypeId.ROADMAP);
        //mapTypeId: google.maps.MapTypeId.HYBRID

        container.addControl({
            position: 'BOTTOM_LEFT',
            text: '',
            html: '<div id="logo">' +
              '<img src="/static/img/water_seal_lucky.png" />' +
              '</div>',
            style: {
                width: '300px',
                background: 'transparent',
                boxShadow: 'none'
            }
        });

        return container;
    }
});

// for Dojo Toolkit integration
services.factory('parseProps', function () {
    return function (props, scope) {
        var result = {};
        if (props != undefined) {
            var propsArray = props.split(";");
            angular.forEach(propsArray, function (prop, index) {
                var propSplit = prop.split(":");
                if (scope.$parent[propSplit[1].trim()]) {
                    result[propSplit[0].trim()] = scope.$parent[propSplit[1].trim()];
                } else {
                    result[propSplit[0].trim()] = eval(propSplit[1].trim());
                }
            });
        }
        return result;
    };
});

services.factory('drawLayers', [function () {
    return function (fn, jsonlayer, type) {

        var draw = function (jsonobject, opts) {
            var poly = fn.drawPolygon(jsonobject);
            poly.properties = opts;
            poly.properties.TYPE = type;
            poly.setOptions(poly.properties);
            return poly;
        }

        var toGobject = function (fn, geoJSON) {
            var options = Object();
            options.geojson = geoJSON;
            options.geotype = 0;
            return fn.getFromGeoJSON(options);
        }

        var objects = [];
        jsonlayer = toGobject(fn, jsonlayer);

        angular.forEach(jsonlayer, function (jsonobject) {
            /* obtaining base json options for every object (polygon) displayed on map */
            var opts = getRandomOptions();
            /* condition for evaluating if object is a polygon or multipolygon */
            if (jsonobject.length == undefined) {
                objects.push(draw(jsonobject, opts));
            }
            else {
                /* is multipolygon */
                angular.forEach(jsonobject, function (object) {
                    objects.push(draw(object, opts));
                });
            }
        });

        return objects;
    }
}]);

services.factory('loadLayers', ['$resource', 'drawLayers', function ($resource, drawLayers) {
    return function (layers, fn) {
        var json = Object();
        var load = function (source) {
            var layer = $resource('/static/js/data/:file', { file: '@file' }, { get: { method: 'GET' } });
            layer.get(
                { file: source + '.json' },
                function (data) {
                    /* success */
                    console.log("3a");
                    console.log("name :" +data.name);
                    var loaded = Object();
                    angular.extend(loaded, data);
                    json[loaded.name] = drawLayers(fn, loaded, "1");
                    console.log("json");
                    console.log(json);
                    datos_global.data = json;
                },
                function (data) {
                    console.log("Falló al cargar json de ");
                }
            );
        }

        angular.forEach(layers, function (layer) {
            load(layer.source);
        });

        console.log("return");
        console.log(json);
        return json;
    };
}]);

services.factory('load', ['$resource', 'drawLayers', function ($resource, drawLayers) {
    return function (layer_arch) {
        var json = Object();
        var load = function (source) {            
            var layer = $resource('/static/js/data/:file', { file: '@file' }, { get: { method: 'GET' } });
            layer.get(
                { file: source + '.geojson' },
                function (data) {
                    var loaded = Object();
                    angular.extend(loaded, data);
                    json = loaded;
                    return json;
                },
                function (data) {
                    console.log("Falló al cargar json de ");
                }
            );
        }
        //load(layer_arch);
        return load(layer_arch);
    };
}]);
