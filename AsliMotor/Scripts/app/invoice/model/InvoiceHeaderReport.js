define([
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date'
], function (_, Backbone, ns) {
    ns.define('am.invoice.model');
    am.invoice.model.InvoiceHeaderReport = Backbone.Model.extend({
        url: '/invoice/HeaderReport'
    });
    return am;
});