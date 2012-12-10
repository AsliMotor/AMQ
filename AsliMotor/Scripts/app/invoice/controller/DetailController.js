define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/InvoiceHeaderReport',
        'model/InvoiceItemReports',
        'view/DetailInvoice',
        'view/ItemDetailInvoice',
        '../../../libs/Animation',
        '../../../libs/Date',
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
                    model: headerModel
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
                        title: "Batal",
                        tooltip: "Batal Transaksi Ini",
                        iconClass: 'icon-remove',
                        id: "btn-cancel",
                        renderer: function () {
                            if (headerModel.get("Status") == 0)
                                return true;
                            return false;
                        },
                        action: function () {
                            if (confirm("Anda yakin untuk membatalkan transaksi ini?")) {
                                $.ajax({
                                    type: "POST",
                                    url: "/invoice/cancel",
                                    data: { id: id },
                                    dataType: "json",
                                    success: function (resp) {
                                        if (resp.error) {
                                            HomeJS.components.ErrorAlert(resp.message);
                                        } else {
                                            window.location.reload();
                                            //am.eventAggregator.trigger("showDetail", resp.data);
                                        }
                                    }
                                });
                            }
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
                            if (headerModel.get("Status") == 2 && headerModel.get("AngsuranBulanan") <= 0)
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
                            if (headerModel.get("Status") == 1 || headerModel.get("Status") == 4)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintSuratPernyataanKredit/" + id, 'Surat Penyataan Kredit', null, null);
                        }
                    }, {
                        title: "Surat Pernyataan",
                        tooltip: "Cetak Surat Pernyataan",
                        iconClass: 'icon-print',
                        id: "printPernyataan",
                        renderer: function () {
                            if (headerModel.get("Status") == 1 || headerModel.get("Status") == 4)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintSuratPernyataan/" + id, 'Surat Penyataan', null, null);
                        }
                    }, {
                        title: "Pernyataan Mampu",
                        tooltip: "Cetak Surat Pernyataan Mampu",
                        iconClass: 'icon-print',
                        id: "printPernyataanMampu",
                        renderer: function () {
                            if (headerModel.get("Status") == 1 || headerModel.get("Status") == 4)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintSuratPernyataanMampu/" + id, 'Surat Penyataan Mampu', null, null);
                        }
                    }, {
                        title: "Surat Kuasa",
                        tooltip: "Cetak Surat Kuasa",
                        iconClass: 'icon-print',
                        id: "printSuratKuasa",
                        renderer: function () {
                            if (headerModel.get("Status") == 1 || headerModel.get("Status") == 4)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintSuratKuasa/" + id, 'Surat Kuasa', null, null);
                        }
                    }, {
                        title: "JB Angsuran",
                        tooltip: "Cetak PERJANJIAN JUAL BELI DENGAN PEMBAYARAN ANGSURAN",
                        iconClass: 'icon-print',
                        id: "printJBAngsuran",
                        renderer: function () {
                            if (headerModel.get("Status") == 1 || headerModel.get("Status") == 4)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintJBAngsuran/" + id, 'PERJANJIAN JUAL BELI DENGAN PEMBAYARAN ANGSURAN', null, null);
                        }
                    }, {
                        title: "JB Fidusia",
                        tooltip: "Cetak PERJANJIAN JUAL BELI DENGAN PEMBAYARAN ANGSURAN",
                        iconClass: 'icon-print',
                        id: "printJBFidusia",
                        renderer: function () {
                            if (headerModel.get("Status") == 1 || headerModel.get("Status") == 4)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintJBFidusia/" + id, 'PERJANJIAN JUAL BELI DENGAN PEMBAYARAN ANGSURAN', null, null);
                        }
                    }, {
                        title: "Tanda Terima",
                        tooltip: "Cetak Surat Tanda Terima",
                        iconClass: 'icon-print',
                        id: "printTandaTerima",
                        renderer: function () {
                            if (headerModel.get("Status") == 1 || headerModel.get("Status") == 4)
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintSuratTandaTerima/" + id, 'Surat Tanda Terima', null, null);
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
                    }, {
                        title: "Surat Penarikan",
                        tooltip: "Cetak Surat Penarikan",
                        iconClass: 'icon-print',
                        id: "printSuratPenarikan",
                        renderer: function () {
                            if ((headerModel.get("Status") == 1 || headerModel.get("Status") == 4) && headerModel.get("DueDate") && headerModel.get("DueDate").toDateTime() < getCurrentDateTime())
                                return true;
                            return false;
                        },
                        action: function () {
                            window.open("/invoice/PrintSuratPeringatan/" + id, 'Surat Penarikan Kendaraan', null, null);
                        }
                    }, {
                        title: "Tarik Kendaraan",
                        tooltip: "Tarik Kendaraan",
                        iconClass: 'icon-lock',
                        id: "btn-cancel",
                        renderer: function () {
                            if (headerModel.get("Status") == 1 && headerModel.get("DueDate") && headerModel.get("DueDate").toDateTime() < getCurrentDateTime())
                                return true;
                            return false;
                        },
                        action: function () {
                            if (confirm("Anda yakin kendaraan ini telah ditarik?")) {
                                $.ajax({
                                    type: "POST",
                                    url: "/invoice/pull",
                                    data: { id: id },
                                    dataType: "json",
                                    success: function (resp) {
                                        if (resp.error) {
                                            HomeJS.components.ErrorAlert(resp.message);
                                        } else {
                                            window.location.reload();
                                            //am.eventAggregator.trigger("showDetail", resp.data);
                                        }
                                    }
                                });
                            }
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