define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/SideBarController'
],
    function ($, _, Backbone, ns, sidebarController) {
        ns.define('am.statementreport.router');

        am.statementreport.router.StatementReportRouter = Backbone.Router.extend({
            initialize: function () {
                var self = this;
                am.eventAggregator.on('showTransactionListing', function () {
                    self.showTransactionListingReport();
                });
            },
            routes: {
                'statementreport': 'index'
            },
            index: function () {
                sidebarController.show();
                $("#transactionListingReport").click();
            },
            showTransactionListingReport: function () {
                require(['controller/TransactionListingController'], function () {
                    this.transactionListingController = new am.statementreport.controller.TransactionListingController();
                    this.transactionListingController.show();
                });
            }
        });
        return am;
    }
);