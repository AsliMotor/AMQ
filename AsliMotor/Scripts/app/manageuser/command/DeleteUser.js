define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.manageuser.command');

    am.manageuser.command.DeleteUser = Backbone.Model.extend({
        url: '/manageuser/deleteuser',
        defaults: {
            Username: ''
        }
    });

    return am.manageuser.command.DeleteUser;
});