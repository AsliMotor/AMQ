define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/bayaruangangsuran',
    'view/bayaruangangsuran'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.bayarUangAngsuran = function (spec) {
        var that = {};
        var bayarUangAngsuranCommander = am.invoice.command.bayarUangAngsuranCommander();
        var execute = function () {
            var bayarUangAngsuranCommand = bayarUangAngsuranCommander.createCommand(spec.model);
            var editor = new am.invoice.view.BayarUangAngsuran({ model: bayarUangAngsuranCommand });
            editor.render();
            bayarUangAngsuranCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});