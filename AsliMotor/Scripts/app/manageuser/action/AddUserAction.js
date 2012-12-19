define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/manageuser/command/deleteuser',
    'view/deleteuser'
], function ($, _, Backbone, ns, am) {
    ns.define('am.manageuser.action');
    am.manageuser.action.addUser = function (spec) {
        var that = {};
        var command = new am.manageuser.command.AddUser();
        var execute = function (model) {
            var editor = new am.manageuser.view.AddUser({ model: command });
            editor.render();
            command.on('success', function (data) {
                window.location.reload();
                //spec.model.trigger('showDetail');
            });
        };

        that.execute = execute;
        return that;
    };
});