"use strict";
define.amd.jQuery = true;
requirejs.config({
    paths: {
        jquery: '../../libs/jquery/jquery-1.7.2.min',
        underscore: '../../libs/underscore/underscore.min',
        backbone: '../../libs/backbone/backbone.min',
        namespace: '../namespace',
        eventAggregator: '../eventAggregator',
        bootstrap: '../../libs/bootstrap/bootstrap.min',
        bootstrapdatepicker: '../../libs/bootstrap/bootstrap-datepicker'
    },
    shim: {
        underscore: {
            exports: '_'
        },
        backbone: {
            deps: ["underscore", "jquery"],
            exports: "Backbone"
        },
        marionette: {
            deps: ['backbone']
        }
    }
});

define([
  'jquery',
  'underscore',
  'backbone',
  'eventAggregator',
  'router/DashboardRouter',
  '../../libs/ajaxloading',
  '../../libs/bootstrap/bootstrap-dropdown',
  '../shared/template'
], function ($, _, Backbone, am) {
    $(function () {
        am.Navigation('dashboard');
        $.ajaxSetup({
            beforeSend: function () {
                am.tools.ShowAjaxLoading();
            },
            complete: function () {
                am.tools.HideAjaxLoading();
            }
        });

        var router = new am.dashboard.router.DashboardRouter();
        Backbone.history.start({ pushState: true });

        am.eventAggregator.on('showDashboard', function (data) {
            router.navigate("/dashboard", true);
        });
        am.eventAggregator.on('showDetailInvoice', function (id) {
            window.open("/invoice/detail/" + id, '', null, null);
        });
    });
});
