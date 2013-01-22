define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/buttontype',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/inputfield/combofield'
], function ($, _, Backbone, ns) {
    ns.define("am.invoice.view");
    am.invoice.view.ChangeTerm = Backbone.View.extend({
        tagName: 'div',
        className: 'change-term',
        initialize: function () {

        },
        render: function () {
            var self = this;
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
                selectedItem: {
                    field: "id",
                    value: "TermId"
                },
                displayItemField: {
                    value: 'id',
                    name: 'Name'
                },
                setModel: function (model, data) {
                    model.set("TermId", data.id);
                }
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

            var termFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [termfield, buttonFormPanel]
            });

            var changeTermDialog = new HomeJS.components.ModalDialog({
                title: 'Ubah Termin Pembayaran',
                model: this.model,
                view: termFormPanel
            });
            changeTermDialog.show();
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
                TermId: this.model.get("TermId")
            };
            $.ajax({
                type: "POST",
                url: "/invoice/changeterm",
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