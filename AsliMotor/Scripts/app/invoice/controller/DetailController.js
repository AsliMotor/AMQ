define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/InvoiceHeaderReport',
        'model/InvoiceItemReports',
        'view/DetailInvoice',
        'view/ItemDetailInvoice',
        'view/changeuangmuka',
        '../../../app/invoice/action/changeuangmuka',
        '../../../app/invoice/command/changeuangmuka',
        '../../../app/invoice/action/changepaymenttype',
        '../../../app/invoice/action/bayaruangangsuran',
        '../../../libs/Animation',
        '../../../libs/homejs/dialog/modaldialog',
        '../../../libs/homejs/DetailPanel'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.invoice.controller');
        am.invoice.controller.DetailController = function (id) {
            var headerModel;
            var itemsModel;
            var detailHeaderInvoice;
            var detailItemInvoice;
            var toolbar;
            var detailPanel;

            var show = function () {
                loadModel();
                loadEventHandlers();
                createDetailHeaderInvoicePanel();
                createDetailItemInvoice();
                createToolbar();
                createDetailPanel();
                am.tools.Animation(detailPanel.render().el, "#main-container");
            };

            var loadEventHandlers = function () {
                headerModel.on('changed', function () {
                    loadHeaderModel();
                    loadItemModel();
                });
            }

            var loadHeaderModel = function () {
                headerModel.fetch({ data: { invId: id} });
            };

            var loadItemModel = function () {
                itemsModel.fetch({ data: { invId: id} });
            };

            var loadModel = function () {
                headerModel = new am.invoice.model.InvoiceHeaderReport();
                loadHeaderModel();
                itemsModel = new am.invoice.model.InvoiceItemReports();
                loadItemModel();
            };

            var createDetailHeaderInvoicePanel = function () {
                detailHeaderInvoice = new am.invoice.view.DetailInvoice({
                    model: headerModel,
                    changeUangMuka: function () {
                        am.invoice.action.changeUangMuka({ model: headerModel }).execute();
                    }
                });
            };

            var createDetailItemInvoice = function () {
                detailItemInvoice = new am.invoice.view.ItemDetailInvoice({
                    collection: itemsModel
                });
            };

            var createToolbar = function () {
                toolbar = new HomeJS.components.DetailPanel.Toolbar({
                    model: headerModel,
                    title: '',
                    buttons: [{
                        title: "Kwitansi Tanda Jadi",
                        tooltip: "Cetak kwitansi tanda jadi",
                        iconClass: 'icon-print',
                        id: "prinDebitNote",
                        renderer: function () {
                            if (headerModel.get("Status") == 0)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintKwitansiTandaJadi/" + id, 'Kwitansi Tanda Jadi', null, null);
                        }
                    }, {
                        title: "Kwitansi Uang Muka",
                        tooltip: "Print kwitansi uang muka",
                        iconClass: 'icon-print',
                        id: "kwitansiUangMuka",
                        renderer: function () {
                            if (headerModel.get("Status") == 1)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintKwitansiUangMuka/" + id, 'Kwitansi Kontan', null, null);
                        }
                    }, {
                        title: "Kwitansi Kontan",
                        tooltip: "Cetak Kwitansi Kontan",
                        iconClass: 'icon-print',
                        id: "printKwitansiCash",
                        renderer: function () {
                            if (headerModel.get("Status") == 2)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintKwitansiKontan/" + id, 'Kwitansi Kontan', null, null);
                        }
                    }, {
                        title: "Pernyataan Kredit",
                        tooltip: "Cetak Surat Pernyataan Kredit",
                        iconClass: 'icon-print',
                        id: "printPernyataanKredit",
                        renderer: function () {
                            if (headerModel.get("Status") == 1)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintSuratPernyataanKredit/" + id, 'Surat Penyataan Kredit', null, null);
                        }
                    }, {
                        title: "Bayar Angsuran",
                        tooltip: "Bayar Uang Angsuran",
                        iconClass: 'icon-print',
                        id: "bayarUangAngsuran",
                        renderer: function () {
                            if (headerModel.get("Status") == 1)
                                return true;
                            return false;
                        },
                        action: function () {
                            am.invoice.action.bayarUangAngsuran({ model: headerModel }).execute();
                        }
                    }, {
                        title: "Pembayaran",
                        tooltip: "Pembayaran Selanjutnya",
                        iconClass: 'icon-print',
                        id: "nextPayment",
                        renderer: function () {
                            if (headerModel.get("Status") == 0)
                                return true;
                            return false;
                        },
                        action: function () {
                            am.invoice.action.changePaymentType({ model: headerModel }).execute();
                        }
                    }]
                });
            };
            var createDetailPanel = function () {
                detailPanel = new HomeJS.components.DetailPanel({
                    leftSection: {
                        caption: {
                            title: 'Detail Transaksi',
                            status: 'Booking',
                            action: function () { am.eventAggregator.trigger("showList"); }
                        },
                        headers: [detailHeaderInvoice],
                        item: detailItemInvoice
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