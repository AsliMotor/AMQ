define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'view/selectdateview',
        'view/SelectDateViewForRateProduct',
        'view/tabledata',
        'view/Container',
        'model/rateproductreports',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/Breadcrumb'],
    function ($, _, Backbone, ns) {
        ns.define('am.purchasereport.controller');
        am.purchasereport.controller.RateProductController = function () {
            var rateProductReport = new am.purchasereport.model.RateProductReports();
            var breadcrumb = new HomeJS.components.Breadcrumb({
                title: 'Peringkat Pembelian Berdasarkan Kuantitas',
                icon: 'icon-barcode icon-white'
            });

            var SelectDateModel = Backbone.Model.extend();
            var selectDateModel = new SelectDateModel({ FromDate: getStartDayInMonth(), ToDate: getCurrentDate(), Periority: 10 });
            var selectDateView = new am.purchasereport.view.SelectDateViewForRateProduct({
                model: selectDateModel
            });
            selectDateModel.on('change', function (model) {
                fetchData();
            });

            var fetchData = function () {
                rateProductReport.fetch({
                    data: {
                        fromDate: selectDateModel.get("FromDate"),
                        toDate: selectDateModel.get("ToDate"),
                        Periority: selectDateModel.get("Periority")
                    }
                });
            };

            var dataTable = new am.purchasereport.view.TableData({
                collection: rateProductReport,
                headers: [,
                {
                    name: "Peringkat",
                    width: '20px',
                    dataIndex: "No"
                },
                {
                    name: "Type Kendaraan",
                    dataIndex: "Type"
                }, {
                    name: "Jumlah",
                    dataIndex: "Total",
                    align: 'right'
                }],
                items: [{
                    dataIndex: "No"
                }, {
                    dataIndex: "Type"
                }, {
                    dataIndex: "Total",
                    align: "right"
                }],
                createTotalView: false
            });

            var reportContainer = new am.purchasereport.view.Container({
                items: [{
                    class: 'span6',
                    id: 'left',
                    view: dataTable
                }]
            });

            var show = function () {
                $("#main-content").html(breadcrumb.render().el);
                $("#main-content").append(selectDateView.render().el);
                $("#main-content").append(reportContainer.render().el);
                fetchData();
            };
            return {
                show: show
            }
        };
        return am;
    }
);