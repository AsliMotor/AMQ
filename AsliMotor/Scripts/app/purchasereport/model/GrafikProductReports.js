define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.purchasereport.model');

    am.purchasereport.model.GrafikProductReport = Backbone.Model.extend();

    am.purchasereport.model.GrafikProductReports = Backbone.Collection.extend({
        url: 'salesreport/grafikproductsalesreport',
        model: am.purchasereport.model.GrafikProductReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});