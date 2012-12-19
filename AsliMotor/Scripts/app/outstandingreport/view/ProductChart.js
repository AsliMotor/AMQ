define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/homejs/chart/chart'
], function ($, _, Backbone, ns) {
    ns.define("am.report.view");
    am.report.view.ProductChart = Backbone.View.extend({
        initialize: function () {
        },
        render: function () {
            this.createChart();
            return this;
        },
        createChart: function () {
            var self = this;
            var chartView = new HomeJS.components.Chart({
                collection: self.collection,
                attr: {
                    data: function () {
                        var arrayData = [];
                        this.total = self.collection.SumTotal();
                        self.collection.forEach(function (item) {
                            arrayData.push([item.get("Type"), item.get("Total")]);
                        }, this);
                        this.view.series[0].data = arrayData;
                    },
                    view: {
                        colors: ['#7d1214', '#ab4749', '#401e72', '#7656a5', '#8f8999', '#156f7f', '#65aebb', '#0f4c0c', '#1c7d17', "#79cd75", "#bab800", "#e5e349", "#a4530a", "#fa841b", "#f6ac6a", "#d13100"],
                        chart: {
                            renderTo: this.options.renderTo,
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            height: 500
                        },
                        title: {
                            text: this.options.title,
                            style: {
                                color: '#333',
                                font: 'bold 16px "Trebuchet MS", Verdana, sans-serif'
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                return 'Total : <b>' + Math.round((this.point.percentage * this.total) / 100) + ' Unit</b>';
                            }
                        },
                        exporting: {
                            enabled: true
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    color: '#000000',
                                    connectorColor: '#000000',
                                    formatter: function () {
                                        return '<b>' + this.point.name + '</b>: ' + this.point.percentage.round(2) + '%';
                                    }
                                }
                            }
                        },
                        series: [{
                            type: 'pie',
                            name: 'Produk'
                        }]
                    }
                }
            });
        }
    });
});