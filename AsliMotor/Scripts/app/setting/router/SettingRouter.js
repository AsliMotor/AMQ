define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/SideBarController'
],
    function ($, _, Backbone, ns, sidebarController) {
        ns.define('am.setting.router');

        am.setting.router.SettingRouter = Backbone.Router.extend({
            initialize: function () {
                var self = this;
                am.eventAggregator.on('changePassword', function () {
                    self.changePassword();
                });
                am.eventAggregator.on('profileUser', function () {
                    self.profileUser();
                });
            },
            routes: {
                'setting': 'index'
            },
            index: function () {
                sidebarController.show();
                $("#profileUser").click();
            },
            changePassword: function () {
                require(['controller/ChangePasswordController'], function () {
                    var changePasswordController = new am.setting.controller.ChangePasswordController();
                    changePasswordController.show();
                });
            },
            profileUser: function () {
                require(['controller/ProfileUserController'], function () {
                    var profileUserController = new am.setting.controller.ProfileUserController();
                    profileUserController.show();
                });
            }
        });
        return am;
    }
);