define(['jquery',
'underscore',
'backbone',
'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.product.router');

        am.product.router.ProductRouter = Backbone.Router.extend({
            initialize: function () {
            },
            routes: {
                '': 'index',
                'product': 'index',
                'product/detail/:id': 'detail',
                'product/edit/:id': 'edit'
            },
            index: function () {
                require([
                    'controller/ListController'
                ], function () {
                    this.listController = new am.product.controller.ListController();
                    this.listController.show();
                });
            },
            detail: function (id) {
                require([
                    'controller/DetailController'
                ], function () {
                    this.detailController = new am.product.controller.DetailController(id);
                    this.detailController.show();
                });
            },
            edit: function (id) {
                require([
                    'controller/EditController'
                ], function () {
                    this.editController = new am.product.controller.EditController(id);
                    this.editController.show();
                });
            }
        });
        return am;
    }
);