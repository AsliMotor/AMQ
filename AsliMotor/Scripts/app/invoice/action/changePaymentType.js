define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changepaymenttype',
    'view/changepaymenttype'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changePaymentType = function (spec) {
        var that = {};
        var changePaymentTypeCommander = am.invoice.command.changePaymentTypeCommander();
        var execute = function () {
            var changePaymentTypeCommand = changePaymentTypeCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangePaymentType({ model: changePaymentTypeCommand });
            editor.render();
            changePaymentTypeCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});