define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        'eventAggregator',
        'model/totaltransaction',
        'view/TotalTransactionView'],
    function ($, _, Backbone, ns, am) {
        ns.define('am.dashboard.controller');
        am.dashboard.controller.DashboardController = function () {
            var totalTransactionModel;
            var totalTransactionView;

            var loadModel = function () {
                totalTransactionModel = new am.dashboard.model.TotalTransaction({ TotalDailyTransaction: 12 });
            };

            var createTotalTransactionView = function () {
                totalTransactionView = new am.dashboard.view.TotalTransactionView({
                    model: totalTransactionModel
                });
            };

            var fetchData = function () {
                totalTransactionModel.fetch();
            };

            var show = function () {
                loadModel();
                createTotalTransactionView();
                $("#main-container").html(totalTransactionView.render().el);
            };

            return { show: show };
        };
    });