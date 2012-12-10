define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.manageuser.command');

    am.manageuser.command.AddUser = Backbone.Model.extend({
        url: '/manageuser/adduser',
        defaults: {
            username: '',
            email: '',
            password: '',
            confirmPassword: '',
            role: ''
        }
    });

    return am.manageuser.command.AddUser;
});