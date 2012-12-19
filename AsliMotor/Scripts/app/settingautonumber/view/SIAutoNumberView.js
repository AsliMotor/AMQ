define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/radiobutton',
    '../../../libs/homejs/dialog/erroralert',
    '../../../libs/homejs/dialog/confirm',
    '../../../libs/homejs/dialog/successalert'
], function ($, _, Backbone, ns) {
    ns.define("am.settingautonumber.view");
    am.settingautonumber.view.SIAutoNumberView = Backbone.View.extend({
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var self = this;
            var RadioButtonCollection = Backbone.Collection.extend();
            var radioButtonCollection = new RadioButtonCollection();
            radioButtonCollection.reset([
                           { id: "monthly", title: "Bulanan", value: "0" },
                           { id: "yearly", title: "Tahunan", value: "1" }
                        ]);

            var paymentTypeView = new HomeJS.components.RadioButton({
                collection: radioButtonCollection,
                model: this.model,
                id: "mode",
                name: "mode",
                title: "Format",
                dataIndex: "Mode",
                setModel: function (model, data) {
                    model.set("Mode", data.value);
                }
            });

            var prefixView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Prefix',
                placeholder: 'Ketik Prefix',
                dataIndex: "Prefix",
                type: 'text'
            });

            var yesButtonView = new HomeJS.components.Button({
                title: "Simpan",
                model: this.model,
                typeButton: HomeJS.components.ButtonType.Success,
                icon: "icon-ok",
                iconColor: HomeJS.components.ButtonColor.White,
                events: {
                    "click": function (ev) {
                        ev.preventDefault();
                        self.sendCommand();
                    }
                }
            });

            var buttonFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [yesButtonView],
                vertical: true
            });

            var modeFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.HORIZONTAL,
                class: "setting-si-auto-number",
                items: [paymentTypeView, prefixView, buttonFormPanel]
            });

            this.$el.html(modeFormPanel.render().el);
            return this;
        },
        sendCommand: function () {
            var check = this.model.validateAll();
            if (check.isValid === false) {
                utils.displayValidationErrors(check.errors);
                return false;
            }
            else {
                var self = this;
                var data = {
                    Mode: this.model.get("Mode"),
                    Prefix: this.model.get("Prefix")
                };
                $.ajax({
                    type: "POST",
                    url: "/settingautonumber/UpdateAutoNumberSI",
                    data: data,
                    dataType: "json",
                    success: function (data) {
                        if (data.error)
                            HomeJS.components.ErrorAlert(data.message);
                        else {
                            HomeJS.components.SuccessAlert("Pengaturan berhasil simpan");
                        }
                    }
                });
            }
        }
    });
    return am.settingautonumber.view.SIAutoNumberView;
});