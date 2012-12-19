define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/inputfield/textarea',
    '../../../libs/homejs/buttontype',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/dialog/erroralert',
    '../../../libs/homejs/dialog/ModalDialog'
], function ($, _, Backbone, ns) {
    ns.define("am.organization.view");
    am.organization.view.ChangeProfile = Backbone.View.extend({
        tagName: 'div',
        className: 'change-profile',
        initialize: function () {
        },
        render: function () {
            var self = this;
            var nameView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nama Perusahaan',
                type: 'text',
                placeholder: 'Ketik Nama Perusahaan',
                dataIndex: "OrganizationName"
            });
            var addressView = new HomeJS.components.TextArea({
                model: this.model,
                title: 'Alamat Perusahaan',
                placeholder: 'Ketik Alamat Perusahaan',
                dataIndex: "OrganizationAddress"
            });
            var cityView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Kota',
                type: 'text',
                placeholder: 'Ketik Kota Perusahaan',
                dataIndex: "City"
            });
            var countryView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Negara',
                type: 'text',
                placeholder: 'Ketik Negara Perusahaan',
                dataIndex: "Country"
            });
            var telpView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Telpon',
                type: 'text',
                placeholder: 'Ketik Telpon Perusahaan',
                dataIndex: "Telp"
            });
            var pimpinanView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nama Pimpinan',
                type: 'text',
                placeholder: 'Ketik Nama Pimpinan',
                dataIndex: "Pimpinan"
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
                items: [nameView, addressView, cityView, countryView, telpView, pimpinanView, buttonFormPanel]
            });

            var changeUangMukaDialog = new HomeJS.components.ModalDialog({
                title: 'Ubah Profil Perusahaan',
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
                this.model.save({}, {
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