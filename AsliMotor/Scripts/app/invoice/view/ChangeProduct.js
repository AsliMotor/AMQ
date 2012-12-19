define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/buttontype',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/inputfield/searchfield'
], function ($, _, Backbone, ns) {
    ns.define("am.invoice.view");
    am.invoice.view.ChangeProduct = Backbone.View.extend({
        tagName: 'div',
        className: 'change-product',
        initialize: function () {

        },
        render: function () {
            var self = this;
            var searchProductView = new HomeJS.components.SearchField({
                url: '/product/search',
                model: this.model,
                id: 'product',
                name: 'product',
                title: 'Kendaraan',
                placeholder: 'ketik no polisi, no rangka atau no mesin',
                dataIndex: 'ProductId',
                dataSourceDataIndex: {
                    id: 'id',
                    name: 'NoPolisi',
                    value1: 'NoRangka',
                    value2: 'NoMesin',
                    value3: 'Type'
                },
                setModel: function (model, value) {
                    model.set('ProductId', value.id);
                    model.set('Type', value.Type);
                    model.set('Merk', value.Merk);
                    model.set('NoPolisi', value.NoPolisi);
                    model.set('NoRangka', value.NoRangka);
                    model.set('NoMesin', value.NoMesin);
                    model.set('Tahun', value.Tahun);
                    model.set('Warna', value.Warna);
                    var check = model.validateItem("ProductId");
                    if (check.isValid === false) {
                        utils.addValidationError("ProductId", check.message);
                    } else {
                        utils.removeValidationError("ProductId");
                    }
                },
                resetModel: function (model) {
                    model.set({
                        'Merk': undefined,
                        'Type': undefined,
                        'NoPolisi': undefined,
                        'NoRangka': undefined,
                        'NoMesin': undefined,
                        'Tahun': undefined,
                        'Warna': undefined
                    });
                }
            });

            var merkProductView = new HomeJS.components.Label({
                title: 'Merk',
                dataIndex: 'Merk',
                model: this.model
            });

            var productView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [searchProductView, merkProductView],
                vertical: true
            });

            var noPolisiProductView = new HomeJS.components.Label({
                title: 'Nomor Polisi',
                dataIndex: 'NoPolisi',
                model: this.model
            });

            var noRangkaProductView = new HomeJS.components.Label({
                title: 'Nomor Rangka',
                dataIndex: 'NoRangka',
                model: this.model
            });

            var noMesinProductView = new HomeJS.components.Label({
                title: 'Nomor Mesin',
                dataIndex: 'NoMesin',
                model: this.model
            });

            var noView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [noPolisiProductView, noRangkaProductView, noMesinProductView],
                vertical: true
            });

            var typeProductView = new HomeJS.components.Label({
                title: 'Type',
                dataIndex: 'Type',
                model: this.model
            });

            var tahunProductView = new HomeJS.components.Label({
                title: 'Tahun',
                dataIndex: 'Tahun',
                model: this.model
            });

            var warnaProductView = new HomeJS.components.Label({
                title: 'Warna',
                dataIndex: 'Warna',
                model: this.model
            });

            var additionalView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [typeProductView, tahunProductView, warnaProductView],
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

            var productFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                class: 'edit-product-container-dialog',
                items: [productView, noView, additionalView, buttonFormPanel]
            });

            var changeProductDialog = new HomeJS.components.ModalDialog({
                title: 'Ubah Kendaraan',
                model: this.model,
                view: productFormPanel
            });
            changeProductDialog.show();
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
                    ProductId: this.model.get("ProductId")
                };
                $.ajax({
                    type: "POST",
                    url: "/invoice/changeproduct",
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