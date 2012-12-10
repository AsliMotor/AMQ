"use strict";
define.amd.jQuery = true;
requirejs.config({
    paths: {
        jquery: '../libs/jquery/jquery-1.7.2.min',
        underscore: '../libs/underscore/underscore.min',
        backbone: '../libs/backbone/backbone.min',
        marionette: '../libs/backbone/backbone.marionette',
        namespace: '../app/namespace',
        eventAggregator: '../app/eventAggregator'
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
  '/scripts/libs/homejs/WebCamera.js'
], function ($, _, Backbone, am, WebCamera) {
    $(function () {
        var webCamera = new HomeJS.components.WebCamera({
                title: 'Web Camera',
                url:'/test/UploadCustomerImage'
            });
        webCamera.render().show();
    });
});
