define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/SideBar'],
    function ($, _, Backbone, ns) {
        ns.define('am.report.controller');
        am.report.controller.SideBarController = (function () {
            var sideBar = new HomeJS.components.SideBar({
                title: 'Laporan Penjualan',
                items: [{
                    id: 'dailyReport',
                    title: 'Laporan Harian',
                    class: 'icon-home',
                    action: function () {
                        am.eventAggregator.trigger("showDailyreport");
                    }
                }, {
                    id: 'monthlyReport',
                    title: 'Laporan Bulanan',
                    class: 'icon-calendar',
                    action: function () {
                        am.eventAggregator.trigger("showMonthlyreport");
                    }
                }, {
                    id: 'yearlyReport',
                    title: 'Laporan Tahunan',
                    class: 'icon-th',
                    action: function () {
                        am.eventAggregator.trigger("showYearlyreport");
                    }
                }, {
                    id: 'customerReport',
                    title: 'Laporan Per Pelanggan',
                    class: 'icon-user',
                    action: function () {
                        alert('pelanggan');
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
        return am.report.controller.SideBarController;
    }
);