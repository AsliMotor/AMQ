define([
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date'
], function (_, Backbone, ns) {
    ns.define('am.customer.model');
    am.customer.model.Customer = Backbone.Model.extend({
        url: '/customer/customer',
        defaults: {
            Name:"",
            KTPNo: "",
            KTPDate: getCurrentDate(),
            Birthday: getCurrentDate()
        },
        initialize: function () {
            this.validators = {};
            this.validators.Name = function (value) {
                return value.length > 0 ? { isValid: true} : { isValid: false, message: "Nama pelanggan harus diisi" };
            };
            this.validators.KTPNo = function (value) {
                return value.length > 0 ? { isValid: true} : { isValid: false, message: "Nomor KTP harus diisi" };
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