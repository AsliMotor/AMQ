define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');

    am.invoice.command.ChangeCustomer = Backbone.Model.extend({
        url: '/invoice/changecustomer',
        defaults: {
            InvoiceId: "",
            CustomerId: ""
        },
        initialize: function () {
            this.validators = {};
            this.validators.CustomerId = function (value) {
                return (value == undefined || value == "") ? { isValid: false, message: "Pelanggan harus dipilih"} : { isValid: true };
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

    am.invoice.command.changeCustomerCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var changeCustomerCommand = new am.invoice.command.ChangeCustomer({
                InvoiceId: model.get("id"),
                CustomerId: model.get('CustomerId')
            });
            return changeCustomerCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});