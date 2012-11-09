define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');

    am.invoice.command.ChangeUangMuka = Backbone.Model.extend({
        url: '/invoice/changeuangmuka',
        defaults: {
            InvoiceId: "",
            UangMuka: 0
        },
        initialize: function () {
            this.validators = {};
            this.validators.UangMuka = function (value, model) {
                return (value > 0) ? { isValid: true} : { isValid: false, message: "Uang muka harus diisi" };
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

    am.invoice.command.changeUangMukaCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var changeUangMukaCommand = new am.invoice.command.ChangeUangMuka({
                InvoiceId: model.get("id"),
                UangMuka: model.get('UangMuka')
            });
            return changeUangMukaCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});