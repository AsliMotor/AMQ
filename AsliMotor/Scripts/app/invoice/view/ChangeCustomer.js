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
    am.invoice.view.ChangeCustomer = Backbone.View.extend({
        tagName: 'div',
        className: 'change-customer',
        initialize: function () {

        },
        render: function () {
            var self = this;
            var searchCustView = new HomeJS.components.SearchField({
                url: '/Customer/search',
                model: this.model,
                id: 'customer',
                name: 'customer',
                title: 'Pelanggan',
                placeholder: 'ketik nama atau alamat pelanggan',
                dataIndex: 'CustomerId',
                dataSourceDataIndex: {
                    id: 'id',
                    name: 'Name',
                    value1: 'Address',
                    value2: 'City'
                },
                setModel: function (model, value) {
                    model.set("CustomerId", value.id);
                    model.set("CustomerName", value.Name);
                    model.set("CustomerAddress", value.Address);
                    model.set("City", value.City);
                    model.set("KTPNo", value.KTPNo);
                    model.set("KTPPublisher", value.KTPPublisher);
                    model.set("KTPDate", value.KTPDate);
                    model.set("Job", value.Job);
                    model.set("Gender", value.Gender);
                    var check = model.validateItem("CustomerId");
                    if (check.isValid === false) {
                        utils.addValidationError("CustomerId", check.message);
                    } else {
                        utils.removeValidationError("CustomerId");
                    }
                },
                resetModel: function (model) {
                    model.set({
                        "CustomerAddress": undefined,
                        "CustomerId": undefined,
                        "City": undefined,
                        "KTPNo": undefined,
                        "KTPPublisher": undefined,
                        "KTPDate": undefined,
                        "Job": undefined,
                        "Gender": undefined
                    });
                }
            });
            var alamatCustView = new HomeJS.components.Label({
                title: 'Alamat Pelanggan',
                dataIndex: 'CustomerAddress',
                model: this.model
            });

            var customerView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [searchCustView, alamatCustView],
                vertical: true
            });

            var ktpNoCustView = new HomeJS.components.Label({
                title: 'Nomor KTP',
                dataIndex: 'KTPNo',
                model: this.model
            });

            var ktpPublisherCustView = new HomeJS.components.Label({
                title: 'KTP Dikeluarkan Oleh',
                dataIndex: 'KTPPublisher',
                model: this.model
            });

            var ktpDateCustView = new HomeJS.components.Label({
                title: 'KTP Dikeluarkan Tanggal',
                dataIndex: 'KTPDate',
                model: this.model,
                renderer: function (date) {
                    return date.toDate();
                }
            });

            var ktpView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [ktpNoCustView, ktpPublisherCustView, ktpDateCustView],
                vertical: true
            });

            var cityCustView = new HomeJS.components.Label({
                title: 'Kota',
                dataIndex: 'City',
                model: this.model
            });

            var jobCustView = new HomeJS.components.Label({
                title: 'Pekerjaan',
                dataIndex: 'Job',
                model: this.model
            });

            var genderCustView = new HomeJS.components.Label({
                title: 'Jenis Kelamin',
                dataIndex: 'Gender',
                model: this.model
            });

            var addtionalView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [cityCustView, jobCustView, genderCustView],
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

            var customerFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                class: 'edit-customer-container-dialog',
                items: [customerView,ktpView,addtionalView, buttonFormPanel]
            });

            var changeCustomerDialog = new HomeJS.components.ModalDialog({
                title: 'Ubah Pelanggan',
                model: this.model,
                view: customerFormPanel
            });
            changeCustomerDialog.show();
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
                    CustomerId: this.model.get("CustomerId")
                };
                $.ajax({
                    type: "POST",
                    url: "/invoice/changecustomer",
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