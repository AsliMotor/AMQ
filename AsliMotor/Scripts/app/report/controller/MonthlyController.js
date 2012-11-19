define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'view/selectmonthview',
        '../../../libs/Animation',
        'model/monthlyreports',
        'view/monthly/MonthlyChart',
        'view/tabledata',
        'view/Container',
        '../../../libs/highcharts/exporting',
        '../../../libs/homejs/Breadcrumb'],
    function ($, _, Backbone, ns) {
        ns.define('am.report.controller');
        am.report.controller.MonthlyController = function () {
            var monthlyReport = new am.report.model.MonthlyReports();
            var breadcrumb = new HomeJS.components.Breadcrumb({
                title: 'Laporan Penjualan Bulanan',
                icon: 'icon-calendar icon-white'
            });
            var selectMonthView = new am.report.view.SelectMonthView({
                action: function (year) {
                    fetchData(year);
                }
            });
            var fetchData = function (year) {
                monthlyReport.fetch({
                    data: {
                        year: year
                    }
                });
            };
            var dataTable = new am.report.view.TableData({
                collection: monthlyReport,
                headers: [{
                    name: "Periode",
                    dataIndex: "SalesDate"
                }, {
                    name: "Total",
                    dataIndex: "Total",
                    align: 'right'
                }],
                items: [{
                    dataIndex: "SalesDate",
                    onrender: function (date) {
                        return date.toMonthAndYear();
                    }
                }, {
                    dataIndex: "Total",
                    align: "right",
                    onrender: function (price) {
                        return price.toCurrency();
                    }
                }]
            });

            var chart = new am.report.view.monthly.MonthlyChart({
                collection: monthlyReport,
                title: 'Laporan Penjualan Bulanan',
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
                $("#main-content").append(selectMonthView.render().el);
                $("#main-content").append(reportContainer.render().el);
                fetchData(getCurrentYear());
                chart.render();
            };
            return {
                show: show
            }
        };
        return am;
    }
);