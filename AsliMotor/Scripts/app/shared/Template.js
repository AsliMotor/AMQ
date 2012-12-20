/// <reference path="RoleName.js" />

define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '/scripts/app/shared/AccountModel.js',
    '/scripts/app/shared/RoleName.js'
], function ($, _, Backbone, ns, AccountModel, RoleName) {
    ns.define('am');
    var accountModel = am.AccountModel().get();
    am.Navigation = function (activeElement) {
        var headerHtml = "<div id='header'><h1><a href='#'>Asli Motor</a></h1</div>";
        $(document.body).append(headerHtml);
        $(document.body).append(new am.NavBar({ model: accountModel }).render().el);
        $(document.body).append(new am.SideBar({ model: accountModel, activeElement: activeElement }).render().el);
        $(document.body).append("<div id='content'><div id='main-container'></div></div>");
    };

    am.SideBar = Backbone.View.extend({
        tagName: 'div',
        className: '',
        id: 'sidebar',
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var userRole = (this.model.get("Role")) ? this.model.get("Role")[0] : null;
            var sidebarHtml = "<ul style='display: block;'>" +
			                    "<li id='dashboard'>" +
                                    "<a href='/'>" +
                                        "<i class='icon icon-home'></i>" +
                                        "<span>Dashboard</span>" +
                                    "</a>" +
                                "</li>";

            if (userRole && RoleName.OWNER_ADMINSALES_CASHIER.indexOf(userRole) >= 0) {
                sidebarHtml += "<li id='invoice'>" +
				                "<a href='/invoice'>" +
                                    "<i class='icon icon-book'></i>" +
                                    "<span>Penjualan</span>" +
                                "</a>" +
			                "</li>";
            }

            if (userRole && RoleName.OWNER_ADMINPURCHASE_CASHIER.indexOf(userRole) >= 0) {
                sidebarHtml += "<li id='purchase'>" +
                                "<a href='/purchase'>" +
                                    "<i class='icon icon-shopping-cart'></i>" +
                                    "<span>Pembelian</span>" +
                                "</a>" +
                            "</li>";
            }

            if (userRole && RoleName.OWNER.indexOf(userRole) >= 0) {
                sidebarHtml += "<li class='submenu' id='report'>" +
                                "<a href='#'>" +
                                    "<i class='icon icon-list-alt'></i>" +
                                    "<span>Laporan</span>" +
                                    "<span class='label'>2</span>" +
                                "</a>" +
                                "<ul>" +
					                "<li><a href='/report'>Laporan Penjualan</a></li>" +
					                //"<li><a href='/outstandingreport'>Laporan Piutang</a></li>" +
                                    //"<li><a href='/angsuranreport'>Laporan Angsuran</a></li>" +
					                //"<li><a href='/report'>Laporan Keuangan</a></li>" +
				                "</ul>" +
                            "</li>";
            }

            if (userRole && RoleName.OWNER_ADMINSALES_CASHIER.indexOf(userRole) >= 0) {
                sidebarHtml += "<li id='customer'>" +
                                "<a href='/customer'>" +
                                    "<i class='icon icon-user'></i>" +
                                    "<span>Pelanggan</span>" +
                                "</a>" +
                            "</li>";
            }

            if (userRole && RoleName.OWNER_ADMINISTRATOR.indexOf(userRole) >= 0) {
                sidebarHtml += "<li class='submenu' id='auditlog'>" +
                                "<a href='#'>" +
                                    "<i class='icon icon-list'></i>" +
                                    "<span>Audit Log</span>" +
                                    "<span class='label'>2</span>" +
                                "</a>" +
                                "<ul>" +
					                "<li><a href='/pricechangelog'>Perubahan Harga Pembelian</a></li>" +
					                "<li><a href='/pricechangelog/invoice'>Perubahan Angsuran Penjualan</a></li>" +
				                "</ul>" +
                            "</li>";
            }

            if (userRole && RoleName.ADMINISTRATOR.indexOf(userRole) < 0) {
                sidebarHtml += "<li id='product'>" +
                                "<a href='/product'>" +
                                    "<i class='icon icon-qrcode'></i>" +
                                    "<span>Kendaraan</span>" +
                                "</a>" +
                            "</li>";
            }
            if (userRole && RoleName.ADMINISTRATOR.indexOf(userRole) >= 0) {
                sidebarHtml += "<li id='manageuser'>" +
                                "<a href='/manageuser'>" +
                                    "<i class='icon icon-user'></i>" +
                                    "<span>Manage User</span>" +
                                "</a>" +
                            "</li>";
            }
            if (userRole && RoleName.ADMINISTRATOR.indexOf(userRole) >= 0) {
                sidebarHtml += "<li id='settingautonumber'>" +
                                "<a href='/settingautonumber'>" +
                                    "<i class='icon icon-cog'></i>" +
                                    "<span>Pengaturan Penomoran</span>" +
                                "</a>" +
                            "</li>";
            }
            sidebarHtml += "</ul>";

            this.$el.html(sidebarHtml);
            $("#" + this.options.activeElement, this.$el).addClass('active');
            if (this.options.activeElement == "report") {
                this.reportClicked();
            }
            if (this.options.activeElement == "auditlog")
                this.auditlogClicked();
            return this;
        },
        events: {
            'click #report': 'reportClicked',
            'click #auditlog': 'auditlogClicked'
        },
        reportClicked: function () {
            if ($("#report", this.$el).length > 0 && $("#report", this.$el).attr('class').indexOf('shown') == -1) {
                $("#report ul", this.$el).slideDown();
                $("#report", this.$el).addClass('shown');
            }
            else {
                $("#report ul", this.$el).slideUp();
                $("#report", this.$el).removeClass('shown');
            }
        },
        auditlogClicked: function () {
            if ($("#auditlog", this.$el).length > 0 && $("#auditlog", this.$el).attr('class').indexOf('shown') == -1) {
                $("#auditlog ul", this.$el).slideDown();
                $("#auditlog", this.$el).addClass('shown');
            }
            else {
                $("#auditlog ul", this.$el).slideUp();
                $("#auditlog", this.$el).removeClass('shown');
            }
        }
    });

    am.NavBar = Backbone.View.extend({
        tagName: 'div',
        className: 'navbar navbar-inverse',
        id: 'user-nav',
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var username = this.model.get("Username") || "-";
            var navBarHtml = "<ul class='nav btn-group pull-right'>" +
                                    "<li class='btn btn-inverse'>" +
                                        "<a title='' href='/organization'>" +
                                            "<i class='icon icon-globe'></i> " +
                                            "<span class='text'>Profil Perusahaan</span>" +
                                        "</a>" +
                                    "</li>" +
            //                                    "<li class='btn btn-inverse dropdown' id='menu-messages'>" +
            //                                        "<a href='#' data-toggle='dropdown' data-target='#menu-messages' class='dropdown-toggle'>" +
            //                                            "<i class='icon icon-envelope'></i>" +
            //                                            "<span class='text'>Messages</span>" +
            //                                            "<span class='label label-important'>5</span>" +
            //                                            "<b class='caret'></b>" +
            //                                        "</a>" +
            //                                        "<ul class='dropdown-menu'>" +
            //                                            "<li><a class='sAdd' title='' href='#'>new message</a></li>" +
            //                                            "<li><a class='sInbox' title='' href='#'>inbox</a></li>" +
            //                                            "<li><a class='sOutbox' title='' href='#'>outbox</a></li>" +
            //                                            "<li><a class='sTrash' title='' href='#'>trash</a></li>" +
            //                                        "</ul>" +
            //                                    "</li>" +
                                    "<li class='btn btn-inverse'>" +
                                        "<a title='' href='/setting'>" +
                                            "<i class='icon icon-cog'></i>" +
                                            "<span class='text'>Pengaturan Akun</span>" +
                                        "</a>" +
                                    "</li>" +
                                    "<li class='btn btn-inverse dropdown'>" +
            //                                        "<a title='' href='/account/LogOff'>" +
            //                                            "<i class='icon icon-share-alt'></i>" +
            //                                            "<span class='text'>Logout</span>" +
            //                                        "</a>" +
                                        "<a href='#' class='dropdown-toggle' data-toggle='dropdown' id='username'>" +
                                            "<i class='icon-user icon-white'></i>" +
                                            "&nbsp;" +
                                            "<span>" + username + "</span>" +
                                            "&nbsp;" +
                                            "<b class='caret'></b>" +
                                        "</a>" +
                                        "<ul class='dropdown-menu'>" +
                                            "<li>" +
                                                "<a href='/Account/LogOff'>" +
                                                    "<i class='icon-off'></i>" +
                                                    "&nbsp;" +
                                                    "Log out" +
                                                    "&nbsp;" +
                                                "</a>" +
                                            "</li>" +
                                        "</ul>" +
                                    "</li>" +
                                "</ul>";
            this.$el.html(navBarHtml);
            return this;
        }
    });
});