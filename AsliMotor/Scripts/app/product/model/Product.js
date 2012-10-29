define([
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date'
], function (_, Backbone, ns) {
    ns.define('am.product.model');
    am.product.model.Product = Backbone.Model.extend({
        url: '/product/product',
        defaults: {
            Merk: "Yamaha",
            Type: "",
            Tahun: getCurrentYear(),
            Warna: "",
            NoRangka: "",
            NoMesin: "",
            NoBpkb: "",
            NoPolisi: ""
        },
        initialize: function () {
            this.validators = {};
            this.validators.Merk = function (value) {
                return value.length > 0 ? { isValid: true} : { isValid: false, message: "Merk kendaraan harus diisi" };
            };
            this.validators.Type = function (value) {
                return value.length > 0 ? { isValid: true} : { isValid: false, message: "Tipe kendaraan harus diisi" };
            };
            this.validators.NoPolisi = function (value) {
                return value.length > 0 ? { isValid: true} : { isValid: false, message: "Nomor Polisi harus diisi" };
            };
        },
        validateItem: function (key) {
            return (this.validators[key]) ? this.validators[key](this.get(key)) : { isValid: true };
        },
        validateAll: function () {
            var errors = [];
            for (var key in this.validators) {
                if (this.validators.hasOwnProperty(key)) {
                    var check = this.validators[key](this.get(key));
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