define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.report.model');
   
    am.report.model.RateProductReport = Backbone.Model.extend();

    am.report.model.RateProductReports = Backbone.Collection.extend({
        url: 'salesreport/rateproductsalesreport',
        model: am.report.model.RateProductReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});