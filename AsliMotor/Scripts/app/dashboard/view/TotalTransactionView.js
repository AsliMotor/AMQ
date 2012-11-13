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
                    dataIndex: 'TotalDailyTransaction',
                    title:'Jumlah Penjualan Hari Ini'
                });
                var totalThisMonthTransaction = new am.dashboard.view.TotalTransactionView.Item({
                    model: this.model,
                    dataIndex: 'TotalDailyTransaction',
                    title: 'Jumlah Penjualan Bulan Ini'
                });
                var totalProductSoldToday = new am.dashboard.view.TotalTransactionView.Item({
                    model: this.model,
                    dataIndex: 'TotalDailyTransaction',
                    title: 'Jumlah Motor Yang Terjual Hari Ini'
                });
                var totalProductSoldMonthly = new am.dashboard.view.TotalTransactionView.Item({
                    model: this.model,
                    dataIndex: 'TotalDailyTransaction',
                    title: 'Jumlah Motor Yang Terjual Bulan Ini'
                });

                $("ul.stat-boxes", this.$el).append(totalTodayTransaction.render().el);
                $("ul.stat-boxes", this.$el).append(totalThisMonthTransaction.render().el);
                $("ul.stat-boxes", this.$el).append(totalProductSoldToday.render().el);
                $("ul.stat-boxes", this.$el).append(totalProductSoldMonthly.render().el);
                
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