"use strict";
define.amd.jQuery = true;
requirejs.config({
    paths: {
        jquery: '../../libs/jquery/jquery-1.7.2.min',
        underscore: '../../libs/underscore/underscore.min',
        backbone: '../../libs/backbone/backbone.min',
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
  '../../libs/ajaxloading',
  '../shared/template'
], function ($, _, Backbone, am) {
    $(function () {
        am.Navigation('invoice');
        $.ajaxSetup({
            beforeSend: function () {
                am.tools.ShowAjaxLoading();
            },
            complete: function () {
                am.tools.HideAjaxLoading();
            }
        });
        require.onLoad = function () {
            alert("test");
        };
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
