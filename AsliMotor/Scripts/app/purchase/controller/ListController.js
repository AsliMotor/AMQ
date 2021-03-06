﻿define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        '../model/PurchaseListReport',
        '../model/PurchaseListReports',
        '../model/TotalPurchaseList',
        'eventAggregator',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/List'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.purchase.controller');
        am.purchase.controller.ListController = function () {
            var purchasesModel = new am.purchase.model.PurchaseListReports();
            var totalList = new am.purchase.model.TotalPurchaseList();
            purchasesModel.fetch();
            totalList.fetch();
            var listPurchaseView = new HomeJS.components.List({
                header: {
                    title: "Pembelian",
                    description: "Data Pembelian"
                },
                toolbar: [{
                    type: HomeJS.components.Button,
                    title: "Tambah",
                    typeButton: HomeJS.components.ButtonType.Success,
                    icon: "icon-plus-sign",
                    iconColor: HomeJS.components.ButtonColor.White,
                    events: {
                        'click': function () {
                            am.eventAggregator.trigger("createPurchase");
                        }
                    }
                }, {
                    type: HomeJS.components.InputSearchField,
                    placeholder: 'pencarian',
                    action: function (key) {
                        purchasesModel.searching(key);
                    }
                }],
                list: {
                    resizable: true,
                    collection: purchasesModel,
                    headers: [{
                        name: "No Transaksi",
                        dataIndex: "SupplierInvoiceNo",
                        width: "200px",
                        title: "Klik untuk mengurutkan berdasarkan No Transaksi"
                    }, {
                        name: "Tanggal",
                        dataIndex: "SupplierInvoiceDate",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Tanggal"
                    }, {
                        name: "Nama",
                        dataIndex: "SupplierName",
                        width: "100px",
                        title: "Klik untuk mengurutkan berdasarkan Nama"
                    }, {
                        name: "No Polisi",
                        dataIndex: "NoPolisi",
                        width: "80px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Polisi"
                    }, {
                        name: "Tipe",
                        dataIndex: "Type",
                        width: "100px",
                        title: "Klik untuk mengurutkan berdasarkan Type"
                    }, {
                        name: "Harga Beli",
                        dataIndex: "HargaBeli",
                        width: "150px",
                        align: "right",
                        title: "Klik untuk mengurutkan berdasarkan Harga Beli"
                    }, {
                        name: "Biaya",
                        dataIndex: "Charge",
                        width: "100px",
                        align: "right",
                        title: "Klik untuk mengurutkan berdasarkan Biaya-Biaya"
                    }, {
                        name: "Total",
                        dataIndex: "Total",
                        width: "150px",
                        align: "right",
                        title: "Klik untuk mengurutkan berdasarkan Total"
                    }],
                    items: [{
                        dataIndex: "SupplierInvoiceNo"
                    }, {
                        dataIndex: "SupplierInvoiceDate",
                        onrender: function (data) {
                            return data.toDate();
                        }
                    }, {
                        dataIndex: "SupplierName"
                    }, {
                        dataIndex: "NoPolisi"
                    }, {
                        dataIndex: "Type"
                    }, {
                        dataIndex: "HargaBeli",
                        align: "right",
                        onrender: function (data) {
                            return data.toCurrency();
                        }
                    }, {
                        dataIndex: "Charge",
                        align: "right",
                        onrender: function (data) {
                            return data.toCurrency();
                        }
                    }, {
                        dataIndex: "Total",
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
                am.tools.BackAnimation(listPurchaseView.render().el, "#main-container");
            }
            return {
                show: show
            }
        };
        return am;
    }
);