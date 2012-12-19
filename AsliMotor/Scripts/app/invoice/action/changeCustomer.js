define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changecustomer',
    'view/changecustomer'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changeCustomer = function (spec) {
        var that = {};
        var changeCustomerCommander = am.invoice.command.changeCustomerCommander();
        var execute = function (model) {
            var changeCustomerCommand = changeCustomerCommander.createCommand(model);
            var editor = new am.invoice.view.ChangeCustomer({ model: changeCustomerCommand });
            editor.render();
            changeCustomerCommand.on('success', function () {
                model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});