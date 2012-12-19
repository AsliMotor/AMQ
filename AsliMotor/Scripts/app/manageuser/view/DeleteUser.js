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
    am.manageuser.view.DeleteUser = Backbone.View.extend({
        tagName: 'div',
        className: 'delete-user',
        initialize: function () {
        },
        render: function () {
            var self = this;
            var test = Backbone.View.extend({
                render: function () {
                    var html = "<span>Anda Yakin ingin hapus user ini ?</span>"
                    this.$el.html(html);
                    return this;
                }
            });
            var text = new test();
            var yesButtonView = new HomeJS.components.Button({
                title: "Ya",
                model: this.model,
                typeButton: HomeJS.components.ButtonType.Success,
                icon: "icon-ok",
                iconColor: HomeJS.components.ButtonColor.White,
                events: {
                    "click": this.sendCommand
                }
            });
            var noBbuttonView = new HomeJS.components.Button({
                title: "Tidak",
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
                items: [text, buttonFormPanel]
            });

            var changeUangMukaDialog = new HomeJS.components.ModalDialog({
                title: 'Delete User',
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
                Username: this.model.get("Username")
            };
            $.ajax({
                type: "POST",
                url: "/manageuser/deleteuser",
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