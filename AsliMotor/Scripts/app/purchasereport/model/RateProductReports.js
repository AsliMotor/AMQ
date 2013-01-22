define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.purchasereport.model');

    am.purchasereport.model.RateProductReport = Backbone.Model.extend();

    am.purchasereport.model.RateProductReports = Backbone.Collection.extend({
        url: 'salesreport/rateproductsalesreport',
        model: am.purchasereport.model.RateProductReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});