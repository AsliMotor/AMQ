define(['jquery',
'underscore',
'backbone',
'namespace',
'controller/ListController'],
    function ($, _, Backbone, ns) {
        ns.define('am.manageuser.router');

        am.manageuser.router.ManageUserRouter = Backbone.Router.extend({
            initialize: function () {
            },
            routes: {
                'manageuser': 'index'
            },
            index: function () {
                this.listController = new am.manageuser.controller.ListController();
                this.listController.show();
            }
        });
        return am.manageuser.router.ManageUserRouter;
    }
);