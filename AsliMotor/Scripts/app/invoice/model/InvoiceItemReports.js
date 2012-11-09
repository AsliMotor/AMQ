define([
    'underscore',
    'backbone',
    'namespace',
    '../model/InvoiceItemReport'
], function (_, Backbone, ns, am) {
    ns.define('am.invoice.model');
    am.invoice.model.InvoiceItemReports = Backbone.Collection.extend({
        initialize: function () {
        },
        url: '/invoice/itemsreport',
        model: am.invoice.model.InvoiceItemReport
    });

    return am;
});