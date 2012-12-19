define(['jquery',
'underscore',
'backbone',
'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.auditlog.router');

        am.auditlog.router.AuditLogRouter = Backbone.Router.extend({
            initialize: function () {
            },
            routes: {
                '': 'index',
                'pricechangelog': 'index',
                'pricechangelog/invoice': 'invoicePriceChanged',
                'PriceChangeLog/Invoice': 'invoicePriceChanged'
            },
            index: function () {
                require([
                    'controller/ListController'
                ], function () {
                    var listController = new am.auditlog.controller.ListController();
                    listController.show();
                });
            },
            invoicePriceChanged: function () {
                require([
                    'controller/ListInvoiceController'
                ], function () {
                    var listController = new am.auditlog.controller.ListInvoiceController();
                    listController.show();
                });
            }
        });
        return am;
    }
);