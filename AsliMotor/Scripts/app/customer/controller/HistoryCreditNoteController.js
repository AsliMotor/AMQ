define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        '../model/HistoryReports',
        'eventAggregator',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/List'],
    function ($, _, Backbone, ns) {
        ns.define('am.customer.controller');
        am.customer.controller.HistoryCreditNoteController = function (id) {
            var historyModel = new am.customer.model.HistoryReports();
            historyModel.fetch({
                data: { id: id }
            });
            var historyView = new HomeJS.components.List({
                header: {
                    title: "Riwayat Deposit"
                },
                list: {
                    resizable: true,
                    collection: historyModel,
                    headers: [[{
                        name: "No. Transaksi",
                        dataIndex: "ReceiveNo",
                        minwidth: "120px",
                        title: "Klik untuk mengurutkan berdasarkan nomor transaksi",
                        rowspan: 2
                    }, {
                        name: "Tanggal",
                        dataIndex: "ReceiveDate",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan tanggal transaksi",
                        rowspan: 2
                    }, {
                        name: "Total",
                        dataIndex: "Total",
                        width: "130px",
                        title: "Klik untuk mengurutkan berdasarkan total",
                        align: "right",
                        rowspan: 2
                    }, {
                        name: "Total Yang Dibayar",
                        dataIndex: "TotalYangDiBayar",
                        width: "130px",
                        title: "Klik untuk mengurutkan berdasarkan total yang dibayar",
                        align: "right",
                        rowspan: 2
                    }, {
                        name: "Deposit",
                        dataIndex: "Deposit",
                        width: "240px",
                        align: "center",
                        colspan: 3
                    }], [{
                        name: "Debit",
                        dataIndex: "Debit",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan debit",
                        align: "right"
                    }, {
                        name: "Kredit",
                        dataIndex: "Kredit",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan kredit",
                        align: "right"
                    }, {
                        name: "Saldo",
                        dataIndex: "Saldo",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan saldo",
                        align: "right"
                    }]],
                    items: [{
                        dataIndex: "ReceiveNo"
                    }, {
                        dataIndex: "ReceiveDate",
                        onrender: function (data) {
                            return data.toDate();
                        }
                    }, {
                        dataIndex: "Total",
                        align: "right",
                        onrender: function (data) {
                            return data.toCurrency();
                        }
                    }, {
                        dataIndex: "TotalYangDiBayar",
                        align: "right",
                        onrender: function (data) {
                            return data.toCurrency();
                        }
                    }, {
                        dataIndex: "Debit",
                        align: "right",
                        onrender: function (data) {
                            if (data == 0)
                                return "-";
                            return data.toCurrency();
                        }
                    }, {
                        dataIndex: "Kredit",
                        align: "right",
                        onrender: function (data) {
                            if (data == 0)
                                return "-";
                            return data.toCurrency();
                        }
                    }, {
                        dataIndex: "Saldo",
                        align: "right",
                        onrender: function (data) {
                            return data.toCurrency();
                        }
                    }]
                }
            });
            var show = function () {
                am.tools.BackAnimation(historyView.render().el, "#main-container");
            }
            return {
                show: show
            }
        };
        return am;
    }
);