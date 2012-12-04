define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/command/changeduedate',
    'view/changeduedate'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.action');
    am.invoice.action.changeDueDate = function (spec) {
        var that = {};
        var changeDueDateCommander = am.invoice.command.changeDueDateCommander();
        var execute = function () {
            var changeDueDateCommand = changeDueDateCommander.createCommand(spec.model);
            var editor = new am.invoice.view.ChangeDueDate({ model: changeDueDateCommand });
            editor.render();
            changeDueDateCommand.on('success', function () {
                spec.model.trigger('changed');
            });
        };

        that.execute = execute;
        return that;
    };
});