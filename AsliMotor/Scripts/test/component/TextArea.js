"use strict";
define.amd.jQuery = true;
requirejs.config({
    paths: {
        jquery: '../../libs/jquery/jquery-1.7.2.min',
        underscore: '../../libs/underscore/underscore.min',
        backbone: '../../libs/backbone/backbone.min',
        marionette: '../../libs/backbone/backbone.marionette',
        namespace: '../../app/namespace',
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
  '../../libs/homejs/inputfield/textarea'
], function ($, _, Backbone, am) {
    $(function () {
        var Model = Backbone.Model.extend();
        var _model = new Model({ SupplierName: "BillingAddress" });
        var textarea = new HomeJS.components.TextArea({
            model: _model,
            title: 'Alamat Penjual',
            placeholder: 'Ketik Alamat Penjual',
            required: false,
            size: "input-xlarge",
            dataIndex: "BillingAddress"
        });
        $("#main-container").html(textarea.render().el);
    });
});
