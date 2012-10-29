"use strict";
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
  'router/ProductRouter'
], function ($, _, Backbone, am) {
    $(function () {
        var router = new am.product.router.ProductRouter();
        Backbone.history.start({ pushState: true });

        am.eventAggregator.on('showList', function (data) {
            router.navigate("/product", true);
        });
        am.eventAggregator.on('showDetail', function (id) {
            router.navigate("/product/detail/" + id, true);
        });
        am.eventAggregator.on('editProduct', function (id) {
            router.navigate("/product/edit/" + id, true);
        });
    });
});
