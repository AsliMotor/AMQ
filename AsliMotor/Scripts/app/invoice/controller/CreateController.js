define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/Invoice',
        'view/CreateInvoice',
        '../../../libs/Animation',
        '../../../libs/homejs/DetailPanel'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.invoice.controller');
        am.invoice.controller.CreateController = function (id) {
            var invoiceModel;
            var createInvoice;
            var toolbar;
            var detailPanel;

            var show = function () {
                loadModel();
                createEditInvoicePanel();
                createToolbar();
                createDetailPanel();
                am.tools.Animation(detailPanel.render().el, "#main-container");
            };

            var loadModel = function () {
                invoiceModel = new am.invoice.model.Invoice();
            };

            var createEditInvoicePanel = function () {
                createInvoice = new am.invoice.view.CreateInvoice({ model: invoiceModel });
            };

            var createToolbar = function () {
                toolbar = new HomeJS.components.DetailPanel.Toolbar({
                    model: invoiceModel,
                    title: '',
                    buttons: [{
                        title: "Simpan",
                        tooltip: "Simpan transaksi penjualan",
                        iconClass: 'icon-ok-sign',
                        id: "save",
                        action: function () {
                            var self = this;
                            var check = invoiceModel.validateAll();
                            if (check.isValid === false) {
                                utils.displayValidationErrors(check.errors);
                                return false;
                            }
                            else {
                                if (invoiceModel.get("Status") == "0") {
                                    sendBookingCommand();
                                }
                                else if (invoiceModel.get("Status") == "1") {
                                    sendCreditCommand();
                                }
                                else if (invoiceModel.get("Status") == "2") {
                                    sendCashCommand();
                                }
                            }
                        }
                    }, {
                        title: "Batal",
                        tooltip: "Batal buat transaksi penjualan",
                        iconClass: 'icon-remove-circle',
                        id: "cancel",
                        action: function () {
                            alert("Cancel");
                        }
                    }]
                });
            };

            var sendBookingCommand = function () {
                var data = {
                    CustomerId: invoiceModel.get("CustomerId"),
                    ProductId: invoiceModel.get("ProductId"),
                    InvoiceDate: invoiceModel.get("InvoiceDate"),
                    Price: invoiceModel.get("Price"),
                    DebitNote: invoiceModel.get("DebitNote")
                };
                $.ajax({
                    type: "POST",
                    url: "/invoice/booking",
                    data: data,
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                    }
                });
            };

            var sendCreditCommand = function () {
                var data = {
                    CustomerId: invoiceModel.get("CustomerId"),
                    ProductId: invoiceModel.get("ProductId"),
                    InvoiceDate: invoiceModel.get("InvoiceDate"),
                    Price: invoiceModel.get("Price"),
                    UangMuka: invoiceModel.get("UangMuka"),
                    LamaAngsuran: invoiceModel.get("LamaAngsuran"),
                    SukuBunga: invoiceModel.get("SukuBunga"),
                    DueDate: invoiceModel.get("DueDate")
                };
                $.ajax({
                    type: "POST",
                    url: "/invoice/credit",
                    data: data,
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                    }
                });
            };

            var sendCashCommand = function () {
                var data = {
                    CustomerId: invoiceModel.get("CustomerId"),
                    ProductId: invoiceModel.get("ProductId"),
                    InvoiceDate: invoiceModel.get("InvoiceDate"),
                    Price: invoiceModel.get("Price")
                };
                $.ajax({
                    type: "POST",
                    url: "/invoice/cash",
                    data: data,
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                    }
                });
            };

            var createDetailPanel = function () {
                detailPanel = new HomeJS.components.DetailPanel({
                    leftSection: {
                        caption: {
                            title: 'Buat Tranksasi Penjualan'
                        },
                        item: createInvoice
                    },
                    rightSection: {
                        items: [toolbar]
                    }
                });
            };

            return {
                show: show
            }
        };
        return am;
    }
);