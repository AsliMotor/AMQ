define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/SideBarController'
],
    function ($, _, Backbone, ns, sidebarController) {
        ns.define('am.report.router');

        am.report.router.ReportRouter = Backbone.Router.extend({
            initialize: function () {
                var self = this;
                am.eventAggregator.on('showDailyreport', function () {
                    self.showDailyReport();
                });
                am.eventAggregator.on('showMonthlyreport', function () {
                    self.showMonthlyReport();
                });
                am.eventAggregator.on('showYearlyreport', function () {
                    self.showYearlyReport();
                });
            },
            routes: {
                '': 'index',
                'report': 'index'
            },
            index: function () {
                sidebarController.show();
                $("#dailyReport").click();
            },
            showDailyReport: function () {
                require(['controller/DailyController'], function () {
                    this.dailyController = new am.report.controller.DailyController();
                    this.dailyController.show();
                });
            },
            showMonthlyReport: function () {
                require(['controller/MonthlyController'], function () {
                    this.monthlyController = new am.report.controller.MonthlyController();
                    this.monthlyController.show();
                });
            },
            showYearlyReport: function () {
                require(['controller/YearlyController'], function () {
                    this.yearlyController = new am.report.controller.YearlyController();
                    this.yearlyController.show();
                });
            }
        });
        return am;
    }
);