define(['jquery', 'underscore', 'backbone', 'namespace'], function ($, _, Backbone, ns) {
    ns.define('am');

    am.InvoiceAuditLogAction = {
        InvoiceCreated : 0,
        AngsuranPaid : 1,
        DueDateChanged : 2, 
        LamaAngsuranChanged : 3,
        PaymentTypeChanged : 4,
        SukuBungaChanged : 5,
        UangAngsuranChanged : 6,
        UangMukaChanged : 7,
        InvoiceCanceled : 8,
        InvoicePulled : 9,
        ProductChanged : 10,
        CustomerChanged : 11,
        PriceChanged: 12,
        TermChanged: 13
    };

    return am.InvoiceAuditLogAction;
});