define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/Product',
        '../../../libs/Animation',
        '../../../libs/homejs/DetailPanel',
        'view/DetailProduct'
        ],
    function ($, _, Backbone, ns, am) {
        ns.define('am.product.controller');
        am.product.controller.DetailController = function (id) {
            var productModel;
            var detailProduct;
            var toolbar;
            var detailPanel;

            var show = function () {
                loadModel();
                createDetailProduct();
                createToolbar();
                createDetailPanel();
                am.tools.Animation(detailPanel.render().el, "#main-container");
            };

            var loadModel = function () {
                productModel = new am.product.model.Product();
                productModel.fetch({
                    data: { id: id },
                    success: function (model, data) {
                        if (!data || data == null)
                            am.eventAggregator.trigger("showList");
                    },
                    error: function () {
                        am.eventAggregator.trigger("showList");
                    }
                });
            };

            var createDetailProduct = function () {
                detailProduct = new am.product.view.DetailProduct({
                    model: productModel,
                    items: [{
                        title: "Merk Kendaraan",
                        dataIndex: "Merk"
                    }, {
                        title: "Tipe",
                        dataIndex: "Type"
                    }, {
                        title: "Nomor Polisi",
                        dataIndex: "NoPolisi"
                    }, {
                        title: "Nomor Mesin",
                        dataIndex: "NoMesin"
                    }, {
                        title: "Nomor Rangka",
                        dataIndex: "NoRangka"
                    }, {
                        title: "Nomor BPKB",
                        dataIndex: "NoBpkb"
                    }, {
                        title: "Tahun",
                        dataIndex: "Tahun"
                    }, {
                        title: "Warna",
                        dataIndex: "Warna"
                    }, {
                        title: "Status",
                        dataIndex: "Status"
                    }]
            });
        };

        var createToolbar = function () {
            toolbar = new HomeJS.components.DetailPanel.Toolbar({
                model: productModel,
                title: '',
                buttons: [{
                    title: "Edit",
                    tooltip: "Edit data kendaraan",
                    iconClass: 'icon-edit',
                    id: "edit",
                    action: function () {
                        am.eventAggregator.trigger("editProduct", id);
                    }
                }]
            });
        };

        var createDetailPanel = function () {
            detailPanel = new HomeJS.components.DetailPanel({
                leftSection: {
                    caption: {
                        title: 'Detail Kendaraan',
                        action: function () { am.eventAggregator.trigger("showList"); }
                    },
                    item: detailProduct
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