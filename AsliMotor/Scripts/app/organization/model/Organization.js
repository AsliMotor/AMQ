define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.organization.model');
    am.organization.model.organization = Backbone.Model.extend({
        url: '/organization/ProfileOrganization'
    });

    return am.organization.model.organization;
});