define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/SideBarController'
],
    function ($, _, Backbone, ns, sidebarController) {
        ns.define('am.angsuranreport.router');

        am.angsuranreport.router.AngsuranReportRouter = Backbone.Router.extend({
            initialize: function () {
                var self = this;
                am.eventAggregator.on('showTelahJatuhTempoReport', function () {
                    self.showTelahJatuhTempo();
                });
            },
            routes: {
                '': 'index',
                'angsuranreport': 'index'
            },
            index: function () {
                sidebarController.show();
                $("#telahJatuhTempoReport").click();
            },
            showTelahJatuhTempo: function () {
                require(['controller/TelahJatuhTempoController'], function () {
                    var controller = new am.angsuranreport.controller.TelahJatuhTempoController();
                    controller.show();
                });
            }
        });
        return am;
    }
);