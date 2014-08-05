define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changeprice',
    'view/changeprice'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changePrice = function (spec) {
        var that = {};
        var changePriceCommander = am.invoice.command.changePriceCommander();
        var execute = function () {
            var changePriceCommand = changePriceCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangePrice({ model: changePriceCommand });
            editor.render();
            changePriceCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});