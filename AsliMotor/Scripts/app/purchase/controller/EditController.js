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
        am.purchase.controller.EditController = function (id) {
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
                purchaseModel.fetch({
                    data: { id: id },
                    success: function (model, data) {
                        if (!data || data == null)
                            am.eventAggregator.trigger("showList");
                        else {
                            model.set("SupplierInvoiceDate", model.get("SupplierInvoiceDate").toDefaultFormatDate());
                            model.set("HargaBeli", model.get("HargaBeli").toString());
                        }
                    }
                });
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
                                message: "Anda yakin untuk membatalkan pengubahan data pembelian ini ?",
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
                            title: 'Edit Pembelian'
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