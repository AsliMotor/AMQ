define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/Customer',
        'view/CreateCustomer',
        '../../../libs/Animation',
        '../../../libs/homejs/dialog/erroralert',
        '../../../libs/homejs/DetailPanel'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.customer.controller');
        am.customer.controller.EditController = function (id) {
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
                customerModel.fetch({ data: { id: id }, success: function (m, d) {
                    m.set('Birthday', d.Birthday.toDate());
                    m.set('KTPDate', d.KTPDate.toDate());
                }
                });
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
                                customerModel.url = "/customer/updatecustomer";
                                customerModel.save({}, {
                                    success: function (model, resp) {
                                        if (resp.error)
                                            HomeJS.components.ErrorAlert(resp.message);
                                        else
                                            am.eventAggregator.trigger("showList");
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
                            alert("Cancel");
                        }
                    }]
                });
            };

            var createDetailPanel = function () {
                detailPanel = new HomeJS.components.DetailPanel({
                    leftSection: {
                        caption: {
                            title: 'Ubah Data Pelanggan'
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