define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');

    am.invoice.command.ChangeTerm = Backbone.Model.extend({
        url: '/invoice/changeterm',
        defaults: {
            InvoiceId: "",
            TermId: ""
        }
    });

    am.invoice.command.changeTermCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var changeTermCommand = new am.invoice.command.ChangeTerm({
                InvoiceId: model.get("id"),
                TermId: model.get('TermId')
            });
            return changeTermCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});