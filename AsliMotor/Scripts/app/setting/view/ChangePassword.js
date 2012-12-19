define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/dialog/erroralert',
    '../../../libs/homejs/dialog/successalert'
], function ($, _, Backbone, ns, am) {
    ns.define("am.setting.view");
    am.setting.view.ChangePassword = Backbone.View.extend({
        className: "change-password",
        initialize: function () {
        },
        render: function () {
            var oldPassView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Password Lama',
                placeholder: 'Ketik Password',
                dataIndex: "OldPassword",
                type:'password'
            });

            var newPassView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Password Baru',
                placeholder: 'Ketik Password Baru',
                dataIndex: "NewPassword",
                type: 'password'
            });

            var confirmNewPassView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Konfirmasi Password Baru',
                placeholder: 'Ketik Password Baru',
                dataIndex: "ConfirmNewPassword",
                type: 'password'
            });

            var yesButtonView = new HomeJS.components.Button({
                title: "Ubah",
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
                        alert("cancel");
                    }
                }
            });

            var buttonFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [yesButtonView],
                vertical: true
            });

            var formPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.HORIZONTAL,
                items: [oldPassView, newPassView, confirmNewPassView, buttonFormPanel]
            });

            this.$el.html(formPanel.render().el);
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
                this.model.save({}, {
                    success: function (T, resp) {
                        if (resp.error)
                            HomeJS.components.ErrorAlert(resp.message);
                        else {
                            HomeJS.components.SuccessAlert("Password berhasil diubah");
                        }
                    }
                });
            }
        }
    });

    return am.setting.view.ChangePassword;
});