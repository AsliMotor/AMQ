define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.report.model');
   
    am.report.model.YearlyReport = Backbone.Model.extend();

    am.report.model.YearlyReports = Backbone.Collection.extend({
        url: 'salesreport/yearlysalesreport',
        model: am.report.model.YearlyReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});