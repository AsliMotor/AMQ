﻿define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'view/selectdateview',
        'view/Container',
        'view/ProductChart',
        'view/TotalSummaryGrafikProduk',
        'model/grafikproductreports',
        '../../../libs/Animation',
        '../../../libs/highcharts/exporting',
        '../../../libs/homejs/Breadcrumb'],
    function ($, _, Backbone, ns) {
        ns.define('am.report.controller');
        am.report.controller.GrafikProductController = function () {
            var grafikProductReport = new am.report.model.GrafikProductReports();
            var breadcrumb = new HomeJS.components.Breadcrumb({
                title: 'Grafik Penjualan Berdasarkan Produk',
                icon: 'icon-signal icon-white'
            });

            var SelectDateModel = Backbone.Model.extend();
            var selectDateModel = new SelectDateModel({ FromDate: getStartDayInYear(), ToDate: getCurrentDate() });
            var selectDateView = new am.report.view.SelectDateView({
                model: selectDateModel
            });
            selectDateModel.on('change', function (model) {
                fetchData();
            });

            var fetchData = function () {
                grafikProductReport.fetch({
                    data: {
                        fromDate: selectDateModel.get("FromDate"),
                        toDate: selectDateModel.get("ToDate")
                    }
                });
            };

            var totalSummaryView = new am.report.view.TotalSummaryGrafikProduk({ collection: grafikProductReport, model: selectDateModel });

            var chart = new am.report.view.ProductChart({
                collection: grafikProductReport,
                title: '',
                renderTo: 'right'
            });

            var reportContainer = new am.report.view.Container({
                items: [{
                    class: 'span11',
                    id: 'right',
                    view: chart
                }]
            });

            var show = function () {
                $("#main-content").html(breadcrumb.render().el);
                $("#main-content").append(selectDateView.render().el);
                $("#main-content").append(reportContainer.render().el);
                $(".report-container").prepend(totalSummaryView.render().el);
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