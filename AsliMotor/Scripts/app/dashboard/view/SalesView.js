define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '/scripts/app/shared/RoleName.js'],
    function ($, _, Backbone, ns, RoleName) {
        ns.define('am.dashboard.view');
        am.dashboard.view.SalesView = Backbone.View.extend({
            className: 'sales-view',
            initialize: function () {
                this.model.on('change', this.renderByRole, this);
            },
            renderByRole: function () {
                var userRole = (this.model.get("Role")) ? this.model.get("Role")[0] : null;
                if (userRole && RoleName.OWNER.indexOf(userRole) < 0) {
                    $("#title-penjualan").remove();
                    $("#content-penjualan").remove();
                }
                if (userRole && RoleName.OWNER_ADMINSALES.indexOf(userRole) < 0) {
                    $("#title-piutang").remove();
                    $("#content-piutang").remove();
                }
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
                titleHtml += "<div class='title' id='title-penjualan'>";
                titleHtml += "<span class='icon'><i class='icon-signal'></i></span>";
                titleHtml += "<h5>Penjualan</h5>";
                titleHtml += "</div>";
                this.$el.html(titleHtml);
            },
            createBody: function () {
                var contentHtml = "";
                contentHtml += "<div class='content' id='content-penjualan'>";
                contentHtml += "<div class='row-fluid'>";
                contentHtml += "<div class='span4' id='detail-sales'></div>";
                contentHtml += "<div class='span8' id='chart-sales-container'></div>";
                contentHtml += "</div>";
                contentHtml += "</div>";
                this.$el.append(contentHtml);
            },
            createOutstandingTitle: function () {
                var titleHtml = "";
                titleHtml += "<div class='title' id='title-piutang'>";
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
                contentHtml += "<div class='content' id='content-piutang'>";
                contentHtml += "<div class='row-fluid'>";
                contentHtml += "<div class='span12' id='piutang-telah-jatuh-tempo'></div>";
                contentHtml += "</div>";
                contentHtml += "</div>";
                this.$el.append(contentHtml);
            }
        });
    });