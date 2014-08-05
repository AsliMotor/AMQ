define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.purchasereport.model');

    am.purchasereport.model.YearlyReport = Backbone.Model.extend();

    am.purchasereport.model.YearlyReports = Backbone.Collection.extend({
        url: 'purchasereport/yearlypurchasereport',
        model: am.purchasereport.model.YearlyReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});