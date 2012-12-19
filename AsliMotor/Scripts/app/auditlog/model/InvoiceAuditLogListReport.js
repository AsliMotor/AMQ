define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.auditlog.model');
    am.auditlog.model.InvoiceAuditLogListReport = Backbone.Model.extend();

    return am;
});