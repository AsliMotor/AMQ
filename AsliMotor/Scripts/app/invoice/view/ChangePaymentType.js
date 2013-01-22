define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/buttontype',
    '../../../libs/homejs/formpanel',
    '../../../libs/Date',
    '../../../libs/homejs/inputfield/datefield',
    '../../../libs/homejs/label',
    '../../../libs/homejs/radiobutton'
], function ($, _, Backbone, ns) {
    ns.define("am.invoice.view");
    am.invoice.view.ChangePaymentType = Backbone.View.extend({
        tagName: 'div',
        className: 'change-tipe-pembayaran',
        initialize: function () {

        },
        render: function () {
            var self = this;
            var RadioButtonCollection = Backbone.Collection.extend();
            var radioButtonCollection = new RadioButtonCollection();
            radioButtonCollection.reset([
                           { id: "credit", title: "Credit", value: "1" },
                           { id: "cash", title: "Cash", value: "2" }
                        ]);

            var paymentTypeView = new HomeJS.components.RadioButton({
                collection: radioButtonCollection,
                model: this.model,
                id: "paymenttype",
                name: "paymenttype",
                title: "Pembayaran",
                dataIndex: "Status",
                setModel: function (model, data) {
                    model.set("Status", data.value);
                }
            });

            this.model.on('change:Status', this.paymentTypeChanged, this);

            var paymentTypeFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [paymentTypeView],
                vertical: true
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

            var bodyFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                class: 'payment-type-body',
                items: []
            });

            var paymentFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                class: "change-payment-type",
                items: [paymentTypeFormPanel, bodyFormPanel, buttonFormPanel]
            });

            var changeUangMukaDialog = new HomeJS.components.ModalDialog({
                title: 'Ubah Uang Muka',
                model: this.model,
                view: paymentFormPanel
            });
            changeUangMukaDialog.show();
            return this;
        },
        paymentTypeChanged: function (data) {
            if (data.get("Status") == "1") {
                $(".paymenttype-detail-form-panel").remove();
                var uangMukaView = new HomeJS.components.TextField({
                    model: this.model,
                    title: 'Uang Muka',
                    type: 'price',
                    placeholder: 'Ketik Jumlah Uang Muka',
                    dataIndex: "UangMuka"
                });
                var sukuBungaView = new HomeJS.components.TextField({
                    model: this.model,
                    title: 'Suku Bunga',
                    type: 'number',
                    placeholder: 'Ketik Suku Bunga',
                    dataIndex: "SukuBunga"
                });
                var lamaAngsuranView = new HomeJS.components.TextField({
                    model: this.model,
                    title: 'Lama Angsuran',
                    type: 'number',
                    placeholder: 'Ketik Lama Angsuran',
                    dataIndex: "LamaAngsuran"
                });
                var TermModel = Backbone.Model.extend();
                var TermCollection = Backbone.Collection.extend({
                    url: "/PaymentTerm/Terms",
                    model: TermModel
                });
                var termColl = new TermCollection();
                termColl.fetch();
                var termfield = new HomeJS.components.ComboField({
                    model: this.model,
                    title: 'Termin Pembayaran',
                    dataIndex: "TermId",
                    collection: termColl,
                    displayItemField: {
                        value: 'id',
                        name: 'Name'
                    },
                    setModel: function (model, data) {
                        model.set("TermId", data.id);
                    }
                });
                var nominalFormPanel = new HomeJS.components.FormPanel({
                    formLayout: HomeJS.components.FormLayout.VERTICAL,
                    items: [uangMukaView, sukuBungaView, lamaAngsuranView, termfield],
                    vertical: true
                });
                var dueDateView = new HomeJS.components.DateField({
                    model: this.model,
                    title: 'Tanggal Jatuh Tempo',
                    date: getCurrentDate(),
                    dataIndex: "DueDate"
                });
                var paymentTypeDetailFormPanel = new HomeJS.components.FormPanel({
                    formLayout: HomeJS.components.FormLayout.VERTICAL,
                    class: "paymenttype-detail-form-panel",
                    items: [nominalFormPanel, dueDateView]
                });
                $(".payment-type-body").html(paymentTypeDetailFormPanel.render().el)
            }
            else if (data.get("Status") == "2") {
                $(".paymenttype-detail-form-panel").remove();
            }
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
                if (this.model.get("Status") == "1") {
                    var self = this;
                    var data = {
                        InvoiceId: this.model.get("InvoiceId"),
                        UangMuka: this.model.get("UangMuka"),
                        LamaAngsuran: this.model.get("LamaAngsuran"),
                        SukuBunga: this.model.get("SukuBunga"),
                        DueDate: this.model.get("DueDate"),
                        TermId: this.model.get("TermId")
                    };
                    $.ajax({
                        type: "POST",
                        url: "/invoice/updatetocredit",
                        data: data,
                        dataType: "json",
                        success: function (data) {
                            if (data.error)
                                HomeJS.components.ErrorAlert(data.message);
                            else
                                self.model.trigger("success");
                        }
                    });
                }
                else if (this.model.get("Status") == "2") {
                    var self = this;
                    var data = {
                        InvoiceId: this.model.get("InvoiceId")
                    };
                    $.ajax({
                        type: "POST",
                        url: "/invoice/updatetocash",
                        data: data,
                        dataType: "json",
                        success: function (data) {
                            if (data.error)
                                HomeJS.components.ErrorAlert(data.message);
                            else
                                self.model.trigger("success");
                        }
                    });
                }
            }
        }
    });
});