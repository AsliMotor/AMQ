define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'view/selectdateview',
        'view/tabledata',
        'view/Container',
        'view/daily/DailyChart',
        'model/dailyreports',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/highcharts/exporting',
        '../../../libs/homejs/Breadcrumb'],
    function ($, _, Backbone, ns) {
        ns.define('am.purchasereport.controller');
        am.purchasereport.controller.DailyController = function () {
            var dailyReport = new am.purchasereport.model.DailyReports();
            var breadcrumb = new HomeJS.components.Breadcrumb({
                title: 'Laporan Pembelian Harian',
                icon: 'icon-calendar icon-white'
            });

            var SelectDateModel = Backbone.Model.extend();
            var selectDateModel = new SelectDateModel({ FromDate: getStartDayInMonth(), ToDate: getCurrentDate() });
            var selectDateView = new am.purchasereport.view.SelectDateView({
                model: selectDateModel
            });
            selectDateModel.on('change', function (model) {
                fetchData();
            });

            var fetchData = function () {
                dailyReport.fetch({
                    data: {
                        fromDate: selectDateModel.get("FromDate"),
                        toDate: selectDateModel.get("ToDate")
                    }
                });
            };

            var dataTable = new am.purchasereport.view.TableData({
                collection: dailyReport,
                headers: [{
                    name: "Tanggal",
                    dataIndex: "PurchaseDate"
                }, {
                    name: "Total",
                    dataIndex: "Total",
                    align: 'right'
                }],
                items: [{
                    dataIndex: "PurchaseDate",
                    onrender: function (date) {
                        return date.toDate();
                    }
                }, {
                    dataIndex: "Total",
                    align: "right",
                    onrender: function (price) {
                        return price.toCurrency();
                    }
                }],
                createTotalView:true
            });

            var chart = new am.purchasereport.view.daily.DailyChart({
                collection: dailyReport,
                title: 'Laporan Pembelian Harian',
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
                $("#main-content").append(selectDateView.render().el);
                $("#main-content").append(reportContainer.render().el);
                chart.render();
                fetchData();
            };
            return {
                show: show
            }
        };
        return am;
    }
);