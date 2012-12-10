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
    '../../../libs/homejs/dialog/erroralert',
    '../../../libs/homejs/dialog/ModalDialog'
], function ($, _, Backbone, ns) {
    ns.define("am.manageuser.view");
    am.manageuser.view.AddUser = Backbone.View.extend({
        tagName: 'div',
        className: 'add-user',
        initialize: function () {
        },
        render: function () {
            var self = this;
            var userNameView = new HomeJS.components.TextField({
                model: this.model,
                title: 'User Name',
                type: 'text',
                placeholder: 'Ketik Nama User',
                dataIndex: "UserName"
            });
            var passwordView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Password',
                type: 'password',
                placeholder: 'Ketik Password',
                dataIndex: "Password"
            });
            var confirmPasswordView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Confirm Password',
                type: 'password',
                placeholder: 'Ketik Password',
                dataIndex: "ConfirmPassword"
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
                items: [userNameView, passwordView, confirmPasswordView, buttonFormPanel]
            });

            var changeUangMukaDialog = new HomeJS.components.ModalDialog({
                title: 'Tambah User Baru',
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