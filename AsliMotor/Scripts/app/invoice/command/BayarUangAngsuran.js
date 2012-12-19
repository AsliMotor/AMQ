define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');

    am.invoice.command.BayarUangAngsuran = Backbone.Model.extend({
        url: '/invoice/bayaruangangsuran',
        defaults: {
            InvoiceId: "",
            AngsuranBulanan: 0,
            Date: getCurrentDate(),
            DueDate: ""
        }
    });

    am.invoice.command.bayarUangAngsuranCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var bayarUangAngsuranCommand = new am.invoice.command.BayarUangAngsuran({
                InvoiceId: model.get("id"),
                AngsuranBulanan: model.get("AngsuranBulanan"),
                DueDate: model.get("DueDate")
            });
            return bayarUangAngsuranCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});