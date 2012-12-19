define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.manageuser.model');

    var Role = Backbone.Model.extend();

    am.manageuser.model.RoleSelectionList = Backbone.Collection.extend({
        url: '/ManageUser/allroles',
        model: Role
    });

    return am.manageuser.model.RoleSelectionList;
});