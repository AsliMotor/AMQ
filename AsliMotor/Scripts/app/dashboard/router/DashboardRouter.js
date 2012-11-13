define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/dashboardcontroller'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.router');

        am.dashboard.router.DashboardRouter = Backbone.Router.extend({
            initialize: function () {
            },
            routes: {
                '': 'index',
                'dashboard': 'index'
            },
            index: function () {
                this.dashboardController = new am.dashboard.controller.DashboardController();
                this.dashboardController.show();
            }
        });
        return am;
    }
);