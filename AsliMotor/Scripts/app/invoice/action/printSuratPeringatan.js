define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/printsuratperingatan',
    'view/printSuratPeringatan'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.printSuratPeringatan = function (spec) {
        var that = {};
        var printSuratPeringatanCommander = am.invoice.command.PrintSuratPeringatanCommander();
        var execute = function () {
            var printSuratPeringatanCommand = printSuratPeringatanCommander.createCommand(spec.model);
            var editor = new am.invoice.view.PrintSuratPeringatan({ model: printSuratPeringatanCommand });
            editor.render();
        };

        that.execute = execute;
        return that;
    };
});