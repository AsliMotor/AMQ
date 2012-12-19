define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/SideBar'],
    function ($, _, Backbone, ns) {
        ns.define('am.outstandingreport.controller');
        am.outstandingreport.controller.SideBarController = (function () {
            var sideBar = new HomeJS.components.SideBar({
                title: 'Laporan Piutang',
                items: [{
                    id: 'monthlyReport',
                    title: 'Belum Dibayar',
                    class: 'icon-calendar',
                    action: function () {
                        am.eventAggregator.trigger("showMonthlyreport");
                    }
                },{
                    id: 'telahJatuhTempoReport',
                    title: 'Telah Jatuh Tempo',
                    class: 'icon-home',
                    action: function () {
                        am.eventAggregator.trigger("showPiutangTelahJatuhTempo");
                    }
                }, {
                    id: 'monthlyReport',
                    title: 'Bulanan',
                    class: 'icon-calendar',
                    action: function () {
                        am.eventAggregator.trigger("showMonthlyreport");
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
        return am.outstandingreport.controller.SideBarController;
    }
);