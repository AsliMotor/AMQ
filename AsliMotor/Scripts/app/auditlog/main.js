﻿"use strict";
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
  'router/AuditLogRouter',
  '../../libs/bootstrap/bootstrap-dropdown',
  '../../libs/ajaxloading',
  '../shared/template'
], function ($, _, Backbone, am) {
    $(function () {
        am.Navigation('auditlog');
        $.ajaxSetup({
            beforeSend: function () {
                am.tools.ShowAjaxLoading();
            },
            complete: function () {
                am.tools.HideAjaxLoading();
            }
        });

        var router = new am.auditlog.router.AuditLogRouter();
        Backbone.history.start({ pushState: true });

        am.eventAggregator.on('showList', function (data) {
            router.navigate("/auditlog", true);
        });
    });
});
