define([
    'jquery',
    'underscore',
    'backbone',
    'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.model');
        am.dashboard.model.DailySalesReport = Backbone.Collection.extend({
            url: '/home/dailysalesreport'
    });
});