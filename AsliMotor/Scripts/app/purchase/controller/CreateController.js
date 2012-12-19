define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/Purchase',
        'view/EditPurchase',
        '../../../libs/Animation',
        '../../../libs/homejs/dialog/erroralert',
        '../../../libs/homejs/dialog/confirm',
        '../../../libs/homejs/DetailPanel'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.purchase.controller');
        am.purchase.controller.CreateController = function (id) {
            var purchaseModel;
            var editPurchase;
            var toolbar;
            var detailPanel;

            var show = function () {
                loadModel();
                createEditPurchasePanel();
                createToolbar();
                createDetailPanel();
                am.tools.Animation(detailPanel.render().el, "#main-container");
            };

            var loadModel = function () {
                purchaseModel = new am.purchase.model.Purchase();
                purchaseModel.url = "/purchase/createpurchase";
            };

            var createEditPurchasePanel = function () {
                editPurchase = new am.purchase.view.EditPurchase({ model: purchaseModel });
            };

            var createToolbar = function () {
                toolbar = new HomeJS.components.DetailPanel.Toolbar({
                    model: purchaseModel,
                    title: '',
                    buttons: [{
                        title: "Simpan",
                        tooltip: "Simpan pembelian",
                        iconClass: 'icon-ok-sign',
                        id: "save",
                        action: function () {
                            var self = this;
                            var check = purchaseModel.validateAll();
                            if (check.isValid === false) {
                                utils.displayValidationErrors(check.errors);
                                return false;
                            }
                            else {
                                purchaseModel.save({}, {
                                    success: function (model, resp) {
                                        if (resp.error) {
                                            HomeJS.components.ErrorAlert(resp.message);
                                        } else {
                                            am.eventAggregator.trigger("showDetail", resp.data.id);
                                        }
                                    },
                                    error: function (a, b) {
                                        console.log(b);
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
                                message: "Anda yakin untuk membatalkan penambahan data pembelian ini ?",
                                action: function () {
                                    am.eventAggregator.trigger("showList");
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
                            title: 'Tambah Pembelian'
                        },
                        item: editPurchase
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