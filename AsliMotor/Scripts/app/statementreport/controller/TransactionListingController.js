define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'view/selectdateview',
        'view/Container',
        'model/transactionListingreports',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/Breadcrumb',
        '../../../libs/homejs/list'],
    function ($, _, Backbone, ns) {
        ns.define('am.statementreport.controller');
        am.statementreport.controller.TransactionListingController = function () {
            var transactionListingReport = new am.statementreport.model.TransactionListingReports();
            var breadcrumb = new HomeJS.components.Breadcrumb({
                title: 'Transaction Listing Report Order By CoA KAS',
                icon: 'icon-folder-open icon-white'
            });

            var SelectDateModel = Backbone.Model.extend();
            var selectDateModel = new SelectDateModel({ FromDate: getCurrentDate(), ToDate: getCurrentDate() });
            var selectDateView = new am.statementreport.view.SelectDateView({
                model: selectDateModel
            });
            selectDateModel.on('change', function (model) {
                fetchData();
            });

            var fetchData = function () {
                transactionListingReport.fetch({
                    data: {
                        fromDate: selectDateModel.get("FromDate"),
                        toDate: selectDateModel.get("ToDate")
                    }
                });
            };

            var dataTable = new HomeJS.components.List({
                header: {
                    title: ""
                },
                list: {
                    resizable: true,
                    collection: transactionListingReport,
                    headers: [{
                        name: "Tanggal Transaksi",
                        dataIndex: "TransactionDate",
                        title: "Klik untuk mengurutkan berdasarkan Tanggal Transaksi",
                        width: "150px"
                    }, {
                        name: "Nomor Transaksi",
                        dataIndex: "TransactionNo",
                        width: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Transaksi"
                    }, {
                        name: "Deskripsi",
                        dataIndex: "Description",
                        minwidth: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Deskripsi"
                    }, {
                        name: "Ccy",
                        dataIndex: "Debit",
                        width: "20px",
                        align: 'center'
                    }, {
                        name: "Debit",
                        dataIndex: "Debit",
                        width: "100px",
                        align: 'right',
                        title: "Klik untuk mengurutkan berdasarkan Debit"
                    },
                            {
                                name: "Kredit",
                                dataIndex: "Kredit",
                                width: "100px",
                                align: 'right',
                                title: "Klik untuk mengurutkan berdasarkan Kredit"
                            }],
                    items: [{
                        dataIndex: "TransactionDate",
                        onrender: function (date) {
                            return date.toDate();
                        }
                    }, {
                        dataIndex: "TransactionNo"
                    }, {
                        dataIndex: "Description"
                    }, {
                        dataIndex: "Debit",
                        onrender: function (out) {
                            return "IDR";
                        },
                        align: 'center'
                    }, {
                        dataIndex: "Debit",
                        align: 'right',
                        onrender: function (out) {
                            if (out == 0)
                                return '-';
                            return out.toCurrencyWithOutSymbol();
                        }
                    }, {
                        dataIndex: "Kredit",
                        align: 'right',
                        onrender: function (out) {
                            if (out == 0)
                                return '-';
                            return out.toCurrencyWithOutSymbol();
                        }
                    }],
                    footer: [{
                        onrender: function () {
                            return "IDR";
                        },
                        align: 'center'
                    },{
                        onrender: function () {
                            return "<b>" + transactionListingReport.SumTotalDebit().toCurrencyWithOutSymbol() + "</b>";
                        },
                        align: 'right'
                    }, {
                        onrender: function () {
                            return "<b>" + transactionListingReport.SumTotalKredit().toCurrencyWithOutSymbol() + "</b>";
                        },
                        align: 'right'
                    }],
                    eventclick: function (data) {
                        am.eventAggregator.trigger('showDetailInvoice', data.id);
                    }
                }
            });

            var reportContainer = new am.statementreport.view.Container({
                items: [{
                    class: 'span12',
                    id: 'left',
                    view: dataTable
                }]
            });

            var show = function () {
                $("#main-content").html(breadcrumb.render().el);
                $("#main-content").append(selectDateView.render().el);
                $("#main-content").append(reportContainer.render().el);
                fetchData();
            };
            return {
                show: show
            }
        };
        return am;
    }
);