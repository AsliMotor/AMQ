define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/Breadcrumb',
        '../model/organization',
        '../view/profileorganization'],
    function ($, _, Backbone, ns, evt, Breadcrumb, Model, View) {
        ns.define('am.organization.controller');
        am.organization.controller.OrganizationController = function () {
            var orgModel = new Model();

            am.eventAggregator.on('dataChanged', function () {
                orgModel.fetch();
            });

            var breadcrumb = new Breadcrumb({
                title: 'Profil Perusahaan',
                icon: 'icon-globe icon-white'
            });

            var orgView = new View({
                model: orgModel
            });

            var show = function () {
                orgModel.fetch();
                $("#main-container").prepend(breadcrumb.render().el);
                $("#main-left").append(orgView.render().el);
            };
            return {
                show: show
            }
        };
        return am.organization.controller.OrganizationController;
    }
);