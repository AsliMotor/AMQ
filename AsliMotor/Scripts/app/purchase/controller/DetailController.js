define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/Purchase',
        '../../../libs/Animation',
        '../../../libs/homejs/DetailPanel',
        'view/DetailPurchase'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.purchase.controller');
        am.purchase.controller.DetailController = function (id) {
            var purchaseModel;
            var detailPurchase;
            var toolbar;
            var detailPanel;

            var show = function () {
                loadModel();
                createDetailPurchase();
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
                    },
                    error: function () {
                        am.eventAggregator.trigger("showList");
                    }
                });
            };

            var createDetailPurchase = function () {
                detailPurchase = new am.purchase.view.DetailPurchase({ model: purchaseModel });
            };

            var createToolbar = function () {
                toolbar = new HomeJS.components.DetailPanel.Toolbar({
                    model: purchaseModel,
                    title: '',
                    buttons: [{
                        title: "Cetak",
                        tooltip: "Cetak kwitansi pembelian",
                        iconClass: 'icon-print',
                        id: "print",
                        action: function () {
                            window.open("/purchase/print/" + id, 'Kwitansi', null, null);
                        }
                    }, {
                        title: "Edit",
                        tooltip: "Edit pembelian",
                        iconClass: 'icon-edit',
                        id: "edit",
                        action: function () {
                            am.eventAggregator.trigger("editPurchase", id);
                        }
                    }]
                });
            };

            var createDetailPanel = function () {
                detailPanel = new HomeJS.components.DetailPanel({
                    leftSection: {
                        caption: {
                            title: 'Detail Pembelian',
                            action: function () { am.eventAggregator.trigger("showList"); }
                        },
                        item: detailPurchase
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