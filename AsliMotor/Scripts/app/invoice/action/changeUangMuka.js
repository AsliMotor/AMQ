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
    am.invoice.action.changeUangMuka = function (spec) {
        var that = {};
        var changeUangMukaCommander = am.invoice.command.changeUangMukaCommander();
        var execute = function () {
            var changeUangMukaCommand = changeUangMukaCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangeUangMuka({ model: changeUangMukaCommand });
            editor.render();
            changeUangMukaCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});