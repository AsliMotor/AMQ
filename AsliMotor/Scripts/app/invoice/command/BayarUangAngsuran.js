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
            DueDate: "",
            TermValue: 0,
            Deposit: 0,
            TotalBulanYangDiBayar: 1,
            TotalYangHarusDiBayar: 0,
            PayAmount: 0
        },
        initialize: function () {
            this.validators = {};
            this.validators.PayAmount = function (value, model) {
                //return (value < 1) ? { isValid: false, message: "PayAmount Har"} : { isValid: true };
                if (value < 1)
                    return { isValid: false, message: "Total yang dibayar harus diisi" };
                else if (value < (model.get("TotalYangHarusDiBayar") - model.get("Deposit")))
                    return { isValid: false, message: "Total yang dibayar tidak mencukupi" };
                else
                    return { isValid: true };
            };
            this.validators.TotalBulanYangDiBayar = function (value, model) {
                return (value < 1) ? { isValid: false, message: "Total bulan yang dibayar tidak boleh kecil dari 1"} : { isValid: true };
            };
        },
        validateItem: function (key) {
            return (this.validators[key]) ? this.validators[key](this.get(key), this) : { isValid: true };
        },
        validateAll: function () {
            var errors = [];
            for (var key in this.validators) {
                if (this.validators.hasOwnProperty(key)) {
                    var check = this.validators[key](this.get(key), this);
                    if (check.isValid === false) {
                        var e = {};
                        e.field = key;
                        e.message = check.message;
                        errors.push(e);
                    }
                }
            }
            return _.size(errors) > 0 ? { isValid: false, errors: errors} : { isValid: true };
        }
    });

    am.invoice.command.bayarUangAngsuranCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var bayarUangAngsuranCommand = new am.invoice.command.BayarUangAngsuran({
                InvoiceId: model.get("id"),
                AngsuranBulanan: model.get("AngsuranBulanan"),
                DueDate: model.get("DueDate"),
                Deposit: model.get("CustomerDeposit"),
                TermValue: model.get("TermValue")
            });
            return bayarUangAngsuranCommand;
        }
        that.createCommand = createCommand;
        return that;
    };
});