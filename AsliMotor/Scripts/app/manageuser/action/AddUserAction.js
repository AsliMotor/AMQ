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
    am.manageuser.action.addUser = function (spec) {
        var that = {};
        var command = new am.manageuser.command.AddUser();
        var execute = function () {
            var editor = new am.manageuser.view.AddUser({ model: command });
            editor.render();
            command.on('success', function (data) {
                spec.model.trigger('showDetail');
            });
        };

        that.execute = execute;
        return that;
    };
});