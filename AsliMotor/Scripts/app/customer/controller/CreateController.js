define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/Customer',
        'view/CreateCustomer',
        '../../../libs/Animation',
        '../../../libs/homejs/dialog/erroralert',
        '../../../libs/homejs/dialog/confirm',
        '../../../libs/homejs/DetailPanel'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.customer.controller');
        am.customer.controller.CreateController = function (id) {
            var customerModel;
            var editCustomerView;
            var toolbar;
            var detailPanel;

            var show = function () {
                loadModel();
                createEditCustomerPanel();
                createToolbar();
                createDetailPanel();
                am.tools.Animation(detailPanel.render().el, "#main-container");
            };

            var loadModel = function () {
                customerModel = new am.customer.model.Customer();
            };

            var createEditCustomerPanel = function () {
                editCustomerView = new am.customer.view.EditCustomer({ model: customerModel });
            };

            var createToolbar = function () {
                toolbar = new HomeJS.components.DetailPanel.Toolbar({
                    model: customerModel,
                    title: '',
                    buttons: [{
                        title: "Simpan",
                        tooltip: "Simpan data pelanggan",
                        iconClass: 'icon-ok-sign',
                        id: "save",
                        action: function () {
                            var self = this;
                            var check = customerModel.validateAll();
                            if (check.isValid === false) {
                                utils.displayValidationErrors(check.errors);
                                return false;
                            }
                            else {
                                customerModel.save({}, {
                                    success: function (model, resp) {
                                        if (resp.error)
                                            HomeJS.components.ErrorAlert(resp.message);
                                        else
                                            am.eventAggregator.trigger('editCustomer', resp.data.id);
                                    }
                                });
                            }
                        }
                    }, {
                        title: "Batal",
                        tooltip: "Batal tambah pelanggan",
                        iconClass: 'icon-remove-circle',
                        id: "cancel",
                        action: function () {
                            HomeJS.components.Confirm({
                                message: "Anda yakin untuk membatalkan penambahan data pelanggan ini ?",
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
                            title: 'Tambah Pelanggan'
                        },
                        item: editCustomerView
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