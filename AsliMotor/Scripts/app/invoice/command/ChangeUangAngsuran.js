define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');

    am.invoice.command.ChangeUangAngsuran = Backbone.Model.extend({
        url: '/invoice/changeuangangsuran',
        defaults: {
            InvoiceId: "",
            Angsuran: 0
        },
        initialize: function () {
            this.validators = {};
            this.validators.Angsuran = function (value, model) {
                return (value > 0) ? { isValid: true} : { isValid: false, message: "Uang Angsuran harus diisi" };
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

    am.invoice.command.changeUangAngsuranCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var changeUangAngsuranCommand = new am.invoice.command.ChangeUangAngsuran({
                InvoiceId: model.get("id"),
                Angsuran: model.get('AngsuranBulanan')
            });
            return changeUangAngsuranCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});