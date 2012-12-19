define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/SideBar'],
    function ($, _, Backbone, ns) {
        ns.define('am.setting.controller');
        am.setting.controller.SideBarController = (function () {
            var sideBar = new HomeJS.components.SideBar({
                title: 'Pengaturan Penomoran',
                items: [{
                    id: 'autoNumberSI',
                    title: 'Penomoran Pembelian',
                    class: 'icon-user',
                    action: function () {
                        am.eventAggregator.trigger("autoNumberSI");
                    }
                },{
                    id: 'autoNumberInvoice',
                    title: 'Penomoran Penjualan',
                    class: 'icon-wrench',
                    action: function () {
                        am.eventAggregator.trigger("autoNumberInvoice");
                    }
                }, {
                    id: 'autoNumberSuratPeringatan',
                    title: 'Penomoran Surat Peringatan',
                    class: 'icon-wrench',
                    action: function () {
                        am.eventAggregator.trigger("autoNumberSuratPeringatan");
                    }
                }, {
                    id: 'autoNumberSP',
                    title: 'Penomoran Surat Perjanjian',
                    class: 'icon-wrench',
                    action: function () {
                        am.eventAggregator.trigger("autoNumberSuratPerjanjian");
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
        return am.setting.controller.SideBarController;
    }
);