define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        '../model/ProductListReport',
        '../model/ProductListReports',
        '../model/TotalProductList',
        'eventAggregator',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/List'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.product.controller');
        am.product.controller.ListController = function () {
            var productsModel = new am.product.model.ProductListReports();
            var totalList = new am.product.model.TotalProductList();
            productsModel.fetch();
            totalList.fetch({ data: { status: "Aktif"} });
            var listProductView = new HomeJS.components.List({
                header: {
                    title: "Kendaraan",
                    description: "Daftar Kendaraan"
                },
                toolbar: [{
                    type: HomeJS.components.SearchField,
                    placeholder: 'pencarian',
                    action: function (key) {
                        alert(key);
                    }
                }],
                list: {
                    resizable: true,
                    collection: productsModel,
                    headers: [{
                        name: "Tipe",
                        dataIndex: "Type",
                        width: "150px",
                        title: "Klik untuk mengurutkan berdasarkan Tipe kendaraan"
                    }, {
                        name: "No Polisi",
                        dataIndex: "NoPolisi",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Polisi"
                    }, {
                        name: "No Rangka",
                        dataIndex: "NoRangka",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Rangka"
                    }, {
                        name: "No Mesin",
                        dataIndex: "NoMesin",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Mesin"
                    }, {
                        name: "No BPKB",
                        dataIndex: "NoBpkb",
                        width: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Nomor Bpkb"
                    }, {
                        name: "Status",
                        dataIndex: "Status",
                        align: "center",
                        width: "80px",
                        title: "Klik untuk mengurutkan berdasarkan Status"
                    }],
                    items: [{
                        dataIndex: "Type"
                    }, {
                        dataIndex: "NoPolisi"
                    }, {
                        dataIndex: "NoRangka"
                    }, {
                        dataIndex: "NoMesin"
                    }, {
                        dataIndex: "NoBpkb"
                    }, {
                        dataIndex: "Status",
                        align: "center"
                    }],
                    eventclick: function (data) {
                        am.eventAggregator.trigger('showDetail', data.id);
                    }
                },
                showmore: totalList
            });
            var show = function () {
                am.tools.BackAnimation(listProductView.render().el, "#main-container");
            }
            return {
                show: show
            }
        };
        return am;
    }
);