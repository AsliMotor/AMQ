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
  '../../libs/homejs/inputfield/searchfield'
], function ($, _, Backbone, am) {
    $(function () {
        var Model = Backbone.Model.extend();
        var _model = new Model();
        var searchField = new HomeJS.components.SearchField({
            url: '/Customer/search',
            model: _model,
            //setValue: this.model.get("BranchName"),
            id: 'customer',
            name: 'customer',
            title: 'Pelanggan',
            dataIndex: 'Customer',
            dataSourceDataIndex: {
                id: 'id',
                name: 'Name',
                value1: 'Address',
                value2: 'City'
            },
            setModel: function (model, value) {
                model.set("TransferId", "fef7f18b-a1c0-4a02-89c8-1606f27b6f29");
                model.set("BranchId", value._id);
                model.set("BranchName", value.Name);
                model.set("Address", value.Address);
            },
            resetModel: function (model) {
                if (model.get("_id")) {
                    model.set({ "_id": "" });
                }
            },
            additionalInfo: function (data) {
                //var html = data._id + '<br>' + data.Address;
                //return html;
            }
        });
        $("#main-container").html(searchField.render().el);
    });
});
