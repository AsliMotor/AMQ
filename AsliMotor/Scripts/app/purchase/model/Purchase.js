define([
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date'
], function (_, Backbone, ns) {
    ns.define('am.purchase.model');
    am.purchase.model.Purchase = Backbone.Model.extend({
        url: '/purchase/purchase',
        defaults: {
            SupplierInvoiceDate: "",
            SupplierInvoiceNo: "",
            SupplierName: "",
            NoTelp: "",
            SupplierBillingAddress: "",
            Merk: "Yamaha",
            Type: "",
            Tahun: getCurrentYear(),
            Warna: "",
            NoRangka: "",
            NoMesin: "",
            NoBpkb: "",
            NoPolisi: "",
            HargaBeli: "",
            Note: ""
        },
        initialize: function () {
            this.validators = {};
            this.validators.Merk = function (value) {
                return value.length > 0 ? { isValid: true} : { isValid: false, message: "Merk kendaraan harus diisi" };
            };
            this.validators.Type = function (value) {
                return value.length > 0 ? { isValid: true} : { isValid: false, message: "Type kendaraan harus diisi" };
            };
            this.validators.NoPolisi = function (value) {
                return value.length > 0 ? { isValid: true} : { isValid: false, message: "Nomor Polisi harus diisi" };
            };
            this.validators.HargaBeli = function (value) {
                return value > 0 ? { isValid: true} : { isValid: false, message: "Harga Beli harus diisi" };
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