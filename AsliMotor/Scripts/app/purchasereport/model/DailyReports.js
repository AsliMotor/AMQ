define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.purchasereport.model');

    am.purchasereport.model.DailyReport = Backbone.Model.extend();

    am.purchasereport.model.DailyReports = Backbone.Collection.extend({
        url: 'purchasereport/dailypurchasereport',
        model: am.purchasereport.model.DailyReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});