﻿define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator', 
        'view/selectdateview',
        'view/Container',
        //'model/dailyreports',
        '../../../libs/Animation',
        '../../../libs/Date',
        '../../../libs/Currency',
        '../../../libs/homejs/Breadcrumb'],
    function ($, _, Backbone, ns) {
        ns.define('am.angsuranreport.controller');
        am.angsuranreport.controller.TelahJatuhTempoController = function () {
            //var dailyReport = new am.report.model.DailyReports();
            var breadcrumb = new HomeJS.components.Breadcrumb({
                title: 'Laporan Angsuran Telah Jatuh Tempo',
                icon: 'icon-home icon-white'
            });

            var Selectdatemodel = Backbone.Model.extend();
            var selectdatemodel = new Selectdatemodel({ FromDate: getStartDayInMonth(), ToDate: getCurrentDate() });
            var selectDateView = new am.angsuranreport.view.SelectDateView({
                model: selectdatemodel
            });
            selectdatemodel.on('change', function (model) {
                fetchdata();
            });

//            var fetchData = function () {
//                dailyReport.fetch({
//                    data: {
//                        fromDate: selectDateModel.get("FromDate"),
//                        toDate: selectDateModel.get("ToDate")
//                    }
//                });
//            };

//            var dataTable = new am.report.view.TableData({
//                collection: dailyReport,
//                headers: [{
//                    name: "Tanggal",
//                    dataIndex: "SalesDate"
//                }, {
//                    name: "Total",
//                    dataIndex: "Total",
//                    align: 'right'
//                }],
//                items: [{
//                    dataIndex: "SalesDate",
//                    onrender: function (date) {
//                        return date.toDate();
//                    }
//                }, {
//                    dataIndex: "Total",
//                    align: "right",
//                    onrender: function (price) {
//                        return price.toCurrency();
//                    }
//                }],
//                createTotalView:true
//            });

//            var chart = new am.report.view.daily.DailyChart({
//                collection: dailyReport,
//                title: 'Laporan Penjualan Harian',
//                renderTo: 'right'
//            });

//            var reportContainer = new am.report.view.Container({
//                items: [{
//                    class: 'span4',
//                    id: 'left',
//                    view: dataTable
//                }, {
//                    class: 'span8',
//                    id: 'right',
//                    view: chart
//                }]
//            });

            var show = function () {
                $("#main-content").html(breadcrumb.render().el);
                $("#main-content").append(selectDateView.render().el);
                //$("#main-content").append(reportContainer.render().el);
                //chart.render();
                //fetchData();
            };
            return {
                show: show
            }
        };
        return am;
    }
);