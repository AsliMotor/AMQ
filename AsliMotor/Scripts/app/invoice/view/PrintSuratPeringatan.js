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
    am.invoice.view.PrintSuratPeringatan = Backbone.View.extend({
        tagName: 'div',
        className: 'PrintSuratPeringatan',
        initialize: function () {
        },
        render: function () {
            var self = this;
            var dateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Tanggal Terbit Surat',
                dataIndex: "Date"
            });
            var yesButtonView = new HomeJS.components.Button({
                title: "Cetak",
                model: this.model,
                typeButton: HomeJS.components.ButtonType.Success,
                icon: "icon-ok",
                iconColor: HomeJS.components.ButtonColor.White,
                events: {
                    "click": this.print
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

            var printSuratPeringatanFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [dateView, buttonFormPanel]
            });

            var printSuratPeringatanFormPanelDialog = new HomeJS.components.ModalDialog({
                title: 'Cetak Surat Peringatan/Penarikan',
                model: this.model,
                view: printSuratPeringatanFormPanel
            });
            printSuratPeringatanFormPanelDialog.show();
            return this;
        },
        print: function (ev) {
            ev.preventDefault();
            var id = this.model.get("Id");
            var date = this.model.get("Date");

            $('#mask , .modal-dialog').fadeOut(300, function () {
                $('#mask').remove();
            });
            window.open("/invoice/PrintSuratPeringatan?id=" + id + "&date=" + date, 'Surat Penarikan Kendaraan', null, null);
        }
    });
});