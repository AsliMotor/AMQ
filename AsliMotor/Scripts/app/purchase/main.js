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
  'router/PurchaseRouter',
  '../../libs/ajaxloading',
  '../../libs/bootstrap/bootstrap-dropdown',
  '../shared/template'
], function ($, _, Backbone, am) {
    $(function () {
        am.Navigation('purchase');
        $.ajaxSetup({
            beforeSend: function () {
                am.tools.ShowAjaxLoading();
            },
            complete: function () {
                am.tools.HideAjaxLoading();
            }
        });

        var router = new am.purchase.router.PurchaseRouter();
        Backbone.history.start({ pushState: true });

        am.eventAggregator.on('showList', function (data) {
            router.navigate("/purchase", true);
        });
        am.eventAggregator.on('showDetail', function (id) {
            router.navigate("/purchase/detail/" + id, true);
        });
        am.eventAggregator.on('editPurchase', function (id) {
            router.navigate("/purchase/edit/" + id, true);
        });
        am.eventAggregator.on('createPurchase', function () {
            router.navigate("/purchase/create", true);
        });
    });
});
