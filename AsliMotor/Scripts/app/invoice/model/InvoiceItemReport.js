define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.invoice.model');
    am.invoice.model.InvoiceItemReport = Backbone.Model.extend();

    return am;
});