define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/SideBar'],
    function ($, _, Backbone, ns) {
        ns.define('am.purchasereport.controller');
        am.purchasereport.controller.SideBarController = (function () {
            var sideBar = new HomeJS.components.SideBar({
                title: 'Laporan Pembelian',
                items: [{
                    id: 'dailyReport',
                    title: 'Harian',
                    class: 'icon-calendar',
                    action: function () {
                        am.eventAggregator.trigger("showDailyreport");
                    }
                }, {
                    id: 'monthlyReport',
                    title: 'Bulanan',
                    class: 'icon-th',
                    action: function () {
                        am.eventAggregator.trigger("showMonthlyreport");
                    }
                }, {
                    id: 'yearlyReport',
                    title: 'Tahunan',
                    class: 'icon-text-width',
                    action: function () {
                        am.eventAggregator.trigger("showYearlyreport");
                    }
                }, {
                    id: 'productReport',
                    title: 'Peringkat Produk',
                    class: 'icon-barcode',
                    action: function () {
                        am.eventAggregator.trigger("showRateProductReport");
                    }
                }, {
                    id: 'chartProductReport',
                    title: 'Grafik Produk',
                    class: 'icon-signal',
                    action: function () {
                        am.eventAggregator.trigger("showGrafikProductReport");
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
        return am.purchasereport.controller.SideBarController;
    }
);