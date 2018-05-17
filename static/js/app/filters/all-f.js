'use strict';

filters.filter('toPercent', [function () {
    return function (cantidad, total) {
        var p = ((cantidad / total) * 100).toFixed(0);
        if (isNaN(p))
            return "0";
        else
            return p;
    }
}]);

filters.filter('formatNuevoSol', [function () {
    return function (value) {
        var result = "S/. ";
        var decimal = value.indexOf(".");

        if (decimal != -1) {
            var million = value.substring(0, decimal);
            if (million.length > 3)
                result = result + million.substring(0, million.length - 3) + "," + million.substring(million.length - 3);
            else
                result = result + million;
            result = result + value.substring(decimal);
        }
        else {
            if (value.length > 3)
                result = result + value.substring(0, value.length - 3) + "," + value.substring(value.length - 3);
            else
                result = result + value;
        }
        return result;
    }
} ]);