"use strict";
define.amd.jQuery = true;
requirejs.config({
    paths: {
        jquery: '../../libs/jquery/jquery-1.7.2.min',
        underscore: '../../libs/underscore/underscore.min',
        backbone: '../../libs/backbone/backbone.min',
        marionette: '../../libs/backbone/backbone.marionette',
        namespace: '../../app/namespace',
        eventAggregator: '../../app/eventAggregator'
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
  '../../app/purchase/view/DetailPurchase'
], function ($, _, Backbone, am) {
    $(function () {
        var Model = Backbone.Model.extend();
        var _model = new Model({ SupplierName: "Denny Wu",
            SupplierBillingAddress: "Setajam",
            NoTelp: "0856658915",
            SupplierInvoiceNo: "0001/10/SI-AM/2012",
            SupplierInvoiceDate: "15-10-2012",
            Merk: "Yamaha",
            NoPolisi: "BP 5038 FO",
            Type: "MIO CW",
            Tahun: "2011",
            NoRangka: "12432567",
            NoMesin: "756432132",
            NoBpkb: "12334254sd",
            Warna: "Merah",
            HargaBeli: 9550000,
            Note: "dkfjgdn dfkjgdfkgdfng djfgkdfgkdnfg e980rerjeoroe fdgjdflgdlgf iofdjogjdf wlewejwljrwkjlr weirjwoerjwoerj weirowjeorjweoirjw weirweghriwuer weurh"
        });
        var detailPurchase = new am.purchase.view.DetailPurchase({ model: _model });
        $("#main-container").html(detailPurchase.render().el);
    });
});
