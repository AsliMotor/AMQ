define(['jquery',
'underscore',
'backbone',
'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.purchase.router');

        am.purchase.router.PurchaseRouter = Backbone.Router.extend({
            initialize: function () {
            },
            routes: {
                '': 'index',
                'purchase': 'index',
                'purchase/detail/:id': 'detail',
                'purchase/create': 'create',
                'purchase/edit/:id': 'edit'
            },
            index: function () {
                require([
                    'controller/ListController'
                ], function () {
                    this.listController = new am.purchase.controller.ListController();
                    this.listController.show();
                });
            },
            detail: function (id) {
                require([
                    'controller/DetailController'
                ], function () {
                    this.detailController = new am.purchase.controller.DetailController(id);
                    this.detailController.show();
                });
            },
            create: function () {
                require([
                    'controller/CreateController'
                ], function () {
                    this.createController = new am.purchase.controller.CreateController();
                    this.createController.show();
                });
            },
            edit: function (id) {
                require([
                    'controller/EditController'
                ], function () {
                    this.editController = new am.purchase.controller.EditController(id);
                    this.editController.show();
                });
            }
        });
        return am;
    }
);