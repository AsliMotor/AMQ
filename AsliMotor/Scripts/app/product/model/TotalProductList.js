define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.product.model');
    am.product.model.TotalProductList = Backbone.Model.extend({
        url: "product/totallist"
    });
    return am;
});