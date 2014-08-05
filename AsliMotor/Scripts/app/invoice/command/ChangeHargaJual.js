define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');

    am.invoice.command.ChangeHargaJual = Backbone.Model.extend({
        url: '/invoice/changehargajual',
        defaults: {
            InvoiceId: "",
            Price: 0
        },
        initialize: function () {
            this.validators = {};
            this.validators.Price = function (value, model) {
                return (value > 0) ? { isValid: true} : { isValid: false, message: "Harga Jual harus diisi" };
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

    am.invoice.command.changeHargaJualCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var changeHargaJualCommand = new am.invoice.command.ChangeHargaJual({
                InvoiceId: model.get("id"),
                Price: model.get('Price')
            });
            return changeHargaJualCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});