define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.invoice.model');
    am.invoice.model.InvoiceListReport = Backbone.Model.extend();

    return am;
});