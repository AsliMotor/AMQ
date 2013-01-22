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
        ns.define('am.purchasereport.controller');
        am.purchasereport.controller.MonthlyController = function () {
            var monthlyReport = new am.purchasereport.model.MonthlyReports();
            var breadcrumb = new HomeJS.components.Breadcrumb({
                title: 'Laporan Pembelian Bulanan',
                icon: 'icon-th icon-white'
            });
            var selectMonthView = new am.purchasereport.view.SelectMonthView({
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
            var dataTable = new am.purchasereport.view.TableData({
                collection: monthlyReport,
                headers: [{
                    name: "Periode",
                    dataIndex: "PurchaseDate"
                }, {
                    name: "Total",
                    dataIndex: "Total",
                    align: 'right'
                }],
                items: [{
                    dataIndex: "PurchaseDate",
                    onrender: function (date) {
                        return date.toMonthAndYear();
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

            var chart = new am.purchasereport.view.monthly.MonthlyChart({
                collection: monthlyReport,
                title: 'Laporan Pembelian Bulanan',
                renderTo: 'right'
            });

            var reportContainer = new am.purchasereport.view.Container({
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