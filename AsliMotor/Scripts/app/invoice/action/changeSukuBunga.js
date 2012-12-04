define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changesukubunga',
    'view/changesukubunga'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changeSukuBunga = function (spec) {
        var that = {};
        var changeSukuBungaCommander = am.invoice.command.changeSukuBungaCommander();
        var execute = function () {
            var changeSukuBungaCommand = changeSukuBungaCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangeSukuBunga({ model: changeSukuBungaCommand });
            editor.render();
            changeSukuBungaCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});