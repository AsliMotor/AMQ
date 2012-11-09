define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.report.model');
   
    am.report.model.DailyReport = Backbone.Model.extend();
   
    am.report.model.DailyReports = Backbone.Collection.extend({
        url: 'salesreport/dailysalesreport',
        model: am.report.model.DailyReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});