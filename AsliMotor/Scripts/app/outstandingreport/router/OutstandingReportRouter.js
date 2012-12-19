define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/SideBarController'
],
    function ($, _, Backbone, ns, sidebarController) {
        ns.define('am.outstandingreport.router');

        am.outstandingreport.router.OutstandingReportRouter = Backbone.Router.extend({
            initialize: function () {
                var self = this;
                am.eventAggregator.on('showPiutangTelahJatuhTempo', function () {
                    self.showPiutangTelahJatuhTempo();
                });
            },
            routes: {
                '': 'index',
                'outstandingreport': 'index'
            },
            index: function () {
                sidebarController.show();
                $("#dailyReport").click();
            },
            showPiutangTelahJatuhTempo: function () {
                require(['controller/PiutangTelahJatuhTempoController'], function () {
                    var controller = new am.outstandingreport.controller.PiutangTelahJatuhTempoController();
                    controller.show();
                });
            }
        });
        return am;
    }
);