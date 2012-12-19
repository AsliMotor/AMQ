define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/SideBar'],
    function ($, _, Backbone, ns) {
        ns.define('am.angsuranreport.controller');
        am.angsuranreport.controller.SideBarController = (function () {
            var sideBar = new HomeJS.components.SideBar({
                title: 'Laporan Angsuran',
                items: [{
                    id: 'telahJatuhTempoReport',
                    title: 'Telah Jatuh Tempo',
                    class: 'icon-home',
                    action: function () {
                        am.eventAggregator.trigger("showTelahJatuhTempoReport");
                    }
                }, {
                    id: 'telahJatuhTempoReport',
                    title: 'Belum Jatuh Tempo',
                    class: 'icon-home',
                    action: function () {
                        am.eventAggregator.trigger("showPiutangTelahJatuhTempo");
                    }
                }, {
                    id: 'monthlyReport',
                    title: 'Semua Angsuran',
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
        return am.angsuranreport.controller.SideBarController;
    }
);