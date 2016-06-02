var disabledDays = ["1-15-2009"];

function nationalDays(date) {
    var m = date.getMonth(), d = date.getDate(), y = date.getFullYear();
    for (i = 0; i < disabledDays.length; i++) {
        if (ArrayContains(disabledDays, (m + 1) + '-' + d + '-' + y) || ElHoy > date) {
            return [false];
        }
    }
    return [true];
}

function noWeekendsOrHolidays(date) {
    var noWeekend = jQuery.datepicker.noWeekends(date);
    //return noWeekend[0] ? nationalDays(date) : noWeekend;
    return nationalDays(date);
}

function ArrayIndexOf(array, item, from) {
    var len = array.length;
    for (var i = (from < 0) ? Math.max(0, len + from) : from || 0; i < len; i++) {
        if (array[i] === item) return i;
    }
    return -1;
}

function ArrayContains(array, item, from) {
    return ArrayIndexOf(array, item, from) != -1;
}