define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/ListController' ],
    function ($, _, Backbone, ns) {
        ns.define('am.invoice.router');

        am.invoice.router.InvoiceRouter = Backbone.Router.extend({
            initialize: function () {
            },
            routes: {
                '': 'index',
                'invoice': 'index',
                'invoice/create': 'create',
                'invoice/detail/:id': 'detail'
            },
            index: function () {
                this.listController = new am.invoice.controller.ListController();
                this.listController.show();
            },
            detail: function (id) {
                require([
                    'controller/DetailController'
                ], function () {
                    this.detailController = new am.invoice.controller.DetailController(id);
                    this.detailController.show();
                });
            },
            create: function () {
                require([
                    'controller/CreateController'
                ], function () {
                    this.createController = new am.invoice.controller.CreateController();
                    this.createController.show();
                });
            },
            edit: function (id) {
                require([
                    'controller/EditController'
                ], function () {
                    this.editController = new am.invoice.controller.EditController(id);
                    this.editController.show();
                });
            }
        });
        return am;
    }
);