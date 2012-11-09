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

        },
        render: function () {
            var self = this;
            var paymentDateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Tanggal Pembayaran',
                dataIndex: "Date"
            });
            var uangMukaView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Angsuran Bulanan',
                type: 'price',
                placeholder: 'Ketik Jumlah Angsuran Bulanan',
                dataIndex: "AngsuranBulanan",
                readonly: true
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
                items: [paymentDateView, uangMukaView, buttonFormPanel]
            });

            var changeUangMukaDialog = new HomeJS.components.ModalDialog({
                title: 'Ubah Uang Muka',
                model: this.model,
                view: uangMukaFormPanel
            });
            changeUangMukaDialog.show();
            return this;
        },
        sendCommand: function (ev) {
            ev.preventDefault();
            $('#mask , .modal-dialog').fadeOut(300, function () {
                $('#mask').remove();
            });
            var self = this;
            var data = {
                InvoiceId: this.model.get("InvoiceId"),
                Date: this.model.get("Date")
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
        }
    });
});