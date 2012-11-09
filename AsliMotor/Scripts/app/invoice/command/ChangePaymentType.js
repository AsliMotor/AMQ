define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');

    am.invoice.command.ChangePaymentType = Backbone.Model.extend({
        url: '/invoice/changepaymenttype',
        defaults: {
            "InvoiceId": "",
            "Status": 2,
            "UangMuka": 0,
            "SukuBunga": 0,
            "LamaAngsuran": 0,
            "DueDate": getCurrentDate()
        },
        initialize: function () {
            this.validators = {};
            this.validators.UangMuka = function (value, model) {
                if (model.get("Status") == "1")
                    return (value > 0) ? { isValid: true} : { isValid: false, message: "Uang muka harus diisi" };
                else
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

    am.invoice.command.changePaymentTypeCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var changePaymentTypeCommand = new am.invoice.command.ChangePaymentType({
                InvoiceId: model.get("id")
            });
            return changePaymentTypeCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});