define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changehargajual',
    'view/changehargajual'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changeHargaJual = function (spec) {
        var that = {};
        var changeHargaJualCommander = am.invoice.command.changeHargaJualCommander();
        var execute = function () {
            var changeHargaJualCommand = changeHargaJualCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangeHargaJual({ model: changeHargaJualCommand });
            editor.render();
            changeHargaJualCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});