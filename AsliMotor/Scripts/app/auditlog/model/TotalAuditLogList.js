define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.auditlog.model');
    am.auditlog.model.TotalAuditLogList = Backbone.Model.extend({
        url: "/PriceChangeLog/GetTotalSILog"
});

return am;
});