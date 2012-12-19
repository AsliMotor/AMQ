define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        '../../../libs/homejs/Breadcrumb',
        '../model/SuratPerjanjianautonumber',
        '../view/SuratPerjanjianautonumberview'
        ],
    function ($, _, Backbone, ns, evt, Breadcrumb, Model, View) {
        ns.define('am.settingautonumber.controller');
        am.settingautonumber.controller.SuratPerjanjianAutoNumberController = function () {
            var model = new Model();
            var breadcrumb = new Breadcrumb({
                title: 'Pengaturan Penomoran Otomatis Surat Perjanjian',
                icon: 'icon-cog icon-white'
            });

            var view = new View({
                model: model
            });

            var show = function () {
                model.fetch();
                $("#main-content").html(breadcrumb.render().el);
                $("#main-content").append(view.render().el);
            };
            return {
                show: show
            }
        };
        return am.settingautonumber.controller.SuratPerjanjianAutoNumberController;
    }
);