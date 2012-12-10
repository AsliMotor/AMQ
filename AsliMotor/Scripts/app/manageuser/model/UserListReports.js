define([
    'underscore',
    'backbone',
    'namespace',
    '../model/UserListReport'
], function (_, Backbone, ns, am) {
    ns.define('am.invoice.model');
    am.manageuser.model.UserListReports = Backbone.Collection.extend({
        initialize: function () {
        },
        url: '/manageuser/lists',
        model: am.manageuser.model.UserListReport
    });

    return am;
});