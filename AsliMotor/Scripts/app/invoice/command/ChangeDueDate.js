define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/date'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');
    am.invoice.command.ChangeDueDate = Backbone.Model.extend({
        url: '/invoice/changeduedate',
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

    am.invoice.command.changeDueDateCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var changeDueDateCommand = new am.invoice.command.ChangeDueDate({
                InvoiceId: model.get("id"),
                DueDate: model.get('DueDate').toDefaultFormatDate()
            });
            return changeDueDateCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});