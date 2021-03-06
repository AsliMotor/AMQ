﻿define(['jquery',
'underscore',
'backbone',
'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.customer.router');

        am.customer.router.CustomerRouter = Backbone.Router.extend({
            initialize: function () {
            },
            routes: {
                '': 'index',
                'customer': 'index',
                'customer/create': 'create',
                'customer/edit/:id': 'edit',
                'customer/historycreditnote/:id': 'historyCreditNote'
            },
            index: function () {
                require([
                    'controller/ListController'
                ], function () {
                    this.listController = new am.customer.controller.ListController();
                    this.listController.show();
                });
            },
            create: function () {
                require([
                    'controller/CreateController'
                ], function () {
                    this.createController = new am.customer.controller.CreateController();
                    this.createController.show();
                });
            },
            edit: function (id) {
                require([
                    'controller/EditController'
                ], function () {
                    this.editController = new am.customer.controller.EditController(id);
                    this.editController.show();
                });
            },
            historyCreditNote: function (id) {
                require([
                    'controller/HistoryCreditNoteController'
                ], function () {
                    var controller = new am.customer.controller.HistoryCreditNoteController(id);
                    controller.show();
                });
            }
        });
        return am;
    }
);