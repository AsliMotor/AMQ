define([
    'jquery',
    'underscore',
    'backbone',
    'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.model');
        am.dashboard.model.SalesReport = Backbone.Model.extend({
            url: '/home/salesreport',
            defaults: {
                TodaySales: 0,
                YesterdaySales: 0,
                ThisMonthSales: 0,
                YesterdayMonthSales: 0,
                ThisYearSales: 0,
                TotalOutstanding: 0
            }
        });
    });