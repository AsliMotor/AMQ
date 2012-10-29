define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/inputfield/datefield',
    '../../../libs/homejs/inputfield/searchfield',
    '../../../libs/homejs/label',
    '../../../libs/homejs/radiobutton'
], function ($, _, Backbone, ns, am) {
    ns.define("am.invoice.view");
    am.invoice.view.CreateInvoice = Backbone.View.extend({
        className: "create-invoice",
        initialize: function () {
            this.createCustomerFormPanel();
            this.createProductFormPanel();
            this.createPaymentFormPanel();
        },
        render: function () {
            var invoiceDate = this.model.get("InvoiceDate") ? this.model.get("InvoiceDate") : getCurrentDate();
            this.model.set("InvoiceDate", invoiceDate);
            var invoiceDateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Tanggal Penjualan',
                date: invoiceDate,
                dataIndex: "InvoiceDate"
            });

            var dateFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [invoiceDateView]
            });

            var formPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [dateFormPanel, this.customerFormPanel, this.productFormPanel, this.paymentFormPanel]
            });

            this.$el.html(formPanel.render().el);
            return this;
        },
        createPaymentFormPanel: function () {
            var RadioButtonCollection = Backbone.Collection.extend();
            var radioButtonCollection = new RadioButtonCollection();
            radioButtonCollection.reset([
                           { id: "booking", title: "Booking", value: "0" },
                           { id: "credit", title: "Credit", value: "1" },
                           { id: "cash", title: "Cash", value: "2", checked: true }
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

            var hargaJualView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Harga Jual',
                type: 'Number',
                placeholder: 'Ketik Jumlah Harga Jual',
                dataIndex: "Price"
            });
            this.model.on('change:Status', this.paymentTypeChanged, this);

            var hargaAndPaymentTypeFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [hargaJualView, paymentTypeView],
                vertical: true
            });

            this.paymentFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                class: "payment-form-panel",
                items: [hargaAndPaymentTypeFormPanel]
            });
        },
        paymentTypeChanged: function (data) {
            if (data.get("Status") == "0") {
                $(".paymenttype-detail-form-panel").remove();
                var uangTandaJadiView = new HomeJS.components.TextField({
                    model: this.model,
                    title: 'Uang Tanda Jadi',
                    type: 'number',
                    placeholder: 'Ketik Jumlah Uang Tanda Jadi',
                    dataIndex: "DebitNote"
                });
                var paymentTypeDetailFormPanel = new HomeJS.components.FormPanel({
                    formLayout: HomeJS.components.FormLayout.VERTICAL,
                    class: "paymenttype-detail-form-panel",
                    items: [uangTandaJadiView]
                });
                $(".payment-form-panel").append(paymentTypeDetailFormPanel.render().el)
            }
            else if (data.get("Status") == "1") {
                $(".paymenttype-detail-form-panel").remove();
                var uangMukaView = new HomeJS.components.TextField({
                    model: this.model,
                    title: 'Uang Muka',
                    type: 'number',
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
                var nominalFormPanel = new HomeJS.components.FormPanel({
                    formLayout: HomeJS.components.FormLayout.VERTICAL,
                    items: [uangMukaView, sukuBungaView, lamaAngsuranView],
                    vertical:true
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
                $(".payment-form-panel").append(paymentTypeDetailFormPanel.render().el)
            }
            else if (data.get("Status") == "2") {
                $(".paymenttype-detail-form-panel").remove();
            }
        },
        createProductFormPanel: function () {
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
                    model.set('NoTahun', value.Tahun);
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

            this.productFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                class: 'edit-product-container',
                items: [productView, noView, additionalView]
            });
        },
        createCustomerFormPanel: function () {
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

            this.customerFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                class: 'edit-customer-container',
                items: [customerView,
                    ktpView,
                    addtionalView]
            });
        }
    });

    return am;
});