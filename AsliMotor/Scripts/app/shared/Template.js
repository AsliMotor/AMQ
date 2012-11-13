define([
    'jquery',
    'underscore',
    'backbone',
    'namespace'
], function ($, _, Backbone, ns) {
    ns.define('am');
    am.Navigation = function (activeElement) {
        var headerHtml = "<div id='header'><h1><a href='#'>Asli Motor</a></h1</div>";
        $(document.body).append(headerHtml);
        $(document.body).append(new am.NavBar().render().el);
        $(document.body).append(new am.SideBar({ activeElement: activeElement }).render().el);
        $(document.body).append("<div id='content'><div id='main-container'></div></div>");
    };

    am.SideBar = Backbone.View.extend({
        tagName: 'div',
        className: '',
        id: 'sidebar',
        initialize: function () {
        },
        render: function () {
            var sidebarHtml = "<ul style='display: block;'>" +
			                            "<li id='dashboard'>" +
                                            "<a href='/'>" +
                                                "<i class='icon icon-home'></i>" +
                                                "<span>Dashboard</span>" +
                                            "</a>" +
                                        "</li>" +
			                            "<li id='invoice'>" +
				                            "<a href='/invoice'>" +
                                                "<i class='icon icon-bookmark'></i>" +
                                                "<span>Penjualan</span>" +
                                            "</a>" +
			                            "</li>" +
			                            "<li id='purchase'>" +
                                            "<a href='/purchase'>" +
                                                "<i class='icon icon-tint'></i>" +
                                                "<span>Pembelian</span>" +
                                            "</a>" +
                                        "</li>" +
			                            "<li class='submenu' id='report'>" +
                                            "<a href='#'>" +
                                                "<i class='icon icon-signal'></i>" +
                                                "<span>Laporan</span>" +
                                                "<span class='label'>3</span>" +
                                            "</a>" +
                                            "<ul>" +
					                            "<li><a href='/report'>Laporan Penjualan</a></li>" +
					                            "<li><a href='/report'>Laporan Piutang</a></li>" +
					                            "<li><a href='/report'>Laporan Keuangan</a></li>" +
				                            "</ul>" +
                                        "</li>" +
			                            "<li id='customer'>" +
                                            "<a href='/customer'>" +
                                                "<i class='icon icon-user'></i>" +
                                                "<span>Pelanggan</span>" +
                                            "</a>" +
                                        "</li>" +
			                            "<li id='product'>" +
                                            "<a href='/product'>" +
                                                "<i class='icon icon-file'></i>" +
                                                "<span>Kendaraan</span>" +
                                            "</a>" +
                                        "</li>" +
		                            "</ul>";
            this.$el.html(sidebarHtml);
            $("#" + this.options.activeElement, this.$el).addClass('active');
            if (this.options.activeElement == "report")
                this.reportClicked();
            return this;
        },
        events: {
            'click #report': 'reportClicked'
        },
        reportClicked: function () {
            if ($("#report", this.$el).attr('class').indexOf('shown') == -1) {
                $("#report ul", this.$el).slideDown();
                $("#report", this.$el).addClass('shown');
            }
            else {
                $("#report ul", this.$el).slideUp();
                $("#report", this.$el).removeClass('shown');
            }
        }
    });

    am.NavBar = Backbone.View.extend({
        tagName: 'div',
        className: 'navbar navbar-inverse',
        id: 'user-nav',
        render: function () {
            var navBarHtml = "<ul class='nav btn-group'>" +
                                    "<li class='btn btn-inverse'>" +
                                        "<a title='' href='#'>" +
                                            "<i class='icon icon-user'></i> " +
                                            "<span class='text'>Profile</span>" +
                                        "</a>" +
                                    "</li>" +
                                    "<li class='btn btn-inverse dropdown' id='menu-messages'>" +
                                        "<a href='#' data-toggle='dropdown' data-target='#menu-messages' class='dropdown-toggle'>" +
                                            "<i class='icon icon-envelope'></i>" +
                                            "<span class='text'>Messages</span>" +
                                            "<span class='label label-important'>5</span>" +
                                            "<b class='caret'></b>" +
                                        "</a>" +
                                        "<ul class='dropdown-menu'>" +
                                            "<li><a class='sAdd' title='' href='#'>new message</a></li>" +
                                            "<li><a class='sInbox' title='' href='#'>inbox</a></li>" +
                                            "<li><a class='sOutbox' title='' href='#'>outbox</a></li>" +
                                            "<li><a class='sTrash' title='' href='#'>trash</a></li>" +
                                        "</ul>" +
                                    "</li>" +
                                    "<li class='btn btn-inverse'>" +
                                        "<a title='' href='#'>" +
                                            "<i class='icon icon-cog'></i>" +
                                            "<span class='text'>Settings</span>" +
                                        "</a>" +
                                    "</li>" +
                                    "<li class='btn btn-inverse'>" +
                                        "<a title='' href='/account/LogOff'>" +
                                            "<i class='icon icon-share-alt'></i>" +
                                            "<span class='text'>Logout</span>" +
                                        "</a>" +
                                    "</li>" +
                                "</ul>";
            this.$el.html(navBarHtml);
            return this;
        }
    });
});