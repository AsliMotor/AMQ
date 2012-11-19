define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Currency'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.view');
        am.dashboard.view.DetailSalesView = Backbone.View.extend({
            tagName: 'ul',
            className: 'detail-sales',
            initialize: function () {
                this.model.on('change', this.render, this);
            },
            render: function () {
                this.createDailySales();
                this.createYesterdaySales();
                this.createMonthlySales();
                this.createYearlySales();
                this.createTotalPiutang();
                return this;
            },
            createDailySales: function () {
                this.$el.html("<li><strong>" + this.model.get("TodaySales").toCurrency() + "</strong><div><small>(Penjualan Hari Ini)</small><div></li>");
            },
            createYesterdaySales: function () {
                this.$el.append("<li><strong>" + this.model.get("YesterdaySales").toCurrency() + "</strong><div></i><small>(Penjualan Kemarin)</small><div></li>");
            },
            createMonthlySales: function () {
                this.$el.append("<li><strong>" + this.model.get("ThisMonthSales").toCurrency() + "</strong><div></i><small>(Penjualan Bulan Ini)</small><div></li>");
            },
            createYearlySales: function () {
                this.$el.append("<li><strong>" + this.model.get("ThisYearSales").toCurrency() + "</strong><div></i><small>(Penjualan Tahun Ini)</small><div></li>");
            },
            createTotalPiutang: function () {
                this.$el.append("<li><strong>" + this.model.get("TotalOutstanding").toCurrency() + "</strong><div><small>(Total Piutang)</small><div></li>");
            }
        });
    });