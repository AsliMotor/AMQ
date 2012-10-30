﻿"use strict";
define.amd.jQuery = true;
requirejs.config({
    paths: {
        jquery: '../../libs/jquery/jquery-1.7.2.min',
        underscore: '../../libs/underscore/underscore.min',
        backbone: '../../libs/backbone/backbone.min',
        marionette: '../../libs/backbone/backbone.marionette',
        namespace: '../namespace',
        eventAggregator: '../eventAggregator',
        bootstrap: '../../libs/bootstrap.min',
        bootstrapdatepicker: '../../libs/bootstrap-datepicker'
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
  'router/InvoiceRouter',
  '../../libs/ajaxloading'
], function ($, _, Backbone, am) {
    $(function () {
        $.ajaxSetup({
            beforeSend: function () {
                am.tools.ShowAjaxLoading();
            },
            complete: function () {
                am.tools.HideAjaxLoading();
            }
        });

        var router = new am.invoice.router.InvoiceRouter();
        Backbone.history.start({ pushState: true });

        am.eventAggregator.on('showList', function (data) {
            router.navigate("/invoice", true);
        });
        am.eventAggregator.on('showDetail', function (id) {
            router.navigate("/invoice/detail/" + id, true);
        });
        am.eventAggregator.on('editPurchase', function (id) {
            router.navigate("/invoice/edit/" + id, true);
        });
        am.eventAggregator.on('createInvoice', function () {
            router.navigate("/invoice/create", true);
        });
    });
});