define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changeproduct',
    'view/changeproduct'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changeProduct = function (spec) {
        var that = {};
        var changeProductCommander = am.invoice.command.changeProductCommander();
        var execute = function (model) {
            var changeProductCommand = changeProductCommander.createCommand(model);
            var editor = new am.invoice.view.ChangeProduct({ model: changeProductCommand });
            editor.render();
            changeProductCommand.on('success', function () {
                model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});