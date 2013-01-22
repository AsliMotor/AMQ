define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/Invoice',
        'view/CreateInvoice',
        '../../../libs/Animation',
        '../../../libs/homejs/dialog/erroralert',
        '../../../libs/homejs/dialog/confirm',
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
                            HomeJS.components.Confirm({
                                message: "Anda yakin untuk membatalkan penambahan data penjualan ini ?",
                                action: function () {
                                    am.eventAggregator.trigger("showList");
                                }
                            });
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
                    success: function (resp) {
                        if (resp.error) {
                            HomeJS.components.ErrorAlert(resp.message);
                        } else {
                            am.eventAggregator.trigger("showDetail", resp.data.id);
                        }
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
                    DueDate: invoiceModel.get("DueDate"),
                    TermId: invoiceModel.get("TermId")
                };
                $.ajax({
                    type: "POST",
                    url: "/invoice/credit",
                    data: data,
                    dataType: "json",
                    success: function (resp) {
                        if (resp.error) {
                            HomeJS.components.ErrorAlert(resp.message);
                        } else {
                            am.eventAggregator.trigger("showDetail", resp.data.id);
                        }
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
                    success: function (resp) {
                        if (resp.error) {
                            HomeJS.components.ErrorAlert(resp.message);
                        } else {
                            am.eventAggregator.trigger("showDetail", resp.data.id);
                        }
                    }
                });
            };

            var createDetailPanel = function () {
                detailPanel = new HomeJS.components.DetailPanel({
                    leftSection: {
                        caption: {
                            title: 'Buat Tranksasi Penjualan',
                            action: function () { am.eventAggregator.trigger("showList"); }
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