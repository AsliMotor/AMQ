define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        '../model/CustomerListReports',
        '../model/TotalCustomerList',
        'eventAggregator',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/List'],
    function ($, _, Backbone, ns) {
        ns.define('am.customer.controller');
        am.customer.controller.ListController = function () {
            var clicked = false;
            var customersModel = new am.customer.model.CustomerListReports();
            var totalList = new am.customer.model.TotalCustomerList();
            customersModel.fetch();
            totalList.fetch();
            var listCustomerView = new HomeJS.components.List({
                header: {
                    title: "Pelanggan",
                    description: "Data Pelanggan"
                },
                toolbar: [{
                    type: HomeJS.components.Button,
                    title: "Tambah",
                    typeButton: HomeJS.components.ButtonType.Success,
                    icon: "icon-plus-sign",
                    iconColor: HomeJS.components.ButtonColor.White,
                    events: {
                        'click': function () {
                            am.eventAggregator.trigger("createCustomer");
                        }
                    }
                }, {
                    type: HomeJS.components.InputSearchField,
                    placeholder: 'pencarian',
                    action: function (key) {
                        customersModel.searching(key);
                    }
                }],
                list: {
                    resizable: true,
                    collection: customersModel,
                    headers: [{
                        name: "Nama",
                        dataIndex: "Name",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan nama pelanggan"
                    }, {
                        name: "Alamat",
                        dataIndex: "Address",
                        minwidth: "120px",
                        title: "Klik untuk mengurutkan berdasarkan alamat pelanggan"
                    }, {
                        name: "Kota",
                        dataIndex: "City",
                        width: "100px",
                        title: "Klik untuk mengurutkan berdasarkan kota pelanggan"
                    }, {
                        name: "Telpon",
                        dataIndex: "Phone",
                        width: "80px",
                        title: "Klik untuk mengurutkan berdasarkan No Telpon"
                    }, {
                        name: "Jenis Kelamin",
                        dataIndex: "Gender",
                        width: "100px",
                        title: "Klik untuk mengurutkan berdasarkan jenis kelamin"
                    }, {
                        name: "Sisa Tagihan",
                        dataIndex: "Outstanding",
                        align: "right",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Sisa Tagihan"
                    }, {
                        name: "Deposit",
                        dataIndex: "Deposit",
                        align: "right",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Deposit"
                    }],
                    items: [{
                        dataIndex: "Name"
                    }, {
                        dataIndex: "Address"
                    }, {
                        dataIndex: "City"
                    }, {
                        dataIndex: "Phone"
                    }, {
                        dataIndex: "Gender"
                    }, {
                        dataIndex: "Outstanding",
                        align: "right",
                        onrender: function (data) {
                            return data.toCurrency();
                        }
                    }, {
                        dataIndex: "Deposit",
                        align: "right",
                        onrender: function (data) {
                            return "<u style='color:#0088cc;'>" + data.toCurrency() + "</u>";
                        },
                        actionclick: function (model) {
                            am.eventAggregator.trigger('listHistoryCreditNote', model.get("id"));
                            clicked = true;
                        }
                    }],
                    eventclick: function (data) {
                        if (!clicked)
                            am.eventAggregator.trigger('editCustomer', data.id);
                    }
                },
                showmore: totalList
            });
            var show = function () {
                am.tools.BackAnimation(listCustomerView.render().el, "#main-container");
            }
            return {
                show: show
            }
        };
        return am;
    }
);