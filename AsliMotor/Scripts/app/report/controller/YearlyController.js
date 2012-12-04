define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'view/selectyearview',
        '../../../libs/Animation',
        'model/yearlyreports',
        'view/yearly/YearlyChart',
        'view/tabledata',
        'view/Container',
        '../../../libs/highcharts/exporting',
        '../../../libs/homejs/Breadcrumb'],
    function ($, _, Backbone, ns) {
        ns.define('am.report.controller');
        am.report.controller.YearlyController = function () {
            var yearlyReport = new am.report.model.YearlyReports();
            var breadcrumb = new HomeJS.components.Breadcrumb({
                title: 'Laporan Penjualan Tahunan',
                icon: 'icon-th icon-white'
            });
            var SelectYearModel = Backbone.Model.extend();
            var selectYearModel = new SelectYearModel({ FromYear: getCurrentYear(), ToYear: getCurrentYear() });
            var selectYearView = new am.report.view.SelectYearView({
                model: selectYearModel
            });
            selectYearModel.on('change', function (model) {
                fetchData();
            });
            var fetchData = function () {
                yearlyReport.fetch({
                    data: {
                        fromYear: selectYearModel.get("FromYear"),
                        toYear: selectYearModel.get("ToYear")
                    }
                });
            };
            var dataTable = new am.report.view.TableData({
                collection: yearlyReport,
                headers: [{
                    name: "Tahun",
                    dataIndex: "SalesDate"
                }, {
                    name: "Total",
                    dataIndex: "Total",
                    align: 'right'
                }],
                items: [{
                    dataIndex: "SalesDate",
                    onrender: function (date) {
                        return date.toYear();
                    }
                }, {
                    dataIndex: "Total",
                    align: "right",
                    onrender: function (price) {
                        return price.toCurrency();
                    }
                }],
                createTotalView: true
            });
            var chart = new am.report.view.yearly.YearlyChart({
                collection: yearlyReport,
                title: 'Laporan Penjualan Tahunan',
                renderTo: 'right'
            });
            var reportContainer = new am.report.view.Container({
                items: [{
                    class: 'span4',
                    id: 'left',
                    view: dataTable
                }, {
                    class: 'span8',
                    id: 'right',
                    view: chart
                }]
            });
            var show = function () {
                $("#main-content").html(breadcrumb.render().el);
                $("#main-content").append(selectYearView.render().el);
                $("#main-content").append(reportContainer.render().el);
                fetchData();
                chart.render();
            };
            return {
                show: show
            }
        };
        return am;
    }
);