define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/buttontype',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/dialog/erroralert'
], function ($, _, Backbone, ns) {
    ns.define("am.invoice.view");
    am.invoice.view.BayarUangAngsuran = Backbone.View.extend({
        tagName: 'div',
        className: 'bayar-uang-angsuran',
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var self = this;
            var paymentDateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Tanggal Pembayaran',
                dataIndex: "Date"
            });
            var creditNoteView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Deposit',
                type: 'price',
                dataIndex: "Deposit",
                readonly: true,
                onshow: function (m) {
                    if (m.get('Deposit') && m.get('Deposit') > 0)
                        return true;
                    return false;
                }
            });
            var totalBulanView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Bulan',
                type: 'number',
                placeholder: 'Ketik Jumlah Yang Ingin DiBayar',
                dataIndex: "TotalBulanYangDiBayar"
            });
            var totalUangAngsuranView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Total Angsuran Bulanan',
                type: 'price',
                dataIndex: "AngsuranBulanan",
                readonly: true,
                onrendervalue: function (m) {
                    var totalYangHarusDiBayar = m.get("AngsuranBulanan") * m.get("TotalBulanYangDiBayar");
                    this.model.set("TotalYangHarusDiBayar", totalYangHarusDiBayar);
                    return totalYangHarusDiBayar;
                }
            });
            var angsuranPlusDendaView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Total Angsuran Bulanan + Denda',
                type: 'price',
                placeholder: '',
                dataIndex: "AngsuranBulanan",
                readonly: true,
                onshow: function (m) {
                    var day = this.model.get("Date").split('-')[0];
                    var month = this.model.get("Date").split('-')[1];
                    var year = this.model.get("Date").split('-')[2];

                    var date = new Date(month + "-" + day + "-" + year);
                    if (m.get('DueDate') && m.get('DueDate').toDateTime() < date)
                        return true;
                    return false;
                },
                onrendervalue: function (m) {
                    var day = this.model.get("Date").split('-')[0];
                    var month = this.model.get("Date").split('-')[1];
                    var year = this.model.get("Date").split('-')[2];
                    var date = new Date(month + "-" + day + "-" + year);
                    var termValue = this.model.get("TermValue");
                    var angsuranBulanan = parseInt(m.get("AngsuranBulanan"));
                    var totalDenda = 0;
                    for (var i = 0; i < m.get("TotalBulanYangDiBayar"); i++) {
                        var dueDate = new Date(m.get("DueDate").toDateTime().setDate(m.get("DueDate").toDateTime().getDate() + (termValue * i)));
                        var timeDiff = Math.abs(dueDate.getTime() - date.getTime());
                        var diffDays = 0;
                        if (dueDate < date)
                            diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                        totalDenda += (angsuranBulanan * 0.005) * diffDays;
                    }

                    var totalYangHarusDiBayar = (angsuranBulanan * parseInt(m.get("TotalBulanYangDiBayar"))) + totalDenda;
                    this.model.set("TotalYangHarusDiBayar", totalYangHarusDiBayar);
                    return totalYangHarusDiBayar;
                }
            });
            var payAmountView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Uang Yang Dibayar',
                type: 'price',
                placeholder: 'Ketik Jumlah Yang Ingin DiBayar',
                dataIndex: "PayAmount"
            });
            var yesButtonView = new HomeJS.components.Button({
                title: "Simpan",
                model: this.model,
                typeButton: HomeJS.components.ButtonType.Success,
                icon: "icon-ok",
                iconColor: HomeJS.components.ButtonColor.White,
                events: {
                    "click": this.sendCommand
                }
            });
            var noBbuttonView = new HomeJS.components.Button({
                title: "Batal",
                typeButton: HomeJS.components.ButtonType.Danger,
                icon: "icon-remove",
                iconColor: HomeJS.components.ButtonColor.White,
                events: {
                    "click": function (ev) {
                        ev.preventDefault();
                        $('#mask , .modal-dialog').fadeOut(300, function () {
                            $('#mask').remove();
                        });
                    }
                }
            });

            var buttonFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [yesButtonView, noBbuttonView],
                vertical: true
            });

            var uangMukaFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [paymentDateView, creditNoteView, totalBulanView, totalUangAngsuranView, angsuranPlusDendaView, payAmountView, buttonFormPanel]
            });

            var changeUangMukaDialog = new HomeJS.components.ModalDialog({
                title: 'Bayar Angsuran',
                model: this.model,
                view: uangMukaFormPanel
            });
            changeUangMukaDialog.show();
            return this;
        },
        sendCommand: function (ev) {
            ev.preventDefault();
            var self = this;
            var check = this.model.validateAll();
            if (check.isValid === false) {
                utils.displayValidationErrors(check.errors);
                return false;
            }
            else {
                var postToServer = function (self) {
                    $('#mask , .modal-dialog').fadeOut(300, function () {
                        $('#mask').remove();
                    });
                    var data = {
                        InvoiceId: self.model.get("InvoiceId"),
                        Date: self.model.get("Date"),
                        TotalBulanYangDiBayar: self.model.get("TotalBulanYangDiBayar"),
                        PayAmount: self.model.get("PayAmount")
                    };
                    $.ajax({
                        type: "POST",
                        url: "/invoice/bayarangsuran",
                        data: data,
                        dataType: "json",
                        success: function (data) {
                            if (data.error)
                                HomeJS.components.ErrorAlert(data.message);
                            else {
                                self.model.trigger("success");
                            }
                        }
                    });
                };
                var totalBulanYangDiBayar = this.model.get("TotalBulanYangDiBayar")
                if (totalBulanYangDiBayar > 1) {
                    if (confirm("Anda yakin ingin membayar total angsuran " + totalBulanYangDiBayar + " bulan?")) {
                        postToServer(self);
                    }
                } else {
                    postToServer(self);
                }
            }

        }
    });
});