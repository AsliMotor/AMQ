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
  '../../libs/homejs/radiobutton'
], function ($, _, Backbone, am) {
    $(function () {
        var Model = Backbone.Model.extend();
        var RadioButtonModel = Backbone.Model.extend();
        var RadioButtonCollection = Backbone.Collection.extend({ model: RadioButtonModel });

        var model = new Model();
        var radioButtonCollection = new RadioButtonCollection();
        radioButtonCollection.reset([
                           { id: "booking", title: "Booking", value: "0", checked: true },
                           { id: "credit", title: "Credit", value: "1" },
                           { id: "cash", title: "Cash", value: "2" }
                        ]);

        var radioButtonView = new HomeJS.components.RadioButton({
            collection: radioButtonCollection,
            model: model,
            id: "paymenttype",
            name: "paymenttype",
            title: "Pembayaran",
            dataIndex: "Status",
            setModel: function (model, data) {
                model.set("Tax", { id: data.id, value: data.value });
            }
        });

        //model.on('change', modelchanged, this);
        $("#main-container").html(radioButtonView.render().el);

        //        function modelchanged(data) {
        //            if (data.get("Tax").value == "barang1") {
        //                $("#result").html(addDiscountPercent.render().el)
        //            } else {
        //                $("#result").html(addDiscountNominal.render().el)
        //            }
        //        }
    });
});