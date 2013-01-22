define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changeterm',
    'view/changeterm'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changeTerm = function (spec) {
        var that = {};
        var changeTermCommander = am.invoice.command.changeTermCommander();
        var execute = function () {
            var changeTermCommand = changeTermCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangeTerm({ model: changeTermCommand });
            editor.render();
            changeTermCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});