/* Directives */
'use strict';

directives.directive('dojoWidget',["parseProps", function () {
    return {
        restrict: "A",
        replace: false,
        transclude: false,
        require: '?ngModel',
        scope: {
            'ngModel': "=",
            'ngChange': '&',
            'dojoStore': '&',
            'dojoProps': '@'
        },
        link: function (scope, element, attrs, model) {
            require(["dojo/ready", "dijit/dijit",
				attrs.dojoWidget, "dojo/on"], function (ready, dijit, DojoWidget, on) {
				    var elem = angular.element(element[0]);

				    ready(function () {
				        var properties = {};
				        if (attrs.dojoProps) {
				            properties = parseProps(scope.dojoProps, scope);
				        }

				        if (attrs.dojoStore) {
				            properties.store = scope.dojoStore();
				        };

				        properties.value = scope.ngModel;

				        scope.widget = new DojoWidget(properties, element[0]);
				        on(scope.widget, "change", function (newValue) {
				            scope.ngModel = newValue;
				            scope.$digest();
				            if (scope.ngChange) {
				                scope.ngChange();
				            }
				            scope.$apply();
				        });

				        scope.ngModel = scope.widget.get('value');
				    });
				});
        }
    };
}]);