define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.auditlog.model');
    am.auditlog.model.TotalInvoiceAuditLogList = Backbone.Model.extend({
        url: "/PriceChangeLog/GetTotalInvoiceLog"
});

return am;
});