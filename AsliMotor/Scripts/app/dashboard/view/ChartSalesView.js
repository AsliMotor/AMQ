define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date',
    '../../../libs/homejs/chart/chart'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.view');
        am.dashboard.view.ChartSalesView = Backbone.View.extend({
            tagName: 'div',
            className: 'chart-sales',
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
                            self.collection.forEach(function (item) {
                                arrayData.push([item.get("SalesDateTick"), item.get("Total")]);
                            }, this);
                            this.view.series[0].data = arrayData;
                        },
                        view: {
                            colors: ["#bc2d11", "#228815"],
                            chart: {
                                renderTo: this.options.renderTo,
                                type: 'line',
                                height: 290,
                                marginRight: 2,
                                borderRadius: 0,
                                borderWidth: 0,
                                backgroundColor: '#F9F9F9',
                                plotBorderWidth: 2,
                                plotBorderColor: '#666'
                            },
                            xAxis: {
                                type: 'datetime',
                                dateTimeLabelFormats: {
                                    second: '%H:%M:%S',
                                    minute: '%H:%M',
                                    hour: '%H:%M',
                                    day: '%e%b',
                                    week: '%b',
                                    month: '%b \'%y',
                                    year: '%Y'
                                },
                                gridLineWidth: 1,
                                lineColor: '#666',
                                lineWidth: 2,
                                tickWidth: 1,
                                tickColor: '#666'
                            },
                            yAxis: {
                                lineColor: '#666',
                                lineWidth: 2,
                                tickWidth: 1,
                                tickColor: '#666',
                                min: 0,
                                title: '',
                                labels: {
                                    formatter: function () {
                                        if ((Math.round(Math.abs(this.value) / Math.pow(10, 9))) >= 1)
                                            return Highcharts.numberFormat(Math.round(this.value / Math.pow(10, 9)), 0, ',', '.') + ' Mil';
                                        else if ((Math.round(Math.abs(this.value) / Math.pow(10, 6))) >= 1)
                                            return Highcharts.numberFormat(Math.round(this.value / Math.pow(10, 6)), 0, ',', '.') + ' Jt';
                                        else if ((Math.round(Math.abs(this.value) / Math.pow(10, 3))) >= 1)
                                            return Highcharts.numberFormat(Math.round(this.value / Math.pow(10, 3)), 0, ',', '.') + ' Rb';
                                        else
                                            return this.value;
                                    }
                                }
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
                                    return '<b>Total ' + this.series.name + '</b><br/>' +
            						Highcharts.dateFormat('%e %b %Y', this.x) + '<br/>Rp. ' + Highcharts.numberFormat(this.y);
                                }
                            },
                            legend: {
                                layout: 'vertical',
                                align: 'right',
                                verticalAlign: 'top',
                                x: -10,
                                y: 0,
                                borderWidth: 0
                            },
                            plotOptions: {
                                line: {
                                    marker: {
                                        enabled: true,
                                        symbol: 'circle',
                                        fillColor: '#FFFFFF',
                                        lineWidth: 2,
                                        lineColor: null,
                                        states: {
                                            hover: {
                                                enabled: true
                                            }
                                        }
                                    }
                                },
                                series: {
                                    fillColor: {
                                        linearGradient: [0, 0, 0, 230],
                                        stops: [
                                                [0, 'rgb(69, 114, 167)'],
                                                [1, 'rgba(2,0,0,0)']
                                            ]
                                    }
                                }
                            },
                            series: [{
                                marker: { radius: 3 },
                                name: 'Penjualan'
                            }]
                        }
                    }
                });
            }
        });
    });