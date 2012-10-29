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
  '../../libs/homejs/formpanel',
  '../../libs/homejs/inputfield/datefield',
  '../../libs/homejs/inputfield/textfield',
  '../../libs/homejs/inputfield/textarea'
], function ($, _, Backbone, am) {
    $(function () {
        var Model = Backbone.Model.extend();
        var _model = new Model({
            SupplierName: "Denny Wu",
            Tanggal: "20-10-2012",
            BillingAddress: "test 123 ",
            Merk: "Yamaha"
        });

        var datefield = new HomeJS.components.DateField({
            model: _model,
            title: 'Tanggal',
            date: _model.get("Tanggal"),
            dataIndex: "Tanggal"
        });

        var textfield = new HomeJS.components.TextField({
            model: _model,
            title: 'Nama Penjual',
            placeholder: 'Ketik Nama Penjual',
            required: true,
            dataIndex: "SupplierName"
        });

        var textarea = new HomeJS.components.TextArea({
            model: _model,
            title: 'Alamat Penjual',
            placeholder: 'Ketik Alamat Penjual',
            required: false,
            size: "input-xlarge",
            dataIndex: "BillingAddress"
        });

        var merkView = new HomeJS.components.TextField({
            model: _model,
            title: 'Merk',
            placeholder: 'Ketik Merk Kendaraan',
            required: true,
            dataIndex: "Merk"
        });

        var header = new HomeJS.components.FormPanel({
            model: _model,
            formLayout: HomeJS.components.FormLayout.VERTICAL,
            items: [datefield, textfield, textarea],
            vertical:true
        });

        var formPanel = new HomeJS.components.FormPanel({
            model: _model,
            formLayout: HomeJS.components.FormLayout.VERTICAL,
            items: [header, merkView]
        });

        $("#main-container").html(formPanel.render().el);
    });
});
