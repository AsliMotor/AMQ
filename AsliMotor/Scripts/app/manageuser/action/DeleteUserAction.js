define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/manageuser/command/adduser',
    'view/adduser'
], function ($, _, Backbone, ns, am) {
    ns.define('am.manageuser.action');
    am.manageuser.action.deleteUser = function (spec) {
        var that = {};
        var command = new am.manageuser.command.DeleteUser();
        var execute = function (model) {
            command.set("Username", model.get("Username"));
            var editor = new am.manageuser.view.DeleteUser({ model: command });
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