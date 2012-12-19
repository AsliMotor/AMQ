define(['jquery',
'underscore',
'backbone',
'namespace'],
    function ($, _, Backbone, ns, sidebarController) {
        ns.define('am.organization.router');
        am.organization.router.OrganizationRouter = Backbone.Router.extend({
            initialize: function () {
                var self = this;
            },
            routes: {
                'organization': 'index',
                'Organization': 'index'
            },
            index: function () {
                require(['controller/OrganizationController'], function () {
                    var orgController = new am.organization.controller.OrganizationController();
                    orgController.show();
                });
            }
        });
        return am;
    }
);