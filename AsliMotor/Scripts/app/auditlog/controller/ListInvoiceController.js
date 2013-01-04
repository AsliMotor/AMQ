define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        '../model/InvoiceAuditLogAction',
        '../model/InvoiceAuditLogListReports',
        '../model/TotalInvoiceAuditLogList',
        'eventAggregator',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/List'],
    function ($, _, Backbone, ns) {
        ns.define('am.auditlog.controller');
        am.auditlog.controller.ListInvoiceController = function () {
            var auditlogModel = new am.auditlog.model.InvoiceAuditLogListReports();
            var totalList = new am.auditlog.model.TotalInvoiceAuditLogList();
            auditlogModel.fetch();
            totalList.fetch();
            var listAuditLogView = new HomeJS.components.List({
                header: {
                    title: "Riwayat Perubahan Nilai Angsuran Penjualan"
                },
                list: {
                    resizable: true,
                    collection: auditlogModel,
                    headers: [{
                        name: "Pelanggan",
                        dataIndex: "CustomerName",
                        width: "170px",
                        title: "Klik untuk mengurutkan berdasarkan Nama Pelanggan"
                    }, {
                        name: "No. Polisi",
                        dataIndex: "NoPolisi",
                        width: "100px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Polisi"
                    }, {
                        name: "No. Mesin",
                        dataIndex: "NoMesin",
                        width: "100px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Mesin"
                    }, {
                        name: "Waktu Perubahan",
                        dataIndex: "DateTime",
                        minwidth: "200px",
                        title: "Klik untuk mengurutkan berdasarkan Tanggal Perubahan"
                    }, {
                        name: "Aksi",
                        dataIndex: "Action",
                        minwidth: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Aksi"
                    }, {
                        name: "Oleh User",
                        dataIndex: "Username",
                        width: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Username"
                    }],
                    items: [{
                        dataIndex: "CustomerName"
                    }, {
                        dataIndex: "NoPolisi"
                    }, {
                        dataIndex: "NoMesin"
                    }, {
                        dataIndex: "DateTime",
                        onrender: function (data) {
                            return data.toFormatDateTime();
                        }
                    }, {
                        dataIndex: "Action",
                        onrender: function (action, T) {
                            if (action == am.InvoiceAuditLogAction.LamaAngsuranChanged) {
                                return "Merubah Lama Angsuran";
                            }
                            else if (action == am.InvoiceAuditLogAction.SukuBungaChanged) {
                                return "Merubah Suku Bunga";
                            }
                            else if (action == am.InvoiceAuditLogAction.UangAngsuranChanged) {
                                return "Merubah Nilai Uang Angsuran";
                            }
                            else if (action == am.InvoiceAuditLogAction.UangAngsuranChanged) {
                                return "Merubah Nilai Uang Angsuran";
                            }
                            else if (action == am.InvoiceAuditLogAction.UangMukaChanged) {
                                return "Merubah Nilai Uang Muka";
                            }
                            else if (action == am.InvoiceAuditLogAction.PriceChanged) {
                                return "Merubah Nilai Harga Jual";
                            }
                        }
                    }, {
                        dataIndex: 'UserName'
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