define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.auditlog.model');
    am.auditlog.model.AuditLogListReport = Backbone.Model.extend();

    return am;
});