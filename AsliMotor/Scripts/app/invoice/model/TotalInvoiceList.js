define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.invoice.model');
    am.invoice.model.TotalInvoiceList = Backbone.Model.extend({
        url:"invoice/totallist"
});

return am;
});