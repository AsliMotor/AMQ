define([
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date'
], function (_, Backbone, ns) {
    ns.define('am.invoice.model');
    am.invoice.model.Invoice = Backbone.Model.extend({
        url: '/invoice/invoice',
        defaults: {
            "CustomerId": "",
            "ProductId": "",
            "DebitNote": 0,
            "Status": 2,
            "UangMuka": 0,
            "SukuBunga": 0,
            "LamaAngsuran": 0,
            "DueDate": getCurrentDate(),
            "TermId": ""
        },
        initialize: function () {
            this.validators = {};
            this.validators.CustomerId = function (value) {
                return (value == undefined || value == "") ? { isValid: false, message: "Pelanggan harus dipilih"} : { isValid: true };
            };
            this.validators.ProductId = function (value) {
                return (value == undefined || value == "") ? { isValid: false, message: "Kendaraan harus dipilih"} : { isValid: true };
            };
            this.validators.Price = function (value) {
                return (value == undefined || value == 0) ? { isValid: false, message: "Harga jual harus diisi"} : { isValid: true };
            };
            this.validators.DebitNote = function (value, model) {
                if (model.get("Status") == "0")
                    return (value > 0) ? { isValid: true} : { isValid: false, message: "Uang tanda jadi harus diisi" };
                else
                    return { isValid: true };
            };
            this.validators.UangMuka = function (value, model) {
                if (model.get("Status") == "1") {
                    if (value > 0) {
                        return { isValid: true };
                    } else {
                        return { isValid: false, message: "Uang muka harus diisi" };
                    }
                }
                return { isValid: true };
            };
            this.validators.SukuBunga = function (value, model) {
                if (model.get("Status") == "1")
                    return (value > 0) ? { isValid: true} : { isValid: false, message: "Suku bunga harus diisi" };
                else
                    return { isValid: true };
            };
            this.validators.LamaAngsuran = function (value, model) {
                if (model.get("Status") == "1")
                    return (value > 0) ? { isValid: true} : { isValid: false, message: "Lama angsuran harus diisi" };
                else
                    return { isValid: true };
            };
        },
        validateItem: function (key) {
            return (this.validators[key]) ? this.validators[key](this.get(key), this) : { isValid: true };
        },
        validateAll: function () {
            var errors = [];
            for (var key in this.validators) {
                if (this.validators.hasOwnProperty(key)) {
                    var check = this.validators[key](this.get(key), this);
                    if (check.isValid === false) {
                        var e = {};
                        e.field = key;
                        e.message = check.message;
                        errors.push(e);
                    }
                }
            }
            return _.size(errors) > 0 ? { isValid: false, errors: errors} : { isValid: true };
        }
    });

    return am;
});