controllers.controller('mapa', ['$scope', 'googlemaps', function ($scope, googlemaps) {
    require([
                "dojo/parser",
                "dojo/dom-style",
                "dojo/dom",
                "dijit/registry",
                "dojox/widget/Standby",
                "dijit/layout/BorderContainer",
                "dijit/layout/ContentPane",                
                "dojo/domReady!"
            ],
            function (parser, domStyle, dom, registry, Standby) {
                container = googlemaps('#map', gmaps.global.peru.center.lat, gmaps.global.peru.center.lng, 5, "ROADMAP");
                parser.parse();
                container.refresh();
                domStyle.set("preloader", "display", "none");

                dojo.connect(registry.byId("map"), "resize", function (changeSize, resultSize) {
                    container.refresh();
                });
                
                $scope.standbymap = new Standby({ target: "mapPanel" });
                document.body.appendChild($scope.standbymap.domNode);
                $scope.standbymap.startup();
            });
} ]);