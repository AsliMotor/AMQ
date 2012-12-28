define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/date'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');
    am.invoice.command.ChangeInvoiceDate = Backbone.Model.extend({
        url: '/invoice/changeinvoicedate',
        defaults: {
            InvoiceId: ""
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

    am.invoice.command.changeInvoiceDateCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var changeInvoiceDateCommand = new am.invoice.command.ChangeInvoiceDate({
                InvoiceId: model.get("id"),
                InvoiceDate: model.get('InvoiceDate').toDefaultFormatDate()
            });
            return changeInvoiceDateCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});