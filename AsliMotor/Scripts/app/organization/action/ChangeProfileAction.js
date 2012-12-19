define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/organization/command/changeProfile',
    'view/changeProfile'
], function ($, _, Backbone, ns, am) {
    ns.define('am.organization.action');
    am.organization.action.changeProfile = function (spec) {
        var that = {};
        var changeProfileCommander = am.organization.command.changeProfileCommander();
        var execute = function (model) {
            var command = changeProfileCommander.createCommand(model);
            var editor = new am.organization.view.ChangeProfile({ model: command });
            editor.render();
            command.on('success', function () {
                am.eventAggregator.trigger('dataChanged');
            });
        };
        that.execute = execute;
        return that;
    };
});