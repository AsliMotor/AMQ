define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.purchase.model');
    am.purchase.model.TotalPurchaseList = Backbone.Model.extend({
        url:"purchase/totallist"
});

return am;
});