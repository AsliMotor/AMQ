define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.purchasereport.model');

    am.purchasereport.model.MonthlyReport = Backbone.Model.extend();

    am.purchasereport.model.MonthlyReports = Backbone.Collection.extend({
        url: 'purchasereport/monthlypurchasereport',
        model: am.purchasereport.model.MonthlyReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});