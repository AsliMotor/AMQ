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
  '../../libs/homejs/inputfield/combofield'
], function ($, _, Backbone, am) {
    $(function () {
        var Model = Backbone.Model.extend();
        var _model = new Model({ Term: "59abfe79-37b1-49f7-a272-c41a66300fb3" });

        var CollModel = Backbone.Model.extend();
        var Collection = Backbone.Collection.extend({ url: "/PaymentTerm/Terms", model: CollModel });
        var coll = new Collection();
        coll.fetch();

        var datefield = new HomeJS.components.ComboField({
            model: _model,
            title: 'Termin Pembayaran',
            dataIndex: "Term",
            collection: coll,
            selectedItem: {
                field: "id",
                value: "Term"
            },
            displayItemField: {
                value: 'id',
                name: 'Name'
            },
            setModel: function (model, data) {
                model.set("Term", data.id);
            }
        });
        $("#main-container").html(datefield.render().el);
    });
});
