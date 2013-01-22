define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/SideBarController'
],
    function ($, _, Backbone, ns, sidebarController) {
        ns.define('am.purchasereport.router');

        am.purchasereport.router.ReportRouter = Backbone.Router.extend({
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
                am.eventAggregator.on('showRateProductReport', function () {
                    self.showRateProductReport();
                });
                am.eventAggregator.on('showGrafikProductReport', function () {
                    self.showGrafikProductReport();
                });
            },
            routes: {
                '': 'index',
                'purchasereport': 'index'
            },
            index: function () {
                sidebarController.show();
                $("#dailyReport").click();
            },
            showDailyReport: function () {
                require(['controller/DailyController'], function () {
                    this.dailyController = new am.purchasereport.controller.DailyController();
                    this.dailyController.show();
                });
            },
            showMonthlyReport: function () {
                require(['controller/MonthlyController'], function () {
                    this.monthlyController = new am.purchasereport.controller.MonthlyController();
                    this.monthlyController.show();
                });
            },
            showYearlyReport: function () {
                require(['controller/YearlyController'], function () {
                    this.yearlyController = new am.purchasereport.controller.YearlyController();
                    this.yearlyController.show();
                });
            },
            showRateProductReport: function () {
                require(['controller/RateProductController'], function () {
                    this.rateProductController = new am.purchasereport.controller.RateProductController();
                    this.rateProductController.show();
                });
            },
            showGrafikProductReport: function () {
                require(['controller/GrafikProductController'], function () {
                    this.grafikProductController = new am.purchasereport.controller.GrafikProductController();
                    this.grafikProductController.show();
                });
            }
        });
        return am;
    }
);