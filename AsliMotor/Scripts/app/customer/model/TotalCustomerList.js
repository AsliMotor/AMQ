define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.customer.model');
    am.customer.model.TotalCustomerList = Backbone.Model.extend({
        url:"/customer/totallist"
});

return am;
});