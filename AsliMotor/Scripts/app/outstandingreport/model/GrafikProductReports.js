define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.report.model');
   
    am.report.model.GrafikProductReport = Backbone.Model.extend();

    am.report.model.GrafikProductReports = Backbone.Collection.extend({
        url: 'salesreport/grafikproductsalesreport',
        model: am.report.model.GrafikProductReport,
        SumTotal: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Total") }, 0);
        }
    });
    return am;
});