define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/Product',
        'view/EditProduct',
        '../../../libs/Animation',
        '../../../libs/homejs/dialog/erroralert',
        '../../../libs/homejs/dialog/confirm',
        '../../../libs/homejs/DetailPanel'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.purchase.controller');
        am.product.controller.EditController = function (id) {
            var productModel;
            var editProduct;
            var toolbar;
            var detailPanel;

            var show = function () {
                loadModel();
                createEditProductPanel();
                createToolbar();
                createDetailPanel();
                am.tools.Animation(detailPanel.render().el, "#main-container");
            };

            var loadModel = function () {
                productModel = new am.product.model.Product();
                productModel.fetch({
                    data: { id: id },
                    success: function (model, data) {
                        if (!data || data == null) {
                            am.eventAggregator.trigger("showList");
                        }
                        else {
                            model.set("HargaBeli", model.get("HargaBeli").toString());
                        }
                    }
                });
            };

            var createEditProductPanel = function () {
                editProduct = new am.product.view.EditProduct({ model: productModel });
            };

            var createToolbar = function () {
                toolbar = new HomeJS.components.DetailPanel.Toolbar({
                    model: productModel,
                    title: '',
                    buttons: [{
                        title: "Simpan",
                        tooltip: "Simpan pembelian",
                        iconClass: 'icon-ok-sign',
                        id: "save",
                        action: function () {
                            var self = this;
                            var check = productModel.validateAll();
                            if (check.isValid === false) {
                                utils.displayValidationErrors(check.errors);
                                return false;
                            }
                            else {
                                productModel.save({}, {
                                    success: function (model, resp) {
                                        if (resp.error)
                                            HomeJS.components.ErrorAlert(resp.message);
                                        else
                                            am.eventAggregator.trigger("showDetail", resp.data.id);
                                    }
                                });
                            }
                        }
                    }, {
                        title: "Batal",
                        tooltip: "Batal edit pembelian",
                        iconClass: 'icon-remove-circle',
                        id: "cancel",
                        action: function () {
                            HomeJS.components.Confirm({
                                message: "Anda yakin untuk membatalkan pengubahan data kendaraan ini ?",
                                action: function () {
                                    am.eventAggregator.trigger("showDetail", id);
                                }
                            });
                        }
                    }]
                });
            };

            var createDetailPanel = function () {
                detailPanel = new HomeJS.components.DetailPanel({
                    leftSection: {
                        caption: {
                            title: 'Edit Data Kendaraan'
                        },
                        item: editProduct
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