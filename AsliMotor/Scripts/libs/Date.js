String.prototype.toDate = function () {
    var dateFormated = new Date(parseInt(this.replace(/\/Date\((-?\d+)\)\//, '$1')));
    var day = dateFormated.getDate();
    var month = dateFormated.getMonth();
    var year = dateFormated.getFullYear();
    return day + " " + ConvertMonthToIndonesian(month) + " " + year;
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

String.prototype.toDefaultFormatDate = function () {
    var dateFormated = new Date(parseInt(this.replace(/\/Date\((-?\d+)\)\//, '$1')));
    var day = ("0" + dateFormated.getDate()).slice(-2);
    var month = ("0" + (dateFormated.getMonth() + 1)).slice(-2);
    var year = dateFormated.getFullYear();
    return day + "-" + month + "-" + year;
}