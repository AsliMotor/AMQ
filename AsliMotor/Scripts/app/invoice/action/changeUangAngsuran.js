define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changeuangangsuran',
    'view/changeuangangsuran'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changeUangAngsuran = function (spec) {
        var that = {};
        var changeUangAngsuranCommander = am.invoice.command.changeUangAngsuranCommander();
        var execute = function () {
            var changeUangAngsuranCommand = changeUangAngsuranCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangeUangAngsuran({ model: changeUangAngsuranCommand });
            editor.render();
            changeUangAngsuranCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});