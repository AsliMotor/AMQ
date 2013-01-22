define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/SideBar'],
    function ($, _, Backbone, ns) {
        ns.define('am.statementreport.controller');
        am.statementreport.controller.SideBarController = (function () {
            var sideBar = new HomeJS.components.SideBar({
                title: 'Laporan Keuangan',
                items: [{
                    id: 'transactionListingReport',
                    title: 'Transaction Listing',
                    class: 'icon-folder-open',
                    action: function () {
                        am.eventAggregator.trigger("showTransactionListing");
                    }
                }]
            });
            var show = function () {
                $("#menu-sidebar").html(sideBar.render().el);
            };
            return {
                show: show
            }
        })();
        return am.statementreport.controller.SideBarController;
    }
);