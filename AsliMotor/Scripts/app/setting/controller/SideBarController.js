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
                title: 'Pengaturan',
                items: [{
                    id: 'profileUser',
                    title: 'Profil Akun',
                    class: 'icon-user',
                    action: function () {
                        am.eventAggregator.trigger("profileUser");
                    }
                },{
                    id: 'changePassword',
                    title: 'Ubah Password',
                    class: 'icon-wrench',
                    action: function () {
                        am.eventAggregator.trigger("changePassword");
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