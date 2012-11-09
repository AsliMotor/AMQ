define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.report.model');
   
    am.report.model.MonthlyReport = Backbone.Model.extend();

    am.report.model.MonthlyReports = Backbone.Collection.extend({
        url: 'salesreport/monthlysalesreport',
        model: am.report.model.MonthlyReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});