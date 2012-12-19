define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');

    am.invoice.command.ChangeProduct = Backbone.Model.extend({
        url: '/invoice/changeproduct',
        defaults: {
            InvoiceId: "",
            ProductId: ""
        },
        initialize: function () {
            this.validators = {};
            this.validators.ProductId = function (value) {
                return (value == undefined || value == "") ? { isValid: false, message: "Kendaraan harus dipilih"} : { isValid: true };
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

    am.invoice.command.changeProductCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var changeProductCommand = new am.invoice.command.ChangeProduct({
                InvoiceId: model.get("id")
            });
            return changeProductCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});