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
  'router/StatementReportRouter',
  '../../libs/ajaxloading',
  '../../libs/bootstrap/bootstrap-dropdown',
  '../shared/template'
], function ($, _, Backbone, am) {
    $(function () {
        am.Navigation('statementreport');
        $("#main-container").html("<div class='container-fluid'><div class='row-fluid'><div class='span2' id='menu-sidebar'></div>" +
                                  "<div id='main-content' class='span10'></div></div></div>");
        $.ajaxSetup({
            beforeSend: function () {
                am.tools.ShowAjaxLoading();
            },
            complete: function () {
                am.tools.HideAjaxLoading();
            }
        });

        var router = new am.statementreport.router.StatementReportRouter();
        Backbone.history.start({ pushState: true });
    });
});
