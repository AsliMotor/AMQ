define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.customer.model');
    var History = Backbone.Model.extend();
    am.customer.model.HistoryReports = Backbone.Collection.extend({
        url: '/customer/gethistorycreditnote',
        model: History
    });

    return am;
});