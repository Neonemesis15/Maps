/*!
* GMaps.js
* http://hpneo.github.com/gmaps/
*
* Copyright 2012, Gustavo Leon
* Released under the MIT License.
*/

if (window.google && window.google.maps) {

    var GMaps = (function (global) {
        "use strict";

        var doc = document;
        var getElementById = function (id, context) {
            var ele
            if ('jQuery' in global && context) {
                ele = $("#" + id.replace('#', ''), context)[0]
            } else {
                ele = doc.getElementById(id.replace('#', ''));
            };
            return ele;
        };

        var GMaps = function (options) {
            var self = this;
            window.context_menu = {};

            if (typeof (options.div) == 'string') {
                this.div = getElementById(options.div, options.context);
            }
            else {
                this.div = options.div;
            };

            this.div.style.width = this.div.clientWidth || options.width;
            this.div.style.height = this.div.clientHeight || options.height;

            this.controls = [];
            this.overlays = [];
            this.layers = [];
            this.markers = [];
            this.polylines = [];
            this.routes = [];
            this.polygons = [];
            this.infoWindow = null;
            this.overlay_div = null;
            this.zoom = options.zoom || 15;

            //'Hybrid', 'Roadmap', 'Satellite' or 'Terrain'
            var mapType;

            if (options.mapType) {
                mapType = google.maps.MapTypeId[options.mapType.toUpperCase()];
            }
            else {
                mapType = google.maps.MapTypeId.ROADMAP;
            }

            var map_center = new google.maps.LatLng(options.lat, options.lng);

            delete options.div;
            delete options.lat;
            delete options.lng;
            delete options.mapType;
            delete options.width;
            delete options.height;

            var zoomControlOpt = options.zoomControlOpt || {
                style: 'DEFAULT',
                position: 'TOP_LEFT'
            };

            var zoomControl = options.zoomControl || true,
                zoomControlStyle = zoomControlOpt.style || 'DEFAULT',
                zoomControlPosition = zoomControlOpt.position || 'TOP_LEFT',
                panControl = options.panControl || true,
                mapTypeControl = options.mapTypeControl || true,
                scaleControl = options.scaleControl || true,
                streetViewControl = options.streetViewControl || true,
                overviewMapControl = overviewMapControl || true;

            var map_base_options = {
                zoom: this.zoom,
                center: map_center,
                mapTypeId: mapType,
                panControl: panControl,
                zoomControl: zoomControl,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle[zoomControlStyle], // DEFAULT LARGE SMALL
                    position: google.maps.ControlPosition[zoomControlPosition]
                },
                mapTypeControl: mapTypeControl,
                scaleControl: scaleControl,
                streetViewControl: streetViewControl,
                overviewMapControl: overviewMapControl
            };

            var map_options = extend_object(map_base_options, options);

            this.map = new google.maps.Map(this.div, map_options);

            // Context menus
            var buildContextMenuHTML = function (control, e) {
                var html = '';
                var options = window.context_menu[control];
                for (var i in options) {
                    if (options.hasOwnProperty(i)) {
                        var option = options[i];
                        html += '<li><a id="' + control + '_' + i + '" href="#">' + option.title + '</a></li>';
                    }
                }

                if (!getElementById('gmaps_context_menu')) return;

                var context_menu_element = getElementById('gmaps_context_menu');
                context_menu_element.innerHTML = html;

                var context_menu_items = context_menu_element.getElementsByTagName('a');

                var context_menu_items_count = context_menu_items.length;

                for (var i = 0; i < context_menu_items_count; i++) {
                    var context_menu_item = context_menu_items[i];

                    var assign_menu_item_action = function (ev) {
                        ev.preventDefault();

                        options[this.id.replace(control + '_', '')].action.call(self, e);
                        self.hideContextMenu();
                    };

                    google.maps.event.clearListeners(context_menu_item, 'click');
                    google.maps.event.addDomListenerOnce(context_menu_item, 'click', assign_menu_item_action, false);
                }

                var left = self.div.offsetLeft + e.pixel.x - 15;
                var top = self.div.offsetTop + e.pixel.y - 15;

                context_menu_element.style.left = left + "px";
                context_menu_element.style.top = top + "px";

                context_menu_element.style.display = 'block';
            };

            var buildContextMenu = function (control, e) {
                if (control === 'marker') {
                    e.pixel = {};
                    var overlay = new google.maps.OverlayView();
                    overlay.setMap(self.map);
                    overlay.draw = function () {
                        var projection = overlay.getProjection();
                        var position = e.marker.getPosition();
                        e.pixel = projection.fromLatLngToContainerPixel(position);

                        buildContextMenuHTML(control, e);
                    };
                }
                else {
                    buildContextMenuHTML(control, e);
                }
            };

            this.setContextMenu = function (options) {
                window.context_menu[options.control] = {};
                for (var i in options.options) {
                    if (options.options.hasOwnProperty(i)) {
                        var option = options.options[i];
                        window.context_menu[options.control][option.name] = {
                            title: option.title,
                            action: option.action
                        };
                    }
                }
                var ul = doc.createElement('ul');
                ul.id = 'gmaps_context_menu';
                ul.style.display = 'none';
                ul.style.position = 'absolute';
                ul.style.minWidth = '100px';
                ul.style.background = 'white';
                ul.style.listStyle = 'none';
                ul.style.padding = '8px';
                ul.style.boxShadow = '2px 2px 6px #ccc';

                doc.body.appendChild(ul);

                var context_menu_element = getElementById('gmaps_context_menu');

                google.maps.event.addDomListener(context_menu_element, 'mouseout', function (ev) {
                    if (!ev.relatedTarget || !this.contains(ev.relatedTarget)) {
                        window.setTimeout(function () {
                            context_menu_element.style.display = 'none';
                        }, 400);
                    }
                }, false);
            };

            this.hideContextMenu = function () {
                var context_menu_element = getElementById('gmaps_context_menu');
                if (context_menu_element)
                    context_menu_element.style.display = 'none';
            };

            //Events

            var events_that_hide_context_menu = ['bounds_changed', 'center_changed', 'click', 'dblclick', 'drag', 'dragend', 'dragstart', 'idle', 'maptypeid_changed', 'projection_changed', 'resize', 'tilesloaded', 'zoom_changed'];
            var events_that_doesnt_hide_context_menu = ['mousemove', 'mouseout', 'mouseover'];

            for (var ev = 0; ev < events_that_hide_context_menu.length; ev++) {
                (function (object, name) {
                    google.maps.event.addListener(object, name, function (e) {
                        if (options[name])
                            options[name].apply(this, [e]);

                        self.hideContextMenu();
                    });
                })(this.map, events_that_hide_context_menu[ev]);
            }

            for (var ev = 0; ev < events_that_doesnt_hide_context_menu.length; ev++) {
                (function (object, name) {
                    google.maps.event.addListener(object, name, function (e) {
                        if (options[name])
                            options[name].apply(this, [e]);
                    });
                })(this.map, events_that_doesnt_hide_context_menu[ev]);
            }

            google.maps.event.addListener(this.map, 'rightclick', function (e) {
                if (options.rightclick) {
                    options.rightclick.apply(this, [e]);
                }

                buildContextMenu('map', e);
            });

            this.refresh = function () {
                var center = this.map.getCenter();
                google.maps.event.trigger(this.map, 'resize');
                this.map.setCenter(center);
            };

            this.fitZoom = function (paths) {
                var latLngs = [];
                var markers_length = this.markers.length;

                for (var i = 0; i < markers_length; i++) {
                    latLngs.push(this.markers[i].getPosition());
                }

                this.fitBounds(latLngs);
            };

            this.fitBounds = function (latLngs) {
                var total = latLngs.length;
                var bounds = new google.maps.LatLngBounds();

                for (var i = 0; i < total; i++) {
                    bounds.extend(latLngs[i]);
                }

                this.map.fitBounds(bounds);
            };

            // Map methods
            this.setCenter = function (center, callback) {
                this.map.panTo(center);
                if (callback) {
                    callback();
                }
            };

            this.getCenter = function () {
                return this.map.getCenter();
            };

            // Change map type
            this.setMapType = function (type) {
                if (google.maps.MapTypeId[type.toUpperCase()])
                    this.map.setMapTypeId(google.maps.MapTypeId[type.toUpperCase()]);
                else
                    this.map.setMapTypeId(type.toUpperCase());
            };

            this.getDiv = function () {
                return this.div;
            };

            this.zoomIn = function (value) {
                this.map.setZoom(this.map.getZoom() + value);
            };

            this.zoomOut = function (value) {
                this.map.setZoom(this.map.getZoom() - value);
            };

            var native_methods = [];

            for (var method in this.map) {
                if (typeof (this.map[method]) == 'function' && !this[method]) {
                    native_methods.push(method);
                }
            }

            for (var i = 0; i < native_methods.length; i++) {
                (function (gmaps, scope, method_name) {
                    gmaps[method_name] = function () {
                        return scope[method_name].apply(scope, arguments);
                    };
                })(this, this.map, native_methods[i]);
            }

            this.createControl = function (options) {
                var control = doc.createElement('div');

                control.style.cursor = 'pointer';
                control.style.fontFamily = 'Arial, sans-serif';
                control.style.fontSize = '13px';
                control.style.boxShadow = 'rgba(0, 0, 0, 0.398438) 0px 2px 4px';

                for (var option in options.style) {
                    control.style[option] = options.style[option];
                }

                if (!options.html) {
                    control.textContent = options.text;
                }
                else {
                    control.innerHTML = options.html;
                }

                for (var ev in options.events) {
                    (function (object, name) {
                        google.maps.event.addDomListener(object, name, function () {
                            options.events[name].apply(this, [this]);
                        });
                    })(control, ev);
                }

                control.index = 1;

                return control;
            };

            this.addControl = function (options) {
                var position = google.maps.ControlPosition[options.position.toUpperCase()];

                delete options.position;

                var control = this.createControl(options);
                this.controls.push(control);
                this.map.controls[position].push(control);
                return control;
            };

            this.removeControl = function (options) {
                var position = google.maps.ControlPosition[options.position.toUpperCase()];
                try {
                    this.map.controls[position].removeAt(options.index);
                }
                catch (e) { }
            };

            //this.removeControls = function () {

            //    this.controls = [];

            //    var control = this.createControl(options);
            //    this.controls.push(control);
            //    this.map.controls[position].push(control);

            //    return control;
            //};
            // Markers
            this.createMarker = function (options) {
                if ((options.lat && options.lng) || options.position) {
                    var self = this;
                    var details = options.details;
                    var fences = options.fences;
                    var outside = options.outside;

                    var base_options = {
                        position: new google.maps.LatLng(options.lat, options.lng),
                        map: null
                    };

                    delete options.lat;
                    delete options.lng;
                    delete options.fences;
                    delete options.outside;

                    var marker_options = extend_object(base_options, options);

                    var marker = new google.maps.Marker(marker_options);

                    marker.fences = fences;

                    if (options.infoWindow) {
                        marker.infoWindow = new google.maps.InfoWindow(options.infoWindow);

                        var info_window_events = ['closeclick', 'content_changed', 'domready', 'position_changed', 'zindex_changed'];

                        for (var ev = 0; ev < info_window_events.length; ev++) {
                            (function (object, name) {
                                google.maps.event.addListener(object, name, function (e) {
                                    if (options.infoWindow[name])
                                        options.infoWindow[name].apply(this, [e]);
                                });
                            })(marker.infoWindow, info_window_events[ev]);
                        }
                    }

                    var marker_events = ['animation_changed', 'clickable_changed', 'cursor_changed', 'draggable_changed', 'flat_changed', 'icon_changed', 'position_changed', 'shadow_changed', 'shape_changed', 'title_changed', 'visible_changed', 'zindex_changed'];

                    var marker_events_with_mouse = ['dblclick', 'drag', 'dragend', 'dragstart', 'mousedown', 'mouseout', 'mouseover', 'mouseup'];

                    for (var ev = 0; ev < marker_events.length; ev++) {
                        (function (object, name) {
                            google.maps.event.addListener(object, name, function () {
                                if (options[name])
                                    options[name].apply(this, [this]);
                            });
                        })(marker, marker_events[ev]);
                    }

                    for (var ev = 0; ev < marker_events.length; ev++) {
                        (function (object, name) {
                            google.maps.event.addListener(object, name, function (me) {
                                if (!me.pixel) {
                                    me.pixel = this.map.getProjection().fromLatLngToPoint(me.latLng)
                                }
                                if (options[name])
                                    options[name].apply(this, [me]);
                            });
                        })(marker, marker_events_with_mouse[ev]);
                    }

                    google.maps.event.addListener(marker, 'click', function () {
                        this.details = details;

                        if (options.click) {
                            options.click.apply(this, [this]);
                        }

                        if (marker.infoWindow) {
                            self.hideInfoWindows();
                            marker.infoWindow.open(self.map, marker);
                        }
                    });

                    if (options.dragend || marker.fences) {
                        google.maps.event.addListener(marker, 'dragend', function () {
                            if (marker.fences) {
                                self.checkMarkerGeofence(marker, function (m, f) {
                                    outside(m, f);
                                });
                            }
                        });
                    }

                    return marker;
                }
                else {
                    throw 'No latitude or longitude defined';
                }
            };

            this.addMarker = function (options) {
                if ((options.lat && options.lng) || options.position) {
                    var marker = this.createMarker(options);
                    marker.setMap(this.map);
                    this.markers.push(marker);

                    return marker;
                }
                else {
                    throw 'No latitude or longitude defined';
                }
            };

            this.addMarkers = function (array) {
                for (var i = 0, marker; marker = array[i]; i++) {
                    this.addMarker(marker);
                }
                return this.markers;
            };

            this.hideInfoWindows = function () {
                for (var i = 0, marker; marker = this.markers[i]; i++) {
                    if (marker.infoWindow) {
                        marker.infoWindow.close();
                    }
                }
            };

            this.removeMarkers = function (collection) {
                var collection = (collection || this.markers);

                for (var i = 0; i < this.markers.length; i++) {
                    if (this.markers[i] === collection[i])
                        this.markers[i].setMap(null);
                }

                var new_markers = [];

                for (var i = 0; i < this.markers.length; i++) {
                    if (this.markers[i].getMap() != null)
                        new_markers.push(this.markers[i]);
                }

                this.markers = new_markers;
            };

            // Overlays
            this.drawOverlay = function (options) {
                var overlay = new google.maps.OverlayView();
                overlay.setMap(self.map);

                var auto_show = true;

                if (options.auto_show != null)
                    auto_show = options.auto_show;

                overlay.onAdd = function () {
                    var div = doc.createElement('div');
                    div.style.borderStyle = "none";
                    div.style.borderWidth = "0px";
                    div.style.position = "absolute";
                    div.style.zIndex = 100;
                    div.innerHTML = options.content;

                    overlay.div = div;

                    var panes = this.getPanes();
                    if (!options.layer) {
                        options.layer = 'overlayLayer';
                    }
                    var overlayLayer = panes[options.layer];
                    overlayLayer.appendChild(div);

                    var stop_overlay_events = ['contextmenu', 'DOMMouseScroll', 'dblclick', 'mousedown'];

                    for (var ev = 0; ev < stop_overlay_events.length; ev++) {
                        (function (object, name) {
                            google.maps.event.addDomListener(object, name, function (e) {
                                if (navigator.userAgent.toLowerCase().indexOf('msie') != -1 && document.all) {
                                    e.cancelBubble = true;
                                    e.returnValue = false;
                                }
                                else {
                                    e.stopPropagation();
                                }
                            });
                        })(div, stop_overlay_events[ev]);
                    }

                    google.maps.event.trigger(this, 'ready');
                };

                overlay.draw = function () {
                    var projection = this.getProjection();
                    var pixel = projection.fromLatLngToDivPixel(new google.maps.LatLng(options.lat, options.lng));

                    options.horizontalOffset = options.horizontalOffset || 0;
                    options.verticalOffset = options.verticalOffset || 0;

                    var div = overlay.div;
                    var content = div.children[0];

                    var content_height = content.clientHeight;
                    var content_width = content.clientWidth;

                    switch (options.verticalAlign) {
                        case 'top':
                            div.style.top = (pixel.y - content_height + options.verticalOffset) + 'px';
                            break;
                        default:
                        case 'middle':
                            div.style.top = (pixel.y - (content_height / 2) + options.verticalOffset) + 'px';
                            break;
                        case 'bottom':
                            div.style.top = (pixel.y + options.verticalOffset) + 'px';
                            break;
                    }

                    switch (options.horizontalAlign) {
                        case 'left':
                            div.style.left = (pixel.x - content_width + options.horizontalOffset) + 'px';
                            break;
                        default:
                        case 'center':
                            div.style.left = (pixel.x - (content_width / 2) + options.horizontalOffset) + 'px';
                            break;
                        case 'right':
                            div.style.left = (pixel.x + options.horizontalOffset) + 'px';
                            break;
                    }

                    div.style.display = auto_show ? 'block' : 'none';

                    if (!auto_show) {
                        options.show.apply(this, [div]);
                    }
                };

                overlay.onRemove = function () {
                    var div = overlay.div;

                    if (options.remove) {
                        options.remove.apply(this, [div]);
                    }
                    else {
                        overlay.div.parentNode.removeChild(overlay.div);
                        overlay.div = null;
                    }
                };

                self.overlays.push(overlay);
                return overlay;
            };

            this.removeOverlay = function (overlay) {
                overlay.setMap(null);
            };

            this.removeOverlays = function () {
                for (var i = 0, item; item = self.overlays[i]; i++) {
                    item.setMap(null);
                }
                self.overlays = [];
            };

            this.removePolylines = function () {
                for (var i = 0, item; item = self.polylines[i]; i++) {
                    item.setMap(null);
                }
                self.polylines = [];
            };

            this.drawPolyline = function (options) {
                var path = [];
                var points = options.path;

                if (points.length) {
                    if (points[0][0] === undefined) {
                        path = points;
                    }
                    else {
                        for (var i = 0, latlng; latlng = points[i]; i++) {
                            path.push(new google.maps.LatLng(latlng[0], latlng[1]));
                        }
                    }
                }

                var polyline = new google.maps.Polyline({
                    map: this.map,
                    path: path,
                    strokeColor: options.strokeColor,
                    strokeOpacity: options.strokeOpacity,
                    strokeWeight: options.strokeWeight
                });

                var polyline_events = ['click', 'dblclick', 'mousedown', 'mousemove', 'mouseout', 'mouseover', 'mouseup', 'rightclick'];

                for (var ev = 0; ev < polyline_events.length; ev++) {
                    (function (object, name) {
                        google.maps.event.addListener(object, name, function (e) {
                            if (options[name])
                                options[name].apply(this, [e]);
                        });
                    })(polyline, polyline_events[ev]);
                }

                this.polylines.push(polyline);

                return polyline;
            };

            this.drawCircle = function (options) {
                options = extend_object({
                    map: this.map,
                    center: new google.maps.LatLng(options.lat, options.lng)
                }, options);

                delete options.lat;
                delete options.lng;
                var polygon = new google.maps.Circle(options);

                var polygon_events = ['click', 'dblclick', 'mousedown', 'mousemove', 'mouseout', 'mouseover', 'mouseup', 'rightclick'];

                for (var ev = 0; ev < polygon_events.length; ev++) {
                    (function (object, name) {
                        google.maps.event.addListener(object, name, function (e) {
                            if (options[name])
                                options[name].apply(this, [e]);
                        });
                    })(polygon, polygon_events[ev]);
                }

                return polygon;
            };

            this.drawRectangle = function (options) {
                options = extend_object({
                    map: this.map
                }, options);

                var latLngBounds = new google.maps.LatLngBounds(
                    new google.maps.LatLng(options.bounds[0][0], options.bounds[0][1]),
                    new google.maps.LatLng(options.bounds[1][0], options.bounds[1][1])
                );

                options.bounds = latLngBounds;

                var polygon = new google.maps.Rectangle(options);

                var polygon_events = ['click', 'dblclick', 'mousedown', 'mousemove', 'mouseout', 'mouseover', 'mouseup', 'rightclick'];

                for (var ev = 0; ev < polygon_events.length; ev++) {
                    (function (object, name) {
                        google.maps.event.addListener(object, name, function (e) {
                            if (options[name])
                                options[name].apply(this, [e]);
                        });
                    })(polygon, polygon_events[ev]);
                }

                return polygon;
            };

            this.removePolygons = function () {
                for (var i = 0, item; item = self.polygons[i]; i++) {
                    item.setMap(null);
                }
                self.polygons = [];
            };

            this.drawPolygon = function (options) {
                options = extend_object({
                    map: this.map
                }, options);

                for (var name in options.properties) {
                    options[name] = options.properties[name];
                }

                var polygon = new google.maps.Polygon(options);

                var polygon_events = ['click', 'dblclick', 'mousedown', 'mousemove', 'mouseout', 'mouseover', 'mouseup', 'rightclick'];

                for (var ev = 0; ev < polygon_events.length; ev++) {
                    (function (object, name) {                        
                        google.maps.event.addListener(object, name, function (e) {                            
                            if (options[name])
                                options[name].apply(this, [e]);
                        });
                    })(polygon, polygon_events[ev]);
                }

                this.polygons.push(polygon);
                return polygon;
            };

            this.getFromFusionTables = function (options) {
                var events = options.events;

                delete options.events;

                var fusion_tables_options = options;

                var layer = new google.maps.FusionTablesLayer(fusion_tables_options);

                for (var ev in events) {
                    (function (object, name) {
                        google.maps.event.addListener(object, name, function (e) {
                            events[name].apply(this, [e]);
                        });
                    })(layer, ev);
                }

                this.layers.push(layer);

                return layer;
            };

            this.loadFromFusionTables = function (options) {
                var layer = this.getFromFusionTables(options);
                layer.setMap(this.map);

                return layer;
            };

            this.getFromKML = function (options) {
                var url = options.url;
                var events = options.events;

                delete options.url;
                delete options.events;

                var kml_options = options;

                var layer = new google.maps.KmlLayer(url, kml_options);

                for (var ev in events) {
                    (function (object, name) {
                        google.maps.event.addListener(object, name, function (e) {
                            events[name].apply(this, [e]);
                        });
                    })(layer, ev);
                }

                this.layers.push(layer);

                return layer;
            };

            this.loadFromKML = function (options) {
                var layer = this.getFromKML(options);
                layer.setMap(this.map);
                return layer;
            };

            this.getFromGeoJSON = function (options) {

                var data = options.geojson;

                var geojsonToGM = function (geojson) {

                    var gmObject;

                    switch (geojson.geometry.type) {
                        case "Point":
                            opts.position = new google.maps.LatLng(geojson.geometry.coordinates[1], geojson.geometry.coordinates[0]);
                            gmObject = new google.maps.Marker(opts);
                            if (geojson.properties) {
                                gmObject.setOptions(geojson.properties);
                            }
                            break;

                        case "MultiPoint":
                            gmObject = [];
                            for (var i = 0; i < geojson.geometry.coordinates.length; i++) {
                                opts.position = new google.maps.LatLng(geojson.geometry.coordinates[i][1], geojson.geometry.coordinates[i][0]);
                                gmObject.push(new google.maps.Marker(opts));
                            }
                            if (geojson.properties) {
                                for (var k = 0; k < gmObject.length; k++) {
                                    gmObject[k].setOptions(geojson.properties);
                                }
                            }
                            break;

                        case "LineString":
                            var path = [];
                            for (var i = 0; i < geojson.geometry.coordinates.length; i++) {
                                var coord = geojson.geometry.coordinates[i];
                                var ll = new google.maps.LatLng(coord[1], coord[0]);
                                path.push(ll);
                            }
                            opts.path = path;
                            gmObject = new google.maps.Polyline(opts);
                            if (geojson.properties) {
                                gmObject.setOptions(geojson.properties);
                            }
                            break;

                        case "MultiLineString":
                            gmObject = [];
                            for (var i = 0; i < geojson.geometry.coordinates.length; i++) {
                                var path = [];
                                for (var j = 0; j < geojson.geometry.coordinates[i].length; j++) {
                                    var coord = geojson.geometry.coordinates[i][j];
                                    var ll = new google.maps.LatLng(coord[1], coord[0]);
                                    path.push(ll);
                                }
                                opts.path = path;
                                gmObject.push(new google.maps.Polyline(opts));
                            }
                            if (geojson.properties) {
                                for (var k = 0; k < gmObject.length; k++) {
                                    gmObject[k].setOptions(geojson.properties);
                                }
                            }
                            break;

                        case "Polygon":
                            gmObject = {};
                            var paths = [];
                            var exteriorDirection;
                            var interiorDirection;
                            for (var i = 0; i < geojson.geometry.coordinates.length; i++) {
                                var path = [];
                                for (var j = 0; j < geojson.geometry.coordinates[i].length; j++) {
                                    var ll = new google.maps.LatLng(geojson.geometry.coordinates[i][j][1], geojson.geometry.coordinates[i][j][0]);
                                    path.push(ll);
                                }
                                if (!i) {
                                    exteriorDirection = _ccw(path);
                                    paths.push(path);
                                } else if (i == 1) {
                                    interiorDirection = _ccw(path);
                                    if (exteriorDirection == interiorDirection) {
                                        paths.push(path.reverse());
                                    } else {
                                        paths.push(path);
                                    }
                                } else {
                                    if (exteriorDirection == interiorDirection) {
                                        paths.push(path.reverse());
                                    } else {
                                        paths.push(path);
                                    }
                                }
                            }
                            gmObject.paths = paths;
                            gmObject.properties = geojson.properties;
                            break;

                        case "MultiPolygon":
                            gmObject = [];
                            for (var i = 0; i < geojson.geometry.coordinates.length; i++) {
                                var paths = [];
                                var exteriorDirection;
                                var interiorDirection;
                                for (var j = 0; j < geojson.geometry.coordinates[i].length; j++) {
                                    var path = [];
                                    for (var k = 0; k < geojson.geometry.coordinates[i][j].length; k++) {
                                        var ll = new google.maps.LatLng(geojson.geometry.coordinates[i][j][k][1], geojson.geometry.coordinates[i][j][k][0]);
                                        path.push(ll);
                                    }
                                    if (!j) {
                                        exteriorDirection = _ccw(path);
                                        paths.push(path);
                                    } else if (j == 1) {
                                        interiorDirection = _ccw(path);
                                        if (exteriorDirection == interiorDirection) {
                                            paths.push(path.reverse());
                                        } else {
                                            paths.push(path);
                                        }
                                    } else {
                                        if (exteriorDirection == interiorDirection) {
                                            paths.push(path.reverse());
                                        } else {
                                            paths.push(path);
                                        }
                                    }
                                }
                                gmObject.push({ paths: paths, properties: geojson.properties });
                            }
                            break;

                        case "GeometryCollection":
                            gmObject = [];
                            if (!geojson.geometry.geometries) {
                                gmObject = _error("Invalid GeoJSON object: GeometryCollection object missing \"geometries\" member.");
                            } else {
                                for (var i = 0; i < geojson.geometry.geometries.length; i++) {
                                    gmObject.push(geojsonToGM(geojson.geometry.geometries[i], opts, properties || null));
                                }
                            }
                            break;

                        default:
                            gmObject = _error("Invalid GeoJSON object: Geometry object must be one of \"Point\", \"LineString\", \"Polygon\" or \"MultiPolygon\".");
                    }

                    return gmObject;
                };


                var _error = function (message) {
                    return { type: "Error", message: message };
                };

                var _ccw = function (path) {
                    var isCCW;
                    var a = 0;
                    for (var i = 0; i < path.length - 2; i++) {
                        a += ((path[i + 1].lat() - path[i].lat()) * (path[i + 2].lng() - path[i].lng()) - (path[i + 2].lat() - path[i].lat()) * (path[i + 1].lng() - path[i].lng()));
                    }
                    if (a > 0) { isCCW = true; }
                    else { isCCW = false; }

                    return isCCW;
                };

                var obj;

                var opts = options.options || {};

                switch (data.type) {
                    //current used feature                                                      
                    case "FeatureCollection":
                        if (!data.features) {
                            obj = _error("Invalid GeoJSON object: FeatureCollection object missing \"features\" member.");
                        } else {
                            obj = [];
                            for (var i = 0; i < data.features.length; i++) {
                                var geom = geojsonToGM(data.features[i]);
                                obj.push(geom);
                            }
                        }
                        break;

                    case "GeometryCollection":
                        if (!data.geometries) {
                            obj = _error("Invalid GeoJSON object: GeometryCollection object missing \"geometries\" member.");
                        } else {
                            obj = [];
                            for (var i = 0; i < data.geometries.length; i++) {
                                obj.push(geojsonToGM(data.geometries[i], opts));
                            }
                        }
                        break;

                    case "Feature":
                        if (!(data.properties && data.geometry)) {
                            obj = _error("Invalid GeoJSON object: Feature object missing \"properties\" or \"geometry\" member.");
                        } else {
                            obj = geojsonToGM(data.geometry, opts, data.properties);
                        }
                        break;

                    case "Point": case "MultiPoint": case "LineString": case "MultiLineString": case "Polygon": case "MultiPolygon":
                        obj = data.coordinates ? obj = geojsonToGM(data, opts) : _error("Invalid GeoJSON object: Geometry object missing \"coordinates\" member.");
                        break;

                    default:
                        obj = _error("Invalid GeoJSON object: GeoJSON object must be one of \"Point\", \"LineString\", \"Polygon\", \"MultiPolygon\", \"Feature\", \"FeatureCollection\" or \"GeometryCollection\".");
                }
                return obj;
            };

            this.loadFromGeoJSON = function (options) {
                var geometries = this.getFromGeoJSON(options);

                if (options.geotype == 0) {
                    for (var i = 0; i < geometries.length; i++) {
                        if (geometries[i].length == undefined) {
                            this.drawPolygon(geometries[i]);
                            this.polygons.push(geometries[i]);
                        } else {
                            for (var j = 0; j < geometries[i].length; j++) {
                                this.drawPolygon(geometries[i][j]);
                                this.polygons.push(geometries[i][j]);
                            }
                        }
                    }
                }
                else {
                    for (var i = 0; i < geometries.length; i++) {
                        if (geometries[i].length == undefined) {
                            var geometry = {};
                            var poly = new google.maps.Polygon(geometries[i]);
                            geometry.path = poly.getPath().b;
                            geometry.strokeWeight = 1;
                            this.drawPolyline(geometry);
                        } else {
                            for (var j = 0; j < geometries[i].length; j++) {
                                var geometry = {};
                                var poly = new google.maps.Polygon(geometries[i][j]);
                                geometry.path = poly.getPath().b;
                                geometry.strokeWeight = 1;
                                this.drawPolyline(geometry);
                            }
                        }
                    }
                }
                return geometries;
            };

            // Services
            var travelMode, unitSystem;
            this.getRoutes = function (options) {
                switch (options.travelMode) {
                    case 'bicycling':
                        travelMode = google.maps.TravelMode.BICYCLING;
                        break;
                    case 'driving':
                        travelMode = google.maps.TravelMode.DRIVING;
                        break;
                    // case 'walking':                                                                                     
                    default:
                        travelMode = google.maps.TravelMode.WALKING;
                        break;
                }

                if (options.unitSystem === 'imperial') {
                    unitSystem = google.maps.UnitSystem.IMPERIAL;
                }
                else {
                    unitSystem = google.maps.UnitSystem.METRIC;
                }

                var base_options = {
                    avoidHighways: false,
                    avoidTolls: false,
                    optimizeWaypoints: false,
                    waypoints: []
                };

                var request_options = extend_object(base_options, options);

                request_options.origin = new google.maps.LatLng(options.origin[0], options.origin[1]);
                request_options.destination = new google.maps.LatLng(options.destination[0], options.destination[1]);
                request_options.travelMode = travelMode;
                request_options.unitSystem = unitSystem;

                delete request_options.callback;

                var self = this;
                var service = new google.maps.DirectionsService();

                service.route(request_options, function (result, status) {
                    if (status === google.maps.DirectionsStatus.OK) {
                        for (var r in result.routes) {
                            if (result.routes.hasOwnProperty(r)) {
                                self.routes.push(result.routes[r]);
                            }
                        }
                    }
                    if (options.callback) {
                        options.callback(self.routes);
                    }
                });
            };

            this.removeRoutes = function () {
                this.routes = [];
            };

            this.getElevations = function (options) {
                options = extend_object({
                    locations: [],
                    path: false,
                    samples: 256
                }, options);

                if (options.locations.length > 0) {
                    if (options.locations[0].length > 0) {
                        options.locations = array_map(options.locations, arrayToLatLng);
                    }
                }

                var callback = options.callback;
                delete options.callback;

                var service = new google.maps.ElevationService();

                //location request
                if (!options.path) {
                    delete options.path;
                    delete options.samples;
                    service.getElevationForLocations(options, function (result, status) {
                        if (callback && typeof (callback) === "function") {
                            callback(result, status);
                        }
                    });
                    //path request
                } else {
                    var pathRequest = {
                        path: options.locations,
                        samples: options.samples
                    };

                    service.getElevationAlongPath(pathRequest, function (result, status) {
                        if (callback && typeof (callback) === "function") {
                            callback(result, status);
                        }
                    });
                }
            };

            this.removePolylines = function () {
                var index;
                for (index in this.polylines) {
                    this.polylines[index].setMap(null);
                }
                this.polylines = [];
            };

            // Alias for the method "drawRoute"
            this.cleanRoute = this.removePolylines;

            this.drawRoute = function (options) {
                var self = this;
                this.getRoutes({
                    origin: options.origin,
                    destination: options.destination,
                    travelMode: options.travelMode,
                    waypoints: options.waypoints,
                    callback: function (e) {
                        if (e.length > 0) {
                            self.drawPolyline({
                                path: e[e.length - 1].overview_path,
                                strokeColor: options.strokeColor,
                                strokeOpacity: options.strokeOpacity,
                                strokeWeight: options.strokeWeight
                            });
                            if (options.callback) {
                                options.callback(e[e.length - 1]);
                            }
                        }
                    }
                });
            };

            this.travelRoute = function (options) {
                if (options.origin && options.destination) {
                    this.getRoutes({
                        origin: options.origin,
                        destination: options.destination,
                        travelMode: options.travelMode,
                        waypoints: options.waypoints,
                        callback: function (e) {
                            if (e.length > 0 && options.step) {
                                var route = e[e.length - 1];
                                if (route.legs.length > 0) {
                                    var steps = route.legs[0].steps;
                                    for (var i = 0, step; step = steps[i]; i++) {
                                        step.step_number = i;
                                        options.step(step);
                                    }
                                }
                            }
                        }
                    });
                }
                else if (options.route) {
                    if (options.route.legs.length > 0) {
                        var steps = options.route.legs[0].steps;
                        for (var i = 0, step; step = steps[i]; i++) {
                            step.step_number = i;
                            options.step(step);
                        }
                    }
                }
            };

            this.drawSteppedRoute = function (options) {
                if (options.origin && options.destination) {
                    this.getRoutes({
                        origin: options.origin,
                        destination: options.destination,
                        travelMode: options.travelMode,
                        callback: function (e) {
                            if (e.length > 0 && options.step) {
                                var route = e[e.length - 1];
                                if (route.legs.length > 0) {
                                    var steps = route.legs[0].steps;
                                    for (var i = 0, step; step = steps[i]; i++) {
                                        step.step_number = i;
                                        self.drawPolyline({
                                            path: step.path,
                                            strokeColor: options.strokeColor,
                                            strokeOpacity: options.strokeOpacity,
                                            strokeWeight: options.strokeWeight
                                        });
                                        options.step(step);
                                    }
                                }
                            }
                        }
                    });
                }
                else if (options.route) {
                    if (options.route.legs.length > 0) {
                        var steps = options.route.legs[0].steps;
                        for (var i = 0, step; step = steps[i]; i++) {
                            step.step_number = i;
                            self.drawPolyline({
                                path: step.path,
                                strokeColor: options.strokeColor,
                                strokeOpacity: options.strokeOpacity,
                                strokeWeight: options.strokeWeight
                            });
                            options.step(step);
                        }
                    }
                }
            };

            // Geofence
            this.checkGeofence = function (lat, lng, fence) {
                return fence.containsLatLng(new google.maps.LatLng(lat, lng));
            };

            this.checkMarkerGeofence = function (marker, outside_callback) {
                if (marker.fences) {
                    for (var i = 0, fence; fence = marker.fences[i]; i++) {
                        var pos = marker.getPosition();
                        if (!self.checkGeofence(pos.lat(), pos.lng(), fence)) {
                            outside_callback(marker, fence);
                        }
                    }
                }
            };
        };

        /////////////////////
        // Section : Route //
        /////////////////////
        GMaps.Route = function (options) {
            this.map = options.map;
            this.route = options.route;
            this.step_count = 0;
            this.steps = this.route.legs[0].steps;
            this.steps_length = this.steps.length;

            this.polyline = this.map.drawPolyline({
                path: new google.maps.MVCArray(),
                strokeColor: options.strokeColor,
                strokeOpacity: options.strokeOpacity,
                strokeWeight: options.strokeWeight
            }).getPath();

            this.back = function () {
                if (this.step_count > 0) {
                    this.step_count--;
                    var path = this.route.legs[0].steps[this.step_count].path;
                    for (var p in path) {
                        if (path.hasOwnProperty(p)) {
                            this.polyline.pop();
                        }
                    }
                }
            };

            this.forward = function () {
                if (this.step_count < this.steps_length) {
                    var path = this.route.legs[0].steps[this.step_count].path;
                    for (var p in path) {
                        if (path.hasOwnProperty(p)) {
                            this.polyline.push(path[p]);
                        }
                    }
                    this.step_count++;
                }
            };
        };

        //////////////////////////////////////////////////
        // Section : Geolocation (Modern browsers only) //
        //////////////////////////////////////////////////
        GMaps.geolocate = function (options) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    options.success(position);
                    if (options.always) {
                        options.always();
                    }
                }, function (error) {
                    options.error(error);
                    if (options.always) {
                        options.always();
                    }
                }, options.options);
            }
            else {
                options.not_supported();
                if (options.always) {
                    options.always();
                }
            }
        };

        /////////////////////////
        // Section : Geocoding //
        /////////////////////////
        GMaps.geocode = function (options) {
            this.geocoder = new google.maps.Geocoder();
            var callback = options.callback;
            if (options.lat && options.lng) {
                options.latLng = new google.maps.LatLng(options.lat, options.lng);
            }

            delete options.lat;
            delete options.lng;
            delete options.callback;
            this.geocoder.geocode(options, function (results, status) {
                callback(results, status);
            });
        };

        ///////////////////////////
        // Section : Static maps //
        ///////////////////////////
        GMaps.staticMapURL = function (options) {
            var parameters = [];
            var data;

            var static_root = 'http://maps.googleapis.com/maps/api/staticmap';
            if (options.url) {
                static_root = options.url;
                delete options.url;
            }
            static_root += '?';

            var markers = options.markers;
            delete options.markers;
            if (!markers && options.marker) {
                markers = [options.marker];
                delete options.marker;
            }

            var polyline = options.polyline;
            delete options.polyline;

            /** Map options **/
            if (options.center) {
                parameters.push('center=' + options.center);
                delete options.center;
            }
            else if (options.address) {
                parameters.push('center=' + options.address);
                delete options.address;
            }
            else if (options.lat) {
                parameters.push(['center=', options.lat, ',', options.lng].join(''));
                delete options.lat;
                delete options.lng;
            }
            else if (options.visible) {
                var visible = encodeURI(options.visible.join('|'));
                parameters.push('visible=' + visible);
            }

            var size = options.size;
            if (size) {
                if (size.join) {
                    size = size.join('x');
                }
                delete options.size;
            }
            else {
                size = '630x300';
            }
            parameters.push('size=' + size);

            if (!options.zoom) {
                options.zoom = 15;
            }

            var sensor = options.hasOwnProperty('sensor') ? !!options.sensor : true;
            delete options.sensor;
            parameters.push('sensor=' + sensor);

            for (var param in options) {
                if (options.hasOwnProperty(param)) {
                    parameters.push(param + '=' + options[param]);
                }
            }

            /** Markers **/
            if (markers) {
                var marker, loc;

                for (var i = 0; data = markers[i]; i++) {
                    marker = [];

                    if (data.size && data.size !== 'normal') {
                        marker.push('size:' + data.size);
                    }
                    else if (data.icon) {
                        marker.push('icon:' + encodeURI(data.icon));
                    }

                    if (data.color) {
                        marker.push('color:' + data.color.replace('#', '0x'));
                    }

                    if (data.label) {
                        marker.push('label:' + data.label[0].toUpperCase());
                    }

                    loc = (data.address ? data.address : data.lat + ',' + data.lng);

                    if (marker.length || i === 0) {
                        marker.push(loc);
                        marker = marker.join('|');
                        parameters.push('markers=' + encodeURI(marker));
                    }
                    // New marker without styles
                    else {
                        marker = parameters.pop() + encodeURI('|' + loc);
                        parameters.push(marker);
                    }
                }
            }

            /** Polylines **/
            function parseColor(color, opacity) {
                if (color[0] === '#') {
                    color = color.replace('#', '0x');

                    if (opacity) {
                        opacity = parseFloat(opacity);
                        opacity = Math.min(1, Math.max(opacity, 0));
                        if (opacity === 0) {
                            return '0x00000000';
                        }
                        opacity = (opacity * 255).toString(16);
                        if (opacity.length === 1) {
                            opacity += opacity;
                        }

                        color = color.slice(0, 8) + opacity;
                    }
                }
                return color;
            }

            if (polyline) {
                data = polyline;
                polyline = [];

                if (data.strokeWeight) {
                    polyline.push('weight:' + parseInt(data.strokeWeight, 10));
                }

                if (data.strokeColor) {
                    var color = parseColor(data.strokeColor, data.strokeOpacity);
                    polyline.push('color:' + color);
                }

                if (data.fillColor) {
                    var fillcolor = parseColor(data.fillColor, data.fillOpacity);
                    polyline.push('fillcolor:' + fillcolor);
                }

                var path = data.path;
                if (path.join) {
                    for (var j = 0, pos; pos = path[j]; j++) {
                        polyline.push(pos.join(','));
                    }
                }
                else {
                    polyline.push('enc:' + path);
                }

                polyline = polyline.join('|');
                parameters.push('path=' + encodeURI(polyline));
            }

            parameters = parameters.join('&');
            return static_root + parameters;
        };

        //code not implemented yet


        //==========================
        // Polygon containsLatLng
        // https://github.com/tparkin/Google-Maps-Point-in-Polygon
        // Polygon getBounds extension - google-maps-extensions
        // http://code.google.com/p/google-maps-extensions/source/browse/google.maps.Polygon.getBounds.js
        if (!google.maps.Polygon.prototype.getBounds) {
            google.maps.Polygon.prototype.getBounds = function (latLng) {
                var bounds = new google.maps.LatLngBounds();
                var paths = this.getPaths();
                var path;

                for (var p = 0; p < paths.getLength(); p++) {
                    path = paths.getAt(p);
                    for (var i = 0; i < path.getLength(); i++) {
                        bounds.extend(path.getAt(i));
                    }
                }

                return bounds;
            };
        }

        // Polygon containsLatLng - method to determine if a latLng is within a polygon
        google.maps.Polygon.prototype.containsLatLng = function (latLng) {
            // Exclude points outside of bounds as there is no way they are in the poly
            var bounds = this.getBounds();

            if (bounds !== null && !bounds.contains(latLng)) {
                return false;
            }

            // Raycast point in polygon method
            var inPoly = false;

            var numPaths = this.getPaths().getLength();
            for (var p = 0; p < numPaths; p++) {
                var path = this.getPaths().getAt(p);
                var numPoints = path.getLength();
                var j = numPoints - 1;

                for (var i = 0; i < numPoints; i++) {
                    var vertex1 = path.getAt(i);
                    var vertex2 = path.getAt(j);

                    if (vertex1.lng() < latLng.lng() && vertex2.lng() >= latLng.lng() || vertex2.lng() < latLng.lng() && vertex1.lng() >= latLng.lng()) {
                        if (vertex1.lat() + (latLng.lng() - vertex1.lng()) / (vertex2.lng() - vertex1.lng()) * (vertex2.lat() - vertex1.lat()) < latLng.lat()) {
                            inPoly = !inPoly;
                        }
                    }

                    j = i;
                }
            }

            return inPoly;
        };

        google.maps.LatLngBounds.prototype.containsLatLng = function (latLng) {
            return this.contains(latLng);
        };

        google.maps.Marker.prototype.setFences = function (fences) {
            this.fences = fences;
        };

        google.maps.Marker.prototype.addFence = function (fence) {
            this.fences.push(fence);
        };



        /*********************************************************************\
        *                                                                     *
        * epolys.js                                          by Mike Williams *
        * updated to API v3                                  by Larry Ross    *
        *                                                                     *
        * A Google Maps API Extension                                         *
        *                                                                     *
        * Adds various Methods to google.maps.Polygon and google.maps.Polyline *
        *                                                                     *
        * .Contains(latlng) returns true is the poly contains the specified   *
        *                   GLatLng                                           *
        *                                                                     *
        * .Area()           returns the approximate area of a poly that is    *
        *                   not self-intersecting                             *
        *                                                                     *
        * .Distance()       returns the length of the poly path               *
        *                                                                     *
        * .Bounds()         returns a GLatLngBounds that bounds the poly      *
        *                                                                     *
        * .GetPointAtDistance() returns a GLatLng at the specified distance   *
        *                   along the path.                                   *
        *                   The distance is specified in metres               *
        *                   Reurns null if the path is shorter than that      *
        *                                                                     *
        * .GetPointsAtDistance() returns an array of GLatLngs at the          *
        *                   specified interval along the path.                *
        *                   The distance is specified in metres               *
        *                                                                     *
        * .GetIndexAtDistance() returns the vertex number at the specified    *
        *                   distance along the path.                          *
        *                   The distance is specified in metres               *
        *                   Returns null if the path is shorter than that      *
        *                                                                     *
        * .Bearing(v1?,v2?) returns the bearing between two vertices          *
        *                   if v1 is null, returns bearing from first to last *
        *                   if v2 is null, returns bearing from v1 to next    *
        *                                                                     *
        *                                                                     *
        ***********************************************************************
        *                                                                     *
        *   This Javascript is provided by Mike Williams                      *
        *   Blackpool Community Church Javascript Team                        *
        *   http://www.blackpoolchurch.org/                                   *
        *   http://econym.org.uk/gmap/                                        *
        *                                                                     *
        *   This work is licenced under a Creative Commons Licence            *
        *   http://creativecommons.org/licenses/by/2.0/uk/                    *
        *                                                                     *
        ***********************************************************************
        *                                                                     *
        * Version 1.1       6-Jun-2007                                        *
        * Version 1.2       1-Jul-2007 - fix: Bounds was omitting vertex zero *
        *                                add: Bearing                         *
        * Version 1.3       28-Nov-2008  add: GetPointsAtDistance()           *
        * Version 1.4       12-Jan-2009  fix: GetPointsAtDistance()           *
        * Version 3.0       11-Aug-2010  update to v3                         *
        *                                                                     *
        \*********************************************************************/

        // === first support methods that don't (yet) exist in v3
        google.maps.LatLng.prototype.distanceFrom = function (newLatLng) {
            var EarthRadiusMeters = 6378137.0; // meters
            var lat1 = this.lat();
            var lon1 = this.lng();
            var lat2 = newLatLng.lat();
            var lon2 = newLatLng.lng();
            var dLat = (lat2 - lat1) * Math.PI / 180;
            var dLon = (lon2 - lon1) * Math.PI / 180;
            var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var d = EarthRadiusMeters * c;
            return d;
        }

        google.maps.LatLng.prototype.latRadians = function () {
            return this.lat() * Math.PI / 180;
        }

        google.maps.LatLng.prototype.lngRadians = function () {
            return this.lng() * Math.PI / 180;
        }

        // === A method for testing if a point is inside a polygon
        // === Returns true if poly contains point
        // === Algorithm shamelessly stolen from http://alienryderflex.com/polygon/ 
        google.maps.Polygon.prototype.Contains = function (point) {
            var j = 0;
            var oddNodes = false;
            var x = point.lng();
            var y = point.lat();
            for (var i = 0; i < this.getPath().getLength(); i++) {
                j++;
                if (j == this.getPath().getLength()) { j = 0; }
                if (((this.getPath().getAt(i).lat() < y) && (this.getPath().getAt(j).lat() >= y))
    || ((this.getPath().getAt(j).lat() < y) && (this.getPath().getAt(i).lat() >= y))) {
                    if (this.getPath().getAt(i).lng() + (y - this.getPath().getAt(i).lat())
      / (this.getPath().getAt(j).lat() - this.getPath().getAt(i).lat())
      * (this.getPath().getAt(j).lng() - this.getPath().getAt(i).lng()) < x) {
                        oddNodes = !oddNodes
                    }
                }
            }
            return oddNodes;
        }

        // === A method which returns the approximate area of a non-intersecting polygon in square metres ===
        // === It doesn't fully account for spherical geometry, so will be inaccurate for large polygons ===
        // === The polygon must not intersect itself ===
        google.maps.Polygon.prototype.Area = function () {
            var a = 0;
            var j = 0;
            var b = this.Bounds();
            var x0 = b.getSouthWest().lng();
            var y0 = b.getSouthWest().lat();
            for (var i = 0; i < this.getPath().getLength(); i++) {
                j++;
                if (j == this.getPath().getLength()) { j = 0; }
                var x1 = this.getPath().getAt(i).distanceFrom(new google.maps.LatLng(this.getPath().getAt(i).lat(), x0));
                var x2 = this.getPath().getAt(j).distanceFrom(new google.maps.LatLng(this.getPath().getAt(j).lat(), x0));
                var y1 = this.getPath().getAt(i).distanceFrom(new google.maps.LatLng(y0, this.getPath().getAt(i).lng()));
                var y2 = this.getPath().getAt(j).distanceFrom(new google.maps.LatLng(y0, this.getPath().getAt(j).lng()));
                a += x1 * y2 - x2 * y1;
            }
            return Math.abs(a * 0.5);
        }

        // === A method which returns the length of a path in metres ===
        google.maps.Polygon.prototype.Distance = function () {
            var dist = 0;
            for (var i = 1; i < this.getPath().getLength(); i++) {
                dist += this.getPath().getAt(i).distanceFrom(this.getPath().getAt(i - 1));
            }
            return dist;
        }

        // === A method which returns the bounds as a GLatLngBounds ===
        google.maps.Polygon.prototype.Bounds = function () {
            var bounds = new google.maps.LatLngBounds();
            for (var i = 0; i < this.getPath().getLength(); i++) {
                bounds.extend(this.getPath().getAt(i));
            }
            return bounds;
        }

        // === A method which returns a GLatLng of a point a given distance along the path ===
        // === Returns null if the path is shorter than the specified distance ===
        google.maps.Polygon.prototype.GetPointAtDistance = function (metres) {
            // some awkward special cases
            if (metres == 0) return this.getPath().getAt(0);
            if (metres < 0) return null;
            if (this.getPath().getLength() < 2) return null;
            var dist = 0;
            var olddist = 0;
            for (var i = 1; (i < this.getPath().getLength() && dist < metres); i++) {
                olddist = dist;
                dist += this.getPath().getAt(i).distanceFrom(this.getPath().getAt(i - 1));
            }
            if (dist < metres) {
                return null;
            }
            var p1 = this.getPath().getAt(i - 2);
            var p2 = this.getPath().getAt(i - 1);
            var m = (metres - olddist) / (dist - olddist);
            return new google.maps.LatLng(p1.lat() + (p2.lat() - p1.lat()) * m, p1.lng() + (p2.lng() - p1.lng()) * m);
        }

        // === A method which returns an array of GLatLngs of points a given interval along the path ===
        google.maps.Polygon.prototype.GetPointsAtDistance = function (metres) {
            var next = metres;
            var points = [];
            // some awkward special cases
            if (metres <= 0) return points;
            var dist = 0;
            var olddist = 0;
            for (var i = 1; (i < this.getPath().getLength()); i++) {
                olddist = dist;
                dist += this.getPath().getAt(i).distanceFrom(this.getPath().getAt(i - 1));
                while (dist > next) {
                    var p1 = this.getPath().getAt(i - 1);
                    var p2 = this.getPath().getAt(i);
                    var m = (next - olddist) / (dist - olddist);
                    points.push(new google.maps.LatLng(p1.lat() + (p2.lat() - p1.lat()) * m, p1.lng() + (p2.lng() - p1.lng()) * m));
                    next += metres;
                }
            }
            return points;
        }

        // === A method which returns the Vertex number at a given distance along the path ===
        // === Returns null if the path is shorter than the specified distance ===
        google.maps.Polygon.prototype.GetIndexAtDistance = function (metres) {
            // some awkward special cases
            if (metres == 0) return this.getPath().getAt(0);
            if (metres < 0) return null;
            var dist = 0;
            var olddist = 0;
            for (var i = 1; (i < this.getPath().getLength() && dist < metres); i++) {
                olddist = dist;
                dist += this.getPath().getAt(i).distanceFrom(this.getPath().getAt(i - 1));
            }
            if (dist < metres) { return null; }
            return i;
        }

        // === A function which returns the bearing between two vertices in decgrees from 0 to 360===
        // === If v1 is null, it returns the bearing between the first and last vertex ===
        // === If v1 is present but v2 is null, returns the bearing from v1 to the next vertex ===
        // === If either vertex is out of range, returns void ===
        google.maps.Polygon.prototype.Bearing = function (v1, v2) {
            if (v1 == null) {
                v1 = 0;
                v2 = this.getPath().getLength() - 1;
            } else if (v2 == null) {
                v2 = v1 + 1;
            }
            if ((v1 < 0) || (v1 >= this.getPath().getLength()) || (v2 < 0) || (v2 >= this.getPath().getLength())) {
                return;
            }
            var from = this.getPath().getAt(v1);
            var to = this.getPath().getAt(v2);
            if (from.equals(to)) {
                return 0;
            }
            var lat1 = from.latRadians();
            var lon1 = from.lngRadians();
            var lat2 = to.latRadians();
            var lon2 = to.lngRadians();
            var angle = -Math.atan2(Math.sin(lon1 - lon2) * Math.cos(lat2), Math.cos(lat1) * Math.sin(lat2) - Math.sin(lat1) * Math.cos(lat2) * Math.cos(lon1 - lon2));
            if (angle < 0.0) angle += Math.PI * 2.0;
            angle = angle * 180.0 / Math.PI;
            return parseFloat(angle.toFixed(1));
        }

        // === Copy all the above functions to Polyline ===
        google.maps.Polyline.prototype.Contains = google.maps.Polygon.prototype.Contains;
        google.maps.Polyline.prototype.Area = google.maps.Polygon.prototype.Area;
        google.maps.Polyline.prototype.Distance = google.maps.Polygon.prototype.Distance;
        google.maps.Polyline.prototype.Bounds = google.maps.Polygon.prototype.Bounds;
        google.maps.Polyline.prototype.GetPointAtDistance = google.maps.Polygon.prototype.GetPointAtDistance;
        google.maps.Polyline.prototype.GetPointsAtDistance = google.maps.Polygon.prototype.GetPointsAtDistance;
        google.maps.Polyline.prototype.GetIndexAtDistance = google.maps.Polygon.prototype.GetIndexAtDistance;
        google.maps.Polyline.prototype.Bearing = google.maps.Polygon.prototype.Bearing;

        return GMaps;
    } (this));

    var arrayToLatLng = function (coords) {
        return new google.maps.LatLng(coords[0], coords[1]);
    };

    var extend_object = function (obj, new_obj) {
        if (obj === new_obj) return obj;

        for (var name in new_obj) {
            obj[name] = new_obj[name];
        }

        return obj;
    };

    var array_map = function (array, callback) {
        if (Array.prototype.map && array.map === Array.prototype.map) {
            return array.map(callback);
        } else {
            var array_return = [];

            var array_length = array.length;

            for (var i = 0; i < array_length; i++) {
                array_return.push(callback(array[i]));
            }
            return array_return;
        }
    }

    function getRandomColor() {
        return '#' + Math.floor(Math.random() * 16777215).toString(16);
    }

    function getColourGradient(colour, n) {

        var redHex = colour.substring(0, 2);
        var greenHex = colour.substring(2, 4);
        var blueHex = colour.substring(4, 6);
        var colourGradient = new Array(n);

        for (i = 0; i < n; i++) {
            // get the yellow colour
            var aux = i + 1;
            greenHex = aux.toString(16);
            if (greenHex.length < 2) {
                greenHex = "0" + greenHex;
            }
            // add the new colour to the gradient
            colourGradient[i] = "#" + redHex + greenHex + blueHex;
        }
        return colourGradient;
    }

    function restoreDefaultColor(geometry) {
        if (geometry)
            geometry.setOptions(geometry.properties);
    }

    function getRandomOptions() {
        return {
            fillColor: getRandomColor(),
            fillOpacity: 0.6,
            strokeColor: "#000000",
            strokeOpacity: 0.9,
            strokeWeight: 1
        };
    }
}