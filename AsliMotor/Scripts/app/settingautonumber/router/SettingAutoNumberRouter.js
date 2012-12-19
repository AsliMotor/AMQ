define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/SideBarController'
],
    function ($, _, Backbone, ns, sidebarController) {
        ns.define('am.settingautonumber.router');

        am.settingautonumber.router.SettingAutoNumberRouter = Backbone.Router.extend({
            initialize: function () {
                var self = this;
                am.eventAggregator.on('autoNumberSI', function () {
                    self.autoNumberSI();
                });
                am.eventAggregator.on('autoNumberInvoice', function () {
                    self.autoNumberInvoice();
                });
                am.eventAggregator.on('autoNumberSuratPeringatan', function () {
                    self.autoNumberSuratPeringatan();
                });
                am.eventAggregator.on('autoNumberSuratPerjanjian', function () {
                    self.autoNumberSuratPerjanjian();
                });
            },
            routes: {
                'settingautonumber': 'index'
            },
            index: function () {
                sidebarController.show();
                $("#autoNumberSI").click();
            },
            autoNumberSI: function () {
                require(['controller/SIAutoNumberController'], function () {
                    var siAutoNumberController = new am.settingautonumber.controller.SIAutoNumberController();
                    siAutoNumberController.show();
                });
            },
            autoNumberInvoice: function () {
                require(['controller/InvoiceAutoNumberController'], function () {
                    var invoiceAutoNumberController = new am.settingautonumber.controller.InvoiceAutoNumberController();
                    invoiceAutoNumberController.show();
                });
            },
            autoNumberSuratPeringatan: function () {
                require(['controller/SuratPeringatanAutoNumberController'], function () {
                    var suratPeringatanAutoNumberController = new am.settingautonumber.controller.SuratPeringatanAutoNumberController();
                    suratPeringatanAutoNumberController.show();
                });
            },
            autoNumberSuratPerjanjian: function () {
                require(['controller/SuratPerjanjianAutoNumberController'], function () {
                    var suratPerjanjianAutoNumberController = new am.settingautonumber.controller.SuratPerjanjianAutoNumberController();
                    suratPerjanjianAutoNumberController.show();
                });
            }
        });
        return am;
    }
);