define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/Breadcrumb',
        '../model/changepassword',
        '../view/changepassword'],
    function ($, _, Backbone, ns, evt, Breadcrumb, Model, View) {
        ns.define('am.setting.controller');
        am.setting.controller.ChangePasswordController = function () {
            var changePassModel = new Model();
            var breadcrumb = new Breadcrumb({
                title: 'Ubah Password',
                icon: 'icon-wrench icon-white'
            });

            var changePassView = new View({
                model: changePassModel
            });

            var show = function () {
                $("#main-content").html(breadcrumb.render().el);
                $("#main-content").append(changePassView.render().el);
            };
            return {
                show: show
            }
        };
        return am.setting.controller.ChangePasswordController;
    }
);