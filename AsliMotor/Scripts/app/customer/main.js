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
  'router/CustomerRouter',
  '../../libs/bootstrap/bootstrap-dropdown',
  '../../libs/ajaxloading',
  '../shared/template'
], function ($, _, Backbone, am) {
    $(function () {
        am.Navigation('customer');
        $.ajaxSetup({
            beforeSend: function () {
                am.tools.ShowAjaxLoading();
            },
            complete: function () {
                am.tools.HideAjaxLoading();
            }
        });

        var router = new am.customer.router.CustomerRouter();
        Backbone.history.start({ pushState: true });

        am.eventAggregator.on('showList', function (data) {
            router.navigate("/customer", true);
        });
        am.eventAggregator.on('editCustomer', function (id) {
            router.navigate("/customer/edit/" + id, true);
        });
        am.eventAggregator.on('createCustomer', function () {
            router.navigate("/customer/create", true);
        });
    });
});
