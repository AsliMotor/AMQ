define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/totaltransaction',
        'model/dailysalesreport',
        'model/salesreport',
        'model/piutangtelahjatuhtempo',
        'model/totalpiutangtelahjatuhtempo',
        'view/TotalTransactionView',
        'view/DetailSalesView',
        'view/ChartSalesView',
        'view/SalesView',
        '../../../libs/date',
        '../../../libs/currency',
        '../../../libs/homejs/list',
        '/scripts/app/shared/AccountModel.js'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.dashboard.controller');
        am.dashboard.controller.DashboardController = function () {
            var chartModel;
            var totalTransactionModel;
            var salesReportModel;
            var piutangTelahJatuhTempoModel;
            var totalPiutangTelahJatuhTempoModel;
            var totalTransactionView;
            var salesView;
            var detailSalesView;
            var salesChart;
            var piutangView;
            var accountModel;

            var loadModel = function () {
                totalTransactionModel = new am.dashboard.model.TotalTransaction();
                chartModel = new am.dashboard.model.DailySalesReport();
                salesReportModel = new am.dashboard.model.SalesReport();
                piutangTelahJatuhTempoModel = new am.dashboard.model.PiutangTelahJatuhTempo();
                totalPiutangTelahJatuhTempoModel = new am.dashboard.model.TotalPiutangTelahJatuhTempo();
                accountModel = am.AccountModel().get();
            };

            var createTotalTransactionView = function () {
                totalTransactionView = new am.dashboard.view.TotalTransactionView({
                    model: totalTransactionModel
                });
            };
            var createSalesView = function () {
                salesView = new am.dashboard.view.SalesView({ model: accountModel });
                detailSalesView = new am.dashboard.view.DetailSalesView({ model: salesReportModel });
                salesChart = new am.dashboard.view.ChartSalesView({
                    collection: chartModel,
                    title: '',
                    renderTo: 'chart-sales-container'
                });
            };

            var createPiutangView = function () {
                piutangView = new HomeJS.components.List({
                    header: {
                        title: ""
                    },
                    list: {
                        resizable: true,
                        collection: piutangTelahJatuhTempoModel,
                        headers: [
                            {
                                name: "Type",
                                dataIndex: "Type",
                                title: "Klik untuk mengurutkan berdasarkan Type",
                                width: "100px"
                            }, {
                                name: "Nomor Polisi",
                                dataIndex: "NoPolisi",
                                width: "120px",
                                title: "Klik untuk mengurutkan berdasarkan Nomor Polisi"
                            }, {
                                name: "Nama Pelanggan",
                                dataIndex: "CustomerName",
                                minwidth: "150px",
                                title: "Klik untuk mengurutkan berdasarkan Nama Pelanggan"
                            }, {
                                name: "Tanggal Penjualan",
                                dataIndex: "InvoiceDate",
                                width: "150px",
                                title: "Klik untuk mengurutkan berdasarkan Tanggal Penjualan"
                            },
                            {
                                name: "Sisa Tagihan",
                                dataIndex: "Outstanding",
                                width: "180px",
                                align: 'right',
                                title: "Klik untuk mengurutkan berdasarkan Jumlah Piutang"
                            }, {
                                name: "Jatuh Tempo",
                                dataIndex: "DueDate",
                                width: "150px",
                                title: "Klik untuk mengurutkan berdasarkan Tanggal Jatuh Tempo"
                            }
                         ],
                        items: [{
                            dataIndex: "Type"
                        }, {
                            dataIndex: "NoPolisi"
                        }, {
                            dataIndex: "CustomerName"
                        }, {
                            dataIndex: "InvoiceDate",
                            onrender: function (date) {
                                return date.toDate();
                            }
                        }, {
                            dataIndex: "Outstanding",
                            align: 'right',
                            onrender: function (out) {
                                return out.toCurrency();
                            }
                        }, {
                            dataIndex: "DueDate",
                            onrender: function (date) {
                                return date.toDate();
                            }
                        }],
                        onrenderItem: function (model) {
                            if (model.get("DiffrentMonth") >= 3)
                                return "outstanding-greater-3";
                            if (model.get("DiffrentMonth") >= 2)
                                return "outstanding-greater-2";
                            if (model.get("DiffrentMonth") >= 1)
                                return "outstanding-greater-1";
                        },
                        eventclick: function (data) {
                            am.eventAggregator.trigger('showDetailInvoice', data.id);
                        }
                    },
                    showmore: totalPiutangTelahJatuhTempoModel
                });
            };
            var fetchData = function () {
                totalTransactionModel.fetch();
                chartModel.fetch();
                salesReportModel.fetch();
                piutangTelahJatuhTempoModel.fetch({ data: { offset: 0} });
                totalPiutangTelahJatuhTempoModel.fetch();
            };

            var show = function () {
                loadModel();
                fetchData();
                createSalesView();
                createTotalTransactionView();
                salesChart.render();
                createPiutangView();
                $("#main-container").html(totalTransactionView.render().el);
                $("#main-container").append(salesView.render().el);
                $("#detail-sales").append(detailSalesView.render().el);
                $("#chart-sales").append(salesChart.render().el);
                $("#piutang-telah-jatuh-tempo").html(piutangView.render().el);
            };

            return { show: show };
        };
    });