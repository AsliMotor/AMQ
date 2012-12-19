define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        '../model/AuditLogListReports',
        '../model/TotalAuditLogList',
        'eventAggregator',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/List'],
    function ($, _, Backbone, ns) {
        ns.define('am.auditlog.controller');
        am.auditlog.controller.ListController = function () {
            var auditlogModel = new am.auditlog.model.AuditLogListReports();
            var totalList = new am.auditlog.model.TotalAuditLogList();
            auditlogModel.fetch();
            totalList.fetch();
            var listAuditLogView = new HomeJS.components.List({
                header: {
                    title: "Riwayat Perubahan Harga Pembelian"
                },
                list: {
                    resizable: true,
                    collection: auditlogModel,
                    headers: [{
                        name: "No. Transaksi",
                        dataIndex: "SupplierInvoiceNo",
                        width: "170px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Transaksi"
                    }, {
                        name: "Type",
                        dataIndex: "Type",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Type"
                    }, {
                        name: "No. Polisi",
                        dataIndex: "NoPolisi",
                        width: "100px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Polisi"
                    }, {
                        name: "Waktu Perubahan",
                        dataIndex: "DateTime",
                        minwidth: "200px",
                        title: "Klik untuk mengurutkan berdasarkan Tanggal Perubahan"
                    }, {
                        name: "Total Sebelumnya",
                        dataIndex: "BeforeTotal",
                        align:'right',
                        width: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Total Sebelumnya"
                    }, {
                        name: "Total Sekarang",
                        dataIndex: "Total",
                        align: 'right',
                        width: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Total"
                    }, {
                        name: "Oleh User",
                        dataIndex: "Username",
                        width: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Username"
                    }],
                    items: [{
                        dataIndex: "SupplierInvoiceNo"
                    }, {
                        dataIndex: "Type"
                    }, {
                        dataIndex: "NoPolisi"
                    }, {
                        dataIndex: "DateTime",
                        onrender: function (data) {
                            return data.toFormatDateTime();
                        }
                    }, {
                        dataIndex: "BeforeTotal",
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
                    }, {
                        dataIndex: 'Username'
                    }],
                    eventclick: function (data) {
                        //am.eventAggregator.trigger('editCustomer', data.id);
                    }
                },
                showmore: totalList
            });
            var show = function () {
                am.tools.BackAnimation(listAuditLogView.render().el, "#main-container");
            }
            return {
                show: show
            }
        };
        return am;
    }
);