define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/homejs/inputfield/datefield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/buttontype',
    '../../../libs/homejs/formpanel'
], function ($, _, Backbone, ns) {
    ns.define("am.invoice.view");
    am.invoice.view.ChangeInvoiceDate = Backbone.View.extend({
        tagName: 'div',
        className: 'change-invoicedate',
        initialize: function () {
        },
        render: function () {
            var self = this;
            var invoiceDateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Tanggal',
                dataIndex: "InvoiceDate"
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
                items: [invoiceDateView, buttonFormPanel]
            });

            var changeUangMukaDialog = new HomeJS.components.ModalDialog({
                title: 'Ubah Tanggal Transaksi',
                model: this.model,
                view: uangMukaFormPanel
            });
            changeUangMukaDialog.show();
            return this;
        },
        sendCommand: function (ev) {
            ev.preventDefault();
            var check = this.model.validateAll();
            if (check.isValid === false) {
                utils.displayValidationErrors(check.errors);
                return false;
            }
            else {
                $('#mask , .modal-dialog').fadeOut(300, function () {
                    $('#mask').remove();
                });
                var self = this;
                var data = {
                    InvoiceId: this.model.get("InvoiceId"),
                    InvoiceDate: this.model.get("InvoiceDate")
                };
                $.ajax({
                    type: "POST",
                    url: "/invoice/changeinvoicedate",
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
        }
    });
});