define([
    'jquery',
    'underscore',
    'backbone',
    'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.view');
        am.dashboard.view.SalesView = Backbone.View.extend({
            className: 'sales-view',
            initialize: function () {

            },
            render: function () {
                this.createTitle();
                this.createBody();
                this.createOutstandingTitle();
                this.createBodyOutstanding();
                return this;
            },
            createTitle: function () {
                var titleHtml = "";
                titleHtml += "<div class='title'>";
                titleHtml += "<span class='icon'><i class='icon-signal'></i></span>";
                titleHtml += "<h5>Penjualan</h5>";
                titleHtml += "</div>";
                this.$el.html(titleHtml);
            },
            createBody: function () {
                var contentHtml = "";
                contentHtml += "<div class='content'>";
                contentHtml += "<div class='row-fluid'>";
                contentHtml += "<div class='span4' id='detail-sales'></div>";
                contentHtml += "<div class='span8' id='chart-sales-container'></div>";
                contentHtml += "</div>";
                contentHtml += "</div>";
                this.$el.append(contentHtml);
            },
            createOutstandingTitle: function () {
                var titleHtml = "";
                titleHtml += "<div class='title'>";
                titleHtml += "<span class='icon'><i class='icon-tag'></i></span>";
                titleHtml += "<h5>Piutang Telah Jatuh Tempo</h5>";
                titleHtml += "<div class='legend'>" +
                            "<div class='outstanding-greater-3'>Besar dari 2 bulan</div>" +
                            "<div class='outstanding-greater-2'>1 - 2 bulan</div>" +
                            "<div class='outstanding-greater-1'>0 - 1 bulan</div>" +
                            "</div>";
                titleHtml += "</div>";
                this.$el.append(titleHtml);
            },
            createBodyOutstanding: function () {
                var contentHtml = "";
                contentHtml += "<div class='content'>";
                contentHtml += "<div class='row-fluid'>";
                contentHtml += "<div class='span12' id='piutang-telah-jatuh-tempo'></div>";
                contentHtml += "</div>";
                contentHtml += "</div>";
                this.$el.append(contentHtml);
            }
        });
    });