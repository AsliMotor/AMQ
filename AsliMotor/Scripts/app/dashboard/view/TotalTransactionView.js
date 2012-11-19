define([
    'jquery',
    'underscore',
    'backbone',
    'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.view');
        am.dashboard.view.TotalTransactionView = Backbone.View.extend({
            tagName: 'div',
            className: 'total-transaction row-fluid',
            initialize: function () {
            },
            render: function () {
                var html = "";
                html += "<div class='span12 center'><ul class='stat-boxes'></ul></div>";
                this.$el.html(html);
                var totalTodayTransaction = new am.dashboard.view.TotalTransactionView.Item({
                    model: this.model,
                    dataIndex: 'TotalSoldTransactionToday',
                    title:'Jumlah Penjualan Hari Ini'
                });
                var totalYesterdayTransaction = new am.dashboard.view.TotalTransactionView.Item({
                    model: this.model,
                    dataIndex: 'TotalSoldTransactionYesterday',
                    title: 'Jumlah Penjualan Kemarin'
                });
                var totalSoldTransactionThisMonth = new am.dashboard.view.TotalTransactionView.Item({
                    model: this.model,
                    dataIndex: 'TotalSoldTransactionThisMonth',
                    title: 'Jumlah Penjualan Bulan Ini'
                });
                var totalProductSoldToday = new am.dashboard.view.TotalTransactionView.Item({
                    model: this.model,
                    dataIndex: 'TotalPurchaseTransactionToday',
                    title: 'Jumlah Pembelian Hari Ini'
                });
                var totalProductSoldMonthly = new am.dashboard.view.TotalTransactionView.Item({
                    model: this.model,
                    dataIndex: 'TotalPurchaseTransactionYesterday',
                    title: 'Jumlah Pembelian Kemarin'
                });
                var totalPurchaseThisMonth = new am.dashboard.view.TotalTransactionView.Item({
                    model: this.model,
                    dataIndex: 'TotalPurchaseTransactionThisMonth',
                    title: 'Jumlah Pembelian Bulan Ini'
                });

                $("ul.stat-boxes", this.$el).append(totalTodayTransaction.render().el);
                $("ul.stat-boxes", this.$el).append(totalYesterdayTransaction.render().el);
                $("ul.stat-boxes", this.$el).append(totalSoldTransactionThisMonth.render().el);
                $("ul.stat-boxes", this.$el).append(totalProductSoldToday.render().el);
                $("ul.stat-boxes", this.$el).append(totalProductSoldMonthly.render().el);
                $("ul.stat-boxes", this.$el).append(totalPurchaseThisMonth.render().el);
                
                return this;
            }
        });

        am.dashboard.view.TotalTransactionView.Item = Backbone.View.extend({
            tagName: 'li',
            class: '',
            initialize: function () {
                this.model.on('change', this.render, this);
            },
            render: function () {
                var html = "";
                html += "<div class='value'><strong>" + this.model.get(this.options.dataIndex) + "</strong></div>";
                html += "<div class='title'>" + this.options.title + "</div>";
                this.$el.html(html);
                return this;
            }
        });
    });