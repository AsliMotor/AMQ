define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changeinvoicedate',
    'view/changeinvoicedate'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changeInvoiceDate = function (spec) {
        var that = {};
        var changeInvoiceDateCommander = am.invoice.command.changeInvoiceDateCommander();
        var execute = function () {
            var changeInvoiceDateCommand = changeInvoiceDateCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangeInvoiceDate({ model: changeInvoiceDateCommand });
            editor.render();
            changeInvoiceDateCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});