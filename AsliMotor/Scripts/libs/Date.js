String.prototype.toDate = function () {
    var dateFormated = new Date(parseInt(this.replace(/\/Date\((-?\d+)\)\//, '$1')));
    var day = ("0" + dateFormated.getDate()).slice(-2);
    var month = dateFormated.getMonth();
    var year = dateFormated.getFullYear();
    return day + " " + ConvertMonthToIndonesian(month) + " " + year;
}
String.prototype.toMonthAndYear = function () {
    var dateFormated = new Date(parseInt(this.replace(/\/Date\((-?\d+)\)\//, '$1')));
    var month = dateFormated.getMonth();
    var year = dateFormated.getFullYear();
    return ConvertMonthToIndonesian(month) + " " + year;
}
String.prototype.toYear = function () {
    var dateFormated = new Date(parseInt(this.replace(/\/Date\((-?\d+)\)\//, '$1')));
    var year = dateFormated.getFullYear();
    return year;
}
var ConvertMonthToIndonesian = function (month) {
    var bulan = new Array("Januari",
    "Februari", "Maret", "April",
    "Mei", "Juni", "Juli",
    "Agustus", "September",
    "Oktober", "November",
    "Desember");

    return bulan[month];
}

var getCurrentDate = function () {
    var currentDate = new Date();
    var date = ("0" + currentDate.getDate()).slice(-2);
    var month = ("0" + (currentDate.getMonth() + 1)).slice(-2);
    var year = currentDate.getFullYear();

    return date + "-" + month + "-" + year;
};

var getCurrentYear = function () {
    var currentDate = new Date();
    var year = currentDate.getFullYear();

    return year;
};
var getStartDayInMonth = function () {
    var d = new Date(new Date().getFullYear(), new Date().getMonth(), 1);
    var date = ("0" + d.getDate()).slice(-2);
    var month = ("0" + (d.getMonth() + 1)).slice(-2);
    var year = d.getFullYear();
    return date + "-" + month + "-" + year;
};

Date.prototype.toDateFromFullDateTime = function () {
    var day = ("0" + this.getDate()).slice(-2);
    var month = ("0" + this.getMonth()).slice(-2);
    var year = this.getFullYear();
    return day + "-" + month + "-" + year;
}

String.prototype.toDefaultFormatDate = function () {
    var dateFormated = new Date(parseInt(this.replace(/\/Date\((-?\d+)\)\//, '$1')));
    var day = ("0" + dateFormated.getDate()).slice(-2);
    var month = ("0" + (dateFormated.getMonth() + 1)).slice(-2);
    var year = dateFormated.getFullYear();
    return day + "-" + month + "-" + year;
}

String.prototype.toUTCDate = function () {
    var dateFormated = new Date(parseInt(this.replace(/\/Date\((-?\d+)\)\//, '$1')));
    return parseInt(dateFormated.toUTC());
}

Number.prototype.getDay = function () {
    var date = new Date(this);
    return date.getDate();
}
Number.prototype.getMonth = function () {
    var date = new Date(this);
    return ConvertMonthToIndonesian(date.getMonth());
}