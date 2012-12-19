define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/Breadcrumb',
        '../model/profileuser',
        '../view/profileuser'],
    function ($, _, Backbone, ns, evt, Breadcrumb, Model, View) {
        ns.define('am.setting.controller');
        am.setting.controller.ProfileUserController = function () {
            var profileModel = new Model();
            var breadcrumb = new Breadcrumb({
                title: 'Profil Akun',
                icon: 'icon-user icon-white'
            });

            var profileView = new View({
                model: profileModel
            });

            var show = function () {
                profileModel.fetch();
                $("#main-content").html(breadcrumb.render().el);
                $("#main-content").append(profileView.render().el);
            };
            return {
                show: show
            }
        };
        return am.setting.controller.ProfileUserController;
    }
);