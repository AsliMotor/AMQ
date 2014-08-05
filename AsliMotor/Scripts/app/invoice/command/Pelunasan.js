define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');

    am.invoice.command.Pelunasan = Backbone.Model.extend({
        url: '/invoice/pelunasan',
        defaults: {
            InvoiceId: "",
            Outstanding: 0,
            AngsuranBulanan: 0,
            Date: getCurrentDate(),
            SisaCicilan: 0,
            BanyakCicilan: 0,
            TermValue: 0,
            BanyakCicilanTerbayar: 0,
            DueDate: ""
        }
    });

    am.invoice.command.pelunasanCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var pelunasanCommand = new am.invoice.command.Pelunasan({
                InvoiceId: model.get("id"),
                AngsuranBulanan: model.get("AngsuranBulanan"),
                DueDate: model.get("DueDate"),
                SisaCicilan: model.get("SisaCicilan"),
                BanyakCicilan: model.get("BanyakCicilan"),
                Outstanding: model.get("Outstanding"),
                TermValue: model.get("TermValue"),
                BanyakCicilanTerbayar: model.get("BanyakCicilanTerbayar")
            });
            return pelunasanCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});