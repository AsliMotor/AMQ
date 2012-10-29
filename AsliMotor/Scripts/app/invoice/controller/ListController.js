define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        '../model/InvoiceListReport',
        '../model/InvoiceListReports',
        '../model/TotalInvoiceList',
        'eventAggregator',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/List'],
    function ($, _, Backbone, ns) {
        ns.define('am.invoice.controller');
        am.invoice.controller.ListController = function () {
            var invoicesModel = new am.invoice.model.InvoiceListReports();
            var totalList = new am.invoice.model.TotalInvoiceList();
            invoicesModel.fetch();
            totalList.fetch();
            var listInvoiceView = new HomeJS.components.List({
                header: {
                    title: "Penjualan",
                    description: "Daftar Penjualan"
                },
                toolbar: [{
                    type: HomeJS.components.Button,
                    title: "Tambah",
                    typeButton: HomeJS.components.ButtonType.Info,
                    icon: "icon-file",
                    iconColor: HomeJS.components.ButtonColor.White,
                    events: {
                        'click': function () {
                            am.eventAggregator.trigger("createInvoice");
                        }
                    }
                }, {
                    type: HomeJS.components.SearchField,
                    placeholder: 'pencarian',
                    action: function (key) {
                        alert(key);
                    }
                }],
                list: {
                    resizable: true,
                    collection: invoicesModel,
                    headers: [{
                        name: "No",
                        dataIndex: "InvoiceNo",
                        width: "20px",
                        align:'center',
                        title: "Klik untuk mengurutkan berdasarkan No Transaksi"
                    }, {
                        name: "Tanggal",
                        dataIndex: "InvoiceDate",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Tanggal"
                    }, {
                        name: "Status",
                        dataIndex: "Status",
                        width: "90px",
                        title: "Klik untuk mengurutkan berdasarkan Status"
                    }, {
                        name: "Pelanggan",
                        dataIndex: "CustomerName",
                        minwidth: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Nama Pelanggan"
                    }, {
                        name: "No Polisi",
                        dataIndex: "NoPolisi",
                        width: "100px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Polisi"
                    }, {
                        name: "Type",
                        dataIndex: "Type",
                        width: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Type"
                    }, {
                        name: "Sisa Tagihan",
                        dataIndex: "Outstanding",
                        width: "100px",
                        align: "right",
                        title: "Klik untuk mengurutkan berdasarkan Sisa Tagihan"
                    }, {
                        name: "Harga Jual",
                        dataIndex: "Price",
                        width: "150px",
                        align: "right",
                        title: "Klik untuk mengurutkan berdasarkan Harga Jual"
                    }],
                    items: [{
                        dataIndex: "InvoiceNo",
                        align:'center'
                    }, {
                        dataIndex: "InvoiceDate",
                        onrender: function (data) {
                            return data.toDate();
                        }
                    }, {
                        dataIndex: "Status",
                        onrender: function (data) {
                            if (data == 0)
                                return "Booking";
                            else if (data == 1)
                                return "Kredit";
                            else if (data == 2)
                                return "Lunas";
                            else if (data == 3)
                                return "Batal";
                        }
                    }, {
                        dataIndex: "CustomerName"
                    }, {
                        dataIndex: "NoPolisi"
                    }, {
                        dataIndex: "Type"
                    }, {
                        dataIndex: "Outstanding",
                        align: "right",
                        onrender: function (data) {
                            return data.toCurrency();
                        }
                    }, {
                        dataIndex: "Price",
                        align: "right",
                        onrender: function (data) {
                            return data.toCurrency();
                        }
                    }],
                    eventclick: function (data) {
                        am.eventAggregator.trigger('showDetail', data.id);
                    }
                },
                showmore: totalList
            });
            var show = function () {
                am.tools.BackAnimation(listInvoiceView.render().el, "#main-container");
            }
            return {
                show: show
            }
        };
        return am;
    }
);