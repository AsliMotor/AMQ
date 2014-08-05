define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/pelunasan',
    'view/pelunasan'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.pelunasan = function (spec) {
        var that = {};
        var pelunasanCommander = am.invoice.command.pelunasanCommander();
        var execute = function () {
            var pelunasanCommand = pelunasanCommander.createCommand(spec.model);
            var editor = new am.invoice.view.Pelunasan({ model: pelunasanCommand });
            editor.render();
            pelunasanCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});