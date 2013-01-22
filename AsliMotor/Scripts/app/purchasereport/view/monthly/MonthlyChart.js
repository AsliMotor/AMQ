define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../../libs/Date',
    '../../../../libs/homejs/chart/chart'
], function ($, _, Backbone, ns) {
    ns.define("am.purchasereport.view.monthly");
    am.purchasereport.view.monthly.MonthlyChart = Backbone.View.extend({
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
                            arrayData.push([item.get("PurchaseDateTick"), item.get("Total")]);
                        }, this);
                        this.view.series[0].data = arrayData;
                    },
                    view: {
                        chart: {
                            renderTo: this.options.renderTo,
                            type: 'area',
                            height: 280,
                            marginRight: 20,
                            borderRadius: 8,
                            backgroundColor: {
                                linearGradient: [0, 0, 500, 500],
                                stops: [
                                            [0, 'rgb(255, 255, 255)'],
                                            [1, 'rgb(240, 240, 255)']
                                         ]
                            },
                            borderWidth: 1,
                            plotBackgroundColor: 'rgba(255, 255, 255, .9)',
                            plotShadow: true,
                            plotBorderWidth: 1
                        },
                        xAxis: {
                            type: 'datetime',
                            dateTimeLabelFormats: {
                                second: '%H:%M:%S',
	                            minute: '%H:%M',
	                            hour: '%H:%M',
	                            day: '%e. %b',
	                            week: '%b',
	                            month: '%b \'%y',
	                            year: '%Y'
	                        },
	                        gridLineWidth: 1,
	                        lineColor: '#000',
	                        tickColor: '#000'
                        },
                        yAxis: {
                            minorTickInterval: 'auto',
                            lineColor: '#000',
                            lineWidth: 1,
                            tickWidth: 1,
                            tickColor: '#000',
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
            						Highcharts.dateFormat('%b %Y', this.x) + '<br/>Rp. ' + Highcharts.numberFormat(this.y);
                            }
                        },
                        exporting: {
                            enabled: true
                        },
                        plotOptions: {
                            area: {
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
                            name: 'Pembelian'
                        }]
                    }
                }
            });
        }
    });
});