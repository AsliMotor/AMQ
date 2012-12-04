define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changelamaangsuran',
    'view/changelamaangsuran'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changeLamaAngsuran = function (spec) {
        var that = {};
        var changeLamaAngsuranCommander = am.invoice.command.changeLamaAngsuranCommander();
        var execute = function () {
            var changeLamaAngsuranCommand = changeLamaAngsuranCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangeLamaAngsuran({ model: changeLamaAngsuranCommand });
            editor.render();
            changeLamaAngsuranCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});